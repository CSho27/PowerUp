using Microsoft.Extensions.DependencyInjection;

namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static void RegisterDatabases(this IServiceCollection services, string dataDirectory)
    {
      services.AddTransient<IJsonDatabase>(provider => new JsonDatabase(dataDirectory));
    }
  }
}
