using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IMLBLookupServiceClient
  {
    Task<PlayerSearchResults> SearchPlayer(string name);
    Task<PlayerInfoResult> GetPlayerInfo(int lsPlayerId);
  }

  public class MLBLookupServiceClient : IMLBLookupServiceClient
  {
    private const string BASE_URL = "http://lookup-service-prod.mlb.com/json";

    public static String HITTING_ENDPOINT = "/named.sport_hitting_tm.bam?";
    public static String FIELDING_ENDPOINT = "/named.sport_fielding_tm.bam?";
    public static String PITCHING_ENDPOINT = "/named.sport_pitching_tm.bam?";

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
  }
}
