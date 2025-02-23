using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Fetchers.Algolia;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Fetchers.MLBStatsApi;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.Fetchers.MLBStatsApi
{
  public class MLBStatsApiClientTests
  {
    private IMLBStatsApiClient _client = null!;

    [SetUp]
    public void SetUp()
    {
      _client = new MLBStatsApiClient();
    }

    [Test]
    public async Task GetTeamRoster_ReturnsRosterForTeam()
    {
      var results = await _client.GetTeamRoster(111, 2021);
      results.Roster.Count().ShouldBe(56);
      var firstEntry = results.Roster.First();
      var firstPlayer = firstEntry.Person;
      firstEntry.Status.Code.ShouldBe("RL");
      firstEntry.Position.Code.ShouldBe("1");
      firstEntry.JerseyNumber.ShouldBe("35");
      firstPlayer.Id.ShouldBe(542882);
      firstPlayer.FullName.ShouldBe("Matt Andriese");
    }
  }
}
