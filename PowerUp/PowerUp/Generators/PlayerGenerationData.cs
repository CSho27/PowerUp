using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public enum PlayerGenerationDataset
  {
    LSPlayerInfo,
    LSHittingStats,
    LSFieldingStats,
    LSPitchingStats,
    BaseballReferenceIdDataset
  }

  public class PlayerGenerationData 
  {
    public int Year { get; set; }
    public Position PrimaryPosition => FieldingStats?.PrimaryPosition ?? PlayerInfo!.PrimaryPosition;
    public long? LastTeamForYear_LSTeamId => PrimaryPosition == Position.Pitcher
      ? PitchingStats?.LastTeamForYear_LSTeamId
      : HittingStats?.LastTeamForYear_LSTeamId;
    public LSPlayerInfoDataset? PlayerInfo { get; set; }
    public LSHittingStatsDataset? HittingStats { get; set; }
    public LSFieldingStatDataset? FieldingStats { get; set; }
    public LSPitchingStatsDataset? PitchingStats { get; set; }
  }

  public class LSPlayerInfoDataset
  {
    public string FirstNameUsed { get; }
    public string LastName { get; }
    public string? UniformNumber { get; }
    public Position PrimaryPosition { get; }
    public BattingSide BattingSide { get; }
    public ThrowingArm ThrowingArm { get; }
    public string? BirthCountry { get; }
    public DateTime ProDebutDate { get; }

    public LSPlayerInfoDataset(PlayerInfoResult result)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      UniformNumber = result.UniformNumber;
      PrimaryPosition = result.Position;
      BattingSide = result.BattingSide;
      ThrowingArm = result.ThrowingArm;
      BirthCountry = result.BirthCountry;
      ProDebutDate = result.ProDebutDate!.Value;
    }
  }

  public class LSHittingStatsDataset
  {
    public int? AtBats { get; }
    public int? HomeRuns { get; }
    public double? BattingAverage { get; }
    public int? StolenBases { get; }
    public int? Runs { get; }
    public long LastTeamForYear_LSTeamId { get; }

    public LSHittingStatsDataset(IEnumerable<HittingStatsResult> results)
    {
      AtBats = results.SumOrNull(r => r.AtBats);
      HomeRuns = results.SumOrNull(r => r.HomeRuns);
      BattingAverage = results.CombineAverages(r => r.BattingAverage, r => r.AtBats);
      StolenBases = results.SumOrNull(r => r.StolenBases);
      Runs = results.SumOrNull(r => r.Runs);
      var lastResult = results.MaxBy(r => r.TeamSeq) ?? results.Last();
      LastTeamForYear_LSTeamId = lastResult.LSTeamId;
    }
  }

  public class LSFieldingStatDataset
  {
    public IDictionary<Position, LSFieldingStats> FieldingByPosition { get; }
    public LSFieldingStats OverallFielding { get; }
    public Position? PrimaryPosition => FieldingByPosition.Any()
      ? FieldingByPosition.MaxBy(p => p.Value.Innings).Key
      : null;

    public LSFieldingStatDataset(IEnumerable<FieldingStatsResult> results)
    {
      FieldingByPosition = results.GroupBy(r => r.Position).ToDictionary(g => g.Key, g => new LSFieldingStats(g));
      OverallFielding = new LSFieldingStats(results);
    }
  }

  public class LSFieldingStats
  {
    public double? Innings { get; }
    public int? TotalChances { get; }
    public int? Assists { get; }
    public double? FieldingPercentage { get; }
    public double? RangeFactor { get; }
    public int? Catcher_StolenBasesAllowed { get; }
    public int? Catcher_RunnersThrownOut { get; }

    public LSFieldingStats(IEnumerable<FieldingStatsResult> results)
    {
      Innings = results.SumOrNull(r => r.Innings);
      TotalChances = results.SumOrNull(r => r.TotalChances);
      Assists = results.SumOrNull(r => r.Assists);
      FieldingPercentage = results.CombineAverages(r => r.FieldingPercentage, r => r.TotalChances);
      RangeFactor = results.CombineAverages(r => r.RangeFactor, r => r.Innings ?? r.GamesPlayed);
      Catcher_StolenBasesAllowed = results.SumOrNull(r => r.Catcher_StolenBasesAllowed);
      Catcher_RunnersThrownOut = results.SumOrNull(r => r.Catcher_RunnersThrownOut);
    }
  }

  public class LSPitchingStatsDataset
  {
    public int? GamesPitched { get; }
    public int? GamesStarted { get; }
    public int? GamesFinished { get; }
    public int? SaveOpportunities { get; }
    public double? WalksPer9 { get; }
    public double? StrikeoutsPer9 { get; }
    public double? WHIP { get; }
    public double? EarnedRunAverage { get; }
    public double? BattingAverageAgainst { get; }
    public double? MathematicalInnings { get; }
    public long LastTeamForYear_LSTeamId { get; }

    public LSPitchingStatsDataset(IEnumerable<PitchingStatsResult> results)
    {
      GamesPitched = results.SumOrNull(r => r.GamesPlayed);
      GamesStarted = results.SumOrNull(r => r.GamesStarted);
      GamesFinished = results.SumOrNull(r => r.SaveOpportunities);
      SaveOpportunities = results.SumOrNull(r => r.GamesFinished);
      MathematicalInnings = results.Select(r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0)).Sum(r => r);
      WalksPer9 = results.CombineAverages(r => r.WalksPer9, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      StrikeoutsPer9 = results.CombineAverages(r => r.StrikeoutsPer9, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      WHIP = results.CombineAverages(r => r.WHIP, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      EarnedRunAverage = results.CombineAverages(r => r.EarnedRunAverage, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      BattingAverageAgainst = results.CombineAverages(r => r.BattingAverageAgainst, r => r.AtBats);
      var lastResult = results.MaxBy(r => r.TeamSeq) ?? results.Last();
      LastTeamForYear_LSTeamId = lastResult.LSTeamId;
    }
  }

  public static class InningConversion
  {
    public static double ToMathematicalInnings(double innings)
    {
      var fullInnings = innings.RoundDown();
      var partialInning = innings - fullInnings;
      if (partialInning > .3)
        throw new InvalidOperationException("Value cannot have a decimal larger than .3");

      var fractionalInning = partialInning * 10 * (1.0 / 3);
      return fullInnings + fractionalInning;
    }
  }

}
