using NUnit.Framework;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using Shouldly;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.Tests.GameSave.Objects.Teams
{
  public class TeamReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "./pm2maus_TEST.dat");
    private const int INDIANS_ID = 7;

    private ICharacterLibrary _characterLibrary;
    private IEnumerable<int> _indiansPlayerIds;

    [SetUp]
    public void SetUp()
    {
      _characterLibrary = TestConfig.CharacterLibrary.Value;
      _indiansPlayerIds = new[]
      {
        425, // Victor Martinez
        719, // Kelly Shoppach
        329, // Casey Blake
        423, // Travis Hafner
        782, // Ryan Garko
        538, // Mike Rouse (who the hell was this guy???)
        616, // Josh Barfield
        622, // Andy Marte
        552, // Jhonny Peralta
        402, // Jason Michaels
        626, // Grady Sizemore
        40,  // David Dellucci
        110, // Trot Nixon
        18,  // Paul Byrd
        295, // Jake Westbrook
        370, // CC Sabathia
        526, // Cliff Lee
        932, // Jeremy Sowers
        727, // Fausto Carmona (Fake Person)
        550, // Fernando Cabrera
        271, // Aaron Fultz
        923, // Tom Mastny
        73,  // Scott Baker
        636, // Rafael Betancourt
        15,  // Joe Borowski
        840, // Joe Inglett
        624, // Franklin Gutierrez
        589, // Shin-Soo Choo
        856, // Ben Francisco
        380, // Matt Miller
        533, // Jason Davis
        933, // Rafael Perez
        952, // Edward Mujica
      };
    }

    [Test]
    public void Reads_Team()
    {
      using var reader = new TeamReader(_characterLibrary, TEST_READ_GAME_SAVE_FILE_PATH);
      var team = reader.Read(INDIANS_ID);

      var playerList = team.PlayerEntries.ToList();
      playerList.Count.ShouldBe(40);
      
      var nonEmptyPlayerList = playerList.Where(p => p.PowerProsPlayerId != 0).ToList();
      nonEmptyPlayerList.Count.ShouldBe(33);

      foreach(var id in _indiansPlayerIds)
        nonEmptyPlayerList.ShouldContain(p => p.PowerProsPlayerId == id);
    }
  }
}
