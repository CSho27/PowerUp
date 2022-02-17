using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static void RegisterDatabases(this IServiceCollection services, string dataDirectory)
    {
      services.AddTransient<IPlayerDatabase>(provider => new PlayerDatabase(Path.Combine(dataDirectory, "Players")));
    }
  }
}
