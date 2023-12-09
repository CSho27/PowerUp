using NUnit.Framework;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System.IO;

namespace PowerUp.Tests.GameSave.Objects.Players
{
  public class PlayerWriterTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TEST.dat");
    private readonly static string TEST_WRITE_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TESTWRITE.dat");
    private const int JASON_GIAMBI_ID = 55;
    private const int SAMMY_SPEEDSTER_ID = 20;
    private const int PAUL_PITCHER_ID = 32;

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
    [TestCase(JASON_GIAMBI_ID, "Gambini")]
    [TestCase(SAMMY_SPEEDSTER_ID, "Sausage")]
    [TestCase(PAUL_PITCHER_ID, "Vegetables")]
    public void Writes_SavedName(int playerId, string savedName)
    {
      var playerToWrite = new GSPlayer { SavedName = savedName };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FirstName.ShouldBe(firstName);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 777)]
    [TestCase(PAUL_PITCHER_ID, 23)]
    public void Writes_PlayerNumber(int playerId, int playerNumber)
    {
      var playerToWrite = new GSPlayer { PlayerNumber = (ushort)playerNumber };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PlayerNumber.ShouldBe((ushort)playerNumber);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_PlayerNumberNumberOfDigits(int playerId, int numberOfDigits)
    {
      var playerToWrite = new GSPlayer { PlayerNumberNumberOfDigits = (ushort)numberOfDigits };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PlayerNumberNumberOfDigits.ShouldBe((ushort)numberOfDigits);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 5)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Writes_SkinAndEyes(int playerId, int skinAndEyes)
    {
      var playerToWrite = new GSPlayer { SkinAndEyes = (ushort)skinAndEyes };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SkinAndEyes.ShouldBe((ushort)skinAndEyes);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    public void Writes_Bat(int playerId, int bat)
    {
      var playerToWrite = new GSPlayer { Bat = (ushort)bat };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Bat.ShouldBe((ushort)bat);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    public void Writes_Glove(int playerId, int glove)
    {
      var playerToWrite = new GSPlayer { Glove = (ushort)glove };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Glove.ShouldBe((ushort)glove);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 25)]
    [TestCase(PAUL_PITCHER_ID, 30)]
    public void Writes_Hair(int playerId, int hair)
    {
      var playerToWrite = new GSPlayer { Hair = (ushort)hair };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Hair.ShouldBe((ushort)hair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 14)]
    [TestCase(PAUL_PITCHER_ID, 8)]
    public void Writes_HairColor(int playerId, int hairColor)
    {
      var playerToWrite = new GSPlayer { HairColor = (ushort)hairColor };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HairColor.ShouldBe((ushort)hairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 25)]
    [TestCase(PAUL_PITCHER_ID, 30)]
    public void Writes_FacialHair(int playerId, int facialHair)
    {
      var playerToWrite = new GSPlayer { FacialHair = (ushort)facialHair };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FacialHair.ShouldBe((ushort)facialHair);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 14)]
    [TestCase(PAUL_PITCHER_ID, 8)]
    public void Writes_FacialHairColor(int playerId, int facialHairColor)
    {
      var playerToWrite = new GSPlayer { FacialHairColor = (ushort)facialHairColor };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FacialHairColor.ShouldBe((ushort)facialHairColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 7)]
    [TestCase(PAUL_PITCHER_ID, 13)]
    public void Writes_GlassesType(int playerId, int glassesType)
    {
      var playerToWrite = new GSPlayer { EyewearType = (ushort)glassesType };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EyewearType.ShouldBe((ushort)glassesType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 9)]
    [TestCase(PAUL_PITCHER_ID, 10)]
    public void Writes_GlassesColor(int playerId, int glassesColor)
    {
      var playerToWrite = new GSPlayer { EyewearColor = (ushort)glassesColor };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EyewearColor.ShouldBe((ushort)glassesColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_EarringType(int playerId, int earringType)
    {
      var playerToWrite = new GSPlayer { EarringSide = (ushort)earringType };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EarringSide.ShouldBe((ushort)earringType);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 12)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Writes_EarringColor(int playerId, int earringColor)
    {
      var playerToWrite = new GSPlayer { EarringColor = (ushort)earringColor };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.EarringColor.ShouldBe((ushort)earringColor);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 11)]
    [TestCase(PAUL_PITCHER_ID, 8)]
    public void Writes_RightWristband(int playerId, int rightWristband)
    {
      var playerToWrite = new GSPlayer { RightWristband = (ushort)rightWristband };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RightWristband.ShouldBe((ushort)rightWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 6)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 10)]
    public void Writes_LeftWristband(int playerId, int leftWristband)
    {
      var playerToWrite = new GSPlayer { LeftWristband = (ushort)leftWristband };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.LeftWristband.ShouldBe((ushort)leftWristband);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 6)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_PrimaryPosition(int playerId, int primaryPosition)
    {
      var playerToWrite = new GSPlayer { PrimaryPosition = (ushort)primaryPosition };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PrimaryPosition.ShouldBe((ushort)primaryPosition);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_PitcherCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { PitcherCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PitcherCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 6)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_CatcherCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { CatcherCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CatcherCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Writes_FirstBaseCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { FirstBaseCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FirstBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_SecondBaseCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { SecondBaseCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SecondBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 4)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 7)]
    public void Writes_ThirdBaseCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { ThirdBaseCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ThirdBaseCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 4)]
    public void Writes_ShortstopCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { ShortstopCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ShortstopCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 7)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_LeftFieldCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { LeftFieldCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.LeftFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 4)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 6)]
    public void Writes_CenterFieldCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { CenterFieldCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.CenterFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_RightFieldCapability(int playerId, int capability)
    {
      var playerToWrite = new GSPlayer { RightFieldCapability = (ushort)capability };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RightFieldCapability.ShouldBe((ushort)capability);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, false)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsStarter(int playerId, bool isStarter)
    {
      var playerToWrite = new GSPlayer { IsStarter = isStarter };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsCloser.ShouldBe(isCloser);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneUpAndIn(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUpAndIn = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUpAndIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneUp(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUp = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUp.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneUpAndOut(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneUpAndAway = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneUpAndAway.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneMiddleIn(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneMiddleIn = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneMiddleIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneMiddle(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneMiddle = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneMiddle.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneDownAndIn(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDownAndIn = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDownAndIn.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneDown(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDown = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDown.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_HotZoneDownAndAway(int playerId, int hzValue)
    {
      var playerToWrite = new GSPlayer { HotZoneDownAndAway = (ushort)hzValue };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HotZoneDownAndAway.ShouldBe((ushort)hzValue);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_BattingSide(int playerId, int battingSide)
    {
      var playerToWrite = new GSPlayer { BattingSide = (ushort)battingSide };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BattingSide.ShouldBe((ushort)battingSide);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_ThrowsLefty(int playerId, bool throwsLeft)
    {
      var playerToWrite = new GSPlayer { ThrowsLefty = throwsLeft };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ThrowsLefty.ShouldBe(throwsLeft);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    public void Writes_Durability(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Durability = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Durability.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_Trajectory(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Trajectory = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Trajectory.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 12)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 15)]
    public void Writes_Contact(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Contact = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Contact.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 255)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, 105)]
    public void Writes_Power(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Power = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Power.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 12)]
    public void Writes_RunSpeed(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { RunSpeed = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.RunSpeed.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 12)]
    public void Writes_ArmStrength(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { ArmStrength = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ArmStrength.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 12)]
    public void Writes_Fielding(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Fielding = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fielding.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 8)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 12)]
    public void Writes_ErrorResistance(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { ErrorResistance = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ErrorResistance.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    public void Writes_HittingConsistency(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { HittingConsistency = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingConsistency.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, -3)]
    public void Writes_HittingVersusLefty1(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { HittingVersusLefty1 = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingVersusLefty1.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 1)]
    [TestCase(PAUL_PITCHER_ID, -1)]
    public void Writes_HittingVersusLefty2(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { HittingVersusLefty2 = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HittingVersusLefty2.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_ClutchHit(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { ClutchHitter = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ClutchHitter.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsTableSetter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsTableSetter = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsTableSetter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Morale(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Morale = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Morale.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSparkplug(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSparkplug = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsSprayHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_InfieldHitter(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { InfieldHitter = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.InfieldHitter.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsContactHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsContactHitter = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsFirstballHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Bunting(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Bunting = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Bunting.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_WalkoffHitter(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { WalkoffHitter = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WalkoffHitter.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 0)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2)]
    [TestCase(PAUL_PITCHER_ID, 3)]
    public void Writes_BasesLoadedHitter(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { BasesLoadedHitter = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BasesLoadedHitter.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsRefinedHitter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsRefinedHitter = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsIntimidatingHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Stealing(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Stealing = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Stealing.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_BaseRunning(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { BaseRunning = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.BaseRunning.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_WillSlideHeadFirst(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { WillSlideHeadFirst = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsToughRunner.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Throwing(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Throwing = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Throwing.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsGoldGlover(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsGoldGlover = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsErrorProne.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 4)]
    [TestCase(SAMMY_SPEEDSTER_ID, 3)]
    [TestCase(PAUL_PITCHER_ID, 0)]
    public void Writes_Catching(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Catching = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Catching.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsGoodBlocker(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsGoodBlocker = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsGoodBlocker.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsTrashTalker(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsTrashTalker = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsStar.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_SmallBall(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SmallBall = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SmallBall.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_SlugOrSlap(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SlugOrSlap = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SlugOrSlap.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_AggressiveOrPatientHitter(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { AggressiveOrPatientHitter = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.AggressiveOrPatientHitter.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_AggressiveOrCautiousBaseStealer(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { AggressiveOrCautiousBaseStealer = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.AggressiveOrCautiousBaseStealer.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsAggressiveBaserunner(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsAggressiveBaserunner = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsPullHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 155)]
    [TestCase(SAMMY_SPEEDSTER_ID, 90)]
    [TestCase(PAUL_PITCHER_ID, 130)]
    public void Writes_TopThrowingSpeedKMH(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { TopThrowingSpeedKMH = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TopThrowingSpeedKMH.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 155)]
    [TestCase(SAMMY_SPEEDSTER_ID, 90)]
    [TestCase(PAUL_PITCHER_ID, 130)]
    public void Writes_Control(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Control = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Control.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 155)]
    [TestCase(SAMMY_SPEEDSTER_ID, 90)]
    [TestCase(PAUL_PITCHER_ID, 130)]
    public void Writes_Stamina(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Stamina = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Stamina.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Recovery(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Recovery = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Recovery.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_GroundBallFlyBallPitcher(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { GroundBallOrFlyBallPitcher = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.GroundBallOrFlyBallPitcher.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_SafeOrFatPitch(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SafeOrFatPitch = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SafeOrFatPitch.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_PitchingConsistency(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { PitchingConsistency = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PitchingConsistency.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_WithRunnersInScoringPosition(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { WithRunnersInScoringPosition = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.WithRunnersInScoringPosition.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Spin(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Spin = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Spin.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_FastballLife(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { FastballLife = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.FastballLife.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_Gyroball(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { Gyroball = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.ShuttoSpin.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Poise(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Poise = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Poise.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Luck(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Luck = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Luck.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_Release(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Release = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Release.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_VersusLeftHandedBatter(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { PitchingVersusLefty = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PitchingVersusLefty.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_PoorVersusRunner(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { PoorVersusRunner = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.DoctorK.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_WalkProne(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsWalkProne = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsWalkProne.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSandbag(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSandbag = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
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
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HasPokerFace.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsIntimidatingPitcher(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsIntimidatingPitcher = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsIntimidatingPitcher.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsBattler(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsBattler = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsBattler.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsHotHead(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsHotHead = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsHotHead.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsSlowStarter(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsSlowStarter = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsSlowStarter.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsStarterFinisher(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsStarterFinisher = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsStarterFinisher.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_IsChokeArtist(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { IsChokeArtist = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.IsChokeArtist.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_HasGoodReflexes(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { HasGoodReflexes = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HasGoodReflexes.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, true)]
    [TestCase(SAMMY_SPEEDSTER_ID, true)]
    [TestCase(PAUL_PITCHER_ID, false)]
    public void Writes_HasGoodPace(int playerId, bool value)
    {
      var playerToWrite = new GSPlayer { HasGoodPace = value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.HasGoodPace.ShouldBe(value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, -1)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_PowerOrBreakingBallPitcher(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { PowerOrBreakingBallPitcher = (short)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.PowerOrBreakingBallPitcher.ShouldBe((short)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2284)]
    [TestCase(SAMMY_SPEEDSTER_ID, 2907)]
    [TestCase(PAUL_PITCHER_ID, 2426)]
    public void Writes_VoiceId(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { VoiceId = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.VoiceId.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Slider1Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Slider1Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider1Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Slider1Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Slider1Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider1Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 10)]
    [TestCase(PAUL_PITCHER_ID, 11)]
    public void Writes_Curve1Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Curve1Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve1Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Curve1Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Curve1Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve1Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 13)]
    [TestCase(SAMMY_SPEEDSTER_ID, 15)]
    [TestCase(PAUL_PITCHER_ID, 16)]
    public void Writes_Fork1Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Fork1Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork1Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Fork1Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Fork1Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork1Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 20)]
    [TestCase(SAMMY_SPEEDSTER_ID, 21)]
    [TestCase(PAUL_PITCHER_ID, 22)]
    public void Writes_Sinker1Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Sinker1Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker1Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Sinker1Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Sinker1Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker1Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 23)]
    [TestCase(SAMMY_SPEEDSTER_ID, 24)]
    [TestCase(PAUL_PITCHER_ID, 25)]
    public void Writes_SinkingFastball1Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball1Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball1Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_SinkingFastball1Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball1Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball1Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 2)]
    public void Writes_TwoSeamType(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { TwoSeamType = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TwoSeamType.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 2)]
    [TestCase(SAMMY_SPEEDSTER_ID, 0)]
    [TestCase(PAUL_PITCHER_ID, 1)]
    public void Writes_TwoSeamMovement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { TwoSeamMovement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.TwoSeamMovement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Slider2Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Slider2Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider2Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Slider2Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Slider2Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Slider2Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 7)]
    [TestCase(SAMMY_SPEEDSTER_ID, 10)]
    [TestCase(PAUL_PITCHER_ID, 11)]
    public void Writes_Curve2Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Curve2Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve2Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Curve2Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Curve2Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Curve2Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 13)]
    [TestCase(SAMMY_SPEEDSTER_ID, 15)]
    [TestCase(PAUL_PITCHER_ID, 16)]
    public void Writes_Fork2Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Fork2Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork2Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Fork2Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Fork2Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Fork2Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 20)]
    [TestCase(SAMMY_SPEEDSTER_ID, 21)]
    [TestCase(PAUL_PITCHER_ID, 22)]
    public void Writes_Sinker2Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Sinker2Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker2Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_Sinker2Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { Sinker2Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.Sinker2Movement.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 23)]
    [TestCase(SAMMY_SPEEDSTER_ID, 24)]
    [TestCase(PAUL_PITCHER_ID, 25)]
    public void Writes_SinkingFastball2Type(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball2Type = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball2Type.ShouldBe((ushort)value);
    }

    [Test]
    [TestCase(JASON_GIAMBI_ID, 3)]
    [TestCase(SAMMY_SPEEDSTER_ID, 4)]
    [TestCase(PAUL_PITCHER_ID, 5)]
    public void Writes_SinkingFastball2Movement(int playerId, int value)
    {
      var playerToWrite = new GSPlayer { SinkingFastball2Movement = (ushort)value };
      using (var writer = new PlayerWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        writer.Write(playerId, playerToWrite);

      IGSPlayer loadedPlayer = null;
      using (var reader = new PlayerReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedPlayer = reader.Read(playerId);

      loadedPlayer.SinkingFastball2Movement.ShouldBe((ushort)value);
    }
  }
}
