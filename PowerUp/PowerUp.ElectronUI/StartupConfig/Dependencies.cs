using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.StartupConfig
{
  public static class Dependencies
  {
    public static void RegisterDependencies(this IServiceCollection services)
    {
      services.AddTransient<IRosterImportApi>(provider => new RosterImportApi(provider.GetRequiredService<ICharacterLibrary>()));
    }
  }
}
