using System;

namespace PowerUp.Migrations
{
  [AttributeUsage(AttributeTargets.Property)]
  public class MigrationLateMapAttribute : Attribute
  {
    public Type LateMapper { get; }
    public MigrationLateMapAttribute(Type lateMapperType)
    {
      LateMapper = lateMapperType;
    }
  }

  [AttributeUsage(AttributeTargets.Class)]
  public class MigrationIgnoreAttribute : Attribute { }
}
