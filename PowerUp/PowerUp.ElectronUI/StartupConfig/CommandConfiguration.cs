﻿using PowerUp.ElectronUI.Api.PlayerEditor;
using PowerUp.ElectronUI.Api.Rosters;
using PowerUp.ElectronUI.Api.Searching;
using PowerUp.ElectronUI.Api.Teams;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class CommandConfiguration
  {
    public static void RegisterCommandsForDI(this IServiceCollection services)
    {
      services.AddSingleton(serviceProvider => new CommandRegistry(type => serviceProvider.GetRequiredService(type)));

      services.AddSingleton<ExportRosterCommand>();
      services.AddSingleton<InitializeBaseGameSaveCommand>();
      services.AddSingleton<LoadExistingRosterCommand>();
      services.AddSingleton<LoadExistingRosterOptionsCommand>();
      services.AddSingleton<LoadPlayerEditorCommand>();
      services.AddSingleton<SavePlayerCommand>();
      services.AddSingleton<ReplaceWithNewPlayerCommand>();
      services.AddSingleton<ReplacePlayerWithCopyCommand>();
      services.AddSingleton<ReplaceWithExistingPlayerCommand>();
      services.AddSingleton<PlayerSearchCommand>();
      services.AddSingleton<LoadTeamEditorCommand>();
      services.AddSingleton<CopyPlayerCommand>();
      services.AddSingleton<SaveTeamCommand>();
      services.AddSingleton<DiscardTempTeamCommand>();
      services.AddSingleton<CreatePlayerCommand>();
      services.AddSingleton<GetPlayerDetailsCommand>();
      services.AddSingleton<ReplaceTeamWithCopyCommand>();
      services.AddSingleton<ReplaceTeamWithNewTeamCommand>();
      services.AddSingleton<EditRosterNameCommand>();
      services.AddSingleton<TeamSearchCommand>();
      services.AddSingleton<ReplaceTeamWithExistingCommand>();
    }
     
    public static void AddCommandsToRegistry(this IServiceProvider serviceProvider)
    {
      var commandRegistry = serviceProvider.GetRequiredService<CommandRegistry>();

      commandRegistry.RegisterCommand(typeof(ExportRosterCommand), "ExportRoster");
      commandRegistry.RegisterCommand(typeof(InitializeBaseGameSaveCommand), "InitializeBaseGameSave");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterCommand), "LoadExistingRoster");
      commandRegistry.RegisterCommand(typeof(LoadExistingRosterOptionsCommand), "LoadExistingRosterOptions");
      commandRegistry.RegisterCommand(typeof(LoadPlayerEditorCommand), "LoadPlayerEditor");
      commandRegistry.RegisterCommand(typeof(SavePlayerCommand), "SavePlayer");
      commandRegistry.RegisterCommand(typeof(ReplaceWithNewPlayerCommand), "ReplaceWithNewPlayer");
      commandRegistry.RegisterCommand(typeof(ReplacePlayerWithCopyCommand), "ReplacePlayerWithCopy");
      commandRegistry.RegisterCommand(typeof(ReplaceWithExistingPlayerCommand), "ReplaceWithExistingPlayer");
      commandRegistry.RegisterCommand(typeof(PlayerSearchCommand), "PlayerSearch");
      commandRegistry.RegisterCommand(typeof(LoadTeamEditorCommand), "LoadTeamEditor");
      commandRegistry.RegisterCommand(typeof(CopyPlayerCommand), "CopyPlayer");
      commandRegistry.RegisterCommand(typeof(SaveTeamCommand), "SaveTeam");
      commandRegistry.RegisterCommand(typeof(DiscardTempTeamCommand), "DiscardTempTeam");
      commandRegistry.RegisterCommand(typeof(CreatePlayerCommand), "CreatePlayer");
      commandRegistry.RegisterCommand(typeof(GetPlayerDetailsCommand), "GetPlayerDetails");
      commandRegistry.RegisterCommand(typeof(ReplaceTeamWithCopyCommand), "ReplaceTeamWithCopy");
      commandRegistry.RegisterCommand(typeof(ReplaceTeamWithNewTeamCommand), "ReplaceTeamWithNewTeam");
      commandRegistry.RegisterCommand(typeof(EditRosterNameCommand), "EditRosterName");
      commandRegistry.RegisterCommand(typeof(TeamSearchCommand), "TeamSearch");
      commandRegistry.RegisterCommand(typeof(ReplaceTeamWithExistingCommand), "ReplaceTeamWithExisting");
    }
  }
}
