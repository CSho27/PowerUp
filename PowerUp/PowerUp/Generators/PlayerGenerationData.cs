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

    public LSPlayerInfoDataset(PlayerInfoResult result)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      UniformNumber = result.UniformNumber;
      PrimaryPosition = result.Position;
      BattingSide = result.BattingSide;
    }
  }

  public class LSHittingStatsDataset
  {


    public LSHittingStatsDataset(IEnumerable<HittingStatsResult> results)
    {

    }
  }

  public class LSFieldingStatDataset
  {
    public IDictionary<Position, LSFieldingStats> FieldingByPosition { get; }
    public LSFieldingStats OverallFielding { get; }

    public LSFieldingStatDataset(IEnumerable<FieldingStatsResult> results)
    {
      FieldingByPosition = results.GroupBy(r => r.Position).ToDictionary(g => g.Key, g => new LSFieldingStats(g));
      OverallFielding = new LSFieldingStats(results);
    }
  }

  public class LSFieldingStats
  {
    public int? TotalChances { get; }

    public LSFieldingStats(IEnumerable<FieldingStatsResult> results)
    {
      TotalChances = results.SumOrNull(r => r.TotalChances);
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
