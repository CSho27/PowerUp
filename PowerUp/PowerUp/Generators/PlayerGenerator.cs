using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Fetchers.BaseballReference;
using PowerUp.Fetchers.MLBLookupService;
using System.Collections.Generic;

namespace PowerUp.Generators
{
  public interface IPlayerGenerator
  {
    PlayerGenerationResult GeneratePlayer(long lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm, string? uniformNumber = null);
  }

  public class PlayerGenerationResult
  {
    public long LSPlayerId { get; set; }
    public Player Player { get; set; }
    public long? LastTeamForYear_LSTeamId { get; set; }

    public PlayerGenerationResult(long lsPlayerId, Player player, long? lastTeamForYear_lsTeamId)
    {
      LSPlayerId = lsPlayerId;
      Player = player;
      LastTeamForYear_LSTeamId = lastTeamForYear_lsTeamId;
    }
  }

  public class PlayerGenerator : IPlayerGenerator
  {
    private readonly IPlayerApi _playerApi;
    private readonly IPlayerStatisticsFetcher _playerStatsFetcher;
    private readonly IBaseballReferenceClient _baseballReferenceClient;

    public PlayerGenerator(
      IPlayerApi playerApi,
      IPlayerStatisticsFetcher playerStatsFetcher,
      IBaseballReferenceClient baseballReferenceClient
    )
    {
      _playerApi = playerApi;
      _playerStatsFetcher = playerStatsFetcher;
      _baseballReferenceClient = baseballReferenceClient;
    }

    public PlayerGenerationResult GeneratePlayer(long lsPlayerId, int year, PlayerGenerationAlgorithm generationAlgorithm, string? uniformNumber = null)
    {
      var fracYearPlayed = MLBSeasonUtils.GetFractionOfSeasonPlayed(year);
      PlayerStatisticsResult? currentYearStats = null;
      if(fracYearPlayed > 0)
      {
        currentYearStats = _playerStatsFetcher.GetStatistics(
          lsPlayerId, 
          year,
          excludePlayerInfo: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPlayerInfo),
          excludeHittingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSHittingStats),
          excludeFieldingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSFieldingStats),
          excludePitchingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPitchingStats)
        );
      }

      PlayerStatisticsResult? previousYearStats = null;
      if(fracYearPlayed < 1)
        previousYearStats = _playerStatsFetcher.GetStatistics(
          lsPlayerId,
          year-1,
          excludePlayerInfo: true,
          excludeHittingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSHittingStats),
          excludeFieldingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSFieldingStats),
          excludePitchingStats: !generationAlgorithm.DatasetDependencies.Contains(PlayerGenerationDataset.LSPitchingStats)
      );

      var mostRecentInfo = currentYearStats?.PlayerInfo ?? previousYearStats?.PlayerInfo;
      var data = new PlayerGenerationData
      {
        Year = year,
        PlayerInfo = mostRecentInfo != null
          ? new LSPlayerInfoDataset(mostRecentInfo, uniformNumber)
          : null,
        HittingStats = LSHittingStatsDataset.BuildFor(currentYearStats?.HittingStats?.Results, previousYearStats?.HittingStats?.Results),
        FieldingStats = LSFieldingStatDataset.BuildFor(currentYearStats?.FieldingStats?.Results, previousYearStats?.FieldingStats?.Results),
        PitchingStats = LSPitchingStatsDataset.BuildFor(currentYearStats?.PitchingStats?.Results, previousYearStats?.PitchingStats?.Results)
      };

      var player = _playerApi.CreateDefaultPlayer(EntitySourceType.Generated, isPitcher: data!.PrimaryPosition == Position.Pitcher);
      player.Year = year;
      player.GeneratedPlayer_LSPLayerId = lsPlayerId;
      player.GeneratedPlayer_IsUnedited = true;

      var propertiesThatHaveBeenSet = new HashSet<string>();
      foreach(var setter in generationAlgorithm.PropertySetters)
      {
        if (propertiesThatHaveBeenSet.Contains(setter.PropertyKey))
          continue;

        var wasSet = setter.SetProperty(player, data);
        if (wasSet)
          propertiesThatHaveBeenSet.Add(setter.PropertyKey);
      }
      
      return new PlayerGenerationResult(lsPlayerId, player, data.LastTeamForYear_LSTeamId);
    }
  }

  public abstract class PlayerGenerationAlgorithm : GenerationAlgorithm<Player, PlayerGenerationDataset, PlayerGenerationData> { }
  public abstract  class PlayerPropertySetter : PropertySetter<Player, PlayerGenerationData> { }
}
