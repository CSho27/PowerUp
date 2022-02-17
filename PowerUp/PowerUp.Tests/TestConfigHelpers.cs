using PowerUp.Libraries;
using System;

namespace PowerUp.Tests
{
  public class TestConfigHelpers
  {
    const string DataDirectoryPath = "C:/Users/short/Documents/PowerUp/data/Character_Library.csv";
    private static Lazy<ICharacterLibrary> lazyCharacterLibrary =  new Lazy<ICharacterLibrary>(new CharacterLibrary(DataDirectoryPath));
    public static ICharacterLibrary GetCharacterLibrary() => lazyCharacterLibrary.Value;
  }
}
