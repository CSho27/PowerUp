namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static EntityDatabase Database { get; private set; } = new EntityDatabase("");

    public static void Initialize(string dataDirectory)
    {
      Database = new EntityDatabase(dataDirectory);
    }
  }
}
