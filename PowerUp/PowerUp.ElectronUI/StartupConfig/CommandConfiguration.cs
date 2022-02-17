using PowerUp.ElectronUI.Api.PlayerEditor;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class CommandConfiguration
  {
    public static void RegisterCommandsForDI(this IServiceCollection services)
    {
      services.AddSingleton(serviceProvider => new CommandRegistry(type => serviceProvider.GetRequiredService(type)));
      services.AddSingleton<SavePlayerCommand>();
    }

    public static void AddCommandsToRegistry(this IServiceProvider serviceProvider)
    {
      var commandRegistry = serviceProvider.GetRequiredService<CommandRegistry>();
      commandRegistry.RegisterCommand(typeof(SavePlayerCommand), "SavePlayer");
    }
  }
}
