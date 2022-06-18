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
    public LSPitchingStatsDataset? PitchingStats { get; set; }
  }

  public class LSPlayerInfoDataset
  {
    public string FirstNameUsed { get; }
    public string LastName { get; }
    public string? UniformNumber { get; }
    public Position PrimaryPosition { get; }

    public LSPlayerInfoDataset(PlayerInfoResult result)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      UniformNumber = result.UniformNumber;
      PrimaryPosition = result.Position;
    }
  }

  public class LSPitchingStatsDataset
  {
    public int? GamesStarted { get; }
    public int? GamesFinished { get; }
    public int? SaveOpportunities { get; }

    public LSPitchingStatsDataset(PitchingStatsResults results)
    {
      GamesStarted = results.Results.SumOrNull(r => r.GamesStarted);
      GamesFinished = results.Results.SumOrNull(r => r.SaveOpportunities);
      SaveOpportunities = results.Results.SumOrNull(r => r.GamesFinished);
    }
  }
}
