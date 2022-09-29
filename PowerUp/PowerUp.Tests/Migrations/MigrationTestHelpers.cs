using NUnit.Framework;
using PowerUp.Migrations;
using Shouldly;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace PowerUp.Tests.Migrations
{
  public static class MigrationTestHelpers
  {
    public static void ShouldCoverPropertiesOf(this Type migrationType, Type databaseType)
    {
      var propsOfDatabaseTypeWithPublicSetters = databaseType
        .GetProperties()
        .Where(p => p.SetMethod?.IsPublic ?? false)
        .Where(p => p.GetCustomAttribute<MigrationIgnoreAttribute>() == null);

      var propsOfMigrationType = migrationType.GetProperties();

      // Test all props on migration type
      foreach (var prop in propsOfMigrationType)
      {
        Assert.IsTrue(prop.CanWrite, $"{migrationType.Name}.{prop.Name} does not have a public setter. All Migration type props must have public setters");

        var nullabilityContext = new NullabilityInfoContext();
        var nullabilityInfo = nullabilityContext.Create(prop);
        if (nullabilityInfo.WriteState != NullabilityState.Nullable)
          Assert.Fail($"{migrationType.Name}.{prop.Name} is not nullable. All Migration type props must be nullable");
      }

      // Test that there is a matching prop for each type of database type
      foreach (var propOfDatabaseType in propsOfDatabaseTypeWithPublicSetters)
      {
        var matchingProp = propsOfMigrationType.SingleOrDefault(p => p.Name == propOfDatabaseType.Name);
        Assert.IsNotNull(matchingProp, $"{migrationType.Name} does not contain a matching property for {databaseType.Name}.{propOfDatabaseType.Name}");

        var databasePropType = propOfDatabaseType.PropertyType;
        var matchingPropType = matchingProp.PropertyType;
        var propContext = $"{migrationType.Name}.{matchingProp.Name} => {databaseType.Name}.{propOfDatabaseType.Name}";
        AssertMigrationTypeValid(propContext, databasePropType, matchingPropType);
      }
    }

    public static void AssertMigrationTypeValid(string propContext, Type databasePropType, Type migrationPropType)
    {
      if (databasePropType.IsSystemType() || databasePropType.IsValueType)
      {
        Assert.IsTrue
          (databasePropType == migrationPropType
            || databasePropType == migrationPropType.UnderlyingNullableType()
          , $"{propContext}: type of {migrationPropType.Name} does not match {databasePropType.Name}"
          );
      }
      else if (databasePropType.IsAssignableTo(typeof(ICollection)) || databasePropType.IsAssignableTo(typeof(IEnumerable)) || databasePropType.IsArray)
      {
        Assert.IsTrue
          ( databasePropType.Name == migrationPropType.Name
          , $"{propContext}: {migrationPropType.Name} is not a valid migration type for {databasePropType.Name}"
          );
        AssertMigrationTypeValid(propContext, databasePropType.GenericTypeArguments.First(), migrationPropType.GenericTypeArguments.First());
      }
      else
      {
        Assert.IsTrue
          ( databasePropType == migrationPropType.GetCustomAttribute<MigrationTypeForAttribute>()?.DatabaseType
          , $"{propContext}: {migrationPropType.Name} is not a valid migration type for {databasePropType.Name}"
          );

        migrationPropType.ShouldCoverPropertiesOf(databasePropType);
      }
    }

    public static Type UnderlyingNullableType(this Type type) => Nullable.GetUnderlyingType(type);
    public static bool IsSystemType(this Type type) => type.Namespace == "System";
  } 
}