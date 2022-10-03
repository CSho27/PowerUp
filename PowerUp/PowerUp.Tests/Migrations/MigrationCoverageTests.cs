using NUnit.Framework;
using PowerUp.Migrations;
using PowerUp.Migrations.MigrationTypes;
using System.Reflection;

namespace PowerUp.Tests.Migrations
{
  public class MigrationCoverageTests
  {
    [Test]
    public void ValidMigrationTypeExistsForEveryEntity()
    {
      foreach(var entityType in MigrationHelpers.GetAllEntityTypes())
      {
        var migrationType = MigrationHelpers.GetMigrationTypeFor(entityType);
        Assert.IsNotNull(migrationType, $"No migration type exists for entity {entityType.Name}");
        migrationType.ShouldCoverPropertiesOf(entityType);
      }
    }

    [Test]
    public void MigratorExistsForEveryMigrationType()
    {
      foreach (var migrationType in MigrationHelpers.GetAllMigrationTypes())
      {
        var databaseType = migrationType.GetCustomAttribute<MigrationTypeForAttribute>().DatabaseType;
        var migrator = MigrationHelpers.GetMigratorFor(migrationType, databaseType);
        Assert.IsNotNull(migrator, $"No Migrator exists to convert {migrationType.Name} to {databaseType.Name}");
      }
    }
  }
}
