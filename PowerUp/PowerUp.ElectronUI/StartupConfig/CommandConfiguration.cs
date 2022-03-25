using PowerUp.ElectronUI.Api.PlayerEditor;
using PowerUp.ElectronUI.Api.Rosters;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class CommandConfiguration
  {
    public static void RegisterCommandsForDI(this IServiceCollection services)
    {
      services.AddSingleton(serviceProvider => new CommandRegistry(type => serviceProvider.GetRequiredService(type)));

      services.AddSingleton<ExportRosterCommand>();
      services.AddSingleton<LoadBaseGameSaveCommand>();
      services.AddSingleton<LoadExistingRosterCommand>();
      services.AddSingleton<LoadExistingRosterOptionsCommand>();
      services.AddSingleton<LoadPlayerEditorCommand>();
      services.AddSingleton<SavePlayerCommand>();
    }
     
    public static void AddCommandsToRegistry(this IServiceProvider serviceProvider)
    {
      var commandRegistry = serviceProvider.GetRequiredService<CommandRegistry>();

      commandRegistry.RegisterCommand(typeof(ExportRosterCommand), "ExportRoster");
      commandRegistry.RegisterCommand(typeof(LoadBaseGameSaveCommand), "LoadBaseGameSave");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterCommand), "LoadExistingRoster");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterOptionsCommand), "LoadExistingRosterOptions");
      commandRegistry.RegisterCommand(typeof(LoadPlayerEditorCommand), "LoadPlayerEditor");
      commandRegistry.RegisterCommand(typeof(SavePlayerCommand), "SavePlayer");
    }
  }
}
