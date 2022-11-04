using PowerUp.Databases;
using System.Linq;

namespace PowerUp.Migrations
{
  public interface IMigrationApi
  {
    public void MigrateDataFrom(string dataDirectory);
  }

  public class MigrationApi : IMigrationApi
  {
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
