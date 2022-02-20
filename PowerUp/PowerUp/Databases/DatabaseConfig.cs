namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static IJsonDatabase JsonDatabase { get; private set; } = new JsonDatabase("");

    public static void Initialize(string dataDirectory)
    {
      JsonDatabase = new JsonDatabase(dataDirectory);
    }
  }
}
