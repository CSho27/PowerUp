using NUnit.Framework;
using PowerUp.DebugUtils;
using PowerUp.GameSave;
using Shouldly;

namespace PowerUp.Tests.GameSave
{
  public class PlayerLoaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const string ANALYSIS_BEFORE_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_before.dat";
    private const string ANALYSIS_AFTER_PATH = "C:/dev/PowerUp/SaveFileAnalysis/pm2maus_after.dat";
    private const int JASON_GIAMBI_ID = 55;
    private const int SAMMY_SPEEDSTER_ID = 20;
    private const int PAUL_PITCHER_ID = 32;

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speed")]
    [TestCase(PAUL_PITCHER_ID, "Pitch")]
    public void Loads_SavedName(int playerId, string savedName)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speedster")]
    [TestCase(PAUL_PITCHER_ID, "Pitcher")]
    public void Loads_LastName(int playerId, string lastName)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Jason")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Paul")]
    public void Loads_FirstName(int playerId, string firstName)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Loads_IsEdited(int playerId, bool isEdited)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.IsEdited.ShouldBe(isEdited);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "25")]
    [TestCase(SAMMY_SPEEDSTER_ID, "999")]
    [TestCase(PAUL_PITCHER_ID, "036")]
    public void Loads_PlayerNumer(int playerId, string playerNumber)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 122)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    public void Loads_Face(int playerId, int face)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Face.ShouldBe(face);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Loads_Skin(int playerId, int skin)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Skin.ShouldBe(skin);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Loads_Bat(int playerId, int bat)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Loads_Glove(int playerId, int glove)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Glove.ShouldBe(glove);
    }

    /*
    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    */
    public void BeforeAfterComparison(int playerId, bool isEdited)
    {
      using var beforeLoader = new PlayerLoader(ANALYSIS_BEFORE_PATH);
      using var afterLoader = new PlayerLoader(ANALYSIS_AFTER_PATH);

      var beforePlayer = beforeLoader.Load(playerId);
      var afterPlayer = afterLoader.Load(playerId);

      var beforeBitString = beforePlayer.PlayerNumberBytes.ToBitString();
      var afterBitString = afterPlayer.PlayerNumberBytes.ToBitString();

      beforeBitString.ShouldBe(afterBitString);
    }
  }
}
