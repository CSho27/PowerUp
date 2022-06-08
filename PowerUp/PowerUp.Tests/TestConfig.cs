using Microsoft.Extensions.Configuration;
using PowerUp.Libraries;
using System;
using System.IO;

namespace PowerUp.Tests
{
  public static class TestConfig
  {
    private static Lazy<IConfigurationRoot> configuration = new Lazy<IConfigurationRoot>(() => new ConfigurationBuilder().AddJsonFile("appsettings.json", false, false).Build());

    public static string DataDirectoryPath => configuration.Value["DataDirectory"];
    public static string AssetDirectoryPath => configuration.Value["AssetDirectory"];
    public static Lazy<ICharacterLibrary> CharacterLibrary = new Lazy<ICharacterLibrary>(() => new CharacterLibrary(Path.Combine(DataDirectoryPath, "./data/Character_Library.csv")));
  }
}
