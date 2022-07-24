using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IPlayerStatisticsFetcher
  {
    public PlayerStatisticsResult GetStatistics(
      long lsPlayerId,
      int year,
      bool excludePlayerInfo = false,
      bool excludeHittingStats = false,
      bool excludeFieldingStats = false,
      bool excludePitchingStats = false
    );
  }

  public class LSPlayerStatisticsFetcher : IPlayerStatisticsFetcher
  {
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;

    public LSPlayerStatisticsFetcher(IMLBLookupServiceClient mlbLookupServiceClient)
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
    }

    public PlayerStatisticsResult GetStatistics(
      long lsPlayerId,
      int year,
      bool excludePlayerInfo = false,
      bool excludeHittingStats = false,
      bool excludeFieldingStats = false,
      bool excludePitchingStats = false
    )
    {
      var result = new PlayerStatisticsResult();
      var fetchTasks = new List<Task>();

      if (!excludePlayerInfo)
      {
        var fetchPlayerInfo = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPlayerInfo(lsPlayerId);

          if (response == null)
            throw new InvalidOperationException("No Player Info Found for Id");

          result.PlayerInfo = response;
        });
        fetchTasks.Add(fetchPlayerInfo);
      }

      if (!excludeHittingStats)
      {
        var fetchHittingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetHittingStats(lsPlayerId, year);
          if (response.Results.Any())
            result.HittingStats = response;
        });
        fetchTasks.Add(fetchHittingStats);
      }

      if (!excludeFieldingStats)
      {
        var fetchFieldingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetFieldingStats(lsPlayerId, year);
          if (response.Results.Any())
            result.FieldingStats = response;
        });
        fetchTasks.Add(fetchFieldingStats);
      }

      if (!excludePitchingStats)
      {
        var fetchPitchingStats = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPitchingStats(lsPlayerId, year);
          if (response.Results.Any())
            result.PitchingStats = response;
        });
        fetchTasks.Add(fetchPitchingStats);
      }

      Task.WaitAll(fetchTasks.ToArray(), 5000);

      return result;
    }
  }

  public class PlayerStatisticsResult
  {
    public PlayerInfoResult? PlayerInfo { get; set; }
    public HittingStatsResults? HittingStats { get; set; }
    public FieldingStatsResults? FieldingStats { get; set; }
    public PitchingStatsResults? PitchingStats { get; set; }
  }
}
