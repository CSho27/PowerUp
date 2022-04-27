﻿using PowerUp.Databases;
using PowerUp.Entities.Players.Api;
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
    }
  }
}
