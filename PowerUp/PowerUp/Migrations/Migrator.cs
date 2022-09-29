using System;

namespace PowerUp.Migrations
{
  public abstract class Migrator
  {
    public abstract Type MigrationType { get; }
    public abstract Type DatabaseType { get; }
    public abstract object Migrate(object migrationObject);
    public Migrator() { }
  }

  public abstract class Migrator<TMigrationType, TDatabaseType> : Migrator
  {
    public abstract TDatabaseType Migrate(TMigrationType migrationObject);

    public override Type MigrationType => typeof(TMigrationType);
    public override Type DatabaseType => typeof(TDatabaseType);
    public override object Migrate(object migrationObject) => Migrate(migrationObject);
  }
}
