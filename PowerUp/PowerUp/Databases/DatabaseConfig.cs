using System;

namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static EntityDatabase Database { get; private set; } = new EntityDatabase("");

    public static void Initialize(string dataDirectory)
    {
      Database = new EntityDatabase(dataDirectory);
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(DisposeDatabase);
    }

    private static void DisposeDatabase(object? sender, EventArgs? e) => Database.Dispose();
  }
}
