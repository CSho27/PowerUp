using NUnit.Framework;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using Shouldly;
using System.IO;
using System.Linq;

namespace PowerUp.Tests.GameSave.Objects.GameSaves
{
  public class GameSaveWriterTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TEST.dat");
    private readonly static string TEST_WRITE_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TESTWRITE.dat");

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
    public void Write_WritesData()
    {
      var testPlayer = new GSPlayer { PowerProsId = 1, PowerProsTeamId = 1 };
      var testTeam = new GSTeam { PlayerEntries = new[] { new GSTeamPlayerEntry { PowerProsPlayerId = 1, PowerProsTeamId = 1 } } };
      var testLineupPlayer = new GSLineupPlayer { PowerProsPlayerId = 1, Position = 1 };
      var testLineupDef = new GSLineupDefinition
      {
        NoDHLineup = Enumerable.Repeat(testLineupPlayer, 9),
        DHLineup = Enumerable.Repeat(testLineupPlayer, 9)
      };

      var gameSave = new GSGameSave()
      {
        PowerUpId = 1,
        Players = Enumerable.Repeat(testPlayer, 1250),
        Teams = Enumerable.Repeat(testTeam, 32),
        Lineups = Enumerable.Repeat(testLineupDef, 32)
      };

      using (var writer = new GameSaveWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH))
      {
        writer.Write(gameSave);
      }

      using (var reader = new GameSaveReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, ByteOrder.BigEndian))
      {
        var result = reader.Read();

        result.Players.Where(p => p.PowerProsId != 0).Count().ShouldBe(1250);
        result.Teams.Where(t => t.PlayerEntries.First().PowerProsTeamId == 1).Count().ShouldBe(32);
        result.Lineups.Count().ShouldBe(32);
      }
    }
  }
}
