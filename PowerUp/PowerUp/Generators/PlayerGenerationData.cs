using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public enum PlayerGenerationDataset
  {
    LSPlayerInfo,
    LSHittingStats,
    LSFieldingStats,
    LSPitchingStats
  }

  public class PlayerGenerationData 
  {
    public int Year { get; set; }
    public Position PrimaryPosition => FieldingStats?.PrimaryPosition ?? PlayerInfo!.PrimaryPosition;
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

    public LSPlayerInfoDataset(PlayerInfoResult result)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      UniformNumber = result.UniformNumber;
      PrimaryPosition = result.Position;
      BattingSide = result.BattingSide;
      ThrowingArm = result.ThrowingArm;
      BirthCountry = result.BirthCountry;
    }
  }

  public class LSHittingStatsDataset
  {
    public int? AtBats { get; }
    public int? HomeRuns { get; }
    public double? BattingAverage { get; }
    public int? StolenBases { get; }
    public int? Runs { get; }

    public LSHittingStatsDataset(IEnumerable<HittingStatsResult> results)
    {
      AtBats = results.SumOrNull(r => r.AtBats);
      HomeRuns = results.SumOrNull(r => r.HomeRuns);
      BattingAverage = results.CombineAverages(r => r.BattingAverage, r => r.AtBats);
      StolenBases = results.SumOrNull(r => r.StolenBases);
      Runs = results.SumOrNull(r => r.Runs);
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
    public int? GamesStarted { get; }
    public int? GamesFinished { get; }
    public int? SaveOpportunities { get; }

    public LSPitchingStatsDataset(IEnumerable<PitchingStatsResult> results)
    {
      GamesStarted = results.SumOrNull(r => r.GamesStarted);
      GamesFinished = results.SumOrNull(r => r.SaveOpportunities);
      SaveOpportunities = results.SumOrNull(r => r.GamesFinished);
    }
  }
}
