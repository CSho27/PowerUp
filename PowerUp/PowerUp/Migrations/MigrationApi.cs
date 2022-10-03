using PowerUp.Databases;
using PowerUp.Migrations.MigrationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Migrations
{
  public interface IMigrationApi
  {
    public void MigrateDataFrom(string dataDirectory);
  }

  public class MigrationApi : IMigrationApi
  {
    public void MigrateDataFrom2(string dataDirectory)
    {
      var existingDatabase = new EntityDatabase(dataDirectory);
      foreach(var entityType in MigrationHelpers.GetAllEntityTypes())
      {
        var migrationType = MigrationHelpers.GetMigrationTypeFor(entityType)!;
        var migrator = MigrationHelpers.GetMigratorFor(entityType, migrationType)!;
        var allObjectsForEntity = existingDatabase
          .LoadAll(migrationType)
          .Select(o => migrator.Migrate(o))
          .Where(o => o is not null)
          .Cast<Entity>();

        DatabaseConfig.Database.SaveAll(entityType, allObjectsForEntity);
      }
    }

    public void MigrateDataFrom(string dataDirectory)
    {
      var existingDatabase = new EntityDatabase(dataDirectory);
      foreach (var entityType in MigrationHelpers.GetAllEntityTypes())
      {
        var allObjectsForEntity = existingDatabase.LoadAll(entityType);
        DatabaseConfig.Database.SaveAll(entityType, allObjectsForEntity);
      }
    }
  }
}
