using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.MLBLookupService;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public interface IPlayerGenerator
  {
    PlayerGenerationResult GeneratePlayer(long lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm);
  }

  public class PlayerGenerationResult
  {
    public Player Player { get; set; }
    public IEnumerable<GeneratorWarning> Warnings { get; set; } = Enumerable.Empty<GeneratorWarning>();
    public long? LastTeamForYear_LSTeamId { get; set; }

    public PlayerGenerationResult(Player player, IEnumerable<GeneratorWarning> warnings, long? lastTeamForYear_lsTeamId)
    {
      Player = player;
      Warnings = warnings;
      LastTeamForYear_LSTeamId = lastTeamForYear_lsTeamId;
    }
  }

  public class PlayerGenerator : IPlayerGenerator
  {
    private readonly IPlayerApi _playerApi;
    private readonly IPlayerStatisticsFetcher _playerStatsFetcher;

    public PlayerGenerator(
      IPlayerApi playerApi,
      IPlayerStatisticsFetcher playerStatsFetcher
    )
    {
      _playerApi = playerApi;
      _playerStatsFetcher = playerStatsFetcher;
    }

    public PlayerGenerationResult GeneratePlayer(long lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm)
    {
      var playerStats = _playerStatsFetcher.GetStatistics(
        lsPlayerId, 
        year,
        excludePlayerInfo: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPlayerInfo),
        excludeHittingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSHittingStats),
        excludeFieldingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSFieldingStats),
        excludePitchingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPitchingStats)
      );
      var data = new PlayerGenerationData
      {
        Year = year,
        PlayerInfo = playerStats.PlayerInfo != null
          ? new LSPlayerInfoDataset(playerStats.PlayerInfo)
          : null,
        HittingStats = playerStats.HittingStats != null
          ? new LSHittingStatsDataset(playerStats.HittingStats.Results)
          : null,
        FieldingStats = playerStats.FieldingStats != null
          ? new LSFieldingStatDataset(playerStats.FieldingStats.Results) 
          : null,
        PitchingStats = playerStats.PitchingStats != null
          ? new LSPitchingStatsDataset(playerStats.PitchingStats.Results)
          : null
      };

      var player = _playerApi.CreateDefaultPlayer(EntitySourceType.Generated, isPitcher: data!.PrimaryPosition == Position.Pitcher);
      player.Year = year;

      var propertiesThatHaveBeenSet = new HashSet<string>();
      foreach(var setter in generationAlgorithm.PropertySetters)
      {
        if (propertiesThatHaveBeenSet.Contains(setter.PropertyKey))
          continue;

        var wasSet = setter.SetProperty(player, data);
        if (wasSet)
          propertiesThatHaveBeenSet.Add(setter.PropertyKey);
      }
      
      return new PlayerGenerationResult(player, Enumerable.Empty<GeneratorWarning>(), data.LastTeamForYear_LSTeamId);
    }
  }

  public abstract class PlayerGenerationAlgorithm : GenerationAlgorithm<Player, PlayerGenerationDataset, PlayerGenerationData> { }
  public abstract  class PlayerPropertySetter : PropertySetter<Player, PlayerGenerationData> { }
}
