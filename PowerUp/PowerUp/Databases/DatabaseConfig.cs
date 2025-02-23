using Microsoft.Extensions.Logging;
using System;

namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static EntityDatabase Database { get; private set; } = null!;

    public static void Initialize(ILogger<EntityDatabase> logger, string dataDirectory)
    {
      Database = new EntityDatabase(logger, dataDirectory);
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(DisposeDatabase);
    }

    private static void DisposeDatabase(object? sender, EventArgs? e) => Database.Dispose();
  }
}
