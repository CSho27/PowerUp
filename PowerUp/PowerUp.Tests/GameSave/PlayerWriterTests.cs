using NUnit.Framework;
using PowerUp.GameSave;
using Shouldly;
using System.IO;

namespace PowerUp.Tests.GameSave
{
  public class PlayerWriterTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const string TEST_WRITE_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TESTWRITE.dat";
    private const int JASON_GIAMBI_ID = 55;
    private const int SAMMY_SPEEDSTER_ID = 20;
    private const int PAUL_PITCHER_ID = 32;

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
        catch(IOException _) { }
      }

    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Gambini")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sausage")]
    [TestCase(PAUL_PITCHER_ID, "Vegetables")]
    public void Writes_SavedName(int playerId, string savedName)
    {
      var playerToWrite = new GSPlayer { SavedName = savedName };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SavedName.ShouldBe(savedName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "(3 - 4)/[+]?")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Williams")]
    [TestCase(PAUL_PITCHER_ID, "{}^^^Stout")]
    public void Writes_LastName(int playerId, string lastName)
    {
      var playerToWrite = new GSPlayer { LastName = lastName };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.LastName.ShouldBe(lastName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, "Willy")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sammy")]
    [TestCase(PAUL_PITCHER_ID, "Jojo?")]
    public void Writes_FirstName(int playerId, string firstName)
    {
      var playerToWrite = new GSPlayer { FirstName = firstName };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)777)]
    [TestCase(PAUL_PITCHER_ID, (ushort)23)]
    public void Writes_PlayerNumber(int playerId, ushort playerNumber)
    {
      var playerToWrite = new GSPlayer { PlayerNumber = playerNumber };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId); 
      
      loadedPlayer.PlayerNumber.ShouldBe(playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_PlayerNumberNumberOfDigits(int playerId, ushort numberOfDigits)
    {
      var playerToWrite = new GSPlayer { PlayerNumberNumberOfDigits = numberOfDigits };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PlayerNumberNumberOfDigits.ShouldBe(numberOfDigits);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)5)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Writes_SkinAndEyes(int playerId, ushort skinAndEyes)
    {
      var playerToWrite = new GSPlayer { SkinAndEyes = skinAndEyes };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SkinAndEyes.ShouldBe(skinAndEyes);
      loadedPlayer.Skin.ShouldBe((ushort)(skinAndEyes % 5));
      loadedPlayer.AreEyesBrown.ShouldBe(skinAndEyes >= 5);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)6)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Writes_Bat(int playerId, ushort bat)
    {
      var playerToWrite = new GSPlayer { Bat = bat };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Bat.ShouldBe(bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)6)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Writes_Glove(int playerId, ushort glove)
    {
      var playerToWrite = new GSPlayer { Glove = glove };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Glove.ShouldBe(glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)25)]
    [TestCase(PAUL_PITCHER_ID, (ushort)30)]
    public void Writes_Hair(int playerId, ushort hair)
    {
      var playerToWrite = new GSPlayer { Hair = hair };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Hair.ShouldBe(hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)14)]
    [TestCase(PAUL_PITCHER_ID, (ushort)8)]
    public void Writes_HairColor(int playerId, ushort hairColor)
    {
      var playerToWrite = new GSPlayer { HairColor = hairColor };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HairColor.ShouldBe(hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)25)]
    [TestCase(PAUL_PITCHER_ID, (ushort)30)]
    public void Writes_FacialHair(int playerId, ushort facialHair)
    {
      var playerToWrite = new GSPlayer { FacialHair = facialHair };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FacialHair.ShouldBe(facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)14)]
    [TestCase(PAUL_PITCHER_ID, (ushort)8)]
    public void Writes_FacialHairColor(int playerId, ushort facialHairColor)
    {
      var playerToWrite = new GSPlayer { FacialHairColor = facialHairColor };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FacialHairColor.ShouldBe(facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)7)]
    [TestCase(PAUL_PITCHER_ID, (ushort)13)]
    public void Writes_GlassesType(int playerId, ushort glassesType)
    {
      var playerToWrite = new GSPlayer { GlassesType = glassesType };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GlassesType.ShouldBe(glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)9)]
    [TestCase(PAUL_PITCHER_ID, (ushort)10)]
    public void Writes_GlassesColor(int playerId, ushort glassesColor)
    {
      var playerToWrite = new GSPlayer { GlassesColor = glassesColor };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GlassesColor.ShouldBe(glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_EarringType(int playerId, ushort earringType)
    {
      var playerToWrite = new GSPlayer { EarringType = earringType };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EarringType.ShouldBe(earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)12)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Writes_EarringColor(int playerId, ushort earringColor)
    {
      var playerToWrite = new GSPlayer { EarringColor = earringColor };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EarringColor.ShouldBe(earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)11)]
    [TestCase(PAUL_PITCHER_ID, (ushort)8)]
    public void Writes_RightWristband(int playerId, ushort rightWristband)
    {
      var playerToWrite = new GSPlayer { RightWristband = rightWristband };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RightWristband.ShouldBe(rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)6)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)10)]
    public void Writes_LeftWristband(int playerId, ushort leftWristband)
    {
      var playerToWrite = new GSPlayer { LeftWristband = leftWristband };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.LeftWristband.ShouldBe(leftWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)6)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_PrimaryPosition(int playerId, ushort primaryPosition)
    {
      var playerToWrite = new GSPlayer { PrimaryPosition = primaryPosition };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PrimaryPosition.ShouldBe(primaryPosition);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_PitcherCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { PitcherCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PitcherCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)6)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_CatcherCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { CatcherCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CatcherCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Writes_FirstBaseCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { FirstBaseCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FirstBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_SecondBaseCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { SecondBaseCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SecondBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)4)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)7)]
    public void Writes_ThirdBaseCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { ThirdBaseCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ThirdBaseCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)4)]
    public void Writes_ShortstopCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { ShortstopCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ShortstopCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)7)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_LeftFieldCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { LeftFieldCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.LeftFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)4)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)6)]
    public void Writes_CenterFieldCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { CenterFieldCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CenterFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_RightFieldCapability(int playerId, ushort capability)
    {
      var playerToWrite = new GSPlayer { RightFieldCapability = capability };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RightFieldCapability.ShouldBe(capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsStarter(int playerId, bool isStarter)
    {
      var playerToWrite = new GSPlayer { IsStarter = isStarter };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsStarter.ShouldBe(isStarter);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Writes_IsReliever(int playerId, bool isReliever)
    {
      var playerToWrite = new GSPlayer { IsReliever = isReliever };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsReliever.ShouldBe(isReliever);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, false)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, true)]
    public void Writes_IsCloser(int playerId, bool isCloser)
    {
      var playerToWrite = new GSPlayer { IsCloser = isCloser };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneUpAndIn(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUpAndIn = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUpAndIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneUp(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUp = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUp.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneUpAndOut(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUpAndAway = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUpAndAway.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneMiddleIn(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneMiddleIn = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneMiddleIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneMiddle(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneMiddle = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneMiddle.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneDownAndIn(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDownAndIn = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDownAndIn.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneDown(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDown = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDown.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_HotZoneDownAndAway(int playerId, ushort hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDownAndAway = hzValue };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDownAndAway.ShouldBe(hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_BattingSide(int playerId, ushort battingSide)
    {
      var playerToWrite = new GSPlayer { BattingSide = battingSide };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BattingSide.ShouldBe(battingSide);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_ThrowsLefty(int playerId, bool throwsLeft)
    {
      var playerToWrite = new GSPlayer { ThrowsLefty = throwsLeft };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ThrowsLefty.ShouldBe(throwsLeft);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)1)]
    [TestCase(PAUL_PITCHER_ID, (short)-1)]
    public void Writes_Durability(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Durability = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Durability.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_Trajectory(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Trajectory = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Trajectory.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)12)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)15)]
    public void Writes_Contact(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Contact = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Contact.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)255)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)1)]
    [TestCase(PAUL_PITCHER_ID, (ushort)105)]
    public void Writes_Power(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Power = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Power.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)12)]
    public void Writes_RunSpeed(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { RunSpeed = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RunSpeed.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)12)]
    public void Writes_ArmStrength(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { ArmStrength = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ArmStrength.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)12)]
    public void Writes_Fielding(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Fielding = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fielding.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)8)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)12)]
    public void Writes_ErrorResistance(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { ErrorResistance = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ErrorResistance.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)1)]
    [TestCase(PAUL_PITCHER_ID, (short)-1)]
    public void Writes_HittingConsistency(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { HittingConsistency = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingConsistency.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)-3)]
    public void Writes_HittingVersusLefty1(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { HittingVersusLefty1 = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingVersusLefty1.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)1)]
    [TestCase(PAUL_PITCHER_ID, (short)-1)]
    public void Writes_HittingVersusLefty2(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { HittingVersusLefty2 = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingVersusLefty2.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_ClutchHit(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { ClutchHit = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ClutchHit.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsTableSetter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsTableSetter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsTableSetter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Morale(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Morale = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Morale.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSparkplug(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSparkplug = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsSparkplug.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsRallyHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsRallyHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsRallyHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsHotHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsHotHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsHotHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsBackToBackHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsBackToBackHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsBackToBackHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsToughOut(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsToughOut = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsToughOut.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsPushHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsPushHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsPushHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSprayHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSprayHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsSprayHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_InfieldHitter(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { InfieldHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.InfieldHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsContactHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsContactHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsContactHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsPowerHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsPowerHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsPowerHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsGoodPinchHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsGoodPinchHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsGoodPinchHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsFirstballHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsFirstballHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsFirstballHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_Bunting(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Bunting = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Bunting.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_WalkoffHitter(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { WalkoffHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WalkoffHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)0)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)2)]
    [TestCase(PAUL_PITCHER_ID, (ushort)3)]
    public void Writes_BasesLoadedHitter(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { BasesLoadedHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BasesLoadedHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsRefinedHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsRefinedHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsRefinedHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsIntimidatingHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsIntimidatingHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsIntimidatingHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Stealing(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Stealing = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Stealing.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_BaseRunning(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { BaseRunning = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BaseRunning.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_WillSlideHeadFirst(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { WillSlideHeadFirst = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WillSlideHeadFirst.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_WillBreakupDoublePlay(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { WillBreakupDoublePlay = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WillBreakupDoublePlay.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsToughRunner(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsToughRunner = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsToughRunner.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Throwing(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Throwing = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Throwing.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsGoldGlover(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsGoldGlover = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsGoldGlover.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_CanBarehandCatch(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { CanBarehandCatch = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CanBarehandCatch.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_CanSpiderCatch(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { CanSpiderCatch = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CanSpiderCatch.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsErrorProne(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsErrorProne = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsErrorProne.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)4)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)3)]
    [TestCase(PAUL_PITCHER_ID, (ushort)0)]
    public void Writes_Catching(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Catching = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Catching.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsGoodBlocker(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsGoodBlocker = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsGoodBlocker.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsTrashTalker(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsTrashTalker= value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsTrashTalker.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_HasCannonArm(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { HasCannonArm = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HasCannonArm.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsStar(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsStar = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsStar.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_SmallBall(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { SmallBall = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SmallBall.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_SlugOrSlap(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { SlugOrSlap = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SlugOrSlap.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_AggressiveOrPatientHitter(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { AggressiveOrPatientHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.AggressiveOrPatientHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_AggressiveOrCautiousBaseStealer(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { AggressiveOrCautiousBaseStealer = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.AggressiveOrCautiousBaseStealer.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsAggressiveBaserunner(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsAggressiveBaserunner = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsAggressiveBaserunner.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsAggressiveFielder(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsAggressiveFielder = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsAggressiveFielder.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsPivotMan(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsPivotMan = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsPivotMan.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsPullHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsPullHitter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsPullHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)155)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)90)]
    [TestCase(PAUL_PITCHER_ID, (ushort)130)]
    public void Writes_TopThrowingSpeedKMH(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { TopThrowingSpeedKMH = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TopThrowingSpeedKMH.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)155)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)90)]
    [TestCase(PAUL_PITCHER_ID, (ushort)130)]
    public void Writes_Control(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Control = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Control.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)155)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)90)]
    [TestCase(PAUL_PITCHER_ID, (ushort)130)]
    public void Writes_Stamina(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Stamina = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Stamina.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Recovery(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Recovery = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Recovery.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_GroundBallFlyBallPitcher(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { GroundBallOrFlyBallPitcher = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GroundBallOrFlyBallPitcher.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_SafeOrFatPitch(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { SafeOrFatPitch = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SafeOrFatPitch.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_PitchingConsistency(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { PitchingConsistency = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PitchingConsistency.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_WithRunnersInScoringPosition(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { WithRunnersInScoringPosition = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WithRunnersInScoringPosition.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Spin(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Spin = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Spin.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_FastballLife(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { FastballLife = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FastballLife.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_Gyroball(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { Gyroball = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Gyroball.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_ShuttoSpin(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { ShuttoSpin = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ShuttoSpin.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Poise(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Poise = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Poise.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Luck(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Luck = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Luck.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_Release(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { Release = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Release.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (short)-1)]
    [TestCase(SAMMY_SPEEDSTER_ID, (short)0)]
    [TestCase(PAUL_PITCHER_ID, (short)1)]
    public void Writes_VersusLeftHandedBatter(int playerId, short value)
    {
      var playerToWrite = new GSPlayer { VersusLeftHandedBatter = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.VersusLeftHandedBatter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_PoorVersusRunner(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { PoorVersusRunner = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PoorVersusRunner.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_GoodPickoff(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { GoodPickoff = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GoodPickoff.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_GoodDelivery(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { GoodDelivery = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GoodDelivery.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_GoodLowPitch(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { GoodLowPitch = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GoodLowPitch.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_DoctorK(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { DoctorK = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.DoctorK.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_WalkProne(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { WalkProne = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WalkProne.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSandbag(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSandbag = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsSandbag.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_HasPokerFace(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { HasPokerFace = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HasPokerFace.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)35612)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)35440)]
    [TestCase(PAUL_PITCHER_ID, (ushort)35069)]
    public void Writes_VoiceId(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { VoiceId = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.VoiceId.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Slider1Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Slider1Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider1Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Slider1Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Slider1Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider1Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)10)]
    [TestCase(PAUL_PITCHER_ID, (ushort)11)]
    public void Writes_Curve1Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Curve1Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve1Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Curve1Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Curve1Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve1Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)13)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)15)]
    [TestCase(PAUL_PITCHER_ID, (ushort)16)]
    public void Writes_Fork1Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Fork1Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork1Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Fork1Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Fork1Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork1Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)20)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)21)]
    [TestCase(PAUL_PITCHER_ID, (ushort)22)]
    public void Writes_Sinker1Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Sinker1Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker1Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Sinker1Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Sinker1Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker1Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)23)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)24)]
    [TestCase(PAUL_PITCHER_ID, (ushort)25)]
    public void Writes_SinkingFastball1Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball1Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball1Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_SinkingFastball1Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball1Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball1Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)2)]
    public void Writes_TwoSeamType(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { TwoSeamType = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TwoSeamType.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)2)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)0)]
    [TestCase(PAUL_PITCHER_ID, (ushort)1)]
    public void Writes_TwoSeamMovement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { TwoSeamMovement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TwoSeamMovement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Slider2Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Slider2Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider2Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Slider2Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Slider2Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider2Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)7)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)10)]
    [TestCase(PAUL_PITCHER_ID, (ushort)11)]
    public void Writes_Curve2Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Curve2Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve2Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Curve2Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Curve2Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve2Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)13)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)15)]
    [TestCase(PAUL_PITCHER_ID, (ushort)16)]
    public void Writes_Fork2Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Fork2Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork2Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Fork2Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Fork2Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork2Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)20)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)21)]
    [TestCase(PAUL_PITCHER_ID, (ushort)22)]
    public void Writes_Sinker2Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Sinker2Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker2Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_Sinker2Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { Sinker2Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker2Movement.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)23)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)24)]
    [TestCase(PAUL_PITCHER_ID, (ushort)25)]
    public void Writes_SinkingFastball2Type(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball2Type = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball2Type.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, (ushort)3)]
    [TestCase(SAMMY_SPEEDSTER_ID, (ushort)4)]
    [TestCase(PAUL_PITCHER_ID, (ushort)5)]
    public void Writes_SinkingFastball2Movement(int playerId, ushort value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball2Movement = value };
      using (var writer = new PlayerWriter(TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(playerId, playerToWrite);

      GSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball2Movement.ShouldBe(value);
    }
  }
}
