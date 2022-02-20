using NUnit.Framework;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.GameSave.Objects.GameSaves
{
  public class GameSaveReaderTests
  {
    private const string TEST_READ_GAME_SAVE_FILE_PATH = "C:/dev/PowerUp/PowerUp/PowerUp.Tests/Assets/pm2maus_TEST.dat";

    private ICharacterLibrary _characterLibrary;

    [SetUp]
    public void SetUp()
    {
      _characterLibrary = TestConfigHelpers.GetCharacterLibrary();
    }

    [Test]
    public void Read_ReadsData()
    {
      var reader = new GameSaveReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH);
      var result = reader.Read();

      var players = result.Players.ToList();
      players.Count.ShouldBe(1500);
      players.Where(p => p.PowerProsId != 0).Count().ShouldBe(970);

      var teams = result.Teams.ToList();
      teams.Count.ShouldBe(32);
      foreach (int id in Enumerable.Range(0, 32))
      {
        teams.ShouldContain(t => t.PlayerEntries.First().PowerProsTeamId == id);
      }

      var lineups = result.Lineups.ToList();
      lineups.Count.ShouldBe(32);
    }
  }
}
