using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Generators;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.Generators
{
  public class TeamGeneratorTests
  {
    IMLBLookupServiceClient _client;
    IPlayerGenerator _playerGenerator;
    ITeamGenerator _teamGenerator;
    PlayerGenerationAlgorithm _algorithm;

    [SetUp]
    public void SetUp()
    {
      _client = new MLBLookupServiceClient();
      _playerGenerator = new PlayerGenerator(new PlayerApi(), new LSPlayerStatisticsFetcher(_client));
      _teamGenerator = new TeamGenerator(_client, _playerGenerator);
      _algorithm = new LSStatistcsPlayerGenerationAlgorithm(TestConfig.VoiceLibrary.Value, new SkinColorGuesser(TestConfig.CountryAndSkinColorLibrary.Value));
    }

    [Test] 
    public void GetsAccurate40ManRoster()
    {
      var year = 2006;

      Task.Run(() =>
      {
        foreach (var team in Enum.GetValues<MLBPPTeam>())
        {
          if (team == MLBPPTeam.AmericanLeagueAllStars || team == MLBPPTeam.NationalLeagueAllStars)
            continue;

          var lsTeamId = team.GetLSTeamId();
          _teamGenerator.GenerateTeam(lsTeamId, year, team.GetDisplayName(), _algorithm);

        }
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void Take5()
    {
      var array = new[] { 1, 2, 3 };
      var vals = array.Take(5);
      vals.Count().ShouldBe(3);
    }
  }
}
