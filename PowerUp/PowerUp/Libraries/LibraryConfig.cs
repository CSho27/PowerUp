using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace PowerUp.Libraries
{
  public static class LibraryConfig
  {
    public static void RegisterLibraries(this IServiceCollection services, string dataDirectory)
    {
      services.AddSingleton<ICharacterLibrary>(provider => new CharacterLibrary(Path.Combine(dataDirectory, "./data/Character_Library.csv")));
      services.AddSingleton<ISpecialSavedNameLibrary>(provider => new SpecialSavedNameLibrary(Path.Combine(dataDirectory, "./data/SpecialSavedName_Library.csv")));
      services.AddTransient<IBaseGameSavePathProvider>(provider => new BaseGameSavePathProvider(Path.Combine(dataDirectory, "./data/BASE.pm2maus.dat")));
    }

  }
}
