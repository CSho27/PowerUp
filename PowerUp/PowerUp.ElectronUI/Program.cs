using ElectronNET.API;
using Serilog;

namespace PowerUp.ElectronUI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var timestamp = DateTime.UtcNow.ToString("yyyyMMdd");
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
        .WriteTo.File(Path.Combine("logs", $"log-{timestamp}.txt"), rollingInterval: RollingInterval.Day)
        .CreateLogger();
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseElectron(args);
          webBuilder.UseStartup<Startup>();
        });
  }
}