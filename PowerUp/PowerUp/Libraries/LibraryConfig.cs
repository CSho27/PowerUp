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
      services.AddTransient<IVoiceLibrary>(provider => new VoiceLibrary(Path.Combine(dataDirectory, "./data/Voice_Library.csv")));
      services.AddTransient<IBattingStanceLibrary>(provider => new BattingStanceLibrary(Path.Combine(dataDirectory, "./data/BattingForm_Library.csv")));
      services.AddTransient<IPitchingMechanicsLibrary>(provider => new PitchingMechanicsLibrary(Path.Combine(dataDirectory, "./data/PitchingForm_Library.csv")));
      services.AddTransient<IFaceLibrary>(provider => new FaceLibrary(Path.Combine(dataDirectory, "./data/Face_Library.csv")));
      services.AddTransient<ICountryAndSkinColorLibrary>(provider => new CountryAndSkinColorLibrary(Path.Combine(dataDirectory, "./data/CountryAndSkinColor_Library.csv")));
      services.AddTransient<IBaseGameSavePathProvider>(provider => new BaseGameSavePathProvider(Path.Combine(dataDirectory, "./data/BASE.pm2maus.dat")));
      services.AddTransient<IFranchisesAndNamesLibrary>(provider => new FranchisesAndNamesLibrary(Path.Combine(dataDirectory, "./data/FranchisesAndNames_Library.csv")));
    }
  }
}
