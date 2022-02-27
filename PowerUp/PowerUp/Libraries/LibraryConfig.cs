using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Libraries
{
  public static class LibraryConfig
  {
    public static void RegisterLibraries(this IServiceCollection services, string dataDirectory)
    {
      services.AddSingleton<ICharacterLibrary>(provider => new CharacterLibrary(Path.Combine(dataDirectory, "./data/Character_Library.csv")));
      services.AddSingleton<ISpecialSavedNameLibrary>(provider => new SpecialSavedNameLibrary(Path.Combine(dataDirectory, "./data/SpecialSavedName_Library.csv")));
      services.AddTransient<IVoiceLibrary>(provider => new MockVoiceLibrary()); //new VoiceLibrary(Path.Combine(dataDirectory, "./data/Voice_Library.csv")));
      services.AddTransient<IBattingStanceLibrary>(provider => new BattingStanceLibrary(Path.Combine(dataDirectory, "./data/BattingForm_Library.csv")));
      services.AddTransient<IPitchingMechanicsLibrary>(provider => new PitchingMechanicsLibrary(Path.Combine(dataDirectory, "./data/PitchingForm_Library.csv")));
      services.AddTransient<IBaseGameSavePathProvider>(provider => new BaseGameSavePathProvider(Path.Combine(dataDirectory, "./data/BASE.pm2maus.dat")));
    }


    public class MockVoiceLibrary : IVoiceLibrary
    {
      public int this[string key] => 0;

      public string this[int key] => "";

      public IEnumerable<KeyValuePair<int, string>> GetAll() => Enumerable.Empty<KeyValuePair<int, string>>();
    }

  }
}
