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
        result.TeamSeq.ShouldBe(1);
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
        withOrioles.TeamSeq.ShouldBe(1);
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
        withDodgers.TeamSeq.ShouldBe(2);
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

    [Test]
    public void GetFieldingStats_GetsFieldingStats_ForHistoricPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetFieldingStats(122439, 1992);
        results.TotalResults.ShouldBe(1);
        var result = results.Results.Single();
        result.LSPlayerId.ShouldBe(122439);
        result.Year.ShouldBe(1992);
        result.TeamSeq.ShouldBe(1);
        result.Position.ShouldBe(Position.Shortstop);
        result.GamesPlayed.ShouldBe(132);
        result.GamesStarted.ShouldBeNull();
        result.Innings.ShouldBeNull();
        result.TotalChances.ShouldBe(662);
        result.Errors.ShouldBe(10);
        result.Assists.ShouldBe(420);
        result.PutOuts.ShouldBe(232);
        result.DoublePlays.ShouldBe(82);
        result.RangeFactor.ShouldBe(4.94);
        result.FieldingPercentage.ShouldBe(.985);
        result.Catcher_RunnersThrownOut.ShouldBeNull();
        result.Catcher_StolenBasesAllowed.ShouldBeNull();
        result.Catcher_PastBalls.ShouldBeNull();
        result.Catcher_WildPitches.ShouldBeNull();
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetFieldingStats_GetsFieldingStats_ForCurrentPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetFieldingStats(621035, 2016);
        results.TotalResults.ShouldBe(4);

        var asMarinersShortstop = results.Results.First();
        asMarinersShortstop.LSPlayerId.ShouldBe(621035);
        asMarinersShortstop.Year.ShouldBe(2016);
        asMarinersShortstop.TeamSeq.ShouldBe(1);
        asMarinersShortstop.Position.ShouldBe(Position.Shortstop);
        asMarinersShortstop.GamesPlayed.ShouldBe(1);
        asMarinersShortstop.GamesStarted.ShouldBe(1);
        asMarinersShortstop.Innings.ShouldBe(9);
        asMarinersShortstop.TotalChances.ShouldBe(4);
        asMarinersShortstop.Errors.ShouldBe(2);
        asMarinersShortstop.Assists.ShouldBe(1);
        asMarinersShortstop.PutOuts.ShouldBe(1);
        asMarinersShortstop.DoublePlays.ShouldBe(0);
        asMarinersShortstop.RangeFactor.ShouldBe(2);
        asMarinersShortstop.FieldingPercentage.ShouldBe(.500);
        asMarinersShortstop.Catcher_RunnersThrownOut.ShouldBeNull();
        asMarinersShortstop.Catcher_StolenBasesAllowed.ShouldBeNull();
        asMarinersShortstop.Catcher_PastBalls.ShouldBeNull();
        asMarinersShortstop.Catcher_WildPitches.ShouldBeNull();

        var asDodgersSecondBaseman = results.Results.ElementAt(1);
        asDodgersSecondBaseman.LSPlayerId.ShouldBe(621035);
        asDodgersSecondBaseman.Year.ShouldBe(2016);
        asDodgersSecondBaseman.TeamSeq.ShouldBe(2);
        asDodgersSecondBaseman.Position.ShouldBe(Position.SecondBase);
        asDodgersSecondBaseman.GamesPlayed.ShouldBe(7);
        asDodgersSecondBaseman.GamesStarted.ShouldBe(5);
        asDodgersSecondBaseman.Innings.ShouldBe(39.1);
        asDodgersSecondBaseman.TotalChances.ShouldBe(22);
        asDodgersSecondBaseman.Errors.ShouldBe(1);
        asDodgersSecondBaseman.Assists.ShouldBe(12);
        asDodgersSecondBaseman.PutOuts.ShouldBe(9);
        asDodgersSecondBaseman.DoublePlays.ShouldBe(3);
        asDodgersSecondBaseman.RangeFactor.ShouldBe(3);
        asDodgersSecondBaseman.FieldingPercentage.ShouldBe(.955);
        asDodgersSecondBaseman.Catcher_RunnersThrownOut.ShouldBeNull();
        asDodgersSecondBaseman.Catcher_StolenBasesAllowed.ShouldBeNull();
        asDodgersSecondBaseman.Catcher_PastBalls.ShouldBeNull();
        asDodgersSecondBaseman.Catcher_WildPitches.ShouldBeNull();

        var asDodgersThirdBaseman = results.Results.ElementAt(2);
        asDodgersThirdBaseman.LSPlayerId.ShouldBe(621035);
        asDodgersThirdBaseman.Year.ShouldBe(2016);
        asDodgersThirdBaseman.TeamSeq.ShouldBe(2);
        asDodgersThirdBaseman.Position.ShouldBe(Position.ThirdBase);
        asDodgersThirdBaseman.GamesPlayed.ShouldBe(10);
        asDodgersThirdBaseman.GamesStarted.ShouldBe(2);
        asDodgersThirdBaseman.Innings.ShouldBe(28);
        asDodgersThirdBaseman.TotalChances.ShouldBe(7);
        asDodgersThirdBaseman.Errors.ShouldBe(0);
        asDodgersThirdBaseman.Assists.ShouldBe(5);
        asDodgersThirdBaseman.PutOuts.ShouldBe(2);
        asDodgersThirdBaseman.DoublePlays.ShouldBe(0);
        asDodgersThirdBaseman.RangeFactor.ShouldBe(.7);
        asDodgersThirdBaseman.FieldingPercentage.ShouldBe(1);
        asDodgersThirdBaseman.Catcher_RunnersThrownOut.ShouldBeNull();
        asDodgersThirdBaseman.Catcher_StolenBasesAllowed.ShouldBeNull();
        asDodgersThirdBaseman.Catcher_PastBalls.ShouldBeNull();
        asDodgersThirdBaseman.Catcher_WildPitches.ShouldBeNull();

        var asDodgersShortstop = results.Results.ElementAt(3);
        asDodgersShortstop.LSPlayerId.ShouldBe(621035);
        asDodgersShortstop.Year.ShouldBe(2016);
        asDodgersShortstop.TeamSeq.ShouldBe(2);
        asDodgersShortstop.Position.ShouldBe(Position.Shortstop);
        asDodgersShortstop.GamesPlayed.ShouldBe(5);
        asDodgersShortstop.GamesStarted.ShouldBe(4);
        asDodgersShortstop.Innings.ShouldBe(37);
        asDodgersShortstop.TotalChances.ShouldBe(17);
        asDodgersShortstop.Errors.ShouldBe(0);
        asDodgersShortstop.Assists.ShouldBe(9);
        asDodgersShortstop.PutOuts.ShouldBe(8);
        asDodgersShortstop.DoublePlays.ShouldBe(0);
        asDodgersShortstop.RangeFactor.ShouldBe(3.4);
        asDodgersShortstop.FieldingPercentage.ShouldBe(1);
        asDodgersShortstop.Catcher_RunnersThrownOut.ShouldBeNull();
        asDodgersShortstop.Catcher_StolenBasesAllowed.ShouldBeNull();
        asDodgersShortstop.Catcher_PastBalls.ShouldBeNull();
        asDodgersShortstop.Catcher_WildPitches.ShouldBeNull();
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetPitchingStats_GetsPitchingStats_ForHistoricPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetPitchingStats(120181, 1952);
        results.TotalResults.ShouldBe(1);
        var result = results.Results.Single();
        result.LSPlayerId.ShouldBe(120181);
        result.Year.ShouldBe(1952);
        result.TeamSeq.ShouldBe(1);
        result.GamesPlayed.ShouldBe(46);
        result.GamesStarted.ShouldBe(6);
        result.GamesFinished.ShouldBe(35);
        result.CompleteGames.ShouldBe(3);
        result.ShutOuts.ShouldBe(2);
        result.Wins.ShouldBe(12);
        result.Losses.ShouldBe(10);
        result.QualityStarts.ShouldBeNull();
        result.Saves.ShouldBe(10);
        result.InningsPitched.ShouldBe(138);
        result.SaveOpportunities.ShouldBeNull();
        result.AtBats.ShouldBe(514);
        result.Hits.ShouldBe(116);
        result.Walks.ShouldBe(57);
        result.IntentionalWalks.ShouldBeNull();
        result.HitBatters.ShouldBe(3);
        result.Strikeouts.ShouldBe(91);
        result.Runs.ShouldBe(51);
        result.EarnedRuns.ShouldBe(47);
        result.Doubles.ShouldBeNull();
        result.Triples.ShouldBeNull();
        result.HomeRuns.ShouldBe(5);
        result.TotalBasesAllowed.ShouldBe(582);
        result.WildPitches.ShouldBe(2);
        result.Balks.ShouldBe(0);
        result.RunnersPickedOff.ShouldBeNull();
        result.NumberOfPitches.ShouldBeNull();
        result.Strikes.ShouldBeNull();
        result.GroundOuts.ShouldBeNull();
        result.AirOuts.ShouldBeNull();
        result.DoublePlays.ShouldBeNull();
        result.StrikeoutToWalkRatio.ShouldBe(1.6);
        result.WHIP.ShouldBe(1.25);
        result.HitsPer9.ShouldBe(7.57);
        result.HomeRunsPer9.ShouldBe(.33);
        result.RunScoredPer9.ShouldBe(0);
        result.WalksPer9.ShouldBe(3.72);
        result.StrikeoutsPer9.ShouldBe(5.93);
        result.BattingAverageAgainst.ShouldBe(.226);
        result.SluggingAgainst.ShouldBeNull();
        result.OnBasePercentageAgainst.ShouldBe(.307);
        result.OnBasePlusSluggingAgainst.ShouldBeNull();
        result.WinningPercentage.ShouldBe(.545);
        result.EarnedRunAverage.ShouldBe(3.07);
        result.PitchesPerPlateAppearance.ShouldBe(0);
        result.StrikePercentage.ShouldBeNull();
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetPitchingStats_GetsPitchingStats_ForCurrentPlayer()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetPitchingStats(453286, 2021);
        results.TotalResults.ShouldBe(2);
        var withNationals = results.Results.First();
        withNationals.LSPlayerId.ShouldBe(453286);
        withNationals.Year.ShouldBe(2021);
        withNationals.TeamSeq.ShouldBe(1);
        withNationals.GamesPlayed.ShouldBe(19);
        withNationals.GamesStarted.ShouldBe(19);
        withNationals.GamesFinished.ShouldBe(0);
        withNationals.CompleteGames.ShouldBe(1);
        withNationals.ShutOuts.ShouldBe(0);
        withNationals.Wins.ShouldBe(8);
        withNationals.Losses.ShouldBe(4);
        withNationals.QualityStarts.ShouldBe(11);
        withNationals.Saves.ShouldBe(0);
        withNationals.InningsPitched.ShouldBe(111);
        withNationals.SaveOpportunities.ShouldBe(0);
        withNationals.AtBats.ShouldBe(390);
        withNationals.Hits.ShouldBe(71);
        withNationals.Walks.ShouldBe(28);
        withNationals.IntentionalWalks.ShouldBe(0);
        withNationals.HitBatters.ShouldBe(8);
        withNationals.Strikeouts.ShouldBe(147);
        withNationals.Runs.ShouldBe(36);
        withNationals.EarnedRuns.ShouldBe(34);
        withNationals.Doubles.ShouldBe(11);
        withNationals.Triples.ShouldBe(1);
        withNationals.HomeRuns.ShouldBe(18);
        withNationals.TotalBasesAllowed.ShouldBe(428);
        withNationals.WildPitches.ShouldBe(0);
        withNationals.Balks.ShouldBe(0);
        withNationals.RunnersPickedOff.ShouldBe(0);
        withNationals.NumberOfPitches.ShouldBe(1787);
        withNationals.Strikes.ShouldBe(1166);
        withNationals.GroundOuts.ShouldBe(62);
        withNationals.AirOuts.ShouldBe(112);
        withNationals.DoublePlays.ShouldBe(42);
        withNationals.StrikeoutToWalkRatio.ShouldBe(5.25);
        withNationals.WHIP.ShouldBe(0.89);
        withNationals.HitsPer9.ShouldBe(5.76);
        withNationals.HomeRunsPer9.ShouldBe(1.46);
        withNationals.RunScoredPer9.ShouldBe(4.54);
        withNationals.WalksPer9.ShouldBe(2.27);
        withNationals.StrikeoutsPer9.ShouldBe(11.92);
        withNationals.BattingAverageAgainst.ShouldBe(.182);
        withNationals.SluggingAgainst.ShouldBe(.354);
        withNationals.OnBasePercentageAgainst.ShouldBe(.251);
        withNationals.OnBasePlusSluggingAgainst.ShouldBe(.604);
        withNationals.WinningPercentage.ShouldBe(.667);
        withNationals.EarnedRunAverage.ShouldBe(2.76);
        withNationals.PitchesPerPlateAppearance.ShouldBe(4.18);
        withNationals.StrikePercentage.ShouldBe(65.2);

        var withDodgers = results.Results.ElementAt(1);
        withDodgers.LSPlayerId.ShouldBe(453286);
        withDodgers.Year.ShouldBe(2021);
        withDodgers.TeamSeq.ShouldBe(2);
        withDodgers.GamesPlayed.ShouldBe(11);
        withDodgers.GamesStarted.ShouldBe(11);
        withDodgers.GamesFinished.ShouldBe(0);
        withDodgers.CompleteGames.ShouldBe(0);
        withDodgers.ShutOuts.ShouldBe(0);
        withDodgers.Wins.ShouldBe(7);
        withDodgers.Losses.ShouldBe(0);
        withDodgers.QualityStarts.ShouldBe(7);
        withDodgers.Saves.ShouldBe(0);
        withDodgers.InningsPitched.ShouldBe(68.1);
        withDodgers.SaveOpportunities.ShouldBe(0);
        withDodgers.AtBats.ShouldBe(254);
        withDodgers.Hits.ShouldBe(48);
        withDodgers.Walks.ShouldBe(8);
        withDodgers.IntentionalWalks.ShouldBe(0);
        withDodgers.HitBatters.ShouldBe(2);
        withDodgers.Strikeouts.ShouldBe(89);
        withDodgers.Runs.ShouldBe(17);
        withDodgers.EarnedRuns.ShouldBe(15);
        withDodgers.Doubles.ShouldBe(10);
        withDodgers.Triples.ShouldBe(1);
        withDodgers.HomeRuns.ShouldBe(5);
        withDodgers.TotalBasesAllowed.ShouldBe(265);
        withDodgers.WildPitches.ShouldBe(2);
        withDodgers.Balks.ShouldBe(0);
        withDodgers.RunnersPickedOff.ShouldBe(0);
        withDodgers.NumberOfPitches.ShouldBe(1034);
        withDodgers.Strikes.ShouldBe(705);
        withDodgers.GroundOuts.ShouldBe(46);
        withDodgers.AirOuts.ShouldBe(72);
        withDodgers.DoublePlays.ShouldBe(26);
        withDodgers.StrikeoutToWalkRatio.ShouldBe(11.13);
        withDodgers.WHIP.ShouldBe(0.82);
        withDodgers.HitsPer9.ShouldBe(6.32);
        withDodgers.HomeRunsPer9.ShouldBe(.66);
        withDodgers.RunScoredPer9.ShouldBe(6.59);
        withDodgers.WalksPer9.ShouldBe(1.05);
        withDodgers.StrikeoutsPer9.ShouldBe(11.72);
        withDodgers.BattingAverageAgainst.ShouldBe(.189);
        withDodgers.SluggingAgainst.ShouldBe(.295);
        withDodgers.OnBasePercentageAgainst.ShouldBe(.220);
        withDodgers.OnBasePlusSluggingAgainst.ShouldBe(.515);
        withDodgers.WinningPercentage.ShouldBe(1);
        withDodgers.EarnedRunAverage.ShouldBe(1.98);
        withDodgers.PitchesPerPlateAppearance.ShouldBe(3.9);
        withDodgers.StrikePercentage.ShouldBe(68.2);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetTeamsForYear_GetsTeams_For2021()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetTeamsForYear(2021);
        results.TotalResults.ShouldBe(30);
        var orioles = results.Results.First();
        orioles.LSTeamId.ShouldBe(110);
        orioles.Year.ShouldBe(2021);
        orioles.Name.ShouldBe("Baltimore Orioles");
        orioles.State.ShouldBe("MD");
        orioles.City.ShouldBe("Baltimore");
        orioles.Venue.ShouldBe("Oriole Park at Camden Yards");
        orioles.League.ShouldBe("AL");
        orioles.Division.ShouldBe("E");
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetTeamsForYear_GetsTeams_For1915()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetTeamsForYear(1915);
        results.TotalResults.ShouldBe(24);
        var orioles = results.Results.First();
        orioles.LSTeamId.ShouldBe(111);
        orioles.Year.ShouldBe(1915);
        orioles.Name.ShouldBe("Boston Red Sox");
        orioles.State.ShouldBe("MA");
        orioles.City.ShouldBe("Boston");
        orioles.Venue.ShouldBe("Fenway Park");
        orioles.League.ShouldBe("AL");
        orioles.Division.ShouldBe(null);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetAllStarTeamsForYear_GetsTeams_For2021()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetAllStarTeamsForYear(2021);
        results.TotalResults.ShouldBe(2);
        var orioles = results.Results.First();
        orioles.LSTeamId.ShouldBe(159);
        orioles.Year.ShouldBe(2021);
        orioles.Name.ShouldBe("American League All-Stars");
        orioles.State.ShouldBe("DC");
        orioles.City.ShouldBe("Washington");
        orioles.Venue.ShouldBe("Nationals Park");
        orioles.League.ShouldBe("AL");
        orioles.Division.ShouldBe(null);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetAllStarTeamsForYear_GetsTeams_For1951()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetAllStarTeamsForYear(1951);
        results.TotalResults.ShouldBe(2);
        var orioles = results.Results.First();
        orioles.LSTeamId.ShouldBe(159);
        orioles.Year.ShouldBe(1951);
        orioles.Name.ShouldBe("American League All-Stars");
        orioles.State.ShouldBe("MI");
        orioles.City.ShouldBe("Detroit");
        orioles.Venue.ShouldBe("Briggs Stadium");
        orioles.League.ShouldBe("AL");
        orioles.Division.ShouldBe(null);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public void GetTeamRosterForYear_GetsRoster_For2021()
    {
      Task.Run(async () =>
      {
        var results = await _client.GetTeamRosterForYear(111, 2021);
        results.TotalResults.ShouldBe(56);
        var firstPlayer = results.Results.First();
        firstPlayer.LSPlayerId.ShouldBe(542882);
        firstPlayer.FormalDisplayName.ShouldBe("Andriese, Matt");
        firstPlayer.UniformNumber.ShouldBe("35");
        firstPlayer.Status.ShouldBe(PlayerRosterStatus.Released);
        firstPlayer.Position.ShouldBe(Position.Pitcher);
        firstPlayer.BattingSide.ShouldBe(BattingSide.Right);
        firstPlayer.ThrowingArm.ShouldBe(ThrowingArm.Right);
      }).GetAwaiter().GetResult();
    }
  }
}