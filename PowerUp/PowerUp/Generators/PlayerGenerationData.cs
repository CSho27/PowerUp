using PowerUp.Entities.Players;
using PowerUp.Fetchers.MLBLookupService;

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
  }

  public class LSPlayerInfoDataset
  {
    public string FirstNameUsed { get; }
    public string LastName { get; }
    public Position PrimaryPosition { get; }

    public LSPlayerInfoDataset(PlayerInfoResult result)
    {
      FirstNameUsed = result.FirstNameUsed;
      LastName = result.LastName;
      PrimaryPosition = result.Position;
    }
  }
}
