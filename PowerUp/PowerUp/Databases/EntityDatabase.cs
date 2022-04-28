using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public class EntityDatabase : IDisposable
  {
    public LiteDatabase DBConnection { get; }

    public EntityDatabase(string dataDirectory)
    {
      DBConnection = new LiteDatabase(Path.Combine(dataDirectory, "Data.db"));
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

    public IEnumerable<TEntity> LoadAll<TEntity>() where TEntity : Entity<TEntity>
    {
      var entityCollection = DBConnection.GetCollection<TEntity>(typeof(TEntity).Name);
      return entityCollection.FindAll().ToList();
    }

    public void Dispose() => DBConnection.Dispose();
  }
}
