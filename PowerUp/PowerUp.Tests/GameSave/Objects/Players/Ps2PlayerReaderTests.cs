using NUnit.Framework;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System.IO;

namespace PowerUp.Tests.GameSave.Objects.Players
{
  public class Ps2PlayerReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./BASLUS-21671_TEST");
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
      _characterLibrary = TestConfig.CharacterLibrary.Value;
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, JASON_GIAMBI_ID)]
    [TestCase(SAMMY_SPEEDSTER_ID, SAMMY_SPEEDSTER_ID)]
    [TestCase(PAUL_PITCHER_ID, PAUL_PITCHER_ID)]
    [TestCase(PETE_SALTINE_ID, PETE_SALTINE_ID)]
    public void Reads_PowerProsId(int playerId, int powerProsId)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PowerProsId.ShouldBe((ushort)powerProsId);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speed")]
    [TestCase(PAUL_PITCHER_ID, "Pitch")]
    [TestCase(PETE_SALTINE_ID, "Salt")]
    public void Reads_SavedName(int playerId, string savedName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(28, 3840)]
    [TestCase(47, 3841)]
    [TestCase(54, 3842)]
    [TestCase(63, 3843)]
    public void Reads_SpecialSavedNameId(int playerId, int expectedId)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SpecialSavedNameId.ShouldBe((ushort)expectedId);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speedster")]
    [TestCase(PAUL_PITCHER_ID, "Pitcher")]
    [TestCase(PETE_SALTINE_ID, "Saltine")]
    public void Reads_LastName(int playerId, string lastName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Jason")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Paul")]
    [TestCase(PETE_SALTINE_ID, "Pete")]
    public void Reads_FirstName(int playerId, string firstName)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsEdited(int playerId, bool isEdited)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsEdited.ShouldBe(isEdited);
    }

    /*
    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_PowerProsTeamId(int playerId, int playerNumber)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PowerProsTeamId.ShouldBe(playerNumber);
    }
    */

    [Test]
    [TestCase(JASON_GIAMBI_ID, 25)]
    [TestCase(SAMMY_SPEEDSTER_ID, 999)]
    [TestCase(PAUL_PITCHER_ID, 36)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_PlayerNumber(int playerId, int playerNumber)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PlayerNumber.ShouldBe((ushort)playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_PlayerNumberNumberOfDigits(int playerId, int numberOfDigits)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PlayerNumberNumberOfDigits.ShouldBe((ushort)numberOfDigits);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 102)]
    [TestCase(SAMMY_SPEEDSTER_ID, 180)]
    [TestCase(PAUL_PITCHER_ID, 206)]
    [TestCase(PETE_SALTINE_ID, 191)]
    public void Reads_Face(int playerId, int face)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Face.ShouldBe((ushort)face);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 4)]
    public void Reads_SkinAndEyes(int playerId, int skinAndEyes)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SkinAndEyes.ShouldBe((ushort)skinAndEyes);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    [TestCase(PETE_SALTINE_ID, 4)]
    public void Reads_Bat(int playerId, int bat)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Bat.ShouldBe((ushort)bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_Glove(int playerId, int glove)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Glove.ShouldBe((ushort)glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 17)]
    [TestCase(SAMMY_SPEEDSTER_ID, 12)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_Hair(int playerId, int hair)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Hair.ShouldBe((ushort)hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    [TestCase(PETE_SALTINE_ID, 7)]
    public void Reads_HairColor(int playerId, int hairColor)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HairColor.ShouldBe((ushort)hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    [TestCase(PETE_SALTINE_ID, 4)]
    public void Reads_FacialHair(int playerId, int facialHair)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.FacialHair.ShouldBe((ushort)facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 8)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    [TestCase(PETE_SALTINE_ID, 6)]
    public void Reads_FacialHairColor(int playerId, int facialHairColor)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.FacialHairColor.ShouldBe((ushort)facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_GlassesType(int playerId, int glassesType)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.EyewearType.ShouldBe((ushort)glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 7)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_GlassesColor(int playerId, int glassesColor)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.EyewearColor.ShouldBe((ushort)glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_EarringType(int playerId, int earringType)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.EarringSide.ShouldBe((ushort)earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 8)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_EarringColor(int playerId, int earringColor)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.EarringColor.ShouldBe((ushort)earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    [TestCase(PETE_SALTINE_ID, 8)]
    public void Reads_RightWristband(int playerId, int rightWristband)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.RightWristband.ShouldBe((ushort)rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    [TestCase(PETE_SALTINE_ID, 7)]
    public void Reads_LeftWristband(int playerId, int leftWristband)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.LeftWristband.ShouldBe((ushort)leftWristband);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 8)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 9)]
    public void Reads_PrimaryPosition(int playerId, int primaryPosition)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PrimaryPosition.ShouldBe((ushort)primaryPosition);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 7)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_PitcherCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.PitcherCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_CatcherCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.CatcherCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_FirstBaseCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.FirstBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_SecondBaseCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SecondBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_ThirdBaseCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ThirdBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_ShortstopCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ShortstopCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_LeftFieldCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.LeftFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 7)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_CenterFieldCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.CenterFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 7)]
    public void Reads_RightFieldCapability(int playerId, int capability)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.RightFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsStarter(int playerId, bool isStarter)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsStarter.ShouldBe(isStarter);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsReliever(int playerId, bool isReliever)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsReliever.ShouldBe(isReliever);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsCloser(int playerId, bool isCloser)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_HotZoneUpAndIn(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneUpAndIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_HotZoneUp(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneUp.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_HotZoneUpAndOut(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneUpAndAway.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_HotZoneMiddleIn(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneMiddleIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_HotZoneMiddle(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneMiddle.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_HotZoneMiddleAway(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneMiddleAway.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_HotZoneDownAndIn(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneDownAndIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_HotZoneDown(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneDown.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_HotZoneDownAndAway(int playerId, int hzValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HotZoneDownAndAway.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_BattingSide(int playerId, int battingSide)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.BattingSide.ShouldBe((ushort)battingSide);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_ThrowsLefty(int playerId, bool throwsLefty)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ThrowsLefty.ShouldBe(throwsLefty);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, -1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_Durability(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Durability.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_Trajectory(int playerId, int trajectory)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Trajectory.ShouldBe((ushort)trajectory);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 13)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    [TestCase(PETE_SALTINE_ID, 10)]
    public void Reads_Contact(int playerId, int contact)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Contact.ShouldBe((ushort)contact);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 206)]
    [TestCase(SAMMY_SPEEDSTER_ID, 138)]
    [TestCase(PAUL_PITCHER_ID, 56)]
    [TestCase(PETE_SALTINE_ID, 170)]
    public void Reads_Power(int playerId, int power)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Power.ShouldBe((ushort)power);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 5)]
    [TestCase(SAMMY_SPEEDSTER_ID, 15)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    [TestCase(PETE_SALTINE_ID, 9)]
    public void Reads_RunSpeed(int playerId, int runSpeed)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.RunSpeed.ShouldBe((ushort)runSpeed);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 4)]
    [TestCase(SAMMY_SPEEDSTER_ID, 14)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    [TestCase(PETE_SALTINE_ID, 10)]
    public void Reads_ArmStrength(int playerId, int armStrength)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ArmStrength.ShouldBe((ushort)armStrength);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 4)]
    [TestCase(SAMMY_SPEEDSTER_ID, 12)]
    [TestCase(PAUL_PITCHER_ID, 7)]
    [TestCase(PETE_SALTINE_ID, 8)]
    public void Reads_Fielding(int playerId, int fielding)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Fielding.ShouldBe((ushort)fielding);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 12)]
    public void Reads_ErrorResistance(int playerId, int errorResistance)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ErrorResistance.ShouldBe((ushort)errorResistance);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, -1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, -1)]
    public void Reads_HittingConsistency(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HittingConsistency.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, -3)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, -2)]
    public void Reads_HittingVersusLefty1(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HittingVersusLefty1.ShouldBe((short)abilityValue);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, -3)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, -2)]
    public void Reads_HittingVersusLefty2(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.HittingVersusLefty2.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, -3)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_ClutchHit(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.ClutchHitter.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsTableSetter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsTableSetter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, -1)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_Morale(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Morale.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsSparkplug(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsSparkplug.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsRallyHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsRallyHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsHotHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsHotHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsBackToBackHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsBackToBackHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsToughOut(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsToughOut.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsPushHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsPushHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsSprayHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsSprayHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_InfieldHitter(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.InfieldHitter.ShouldBe((ushort)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsContactHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsContactHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsPowerHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsPowerHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsGoodPinchHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsGoodPinchHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsFreeSwinger(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsFreeSwinger.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsFirstballHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsFirstballHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_Bunting(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Bunting.ShouldBe((ushort)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_WalkoffHitter(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.WalkoffHitter.ShouldBe((ushort)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    [TestCase(PETE_SALTINE_ID, 2)]
    public void Reads_BasesLoadedHitter(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.BasesLoadedHitter.ShouldBe((ushort)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsRefinedHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsRefinedHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsIntimidatingHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsIntimidatingHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_Stealing(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Stealing.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_BaseRunning(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.BaseRunning.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_WillSlideHeadFirst(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.WillSlideHeadFirst.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_WillBreakupDoublePlay(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.WillBreakupDoublePlay.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsToughRunner(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsToughRunner.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_Throwing(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Throwing.ShouldBe((short)abilityValue);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsGoldGlover(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsGoldGlover.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_CanBarehandCatch(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.CanBarehandCatch.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_CanSpiderCatch(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.CanSpiderCatch.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsErrorProne(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsErrorProne.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 3)]
    public void Reads_Catching(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Catching.ShouldBe((ushort)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsGoodBlocker(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsGoodBlocker.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsTrashTalker(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsTrashTalker.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_HasCannonArm(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsErrorProne.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsStar(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsStar.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    [TestCase(PETE_SALTINE_ID, -1)]
    public void Reads_SmallBall(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SmallBall.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, -1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 0)]
    public void Reads_SlugOrSlap(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.SlugOrSlap.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, -1)]
    public void Reads_AggressiveOrPatientHitter(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.AggressiveOrPatientHitter.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(PETE_SALTINE_ID, 1)]
    public void Reads_AggressiveOrCautiousBaseStealer(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.AggressiveOrCautiousBaseStealer.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsAggressiveBaserunner(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsAggressiveBaserunner.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsAggressiveFielder(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsAggressiveFielder.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    [TestCase(PETE_SALTINE_ID, true)]
    public void Reads_IsPivotMan(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsPivotMan.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    [TestCase(PETE_SALTINE_ID, false)]
    public void Reads_IsPullHitter(int playerId, bool abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.IsPullHitter.ShouldBe(abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(197, -1)]
    public void Reads_GoodOrPoorDayGame(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.GoodOrPoorDayGame.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    [TestCase(904, -1)]
    public void Reads_GoodOrPoorRain(int playerId, int abilityValue)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.GoodOrPoorRain.ShouldBe((short)abilityValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 120)]
    [TestCase(SAMMY_SPEEDSTER_ID, 141)]
    [TestCase(PAUL_PITCHER_ID, 169)]
    [TestCase(PETE_SALTINE_ID, 157)]
    public void Reads_TopThrowingSpeedKMH(int playerId, int topSpeedKMH)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.TopThrowingSpeedKMH.ShouldBe((ushort)topSpeedKMH);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 52)]
    [TestCase(PAUL_PITCHER_ID, 246)]
    [TestCase(PETE_SALTINE_ID, 80)]
    public void Reads_Control(int playerId, int control)
    {
      using var loader = new PlayerReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH, GameSaveFormat.Ps2_2007);
      var player = loader.Read(playerId);
      player.Control.ShouldBe((ushort)control);
    }
  }
}
