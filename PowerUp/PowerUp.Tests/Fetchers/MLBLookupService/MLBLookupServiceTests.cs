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
    private readonly IMLBLookupServiceClient _client = new MLBLookupServiceClient();

    [Test]
    public void SearchPlayer_SearchesSingle_CurrentPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _client.SearchPlayer("Giancarlo Stanton");
        result.TotalResults.ShouldBe(1);
        var stanton = result.Results.Single();
        stanton.LSPlayerId.ShouldBe(519317);
        stanton.FirstName.ShouldBe("Giancarlo");
        stanton.LastName.ShouldBe("Stanton");
        stanton.FormalDisplayName.ShouldBe("Stanton, Giancarlo");
        stanton.InformalDisplayName.ShouldBe("Giancarlo Stanton");
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
    public void SearchPlayer_SearchesSingle_HistoricPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _client.SearchPlayer("Sandy Koufax");
        result.TotalResults.ShouldBe(1);
        var stanton = result.Results.Single();
        stanton.LSPlayerId.ShouldBe(117277);
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
        var result = await _client.SearchPlayer("John");
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
        var results = await _client.SearchPlayer("dslk;gfksd;lgj");
        results.TotalResults.ShouldBe(0);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetPlayerInfo_GetsPlayerInfo_ForHistoricPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetPlayerInfo(122566);
        result.LSPlayerId.ShouldBe(122566);
        result.Position.ShouldBe(Position.CenterField);
        result.NamePrefix.ShouldBeNull();
        result.FirstName.ShouldBe("Tristram");
        result.MiddleName.ShouldBe("Edgar");
        result.LastName.ShouldBe("Speaker");
        result.FormalDisplayName.ShouldBe("Speaker, Tristram");
        result.InformalDisplayName.ShouldBe("Tristram Speaker");
        result.NickName.ShouldBe("The Grey Eagle");
        result.UniformNumber.ShouldBeNull();
        result.BattingSide.ShouldBe(BattingSide.Left);
        result.ThrowingArm.ShouldBe(ThrowingArm.Left);
        result.Weight.ShouldBe(193);
        result.HeightFeet.ShouldBe(6);
        result.HeightInches.ShouldBe(0);
        result.BirthDate.ShouldBe(DateTime.Parse("1888-04-04T00:00:00"));
        result.BirthCountry.ShouldBe("USA");
        result.BirthState.ShouldBe("TX");
        result.BirthCity.ShouldBe("Hubbard");
        result.DeathDate.ShouldBe(DateTime.Parse("1958-12-08T00:00:00"));
        result.DeathCountry.ShouldBe("USA");
        result.DeathState.ShouldBe("TX");
        result.DeathCity.ShouldBe("Lake Whitney");
        result.Age.Value.ShouldBeGreaterThanOrEqualTo(134);
        result.HighSchool.ShouldBe("Hubbard, TX");
        result.College.ShouldBe("Texas Wesleyan");
        result.ProDebutDate.ShouldBe(DateTime.Parse("1907-09-12T00:00:00"));
        result.StartDate.ShouldBe(DateTime.Parse("1928-01-01T00:00:00"));
        result.EndDate.ShouldBe(DateTime.Parse("1929-01-01T00:00:00"));
        result.ServiceYears.ShouldBeNull();
        result.TeamName.ShouldBe("Oakland Athletics");
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetPlayerInfo_GetsPlayerInfo_ForCurrentPlayer()
    {
      Task.Run(async () =>
      {
        var result = await _client.GetPlayerInfo(545361);
        result.LSPlayerId.ShouldBe(545361);
        result.Position.ShouldBe(Position.CenterField);
        result.NamePrefix.ShouldBeNull();
        result.FirstName.ShouldBe("Michael");
        result.FirstNameUsed.ShouldBe("Mike");
        result.MiddleName.ShouldBe("Nelson");
        result.LastName.ShouldBe("Trout");
        result.FormalDisplayName.ShouldBe("Trout, Mike");
        result.InformalDisplayName.ShouldBe("Mike Trout");
        result.NickName.ShouldBe("Kiiiiid");
        result.UniformNumber.ShouldBe("27");
        result.BattingSide.ShouldBe(BattingSide.Right);
        result.ThrowingArm.ShouldBe(ThrowingArm.Right);
        result.Weight.ShouldBe(235);
        result.HeightFeet.ShouldBe(6);
        result.HeightInches.ShouldBe(2);
        result.BirthDate.ShouldBe(DateTime.Parse("1991-08-07T00:00:00"));
        result.BirthCountry.ShouldBe("USA");
        result.BirthState.ShouldBe("NJ");
        result.BirthCity.ShouldBe("Vineland");
        result.DeathDate.ShouldBeNull();
        result.DeathCountry.ShouldBeNull();
        result.DeathState.ShouldBeNull();
        result.DeathCity.ShouldBeNull();
        result.Age.Value.ShouldBeGreaterThanOrEqualTo(30);
        result.HighSchool.ShouldBe("Millville, NJ");
        result.College.ShouldBeNull();
        result.ProDebutDate.ShouldBe(DateTime.Parse("2011-07-08T00:00:00"));
        result.StartDate.ShouldBe(DateTime.Parse("2011-07-08T00:00:00"));
        result.EndDate.ShouldBeNull();
        result.ServiceYears.ShouldBeNull();
        result.TeamName.ShouldBe("Los Angeles Angels");
      }).GetAwaiter().GetResult();
    }
  }
}