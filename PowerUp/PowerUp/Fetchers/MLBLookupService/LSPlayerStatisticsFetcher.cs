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

      if(!excludePlayerInfo || !excludeHittingStats || !excludeFieldingStats || !excludePitchingStats)
      {
        var fetchPlayerData = Task.Run(async () =>
        {
          var response = await _mlbLookupServiceClient.GetPlayerData(lsPlayerId, year);
          result.PlayerInfo = response.Info;
          if (response.Hitting is not null && response.Hitting.Results.Any())
            result.HittingStats = response.Hitting;
          if (response.Pitching is not null && response.Pitching.Results.Any())
            result.PitchingStats = response.Pitching;
          if (response.Fielding is not null && response.Fielding.Results.Any())
            result.FieldingStats = response.Fielding;
        });
        fetchTasks.Add(fetchPlayerData);
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
