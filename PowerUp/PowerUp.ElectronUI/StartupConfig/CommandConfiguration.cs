using PowerUp.ElectronUI.Api.Generation;
using PowerUp.ElectronUI.Api.PlayerDetails;
using PowerUp.ElectronUI.Api.PlayerEditor;
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
      services.AddSingleton<ReplacePlayerCommand>();
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
      services.AddSingleton<CopyExistingRosterCommand>();
      services.AddSingleton<GetPlayerFlyoutDetailsCommand>();
      services.AddSingleton<FindClosestVoiceCommand>();
      services.AddSingleton<PlayerLookupCommand>();
      services.AddSingleton<PlayerGenerationCommand>();
      services.AddSingleton<GetPlayerInfoCommand>();
      services.AddSingleton<FranchiseLookupCommand>();
      services.AddSingleton<TeamGenerationCommand>();
      services.AddSingleton<GetTeamGenerationStatusCommand>();
      services.AddSingleton<RosterGenerationCommand>();
      services.AddSingleton<GetRosterGenerationStatusCommand>();
      services.AddSingleton<ReplaceFreeAgentCommand>();
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
      commandRegistry.RegisterCommand(typeof(ReplacePlayerCommand), "ReplaceWithExistingPlayer");
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
      commandRegistry.RegisterCommand(typeof(CopyExistingRosterCommand), "CopyExistingRoster");
      commandRegistry.RegisterCommand(typeof(GetPlayerFlyoutDetailsCommand), "GetPlayerFlyoutDetails");
      commandRegistry.RegisterCommand(typeof(FindClosestVoiceCommand), "FindClosestVoice");
      commandRegistry.RegisterCommand(typeof(PlayerLookupCommand), "PlayerLookup");
      commandRegistry.RegisterCommand(typeof(PlayerGenerationCommand), "PlayerGeneration");
      commandRegistry.RegisterCommand(typeof(GetPlayerInfoCommand), "GetPlayerInfo");
      commandRegistry.RegisterCommand(typeof(FranchiseLookupCommand), "FranchiseLookup");
      commandRegistry.RegisterCommand(typeof(TeamGenerationCommand), "TeamGeneration");
      commandRegistry.RegisterCommand(typeof(GetTeamGenerationStatusCommand), "GetTeamGenerationStatus");
      commandRegistry.RegisterCommand(typeof(RosterGenerationCommand), "RosterGeneration");
      commandRegistry.RegisterCommand(typeof(GetRosterGenerationStatusCommand), "GetRosterGenerationStatus");
      commandRegistry.RegisterCommand(typeof(ReplaceFreeAgentCommand), "ReplaceFreeAgent");
    }
  }
}
