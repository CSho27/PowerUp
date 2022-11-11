using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams.Api;
using PowerUp.Fetchers.BaseballReference;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.GameSaveManagement;
using PowerUp.Generators;
using PowerUp.Libraries;
using PowerUp.Mappers.Players;
using PowerUp.Migrations;
using PowerUp.Providers;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class Dependencies
  {
    public static void RegisterDependencies(this IServiceCollection services)
    {
      services.AddTransient<IRosterImportApi>(provider => new RosterImportApi(provider.GetRequiredService<ICharacterLibrary>(), provider.GetRequiredService<IPlayerMapper>()));
      services.AddTransient<IRosterExportApi>(provider => new RosterExportApi(provider.GetRequiredService<ICharacterLibrary>(), provider.GetRequiredService<IPlayerMapper>(), provider.GetRequiredService<IGameSaveManager>(), provider.GetRequiredService<IPlayerSalariesLibrary>(), provider.GetRequiredService<IPowerProsIdAssigner>()));
      services.AddTransient<IPlayerMapper>(provider => new PlayerMapper(provider.GetRequiredService<ISpecialSavedNameLibrary>()));
      services.AddTransient<IPlayerApi>(provider => new PlayerApi());
      services.AddTransient<ITeamApi>(provider => new TeamApi(provider.GetRequiredService<IPlayerApi>()));
      services.AddTransient<IBaseRosterInitializer>(provider => new BaseRosterInitalizer(provider.GetRequiredService<IBaseGameSavePathProvider>(), provider.GetRequiredService<IRosterImportApi>()));
      services.AddTransient<IRosterApi>(provider => new RosterApi());
      services.AddTransient<IMLBLookupServiceClient>(provider => new MLBLookupServiceClient());
      services.AddTransient<IPlayerStatisticsFetcher>(provider => new LSPlayerStatisticsFetcher(provider.GetRequiredService<IMLBLookupServiceClient>()));
      services.AddTransient<ISkinColorGuesser>(provider => new SkinColorGuesser(provider.GetRequiredService<ICountryAndSkinColorLibrary>()));
      services.AddTransient<IPlayerGenerator>(provider => new PlayerGenerator(provider.GetRequiredService<IPlayerApi>(), provider.GetRequiredService<IPlayerStatisticsFetcher>(), provider.GetRequiredService<IBaseballReferenceClient>()));
      services.AddTransient<ITeamGenerator>(provider => new TeamGenerator(provider.GetRequiredService<IMLBLookupServiceClient>(), provider.GetRequiredService<IPlayerGenerator>()));
      services.AddTransient<IRosterGenerator>(provider => new RosterGenerator(provider.GetRequiredService<IMLBLookupServiceClient>(), provider.GetRequiredService<ITeamGenerator>()));
      services.AddSingleton<IBaseballReferenceClient>(provider => new BaseballReferenceClient());
      services.AddTransient<IBaseballReferenceUrlProvider>(provider => new BaseballReferenceUrlProvider(provider.GetRequiredService<IBaseballReferenceClient>()));
      services.AddTransient<IGameSaveManager>(provider => new GameSaveManager(provider.GetRequiredService<ICharacterLibrary>(), provider.GetRequiredService<IBaseGameSavePathProvider>()));
      services.AddTransient<IMigrationApi>(provider => new MigrationApi());
      services.AddTransient<IPowerProsIdAssigner>(provider => new PowerProsIdAssigner());
    }
  }
}
