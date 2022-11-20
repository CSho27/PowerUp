namespace PowerUp.Migrations
{
  public abstract class LateMapper
  {
    public abstract void Perform(MigrationIdDictionary migrationIdDictionary, object importedEntity, object savedEntity);
  }

  public abstract class LateMapper<T> : LateMapper
  {
    public abstract void Perform(MigrationIdDictionary migrationIdDictionary, T importedEntity, T savedEntity);
    public override void Perform(MigrationIdDictionary migrationIdDictionary, object importedEntity, object savedEntity)
      => Perform(migrationIdDictionary, (T)importedEntity, (T)savedEntity);
  }
}
