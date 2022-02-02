using NUnit.Framework;
using PowerUp.GameSave;
using Shouldly;

namespace PowerUp.Tests.GameSave
{
  public class PlayerLoaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
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
    [TestCase(JASON_GIAMBI_ID, 25)]
    [TestCase(SAMMY_SPEEDSTER_ID, 999)]
    [TestCase(PAUL_PITCHER_ID, 36)]
    public void Loads_PlayerNumber(int playerId, int playerNumber)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Loads_PlayerNumberNumberOfDigits(int playerId, int numberOfDigits)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.PlayerNumberNumberOfDigits.ShouldBe(numberOfDigits);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, "25")]
    [TestCase(SAMMY_SPEEDSTER_ID, "999")]
    [TestCase(PAUL_PITCHER_ID, "036")]
    public void Loads_PlayerNumberDisplay(int playerId, string playerNumberDisplay)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.PlayerNumberDisplay.ShouldBe(playerNumberDisplay);
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
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    public void Loads_Skin(int playerId, int skin)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Skin.ShouldBe(skin);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Loads_AreEyesBlue(int playerId, bool areEyesBlue)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.AreEyesBrown.ShouldBe(areEyesBlue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Loads_Bat(int playerId, int bat)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    public void Loads_Glove(int playerId, int glove)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Glove.ShouldBe(glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 17)]
    [TestCase(SAMMY_SPEEDSTER_ID, 12)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Loads_Hair(int playerId, int hair)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Hair.ShouldBe(hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    public void Loads_HairColor(int playerId, int hairColor)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.HairColor.ShouldBe(hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    public void Loads_FacialHair(int playerId, int facialHair)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.FacialHair.ShouldBe(facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 8)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    public void Loads_FacialHairColor(int playerId, int facialHairColor)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.FacialHairColor.ShouldBe(facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Loads_GlassesType(int playerId, int glassesType)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.GlassesType.ShouldBe(glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 7)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    public void Loads_GlassesColor(int playerId, int glassesColor)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.GlassesColor.ShouldBe(glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Loads_EarringType(int playerId, int earringType)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.EarringType.ShouldBe(earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 8)]
    public void Loads_EarringColor(int playerId, int earringColor)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.EarringColor.ShouldBe(earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    public void Loads_RightWristband(int playerId, int rightWristband)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.RightWristband.ShouldBe(rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    public void Loads_LeftWristband(int playerId, int leftWristband)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.LeftWristband.ShouldBe(leftWristband);
    }
  }
}
