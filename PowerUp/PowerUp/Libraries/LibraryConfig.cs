using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace PowerUp.Libraries
{
  public static class LibraryConfig
  {
    public static void RegisterLibraries(this IServiceCollection services, string dataDirectory)
    {
      services.AddSingleton<ICharacterLibrary>(provider => new CharacterLibrary(Path.Combine(dataDirectory, "./data/Character_Library.csv")));
    }

  }
}
