using NUnit.Framework;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System.IO;

namespace PowerUp.Tests.GameSave.Objects.Players
{
  public class Ps2PlayerReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./BASLUS-21671_TEST");

    private ICharacterLibrary _characterLibrary;

    [SetUp]
    public void SetUp()
    {
      _characterLibrary = TestConfig.CharacterLibrary.Value;
    }

    [Test]
    public void Reads_PowerProsId()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.PowerProsId.ShouldBe((ushort)1);
    }

    [Test]
    public void Reads_SavedName()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.SavedName.ShouldBe("Abreu");
    }

    [Test]
    public void Reads_FirstName()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.FirstName.ShouldBe("Bobby");
    }

    [Test]
    public void Reads_LastName()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.LastName.ShouldBe("Abreu");
    }

    [Test]
    public void Reads_IsEdited()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.IsEdited.ShouldBe(false);
    }

    [Test]
    public void Reads_UniformNumberNumberOfDigits()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.PlayerNumberNumberOfDigits.ShouldBe((ushort)2);
    }

    [Test]
    public void Reads_UniformNumberValue()
    {
      using var objectReader = new GameSaveObjectReader(_characterLibrary, new FileStream(TEST_READ_GAME_SAVE_FILE_PATH, FileMode.Open, FileAccess.Read), isLittleEndian: true);
      using var loader = new PlayerReader(objectReader, GameSaveFormat.Ps2);
      var player = loader.Read(1);
      player.PlayerNumber.ShouldBe((ushort)53);
    }
  }
}
