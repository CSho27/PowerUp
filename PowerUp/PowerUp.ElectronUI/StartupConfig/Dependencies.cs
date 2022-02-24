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
      services.AddTransient<IPlayerMapper>(provider => new PlayerMapper(provider.GetRequiredService<ISpecialSavedNameLibrary>()));
    }
  }
}
