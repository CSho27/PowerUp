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
  }
}
