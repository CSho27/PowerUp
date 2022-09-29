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

    public static Type? GetMigrationTypeFor(Type databaseType)
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(MigrationTypeForAttribute))!.GetTypes();
      return typesInAssembly.SingleOrDefault(t => t.GetCustomAttribute<MigrationTypeForAttribute>()?.DatabaseType == databaseType);
    }

    public static Migrator? GetMigratorFor(Type databaseType, Type migrationType)
    {
      var typesInAssembly = Assembly.GetAssembly(typeof(Migrator))!.GetTypes();
      return typesInAssembly
        .Select(t => t.GetConstructors().First().Invoke(null) as Migrator)
        .SingleOrDefault(m => m != null && m.DatabaseType == databaseType && m.MigrationType == migrationType);
    }
  }
}
