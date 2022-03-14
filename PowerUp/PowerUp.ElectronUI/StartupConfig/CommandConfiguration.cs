using PowerUp.ElectronUI.Api.PlayerEditor;
using PowerUp.ElectronUI.Api.Rosters;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class CommandConfiguration
  {
    public static void RegisterCommandsForDI(this IServiceCollection services)
    {
      services.AddSingleton(serviceProvider => new CommandRegistry(type => serviceProvider.GetRequiredService(type)));
      services.AddSingleton<SavePlayerCommand>();
      services.AddSingleton<LoadBaseGameSaveCommand>();
      services.AddSingleton<LoadPlayerEditorCommand>();
      services.AddSingleton<LoadExistingRosterCommand>();
      services.AddSingleton<LoadExistingRosterOptionsCommand>();
    }
     
    public static void AddCommandsToRegistry(this IServiceProvider serviceProvider)
    {
      var commandRegistry = serviceProvider.GetRequiredService<CommandRegistry>();
      commandRegistry.RegisterCommand(typeof(SavePlayerCommand), "SavePlayer");
      commandRegistry.RegisterCommand(typeof(LoadBaseGameSaveCommand), "LoadBaseGameSave");
      commandRegistry.RegisterCommand(typeof(LoadPlayerEditorCommand), "LoadPlayerEditor");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterCommand), "LoadExistingRoster");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterOptionsCommand), "LoadExistingRosterOptions");
    }
  }
}
