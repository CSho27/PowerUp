using PowerUp.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PowerUp.Migrations.MigrationTypes
{
  public static class MigrationHelpers
  {
    public static IEnumerable<Type> GetAllEntityTypes()
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(Entity))!.GetTypes();
      return typesInAssembly
        .Where(t => t.IsAssignableTo(typeof(Entity)))
        .Where(t => t.GetCustomAttribute<MigrationIgnoreAttribute>() == null)
        .Where(t => t != typeof(Entity))
        .Where(t => t.BaseType != typeof(Entity));
    }

    public static IEnumerable<Type> GetAllMigrationTypes()
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(MigrationTypeForAttribute))!.GetTypes();
      return typesInAssembly.Where(t => t.GetCustomAttribute<MigrationTypeForAttribute>() != null);
    }

    public static Type? GetMigrationTypeFor(Type databaseType)
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(MigrationTypeForAttribute))!.GetTypes();
      return typesInAssembly.SingleOrDefault(t => t.GetCustomAttribute<MigrationTypeForAttribute>()?.DatabaseType == databaseType);
    }

    public static Migrator? GetMigratorFor(Type databaseType, Type migrationType)
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(Migrator))!.GetTypes();
      var migratorType = typesInAssembly
        .Where(t => t.IsAssignableTo(typeof(Migrator)))
        .Where(t => t.BaseType?.GenericTypeArguments?.Length == 2)
        .SingleOrDefault(m => m.BaseType!.GenericTypeArguments[0] == databaseType && m.BaseType!.GenericTypeArguments[1] == migrationType);

      return migratorType != null
        ? migratorType.GetConstructors().First().Invoke(null) as Migrator
        : null;
    }
  }
}
