using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace PowerUp.Databases
{
  public class EntityDatabase : IDisposable
  {
    public LiteDatabase DBConnection { get; }
    public ITransaction? CurrentTransaction { get; }

    public EntityDatabase(string dataDirectory)
    {
      DBConnection = new LiteDatabase(Path.Combine(dataDirectory, "Data.db"));
    }

    public ITransaction BeginTransaction()
    {
      if (CurrentTransaction != null)
        return CurrentTransaction;

      DBConnection.BeginTrans();
      return new Transaction(() => DBConnection.Commit(), () => DBConnection.Rollback());
    }

    public void Save<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);

      if (entity.Id.HasValue)
      {
        entityCollection.Update(entity);
      }
      else
      {
        entity.Id = 0;
        entityCollection.Insert(entity);
      }

      foreach(var propertyGetter in entity.Indexes)
        entityCollection.EnsureIndex(propertyGetter);
    }

    public TEntity? Load<TEntity>(int id) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.FindById(id);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      entityCollection.Delete(entity.Id);
    }

    public void DeleteWhere<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      entityCollection.DeleteMany(predicate);
    }

    public IEnumerable<TEntity> LoadAll<TEntity>() where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.FindAll().ToList();
    }

    public ILiteQueryable<TResult> Query<TResult>(string collectionName)
    {
      var entityCollection = DBConnection.GetCollection<TResult>(collectionName);
      return entityCollection.Query();
    }

    public ILiteQueryable<TResult> Query<TResult, TEntity>() where TEntity : Entity<TEntity>
      => Query<TResult>(typeof(TEntity).Name);

    public void Dispose() => DBConnection.Dispose();
  }

  public interface ITransaction : IDisposable 
  {
    void Commit();
  }

  public class Transaction : ITransaction
  {
    private readonly Action _commit;
    private readonly Action _rollback;

    public bool WasCommitted { get; private set; }

    public Transaction(Action commit, Action rollback)
    {
      _commit = commit;
      _rollback = rollback;
    }
    public void Commit()
    {
      if (WasCommitted)
        return;

      _commit();
      WasCommitted = true;
    }

    public void Dispose()
    {
      if (WasCommitted)
        return;

      _rollback();
    }
  }
}
