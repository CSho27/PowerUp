using NUnit.Framework;
using PowerUp.Migrations.MigrationTypes;

namespace PowerUp.Tests.Migrations
{
  public class MigrationTypeTests
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
  }
}
