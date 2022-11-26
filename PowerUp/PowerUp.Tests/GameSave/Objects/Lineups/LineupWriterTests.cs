using NUnit.Framework;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Tests.GameSave.Objects.Lineups
{
  internal class LineupWriterTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TEST.dat");
    private readonly static string TEST_WRITE_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TESTWRITE.dat");
    private const int INDIANS_ID = 7;

    private ICharacterLibrary _characterLibrary;
    private IEnumerable<GSLineupPlayer> testNoDH;
    private IEnumerable<GSLineupPlayer> testDH;
    private GSLineupDefinition testLineups;

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

      testNoDH = new[]
      {
        new GSLineupPlayer { PowerProsPlayerId = 1, Position = 2 },
        new GSLineupPlayer { PowerProsPlayerId = 2, Position = 3 },
        new GSLineupPlayer { PowerProsPlayerId = 3, Position = 4 },
        new GSLineupPlayer { PowerProsPlayerId = 4, Position = 5 },
        new GSLineupPlayer { PowerProsPlayerId = 5, Position = 6 },
        new GSLineupPlayer { PowerProsPlayerId = 6, Position = 7 },
        new GSLineupPlayer { PowerProsPlayerId = 7, Position = 8 },
        new GSLineupPlayer { PowerProsPlayerId = 9, Position = 9 },
        new GSLineupPlayer { PowerProsPlayerId = 0, Position = 0 }
      };

      testDH = new[]
      {
        new GSLineupPlayer { PowerProsPlayerId = 901, Position = 10 },
        new GSLineupPlayer { PowerProsPlayerId = 902, Position = 9 },
        new GSLineupPlayer { PowerProsPlayerId = 903, Position = 8 },
        new GSLineupPlayer { PowerProsPlayerId = 904, Position = 7 },
        new GSLineupPlayer { PowerProsPlayerId = 905, Position = 6 },
        new GSLineupPlayer { PowerProsPlayerId = 906, Position = 5 },
        new GSLineupPlayer { PowerProsPlayerId = 907, Position = 4 },
        new GSLineupPlayer { PowerProsPlayerId = 909, Position = 3 },
        new GSLineupPlayer { PowerProsPlayerId = 900, Position = 2 }
      };

      testLineups = new GSLineupDefinition
      {
        NoDHLineup = testNoDH,
        DHLineup = testDH
      };
    }

    [Test]
    public void Writes_NoDHLineup()
    {
      using (var writer = new LineupWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(INDIANS_ID, testLineups);

      GSLineupDefinition loadedLineup = null;
      using (var reader = new LineupReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedLineup = reader.Read(INDIANS_ID);

      var noDH = loadedLineup.NoDHLineup.ToArray();
      for(int i=0; i<noDH.Length; i++)
      {
        noDH[i].PowerProsPlayerId.ShouldBe(testNoDH.ElementAt(i).PowerProsPlayerId);
        noDH[i].Position.ShouldBe(testNoDH.ElementAt(i).Position);
      }
    }

    [Test]
    public void Writes_DHLineup()
    {
      using (var writer = new LineupWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(INDIANS_ID, testLineups);

      GSLineupDefinition loadedLineup = null;
      using (var reader = new LineupReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH, GameSaveFormat.Wii_2007))
        loadedLineup = reader.Read(INDIANS_ID);

      var dh = loadedLineup.DHLineup.ToArray();
      for (int i = 0; i < dh.Length; i++)
      {
        dh[i].PowerProsPlayerId.ShouldBe(testDH.ElementAt(i).PowerProsPlayerId);
        dh[i].Position.ShouldBe(testDH.ElementAt(i).Position);
      }
    }
  }
}
