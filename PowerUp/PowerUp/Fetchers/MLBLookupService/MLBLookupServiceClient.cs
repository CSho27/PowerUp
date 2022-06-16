using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IMLBLookupServiceClient
  {
    Task<PlayerSearchResults> SearchPlayer(string name);
    Task<PlayerInfoResult> GetPlayerInfo(int lsPlayerId);
    Task<HittingStatsResults> GetHittingStats(int lsPlayerId, int year);
    Task<PitchingStatsResults> GetPitchingStats(int lsPlayerId, int year);
  }

  public partial class MLBLookupServiceClient : IMLBLookupServiceClient
  {
    private const string BASE_URL = "http://lookup-service-prod.mlb.com/json";

    public static String FIELDING_ENDPOINT = "/named.sport_fielding_tm.bam?";
    public static String PITCHING_ENDPOINT = "/?";

    private readonly ApiClient _apiClient = new ApiClient();

    public async Task<PlayerSearchResults> SearchPlayer(string name)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.search_player_all.bam" },
        new { sport_code = "\'mlb\'", name_part = $"\'{name}%\'" }
      );

      var response = await _apiClient.Get<LSPlayerSearchResults>(url);
      var results = response!.search_player_all!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSPlayerSearchResult>(results.row);
      return new PlayerSearchResults(totalResults, deserializedResults);
    }

    public async Task<PlayerInfoResult> GetPlayerInfo(int lsPlayerId)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.player_info.bam" },
        new { sport_code = "\'mlb\'", player_id = $"\'{lsPlayerId}\'" }
      );

      var response = await _apiClient.Get<LSPlayerInfoResponse>(url);
      var results = response!.player_info!.queryResults!;
      if (!results.row.HasValue)
        throw new InvalidOperationException("No player info found for this id");

      var deserializedResult = JsonSerializer.Deserialize<LSPlayerInfoResult>(results.row!.Value)!;
      return new PlayerInfoResult(deserializedResult);
    }

    public async Task<HittingStatsResults> GetHittingStats(int lsPlayerId, int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.sport_hitting_tm.bam" },
        new { league_list_id = "\'mlb\'", game_type="\'R\'", player_id = $"\'{lsPlayerId}\'", season = $"\'{year}\'" }
      );

      var response = await _apiClient.Get<LSHittingStatsResponse>(url);
      var results = response!.sport_hitting_tm!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSHittingStatsResult>(results.row)!;
      return new HittingStatsResults(totalResults, deserializedResults);
    }

    public async Task<PitchingStatsResults> GetPitchingStats(int lsPlayerId, int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.sport_pitching_tm.bam" },
        new { league_list_id = "\'mlb\'", game_type = "\'R\'", player_id = $"\'{lsPlayerId}\'", season = $"\'{year}\'" }
      );

      var response = await _apiClient.Get<LSPitchingStatsResponse>(url);
      var results = response!.sport_pitching_tm!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSPitchingStatsResult>(results.row)!;
      return new PitchingStatsResults(totalResults, deserializedResults);
    }
  }
}
