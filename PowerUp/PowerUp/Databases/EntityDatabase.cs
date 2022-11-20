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

    public void Save<TEntity>(TEntity entity) where TEntity : Entity<TEntity> => Save(typeof(TEntity), entity);
    public void Save(Type entityType, Entity entity)
    {
      var entityCollection = DBConnection.GetCollection(entityType.Name);
      var mapper = new BsonMapper();

      if (entity.Id.HasValue)
      {
        var mappedDoc = mapper.ToDocument(entity);
        entityCollection.Update(mappedDoc);
      }
      else
      {
        entity.Id = 0;

        var mappedDoc = mapper.ToDocument(entity);
        entity.Id = entityCollection.Insert(mappedDoc);
      }

      foreach (var propertyGetter in entity.UntypedIndexes)
        entityCollection.EnsureIndex(e => propertyGetter((Entity)mapper.ToObject(entityType, e)));
    }

    public void SaveAll<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);

      var existingEntities = entities.Where(e => e.Id.HasValue);
      entityCollection.Update(existingEntities);

      var newEntities = entities.Where(e => !e.Id.HasValue).Select(e => { e.Id = 0; return e; });
      entityCollection.Insert(newEntities);

      foreach (var propertyGetter in entities.FirstOrDefault()?.Indexes ?? Enumerable.Empty<Func<TEntity, object>>())
        entityCollection.EnsureIndex(e => propertyGetter(e));
    }

    public IDictionary<int, int> ImportAll(Type entityType, IEnumerable<Entity> entities)
    {
      var savedIdsByImportIds = new Dictionary<int, int>();
      var entityCollection = DBConnection.GetCollection<BsonDocument>(entityType.Name);
      var mapper = new BsonMapper();

      var lastInsert = entityCollection.FindOne(LiteDB.Query.All(LiteDB.Query.Descending));
      var nextId = lastInsert != null
        ? lastInsert["_id"].AsInt32 + 1
        : 1;

      var newEntities = entities
        .Select(e =>
          { 
            savedIdsByImportIds.Add(e.Id!.Value, nextId);
            e.Id = nextId;
            nextId++;
            return e; 
          })
        .Select(e => mapper.ToDocument(entityType, e));
      entityCollection.Insert(newEntities);

      foreach (var propertyGetter in entities.FirstOrDefault()?.UntypedIndexes ?? Enumerable.Empty<Func<Entity, object>>())
      {
        Expression<Func<BsonDocument, object>> bsonGetter = d => propertyGetter((Entity)mapper.ToObject(entityType, d));
        entityCollection.EnsureIndex(bsonGetter);
      }

      return savedIdsByImportIds;
    }

    public TEntity? Load<TEntity>(int id) where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.FindById(id);
    }

    public Entity? Load(Type entityType, int entityId)
    {
      var entityCollection = DBConnection.GetCollection(entityType.Name);
      var mapper = new BsonMapper();
      var bsonDoc = entityCollection.FindById(entityId);
      return bsonDoc != null
        ? (Entity)mapper.ToObject(entityType, bsonDoc)
        : null;
    }

    public TEntity? LoadOnly<TEntity>() where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.FindOne(_ => true);
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

    public IEnumerable<Entity> LoadAll(Type entityType)
    {
      var entityCollection = DBConnection.GetCollection(entityType.Name);
      var mapper = new BsonMapper();
      return entityCollection.FindAll().Select(d => (Entity)mapper.ToObject(entityType, d)).ToList();
    }

    public ILiteQueryable<TEntity> Query<TEntity>()
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.Query();
    }

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
