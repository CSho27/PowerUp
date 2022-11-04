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
      using var existingDatabase = new EntityDatabase(dataDirectory);
      foreach(var entityType in MigrationHelpers.GetAllEntityTypes())
      {
        var migrationType = MigrationHelpers.GetMigrationTypeFor(entityType)!;
        var migrator = MigrationHelpers.GetMigratorFor(entityType, migrationType)!;
        var allObjectsForEntity = existingDatabase
          .LoadAll(migrationType)
          .Select(o => migrator.Migrate(o))
          .Cast<Entity>()
          .Where(o => o is not null && !o.ShouldIgnoreInMigration);

        DatabaseConfig.Database.SaveAll(entityType, allObjectsForEntity);
      }
    }

    public void MigrateDataFrom(string dataDirectory)
    {
      using var existingDatabase = new EntityDatabase(dataDirectory);
      foreach (var entityType in MigrationHelpers.GetAllEntityTypes())
      {
        var allEntitiesOfType = existingDatabase.LoadAll(entityType).Where(e => !e.ShouldIgnoreInMigration);
        // clear ids
        foreach (var entity in allEntitiesOfType)
          entity.Id = null;

        DatabaseConfig.Database.SaveAll(entityType, allEntitiesOfType);
      }
    }
  }
}
