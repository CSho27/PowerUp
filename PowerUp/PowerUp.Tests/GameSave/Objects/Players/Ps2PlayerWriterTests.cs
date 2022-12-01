using NUnit.Framework;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System.IO;

namespace PowerUp.Tests.GameSave.Objects.Players
{
  public class Ps2PlayerWriterTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./BASLUS-21671_TEST");
    private readonly static string TEST_WRITE_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./BASLUS-21671_TESTWRITE");
    private const int JASON_GIAMBI_ID = 55;
    // Miguel Cairo in base roster
    private const int SAMMY_SPEEDSTER_ID = 20;
    // Roger Clemens in base roster
    private const int PAUL_PITCHER_ID = 32;
    // Hideki Matsui in base roster
    private const int PETE_SALTINE_ID = 583;

    private ICharacterLibrary _characterLibrary;

    [SetUp]
    public void SetUp()
    {
      var success = false;
      while (!success)
      {
        try
        {
          File.Copy(TEST_READ_GAME_SAVE_FILE_PATH, TEST_WRITE_GAME_SAVE_FILE_PATH, overwrite: true);
          success = true;
        }
        catch (IOException _) { }
      }

      _characterLibrary = TestConfig.CharacterLibrary.Value;
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 4)]
    public void Writes_PowerProsId(int playerId, int powerProsId)
    {
      var playerToWrite = new Ps2GSPlayer { PowerProsId = (ushort)powerProsId };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PowerProsId.ShouldBe((ushort)powerProsId);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Gambini")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sausage")]
    [TestCase(PAUL_PITCHER_ID, "Vegetables")]
    [TestCase(PETE_SALTINE_ID, "Brownies")]
    public void Writes_SavedName(int playerId, string savedName)
    {
      var playerToWrite = new Ps2GSPlayer { SavedName = savedName };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 777)]
    [TestCase(PAUL_PITCHER_ID, 23)]
    public void Writes_PlayerNumber(int playerId, int playerNumber)
    {
      var playerToWrite = new Ps2GSPlayer { PlayerNumber = (ushort)playerNumber };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PlayerNumber.ShouldBe((ushort)playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_PlayerNumberNumberOfDigits(int playerId, int numberOfDigits)
    {
      var playerToWrite = new Ps2GSPlayer { PlayerNumberNumberOfDigits = (ushort)numberOfDigits };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PlayerNumberNumberOfDigits.ShouldBe((ushort)numberOfDigits);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Writes_SkinAndEyes(int playerId, int skinAndEyes)
    {
      var playerToWrite = new Ps2GSPlayer { SkinAndEyes = (ushort)skinAndEyes };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SkinAndEyes.ShouldBe((ushort)skinAndEyes);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    public void Writes_Bat(int playerId, int bat)
    {
      var playerToWrite = new Ps2GSPlayer { Bat = (ushort)bat };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Bat.ShouldBe((ushort)bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2284)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2907)]
    [TestCase(PAUL_PITCHER_ID, 2426)]
    public void Writes_VoiceId(int playerId, int value)
    {
      var playerToWrite = new Ps2GSPlayer { VoiceId = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.VoiceId.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 284)]
    [TestCase(SAMMY_SPEEDSTER_ID, 405)]
    [TestCase(PAUL_PITCHER_ID, 34)]
    public void Writes_BattingAveragePoints(int playerId, int value)
    {
      var playerToWrite = new Ps2GSPlayer { BattingAveragePoints = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BattingAveragePoints.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    public void Writes_Durability(int playerId, int value)
    {
      var playerToWrite = new Ps2GSPlayer { Durability = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Durability.ShouldBe((short)value);
    }
  }
}
