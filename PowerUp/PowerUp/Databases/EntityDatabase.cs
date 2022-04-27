using LiteDB;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Databases
{
  public class EntityDatabase
  {
    private readonly string _databaseDirectory;

    public EntityDatabase(string dataDirectory)
    {
      _databaseDirectory = Path.Combine(dataDirectory, "Data.db");
    }

    public void Save<TEntity>(TEntity entity) where TEntity : Entity<TEntity>
    {
      using (var db = new LiteDatabase(_databaseDirectory))
      {
        var entityCollection = db.GetCollection<TEntity>(typeof(TEntity).Name);

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
    }

    public TEntity? Load<TEntity>(int id) where TEntity : Entity<TEntity>
    {
      using (var db = new LiteDatabase(_databaseDirectory))
      {
        var entityCollection = db.GetCollection<TEntity>(typeof(TEntity).Name);
        return entityCollection.FindById(id);
      }
    }

    public IEnumerable<TEntity> LoadAll<TEntity>() where TEntity : Entity<TEntity>
    {
      using (var db = new LiteDatabase(_databaseDirectory))
      {
        var entityCollection = db.GetCollection<TEntity>(typeof(TEntity).Name);
        return entityCollection.FindAll().ToList();
       }
    }
  }
}
