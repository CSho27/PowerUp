using NUnit.Framework;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.GameSave.Objects.Teams
{
  public  class TeamWriterTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";
    private const string TEST_WRITE_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TESTWRITE.dat";
    private const int INDIANS_ID = 7;

    private ICharacterLibrary _characterLibrary;
    private IEnumerable<int> testTeamPlayerIds;
    private GSTeam testTeam;

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

      _characterLibrary = TestConfigHelpers.GetCharacterLibrary();

      testTeamPlayerIds = new[]
      {
        1,
        970,
        42,
        31,
        8,
        6,
        10,
        42,
        7,
        25,
        83,
        476,
        500,
        567,
        876,
        354,
        232,
        444,
        666,
        253,
        245,
        764,
        409,
        464,
        276,
        111,
        432,
        329,
        114,
        631,
        335
      };

      testTeam = new GSTeam
      {
        PlayerEntries = testTeamPlayerIds.Select(id => ToPlayerEntry(id)),
      };
    }

    private GSTeamPlayerEntry ToPlayerEntry(int powerProsPlayerId)
    {
      return new GSTeamPlayerEntry { PowerProsPlayerId = (ushort)powerProsPlayerId };
    }

    [Test]
    public void Writes_Team()
    {
      using (var writer = new TeamWriter(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH))
        writer.Write(INDIANS_ID, testTeam);

      GSTeam loadedTeam = null;
      using (var reader = new TeamReader(_characterLibrary, TEST_WRITE_GAME_SAVE_FILE_PATH))
        loadedTeam = reader.Read(INDIANS_ID);

      var playerList = loadedTeam.PlayerEntries.ToList();
      playerList.Count.ShouldBe(40);

      var nonEmptyPlayerList = playerList.Where(p => p.PowerProsPlayerId != 0).ToList();
      nonEmptyPlayerList.Count.ShouldBe(31);

      foreach (var id in testTeamPlayerIds)
        loadedTeam.PlayerEntries.ShouldContain(p => p.PowerProsPlayerId == id);
    }
  }
}
