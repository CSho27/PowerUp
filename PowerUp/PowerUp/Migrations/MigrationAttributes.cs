using System;

namespace PowerUp.Migrations
{
  public class MigrationTypeForAttribute : Attribute
  {
    public Type DatabaseType { get; }
    public MigrationTypeForAttribute(Type databaseType)
    {
      DatabaseType = databaseType;
    }
  }

  public class MigrationIgnoreAttribute : Attribute { }
}
