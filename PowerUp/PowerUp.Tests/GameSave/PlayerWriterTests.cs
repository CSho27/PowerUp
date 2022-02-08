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
  }
}
