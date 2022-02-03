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
      File.Copy(TEST_READ_GAME_SAVE_FILE_PATH, TEST_WRITE_GAME_SAVE_FILE_PATH, overwrite: true);
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
  }
}
