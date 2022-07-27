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
    public double PortionOfSeason => MLBSeasonUtils.GetFractionOfSeasonPlayed(Year);
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
    public DateTime? ProDebutDate { get; }

    public LSPlayerInfoDataset(PlayerInfoResult result, string? uniformNumber)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      UniformNumber = uniformNumber ?? result.UniformNumber;
      PrimaryPosition = result.Position;
      BattingSide = result.BattingSide;
      ThrowingArm = result.ThrowingArm;
      BirthCountry = result.BirthCountry;
      ProDebutDate = result.ProDebutDate;
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

    public LSHittingStatsDataset(IEnumerable<HittingStatsResult> results, (IEnumerable<HittingStatsResult> results, double prorateBy)? supplementaryStats = null)
    {
      var supplementaryResults = supplementaryStats?.results;
      var proratedSupplementaryBy = supplementaryStats?.prorateBy ?? 0;

      AtBats = results.SumOrNull(r => r.AtBats) + supplementaryResults.SumOrNullProrated(r => r.AtBats, proratedSupplementaryBy);
      HomeRuns = results.SumOrNull(r => r.HomeRuns) + supplementaryResults.SumOrNullProrated(r => r.HomeRuns, proratedSupplementaryBy);
      BattingAverage = results.CombineAverages(r => r.BattingAverage, r => r.AtBats);
      StolenBases = results.SumOrNull(r => r.StolenBases) + supplementaryResults.SumOrNullProrated(r => r.StolenBases, proratedSupplementaryBy);
      Runs = results.SumOrNull(r => r.Runs) + +supplementaryResults.SumOrNullProrated(r => r.Runs, proratedSupplementaryBy);
      var lastResult = results.MaxBy(r => r.TeamSeq) ?? results.Last();
      LastTeamForYear_LSTeamId = lastResult.LSTeamId;
    }

    public static LSHittingStatsDataset? BuildFor(IEnumerable<HittingStatsResult>? results, IEnumerable<HittingStatsResult>? previousYearResults)
    {
      if(results == null && previousYearResults == null)
      {
        return null;
      }
      else if(results != null && previousYearResults == null)
      {
        return new LSHittingStatsDataset(results);
      }
      else if(results == null && previousYearResults != null)
      {
        return new LSHittingStatsDataset(previousYearResults);
      }
      else
      {
        return new LSHittingStatsDataset(results!, (previousYearResults!, 1 - MLBSeasonUtils.GetFractionOfCurrentSeasonPlayed()));
      }
    }
  }

  public class LSFieldingStatDataset
  {
    public IDictionary<Position, LSFieldingStats> FieldingByPosition { get; }
    public LSFieldingStats OverallFielding { get; }
    public Position? PrimaryPosition => FieldingByPosition.Any()
      ? FieldingByPosition.MaxBy(p => p.Value.Innings).Key
      : null;

    public LSFieldingStatDataset(IEnumerable<FieldingStatsResult> results, (IEnumerable<FieldingStatsResult> results, double prorateBy)? supplementaryStats = null)
    {
      var supplementaryResults = supplementaryStats?.results;
      var proratedSupplementaryBy = supplementaryStats?.prorateBy ?? 0;

      FieldingByPosition = results.GroupBy(r => r.Position).ToDictionary(g => g.Key, g => new LSFieldingStats(g, (supplementaryResults?.Where(r => r.Position == g.Key) ?? Enumerable.Empty<FieldingStatsResult>(), proratedSupplementaryBy)));
      OverallFielding = new LSFieldingStats(results, supplementaryStats);
    }

    public static LSFieldingStatDataset? BuildFor(IEnumerable<FieldingStatsResult>? results, IEnumerable<FieldingStatsResult>? previousYearResults)
    {
      if (results == null && previousYearResults == null)
      {
        return null;
      }
      else if (results != null && previousYearResults == null)
      {
        return new LSFieldingStatDataset(results);
      }
      else if (results == null && previousYearResults != null)
      {
        return new LSFieldingStatDataset(previousYearResults);
      }
      else
      {
        return new LSFieldingStatDataset(results!, (previousYearResults!, 1 - MLBSeasonUtils.GetFractionOfCurrentSeasonPlayed()));
      }
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

    public LSFieldingStats(IEnumerable<FieldingStatsResult> results, (IEnumerable<FieldingStatsResult> results, double prorateBy)? supplementaryStats = null)
    {
      var supplementaryResults = supplementaryStats?.results;
      var proratedSupplementaryBy = supplementaryStats?.prorateBy ?? 0;

      Innings = results.SumOrNull(r => r.Innings) + supplementaryResults.SumOrNullProrated(r => r.Innings, proratedSupplementaryBy);
      TotalChances = results.SumOrNull(r => r.TotalChances) + supplementaryResults.SumOrNullProrated(r => r.TotalChances, proratedSupplementaryBy);
      Assists = results.SumOrNull(r => r.Assists) + supplementaryResults.SumOrNullProrated(r => r.Assists, proratedSupplementaryBy);
      FieldingPercentage = results.CombineAverages(r => r.FieldingPercentage, r => r.TotalChances);
      RangeFactor = results.CombineAverages(r => r.RangeFactor, r => r.Innings ?? r.GamesPlayed);
      Catcher_StolenBasesAllowed = results.SumOrNull(r => r.Catcher_StolenBasesAllowed) + supplementaryResults.SumOrNullProrated(r => r.Catcher_StolenBasesAllowed, proratedSupplementaryBy);
      Catcher_RunnersThrownOut = results.SumOrNull(r => r.Catcher_RunnersThrownOut) + supplementaryResults.SumOrNullProrated(r => r.Catcher_RunnersThrownOut, proratedSupplementaryBy);
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

    public LSPitchingStatsDataset(IEnumerable<PitchingStatsResult> results, (IEnumerable<PitchingStatsResult> results, double prorateBy)? supplementaryStats = null)
    {
      var supplementaryResults = supplementaryStats?.results;
      var proratedSupplementaryBy = supplementaryStats?.prorateBy ?? 0;

      GamesPitched = results.SumOrNull(r => r.GamesPlayed) + supplementaryResults.SumOrNullProrated(r => r.GamesPlayed, proratedSupplementaryBy);
      GamesStarted = results.SumOrNull(r => r.GamesStarted) + supplementaryResults.SumOrNullProrated(r => r.GamesStarted, proratedSupplementaryBy);
      GamesFinished = results.SumOrNull(r => r.GamesFinished) + supplementaryResults.SumOrNullProrated(r => r.GamesFinished, proratedSupplementaryBy);
      SaveOpportunities = results.SumOrNull(r => r.SaveOpportunities) + supplementaryResults.SumOrNullProrated(r => r.SaveOpportunities, proratedSupplementaryBy);
      MathematicalInnings = results.SumOrNull(r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0)) 
        + supplementaryResults.SumOrNullProrated(r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0), proratedSupplementaryBy);
      WalksPer9 = results.CombineAverages(r => r.WalksPer9, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      StrikeoutsPer9 = results.CombineAverages(r => r.StrikeoutsPer9, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      WHIP = results.CombineAverages(r => r.WHIP, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      EarnedRunAverage = results.CombineAverages(r => r.EarnedRunAverage, r => InningConversion.ToMathematicalInnings(r.InningsPitched ?? 0));
      BattingAverageAgainst = results.CombineAverages(r => r.BattingAverageAgainst, r => r.AtBats);
      var lastResult = results.MaxBy(r => r.TeamSeq) ?? results.Last();
      LastTeamForYear_LSTeamId = lastResult.LSTeamId;
    }

    public static LSPitchingStatsDataset? BuildFor(IEnumerable<PitchingStatsResult>? results, IEnumerable<PitchingStatsResult>? previousYearResults)
    {
      if (results == null && previousYearResults == null)
      {
        return null;
      }
      else if (results != null && previousYearResults == null)
      {
        return new LSPitchingStatsDataset(results);
      }
      else if (results == null && previousYearResults != null)
      {
        return new LSPitchingStatsDataset(previousYearResults);
      }
      else
      {
        return new LSPitchingStatsDataset(results!, (previousYearResults!, 1 - MLBSeasonUtils.GetFractionOfCurrentSeasonPlayed()));
      }
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
