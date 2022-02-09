using NUnit.Framework;
using PowerUp.GameSave;
using Shouldly;

namespace PowerUp.Tests.GameSave
{
  public class PlayerReaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const int JASON_GIAMBI_ID = 55;
    private const int SAMMY_SPEEDSTER_ID = 20;
    private const int PAUL_PITCHER_ID = 32;

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speed")]
    [TestCase(PAUL_PITCHER_ID, "Pitch")]
    public void Reads_SavedName(int playerId, string savedName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Giambi")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Speedster")]
    [TestCase(PAUL_PITCHER_ID, "Pitcher")]
    public void Reads_LastName(int playerId, string lastName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Jason")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Paul")]
    public void Reads_FirstName(int playerId, string firstName)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Reads_IsEdited(int playerId, bool isEdited)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.IsEdited.ShouldBe(isEdited);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)25)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)999)]
    [TestCase(PAUL_PITCHER_ID, (ushort)36)]
    public void Reads_PlayerNumber(int playerId, ushort playerNumber)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_PlayerNumberNumberOfDigits(int playerId, ushort numberOfDigits)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumberNumberOfDigits.ShouldBe(numberOfDigits);
    }


    [Test]
    [TestCase(JASON_GIAMBI_ID, "25")]
    [TestCase(SAMMY_SPEEDSTER_ID, "999")]
    [TestCase(PAUL_PITCHER_ID, "036")]
    public void Reads_PlayerNumberDisplay(int playerId, string playerNumberDisplay)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PlayerNumberDisplay.ShouldBe(playerNumberDisplay);
    }

    // TODO: Fix face
    /*
    [Test]
    [TestCase(JASON_GIAMBI_ID, 122)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    public void Reads_Face(int playerId, int face)
    {
      using var loader = new PlayerLoader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Load(playerId);
      player.Face.ShouldBe(face);
    }
    */

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_Skin(int playerId, ushort skin)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Skin.ShouldBe(skin);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Reads_AreEyesBrown(int playerId, bool areEyesBrown)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.AreEyesBrown.ShouldBe(areEyesBrown);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Reads_Bat(int playerId, ushort bat)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_Glove(int playerId, ushort glove)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Glove.ShouldBe(glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)17)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)12)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Reads_Hair(int playerId, ushort hair)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Hair.ShouldBe(hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_HairColor(int playerId, ushort hairColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HairColor.ShouldBe(hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_FacialHair(int playerId, ushort facialHair)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FacialHair.ShouldBe(facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)8)]
    [TestCase(PAUL_PITCHER_ID, (ushort)13)]
    public void Reads_FacialHairColor(int playerId, ushort facialHairColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FacialHairColor.ShouldBe(facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Reads_GlassesType(int playerId, ushort glassesType)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.GlassesType.ShouldBe(glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)7)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_GlassesColor(int playerId, ushort glassesColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.GlassesColor.ShouldBe(glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_EarringType(int playerId, ushort earringType)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.EarringType.ShouldBe(earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)8)]
    public void Reads_EarringColor(int playerId, ushort earringColor)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.EarringColor.ShouldBe(earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_RightWristband(int playerId, ushort rightWristband)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.RightWristband.ShouldBe(rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_LeftWristband(int playerId, ushort leftWristband)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.LeftWristband.ShouldBe(leftWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)8)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_PrimaryPosition(int playerId, ushort primaryPosition)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PrimaryPosition.ShouldBe(primaryPosition);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)7)]
    public void Reads_PitcherCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.PitcherCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_CatcherCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.CatcherCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_FirstBaseCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.FirstBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_SecondBaseCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SecondBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_ThirdBaseCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.ThirdBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_ShortstopCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.ShortstopCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_LeftFieldCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.LeftFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)7)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_CenterFieldCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.CenterFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_RightFieldCapability(int playerId, ushort capability)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.RightFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Reads_IsStarter(int playerId, bool isStarter)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.IsStarter.ShouldBe(isStarter);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Reads_IsReliever(int playerId, bool isReliever)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.IsReliever.ShouldBe(isReliever);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Reads_IsCloser(int playerId, bool isCloser)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Reads_HotZoneUpAndIn(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneUpAndIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Reads_HotZoneUp(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneUp.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Reads_HotZoneUpAndOut(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneUpAndAway.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_HotZoneMiddleIn(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneMiddleIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_HotZoneMiddle(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneMiddle.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_HotZoneMiddleAway(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneMiddleAway.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_HotZoneDownAndIn(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneDownAndIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_HotZoneDown(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneDown.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_HotZoneDownAndAway(int playerId, ushort hzValue)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.HotZoneDownAndAway.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Reads_BattingSide(int playerId, ushort battingSide)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.BattingSide.ShouldBe(battingSide);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Reads_ThrowsLefty(int playerId, bool throwsLefty)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.ThrowsLefty.ShouldBe(throwsLefty);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Reads_Trajectory(int playerId, ushort trajectory)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Trajectory.ShouldBe(trajectory);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)13)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_Contact(int playerId, ushort contact)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Contact.ShouldBe(contact);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)206)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)138)]
    [TestCase(PAUL_PITCHER_ID, (ushort)56)]
    public void Reads_Power(int playerId, ushort power)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Power.ShouldBe(power);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)5)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)15)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_RunSpeed(int playerId, ushort runSpeed)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.RunSpeed.ShouldBe(runSpeed);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)4)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)14)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Reads_ArmStrength(int playerId, ushort armStrength)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.ArmStrength.ShouldBe(armStrength);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)4)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)12)]
    [TestCase(PAUL_PITCHER_ID, (ushort)7)]
    public void Reads_Fielding(int playerId, ushort fielding)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Fielding.ShouldBe(fielding);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)6)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_ErrorResistance(int playerId, ushort errorResistance)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.ErrorResistance.ShouldBe(errorResistance);
    }

    // Hitting Special Abilities

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)120)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)141)]
    [TestCase(PAUL_PITCHER_ID, (ushort)169)]
    public void Reads_TopThrowingSpeedKMH(int playerId, ushort topSpeedKMH)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.TopThrowingSpeedKMH.ShouldBe(topSpeedKMH);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)52)]
    [TestCase(PAUL_PITCHER_ID, (ushort)246)]
    public void Reads_Control(int playerId, ushort control)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Control.ShouldBe(control);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)67)]
    [TestCase(PAUL_PITCHER_ID, (ushort)237)]
    public void Reads_Stamina(int playerId, ushort stamina)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Stamina.ShouldBe(stamina);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_Slider1Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Slider1Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_Slider1Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Slider1Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)9)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Reads_Curve1Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Curve1Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Reads_Curve1Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Curve1Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)19)]
    [TestCase(PAUL_PITCHER_ID, (ushort)18)]
    public void Reads_Fork1Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Fork1Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Reads_Fork1Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Fork1Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)22)]
    [TestCase(PAUL_PITCHER_ID, (ushort)20)]
    public void Reads_Sinker1Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Sinker1Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Reads_Sinker1Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Sinker1Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)23)]
    [TestCase(PAUL_PITCHER_ID, (ushort)25)]
    public void Reads_SinkingFastball1Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SinkingFastball1Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Reads_SinkingFastball1Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SinkingFastball1Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_TwoSeamType(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.TwoSeamType.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_TwoSeamMovement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.TwoSeamMovement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Reads_Slider2Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Slider2Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_Slider2Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Slider2Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)11)]
    public void Reads_Curve2Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Curve2Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_Curve2Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Curve2Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)17)]
    public void Reads_Fork2Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Fork2Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Reads_Fork2Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Fork2Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)21)]
    public void Reads_Sinker2Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Sinker2Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Reads_Sinker2Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.Sinker2Movement.ShouldBe(movement);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)24)]
    public void Reads_SinkingFastball2Type(int playerId, ushort type)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SinkingFastball2Type.ShouldBe(type);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Reads_SinkingFastball2Movement(int playerId, ushort movement)
    {
      using var loader = new PlayerReader(TEST_READ_GAME_SAVE_FILE_PATH);
      var player = loader.Read(playerId);
      player.SinkingFastball2Movement.ShouldBe(movement);
    }
  }
}
