using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Tests.Fetchers.MLBLookupService
{
  public class MLBLookupServiceTests
  {
    private readonly IMLBLookupServiceClient _fetcher = new MLBLookupServiceClient();

    [Test]
    public void SearchPlayer_SearchesSingleCurrentPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _fetcher.SearchPlayer("Giancarlo Stanton");
        result.TotalResults.ShouldBe(1);
        var stanton = result.Results.Single();
        stanton.PlayerId.ShouldBe(519317);
        stanton.FirstName.ShouldBe("Giancarlo");
        stanton.LastName.ShouldBe("Stanton");
        stanton.Position.ShouldBe(Position.DesignatedHitter);
        stanton.BattingSide.ShouldBe(BattingSide.Right);
        stanton.ThrowingArm.ShouldBe(ThrowingArm.Right);
        stanton.Weight.ShouldBe(245);
        stanton.HeightFeet.ShouldBe(6);
        stanton.HeightInches.ShouldBe(6);
        stanton.BirthDate.ShouldBe(DateTime.Parse("1989-11-08T00:00:00"));
        stanton.BirthCountry.ShouldBe("USA");
        stanton.BirthState.ShouldBe("CA");
        stanton.BirthCity.ShouldBe("Panorama");
        stanton.HighSchool.ShouldBe("Notre Dame, Sherman Oaks, CA");
        stanton.College.ShouldBe(null);
        stanton.ProDebutDate.ShouldBe(DateTime.Parse("2010-06-08T00:00:00"));
        stanton.ServiceYears.ShouldBe(null);
        stanton.IsActive.ShouldBe(true);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void SearchPlayer_SearchesSingleHistoricPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _fetcher.SearchPlayer("Sandy Koufax");
        result.TotalResults.ShouldBe(1);
        var stanton = result.Results.Single();
        stanton.PlayerId.ShouldBe(117277);
        stanton.FirstName.ShouldBe("Sandy");
        stanton.LastName.ShouldBe("Koufax");
        stanton.Position.ShouldBe(Position.Pitcher);
        stanton.BattingSide.ShouldBe(BattingSide.Right);
        stanton.ThrowingArm.ShouldBe(ThrowingArm.Left);
        stanton.Weight.ShouldBe(210);
        stanton.HeightFeet.ShouldBe(6);
        stanton.HeightInches.ShouldBe(2);
        stanton.BirthDate.ShouldBe(DateTime.Parse("1935-12-30T00:00:00"));
        stanton.BirthCountry.ShouldBe("USA");
        stanton.BirthState.ShouldBe("NY");
        stanton.BirthCity.ShouldBe("Brooklyn");
        stanton.HighSchool.ShouldBe("Lafayette, Brooklyn, NY");
        stanton.College.ShouldBe("Cincinnati");
        stanton.ProDebutDate.ShouldBe(DateTime.Parse("1955-06-24T00:00:00"));
        stanton.ServiceYears.ShouldBe(null);
        stanton.IsActive.ShouldBe(false);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void SearchPlayer_SearchesMultiplePlayers()
    {
      Task.Run(async () =>
      {
        var result = await _fetcher.SearchPlayer("John");
        result.TotalResults.ShouldBeGreaterThanOrEqualTo(651);
        var results = result.Results;
        results.ShouldContain(r => r.InformalDisplayName == "Tommy John");
        results.ShouldContain(r => r.InformalDisplayName == "Johnny Giavotella");
        results.ShouldContain(r => r.InformalDisplayName == "John Young");
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void SearchPlayer_HandlesEmptyResults()
    {
      Task.Run(async () =>
      {
        var results = await _fetcher.SearchPlayer("dslk;gfksd;lgj");
        results.TotalResults.ShouldBe(0);
      }).GetAwaiter().GetResult();
    }
  }
}