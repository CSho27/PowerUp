using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerUp;
using PowerUp.CommandLine.Commands;
using Serilog;
using System.CommandLine;


var appHost = StartUp();
await Run(appHost);

async Task Run(IHost host)
{
  var quit = false;

  await using var scope = host.Services.CreateAsyncScope();
  var commands = CommandRegistry.BuildRootCommand(scope.ServiceProvider, () => { quit = true; });

  while (!quit)
  {
    Console.WriteLine();
    Console.WriteLine("Enter command:");
    Console.Write(">");
    var command = Console.ReadLine();

    if (!string.IsNullOrEmpty(command))
    {
      await commands.InvokeAsync(command);
    }
  }
}

static IHost StartUp()
{
  var builder = Host.CreateDefaultBuilder();
  var configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

  var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss_ffffff");
  Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
      .WriteTo.File(Path.Combine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), "PowerUp", "logs", $"log-{timestamp}.txt"), rollingInterval: RollingInterval.Infinite)
      .CreateLogger();

  builder.UseSerilog();

  var dataDirectory = configuration["DataDirectory"];
  Log.Information($"Data Directory: {Path.GetFullPath(dataDirectory ?? "null")}");

  builder.ConfigureServices(services =>
  {
    services.RegisterCommands();
    services.RegisterDependencies();
    services.AddLogging();
  });

  var host = builder.Build();
  host.Start();
  return host;
}