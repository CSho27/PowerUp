using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams.Api;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class Dependencies
  {
    public static void RegisterDependencies(this IServiceCollection services)
    {
      services.AddTransient<IRosterImportApi>(provider => new RosterImportApi(provider.GetRequiredService<ICharacterLibrary>(), provider.GetRequiredService<IPlayerMapper>()));
      services.AddTransient<IRosterExportApi>(provider => new RosterExportApi(provider.GetRequiredService<IBaseGameSavePathProvider>(), provider.GetRequiredService<ICharacterLibrary>(), provider.GetRequiredService<IPlayerMapper>()));
      services.AddTransient<IPlayerMapper>(provider => new PlayerMapper(provider.GetRequiredService<ISpecialSavedNameLibrary>()));
      services.AddTransient<IPlayerApi>(provider => new PlayerApi());
      services.AddTransient<ITeamApi>(provider => new TeamApi());
      services.AddTransient<IBaseRosterInitializer>(provider => new BaseRosterInitalizer(provider.GetRequiredService<IBaseGameSavePathProvider>(), provider.GetRequiredService<IRosterImportApi>()));
      services.AddTransient<IRosterApi>(provider => new RosterApi());
    }
  }
}
