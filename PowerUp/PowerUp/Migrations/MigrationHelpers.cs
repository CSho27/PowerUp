using PowerUp.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PowerUp.Migrations
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
  }
}
