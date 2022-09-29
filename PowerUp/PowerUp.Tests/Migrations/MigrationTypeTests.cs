using NUnit.Framework;
using PowerUp.Databases;
using PowerUp.Databases.MigrationEntities;
using PowerUp.Entities.Players;
using PowerUp.Migrations;
using Shouldly;
using System;
using System.Linq;
using System.Reflection;

namespace PowerUp.Tests.Migrations
{
  public class MigrationTypeTests
  {
    [Test]
    public void ValidMigrationTypeExistsForEveryEntity()
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(EntityDatabase)).GetTypes();
      var entityTypes = typesInAssembly.Where(t => t.IsAssignableTo(typeof(Entity)) && t.GetCustomAttribute<MigrationIgnoreAttribute>() == null);

      foreach(var entityType in entityTypes)
      {
        var migrationType = typesInAssembly.SingleOrDefault(t => t.GetCustomAttribute<MigrationTypeForAttribute>()?.DatabaseType == entityType);
        Assert.IsNotNull(migrationType, $"No migration type exists for entity {entityType.Name}");

        migrationType.ShouldCoverPropertiesOf(entityType);
      }
    }

  }
}
