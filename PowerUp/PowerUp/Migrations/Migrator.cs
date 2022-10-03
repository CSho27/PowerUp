using System;

namespace PowerUp.Migrations
{
  public abstract class Migrator
  {
    public abstract object? Migrate(object migrationObject);
    public Migrator() { }
  }

  public abstract class Migrator<TMigrationType, TDatabaseType> : Migrator
  {
    public abstract TDatabaseType? Migrate(TMigrationType migrationObject);
    public override object? Migrate(object migrationObject) => Migrate(migrationObject);
  }
}
