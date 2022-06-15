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

    [Test]
    public void GetHittingStats_GetsHittingStats_ForHistoricPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetHittingStats(115096, 1935);
        results.TotalResults.ShouldBe(1);
        var result = results.Results.Single();
        result.LSPlayerId.ShouldBe(115096);
        result.Year.ShouldBe(1935);
        result.TeamSeq.ShouldBe(1.0);
        result.GamesPlayed.ShouldBe(152);
        result.AtBats.ShouldBe(619);
        result.PlateAppearances.ShouldBe(710);
        result.Hits.ShouldBe(203);
        result.Doubles.ShouldBe(46);
        result.Triples.ShouldBe(16);
        result.HomeRuns.ShouldBe(36);
        result.ExtraBaseHits.ShouldBe(98);
        result.TotalBases.ShouldBe(389);
        result.Walks.ShouldBe(87);
        result.IntentionalWalks.ShouldBeNull();
        result.HitByPitches.ShouldBe(0);
        result.RunsBattedIn.ShouldBe(170);
        result.RunnersLeftOnBase.ShouldBeNull();
        result.Runs.ShouldBe(121);
        result.Strikeouts.ShouldBe(91);
        result.GroundOuts.ShouldBeNull();
        result.AirOuts.ShouldBe(0);
        result.HardGrounders.ShouldBeNull();
        result.HardFlyBalls.ShouldBeNull();
        result.HardPopUps.ShouldBeNull();
        result.GroundedIntoDoublePlay.ShouldBeNull();
        result.SacrificeFlies.ShouldBeNull();
        result.SacrificeBunts.ShouldBe(4);
        result.ReachedOnErrors.ShouldBeNull();
        result.BattingAverage.ShouldBe(.328);
        result.SluggingPercentage.ShouldBe(.628);
        result.OnBasePercentage.ShouldBe(.411);
        result.OnBasePlusSluggingPercentage.ShouldBe(1.039);
        result.BattingAverageOnBallsInPlay.ShouldBe(.339);
        result.PitchesPerPlateAppearance.ShouldBe(0);
        result.StolenBases.ShouldBe(4);
        result.CaughtStealing.ShouldBe(3);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetHittingStats_GetsHittingStats_ForCurrentPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetHittingStats(592518, 2018);
        results.TotalResults.ShouldBe(2);
        var withOrioles = results.Results.First();
        withOrioles.LSPlayerId.ShouldBe(592518);
        withOrioles.Year.ShouldBe(2018);
        withOrioles.TeamSeq.ShouldBe(1.0);
        withOrioles.GamesPlayed.ShouldBe(96);
        withOrioles.AtBats.ShouldBe(365);
        withOrioles.PlateAppearances.ShouldBe(413);
        withOrioles.Hits.ShouldBe(115);
        withOrioles.Doubles.ShouldBe(21);
        withOrioles.Triples.ShouldBe(1);
        withOrioles.HomeRuns.ShouldBe(24);
        withOrioles.ExtraBaseHits.ShouldBe(46);
        withOrioles.TotalBases.ShouldBe(210);
        withOrioles.Walks.ShouldBe(45);
        withOrioles.IntentionalWalks.ShouldBe(12);
        withOrioles.HitByPitches.ShouldBe(0);
        withOrioles.RunsBattedIn.ShouldBe(65);
        withOrioles.RunnersLeftOnBase.ShouldBe(134);
        withOrioles.Runs.ShouldBe(48);
        withOrioles.Strikeouts.ShouldBe(51);
        withOrioles.GroundOuts.ShouldBe(96);
        withOrioles.AirOuts.ShouldBe(120);
        withOrioles.HardGrounders.ShouldBe(39);
        withOrioles.HardLineDrives.ShouldBe(49);
        withOrioles.HardFlyBalls.ShouldBe(27);
        withOrioles.HardPopUps.ShouldBe(0);
        withOrioles.GroundedIntoDoublePlay.ShouldBe(14);
        withOrioles.SacrificeFlies.ShouldBe(3);
        withOrioles.SacrificeBunts.ShouldBe(0);
        withOrioles.ReachedOnErrors.ShouldBe(1);
        withOrioles.BattingAverage.ShouldBe(.315);
        withOrioles.SluggingPercentage.ShouldBe(.575);
        withOrioles.OnBasePercentage.ShouldBe(.387);
        withOrioles.OnBasePlusSluggingPercentage.ShouldBe(.963);
        withOrioles.BattingAverageOnBallsInPlay.ShouldBe(.311);
        withOrioles.PitchesPerPlateAppearance.ShouldBe(3.58);
        withOrioles.StolenBases.ShouldBe(8);
        withOrioles.CaughtStealing.ShouldBe(1);

        var withDodgers = results.Results.ElementAt(1);
        withDodgers.LSPlayerId.ShouldBe(592518);
        withDodgers.Year.ShouldBe(2018);
        withDodgers.TeamSeq.ShouldBe(2.0);
        withDodgers.GamesPlayed.ShouldBe(66);
        withDodgers.AtBats.ShouldBe(267);
        withDodgers.PlateAppearances.ShouldBe(296);
        withDodgers.Hits.ShouldBe(73);
        withDodgers.Doubles.ShouldBe(14);
        withDodgers.Triples.ShouldBe(2);
        withDodgers.HomeRuns.ShouldBe(13);
        withDodgers.ExtraBaseHits.ShouldBe(29);
        withDodgers.TotalBases.ShouldBe(130);
        withDodgers.Walks.ShouldBe(25);
        withDodgers.IntentionalWalks.ShouldBe(6);
        withDodgers.HitByPitches.ShouldBe(2);
        withDodgers.RunsBattedIn.ShouldBe(42);
        withDodgers.RunnersLeftOnBase.ShouldBe(125);
        withDodgers.Runs.ShouldBe(36);
        withDodgers.Strikeouts.ShouldBe(53);
        withDodgers.GroundOuts.ShouldBe(83);
        withDodgers.AirOuts.ShouldBe(72);
        withDodgers.HardGrounders.ShouldBe(21);
        withDodgers.HardLineDrives.ShouldBe(38);
        withDodgers.HardFlyBalls.ShouldBe(14);
        withDodgers.HardPopUps.ShouldBe(0);
        withDodgers.GroundedIntoDoublePlay.ShouldBe(12);
        withDodgers.SacrificeFlies.ShouldBe(2);
        withDodgers.SacrificeBunts.ShouldBe(0);
        withDodgers.ReachedOnErrors.ShouldBe(1);
        withDodgers.BattingAverage.ShouldBe(.273);
        withDodgers.SluggingPercentage.ShouldBe(.487);
        withDodgers.OnBasePercentage.ShouldBe(.338);
        withDodgers.OnBasePlusSluggingPercentage.ShouldBe(.825);
        withDodgers.BattingAverageOnBallsInPlay.ShouldBe(.296);
        withDodgers.PitchesPerPlateAppearance.ShouldBe(3.55);
        withDodgers.StolenBases.ShouldBe(6);
        withDodgers.CaughtStealing.ShouldBe(1);
      }).GetAwaiter().GetResult();
    }
  }
}