﻿using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Fetchers.Algolia;
using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Fetchers.MLBStatsApi;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Tests.Fetchers.MLBLookupService
{
  public class MLBLookupServiceTests
  {
    private readonly IMLBLookupServiceClient _client = new MLBLookupServiceClient(new AlgoliaClient(), new MLBStatsApiClient());

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
        stanton.HighSchool.ShouldBeNull();
        stanton.College.ShouldBeNull();
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
        var koufax = result.Results.Single();
        koufax.LSPlayerId.ShouldBe(117277);
        koufax.FirstName.ShouldBe("Sanford");
        koufax.FirstNameUsed.ShouldBe("Sandy");
        koufax.LastName.ShouldBe("Koufax");
        koufax.Position.ShouldBe(Position.Pitcher);
        koufax.BattingSide.ShouldBe(BattingSide.Right);
        koufax.ThrowingArm.ShouldBe(ThrowingArm.Left);
        koufax.Weight.ShouldBe(210);
        koufax.HeightFeet.ShouldBe(6);
        koufax.HeightInches.ShouldBe(2);
        koufax.BirthDate.ShouldBe(DateTime.Parse("1935-12-30T00:00:00"));
        koufax.BirthCountry.ShouldBe("USA");
        koufax.BirthState.ShouldBe("NY");
        koufax.BirthCity.ShouldBe("Brooklyn");
        koufax.HighSchool.ShouldBeNull();
        koufax.College.ShouldBeNull();
        koufax.ProDebutDate.ShouldBe(DateTime.Parse("1955-06-24T00:00:00"));
        koufax.ServiceYears.ShouldBe(null);
        koufax.IsActive.ShouldBe(false);
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
        result.Age.Value.ShouldBeGreaterThanOrEqualTo(70);
        result.HighSchool.ShouldBeNull();
        result.College.ShouldBeNull();
        result.ProDebutDate.ShouldBe(DateTime.Parse("1907-09-12T00:00:00"));
        result.StartDate.ShouldBe(DateTime.Parse("1907-09-12T00:00:00"));
        result.EndDate.ShouldBe(DateTime.Parse("1928-08-30T00:00:00"));
        result.ServiceYears.ShouldBeNull();
        result.TeamName.ShouldBe("Philadelphia Athletics");
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
        result.UniformNumber.ShouldBeNull();
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
        result.HighSchool.ShouldBeNull();
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
        result.LSTeamId.ShouldBe((int)MLBPPTeam.Tigers.GetLSTeamId());
        result.Year.ShouldBe(1935);
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
        result.AirOuts.ShouldBeNull();
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
        result.PitchesPerPlateAppearance.ShouldBeNull();
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
        var withOrioles = results.Results.Single(r => r.LSTeamId == (int)MLBPPTeam.Orioles.GetLSTeamId());
        withOrioles.LSPlayerId.ShouldBe(592518);
        withOrioles.LSTeamId.ShouldBe(110);
        withOrioles.Year.ShouldBe(2018);
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
        withOrioles.GroundOuts.ShouldBe(82);
        withOrioles.AirOuts.ShouldBe(120);
        withOrioles.HardGrounders.ShouldBeNull();
        withOrioles.HardLineDrives.ShouldBeNull();
        withOrioles.HardFlyBalls.ShouldBeNull();
        withOrioles.HardPopUps.ShouldBeNull();
        withOrioles.GroundedIntoDoublePlay.ShouldBe(14);
        withOrioles.SacrificeFlies.ShouldBe(3);
        withOrioles.SacrificeBunts.ShouldBe(0);
        withOrioles.ReachedOnErrors.ShouldBeNull();
        withOrioles.BattingAverage.ShouldBe(.315);
        withOrioles.SluggingPercentage.ShouldBe(.575);
        withOrioles.OnBasePercentage.ShouldBe(.387);
        withOrioles.OnBasePlusSluggingPercentage.ShouldBe(.962);
        withOrioles.BattingAverageOnBallsInPlay.ShouldBe(.311);
        withOrioles.PitchesPerPlateAppearance.Value.ShouldBeInRange(3.57, 3.58);
        withOrioles.StolenBases.ShouldBe(8);
        withOrioles.CaughtStealing.ShouldBe(1);

        var withDodgers = results.Results.Single(r => r.LSTeamId == (int)MLBPPTeam.Dodgers.GetLSTeamId());
        withDodgers.LSPlayerId.ShouldBe(592518);
        withDodgers.LSTeamId.ShouldBe(119);
        withDodgers.Year.ShouldBe(2018);
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
        withDodgers.GroundOuts.ShouldBe(71);
        withDodgers.AirOuts.ShouldBe(72);
        withDodgers.HardGrounders.ShouldBeNull();
        withDodgers.HardLineDrives.ShouldBeNull();
        withDodgers.HardFlyBalls.ShouldBeNull();
        withDodgers.HardPopUps.ShouldBeNull();
        withDodgers.GroundedIntoDoublePlay.ShouldBe(12);
        withDodgers.SacrificeFlies.ShouldBe(2);
        withDodgers.SacrificeBunts.ShouldBe(0);
        withDodgers.ReachedOnErrors.ShouldBeNull();
        withDodgers.BattingAverage.ShouldBe(.273);
        withDodgers.SluggingPercentage.ShouldBe(.487);
        withDodgers.OnBasePercentage.ShouldBe(.338);
        withDodgers.OnBasePlusSluggingPercentage.ShouldBe(.825);
        withDodgers.BattingAverageOnBallsInPlay.ShouldBe(.296);
        withDodgers.PitchesPerPlateAppearance.Value.ShouldBeInRange(3.55, 3.56);
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
        result.LSTeamId.ShouldBe((int)MLBPPTeam.Cardinals.GetLSTeamId());
        result.Position.ShouldBe(Position.Shortstop);
        result.GamesPlayed.ShouldBe(132);
        result.GamesStarted.ShouldBe(128);
        result.Innings.ShouldBe(1156.1);
        result.TotalChances.ShouldBe(659);
        result.Errors.ShouldBe(10);
        result.Assists.ShouldBe(418);
        result.PutOuts.ShouldBe(231);
        result.DoublePlays.ShouldBe(82);
        result.RangeFactor.ShouldBe(4.92);
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
        results.TotalResults.ShouldBe(5);

        var asMarinersDH = results.Results
          .First(r => r.LSTeamId == (int)MLBPPTeam.Mariners.GetLSTeamId() && r.Position == Position.DesignatedHitter);
        asMarinersDH.LSPlayerId.ShouldBe(621035);
        asMarinersDH.Year.ShouldBe(2016);
        asMarinersDH.GamesPlayed.ShouldBe(1);
        asMarinersDH.GamesStarted.ShouldBe(0);
        asMarinersDH.Innings.ShouldBe(0);
        asMarinersDH.TotalChances.ShouldBe(0);
        asMarinersDH.Errors.ShouldBe(0);
        asMarinersDH.Assists.ShouldBe(0);
        asMarinersDH.PutOuts.ShouldBe(0);
        asMarinersDH.DoublePlays.ShouldBe(0);
        asMarinersDH.RangeFactor.ShouldBe(0);
        asMarinersDH.FieldingPercentage.ShouldBe(0);
        asMarinersDH.Catcher_RunnersThrownOut.ShouldBeNull();
        asMarinersDH.Catcher_StolenBasesAllowed.ShouldBeNull();
        asMarinersDH.Catcher_PastBalls.ShouldBeNull();
        asMarinersDH.Catcher_WildPitches.ShouldBeNull();

        var asMarinersShortstop = results.Results
          .First(r => r.LSTeamId == (int)MLBPPTeam.Mariners.GetLSTeamId() && r.Position == Position.Shortstop);
        asMarinersShortstop.LSPlayerId.ShouldBe(621035);
        asMarinersShortstop.Year.ShouldBe(2016);
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

        var asDodgersShortstop = results.Results
          .First(r => r.LSTeamId == (int)MLBPPTeam.Dodgers.GetLSTeamId() && r.Position == Position.Shortstop);
        asDodgersShortstop.LSPlayerId.ShouldBe(621035);
        asDodgersShortstop.Year.ShouldBe(2016);
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

        var asDodgersThirdBaseman = results.Results
          .First(r => r.LSTeamId == (int)MLBPPTeam.Dodgers.GetLSTeamId() && r.Position == Position.ThirdBase);
        asDodgersThirdBaseman.LSPlayerId.ShouldBe(621035);
        asDodgersThirdBaseman.Year.ShouldBe(2016);
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

        var asDodgersSecondBaseman = results.Results
          .First(r => r.LSTeamId == (int)MLBPPTeam.Dodgers.GetLSTeamId() && r.Position == Position.SecondBase);
        asDodgersSecondBaseman.LSPlayerId.ShouldBe(621035);
        asDodgersSecondBaseman.Year.ShouldBe(2016);
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
        // result.TeamSeq.ShouldBe(1);
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
        result.TotalBasesAllowed.ShouldBeNull();
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
        result.RunScoredPer9.ShouldBe(3.33);
        result.WalksPer9.ShouldBe(3.72);
        result.StrikeoutsPer9.ShouldBe(5.93);
        result.BattingAverageAgainst.ShouldBe(.226);
        result.SluggingAgainst.ShouldBeNull();
        result.OnBasePercentageAgainst.ShouldBe(.307);
        result.OnBasePlusSluggingAgainst.ShouldBeNull();
        result.WinningPercentage.ShouldBe(.545);
        result.EarnedRunAverage.ShouldBe(3.07);
        result.PitchesPerPlateAppearance.ShouldBeNull();
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
        var withNationals = results.Results.Single(r => r.LSTeamId == MLBPPTeam.Nationals.GetLSTeamId());
        withNationals.LSPlayerId.ShouldBe(453286);
        withNationals.Year.ShouldBe(2021);
        // withNationals.TeamSeq.ShouldBe(1);
        withNationals.GamesPlayed.ShouldBe(19);
        withNationals.GamesStarted.ShouldBe(19);
        withNationals.GamesFinished.ShouldBe(0);
        withNationals.CompleteGames.ShouldBe(1);
        withNationals.ShutOuts.ShouldBe(0);
        withNationals.Wins.ShouldBe(8);
        withNationals.Losses.ShouldBe(4);
        withNationals.QualityStarts.ShouldBeNull();
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
        withNationals.TotalBasesAllowed.ShouldBe(138);
        withNationals.WildPitches.ShouldBe(0);
        withNationals.Balks.ShouldBe(0);
        withNationals.RunnersPickedOff.ShouldBe(0);
        withNationals.NumberOfPitches.ShouldBe(1787);
        withNationals.Strikes.ShouldBe(1166);
        withNationals.GroundOuts.ShouldBe(62);
        withNationals.AirOuts.ShouldBe(112);
        withNationals.DoublePlays.ShouldBeNull();
        withNationals.StrikeoutToWalkRatio.ShouldBe(5.25);
        withNationals.WHIP.ShouldBe(0.89);
        withNationals.HitsPer9.ShouldBe(5.76);
        withNationals.HomeRunsPer9.ShouldBe(1.46);
        withNationals.RunScoredPer9.ShouldBe(2.92);
        withNationals.WalksPer9.ShouldBe(2.27);
        withNationals.StrikeoutsPer9.ShouldBe(11.92);
        withNationals.BattingAverageAgainst.ShouldBe(.182);
        withNationals.SluggingAgainst.ShouldBe(.354);
        withNationals.OnBasePercentageAgainst.ShouldBe(.251);
        withNationals.OnBasePlusSluggingAgainst.ShouldBe(.605);
        withNationals.WinningPercentage.ShouldBe(.667);
        withNationals.EarnedRunAverage.ShouldBe(2.76);
        withNationals.PitchesPerPlateAppearance.ShouldBeNull();
        withNationals.StrikePercentage.ShouldBe(.65);

        var withDodgers = results.Results.Single(r => r.LSTeamId == MLBPPTeam.Dodgers.GetLSTeamId()); ;
        withDodgers.LSPlayerId.ShouldBe(453286);
        withDodgers.Year.ShouldBe(2021);
        // withDodgers.TeamSeq.ShouldBe(2);
        withDodgers.GamesPlayed.ShouldBe(11);
        withDodgers.GamesStarted.ShouldBe(11);
        withDodgers.GamesFinished.ShouldBe(0);
        withDodgers.CompleteGames.ShouldBe(0);
        withDodgers.ShutOuts.ShouldBe(0);
        withDodgers.Wins.ShouldBe(7);
        withDodgers.Losses.ShouldBe(0);
        withDodgers.QualityStarts.ShouldBeNull();
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
        withDodgers.TotalBasesAllowed.ShouldBe(75);
        withDodgers.WildPitches.ShouldBe(2);
        withDodgers.Balks.ShouldBe(0);
        withDodgers.RunnersPickedOff.ShouldBe(0);
        withDodgers.NumberOfPitches.ShouldBe(1034);
        withDodgers.Strikes.ShouldBe(705);
        withDodgers.GroundOuts.ShouldBe(46);
        withDodgers.AirOuts.ShouldBe(72);
        withDodgers.DoublePlays.ShouldBeNull();
        withDodgers.StrikeoutToWalkRatio.ShouldBe(11.13);
        withDodgers.WHIP.ShouldBe(0.82);
        withDodgers.HitsPer9.ShouldBe(6.32);
        withDodgers.HomeRunsPer9.ShouldBe(.66);
        withDodgers.RunScoredPer9.ShouldBe(2.24);
        withDodgers.WalksPer9.ShouldBe(1.05);
        withDodgers.StrikeoutsPer9.ShouldBe(11.72);
        withDodgers.BattingAverageAgainst.ShouldBe(.189);
        withDodgers.SluggingAgainst.ShouldBe(.295);
        withDodgers.OnBasePercentageAgainst.ShouldBe(.220);
        withDodgers.OnBasePlusSluggingAgainst.ShouldBe(.515);
        withDodgers.WinningPercentage.ShouldBe(1);
        withDodgers.EarnedRunAverage.ShouldBe(1.98);
        withDodgers.PitchesPerPlateAppearance.ShouldBeNull();
        withDodgers.StrikePercentage.ShouldBe(.68);
      }).GetAwaiter().GetResult();
    }

    [Test]
    public async Task GetTeamsForYear_GetsTeams_For2021()
    {
      var results = await _client.GetTeamsForYear(2021);
      results.TotalResults.ShouldBe(30);
      var orioles = results.Results.First(r => r.LSTeamId == 110);
      orioles.Year.ShouldBe(2021);
      orioles.FullName.ShouldBe("Baltimore Orioles");
      orioles.State.ShouldBe("MD");
      orioles.City.ShouldBe("Baltimore");
      orioles.Venue.ShouldBe("Oriole Park at Camden Yards");
      orioles.League.ShouldBe("American League");
      orioles.Division.ShouldBe("American League East");
    }

    [Test]
    public async Task GetTeamsForYear_GetsTeams_For1915()
    {
      var results = await _client.GetTeamsForYear(1915);
      results.TotalResults.ShouldBe(24);
      var redSox = results.Results.First(r => r.LSTeamId == 111);
      redSox.Year.ShouldBe(1915);
      redSox.FullName.ShouldBe("Boston Red Sox");
      redSox.State.ShouldBe("MA");
      redSox.City.ShouldBe("Boston");
      redSox.Venue.ShouldBe("Fenway Park");
      redSox.League.ShouldBe("American League");
      redSox.Division.ShouldBe(null);
    }

    [Test]
    public async Task GetAllStarTeamsForYear_GetsTeams_For2021()
    {
      var results = await _client.GetAllStarTeamsForYear(2021);
      results.TotalResults.ShouldBe(2);
      var orioles = results.Results.First();
      orioles.LSTeamId.ShouldBe(159);
      orioles.Year.ShouldBe(2021);
      orioles.FullName.ShouldBe("American League All-Stars");
      orioles.State.ShouldBe("DC");
      orioles.City.ShouldBe("Washington");
      orioles.Venue.ShouldBe("Nationals Park");
      orioles.League.ShouldBe("American League");
      orioles.Division.ShouldBe(null);
    }

    [Test]
    public async Task GetAllStarTeamsForYear_GetsTeams_For1951()
    {
      var results = await _client.GetAllStarTeamsForYear(1951);
      results.TotalResults.ShouldBe(2);
      var orioles = results.Results.First();
      orioles.LSTeamId.ShouldBe(159);
      orioles.Year.ShouldBe(1951);
      orioles.FullName.ShouldBe("American League All-Stars");
      orioles.State.ShouldBe("MI");
      orioles.City.ShouldBe("Detroit");
      orioles.Venue.ShouldBe("Briggs Stadium");
      orioles.League.ShouldBe("American League");
      orioles.Division.ShouldBe(null);
    }
  }
}