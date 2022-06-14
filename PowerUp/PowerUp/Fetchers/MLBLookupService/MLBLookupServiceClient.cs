using System;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IMLBLookupServiceClient
  {
    Task<PlayerSearchResults> SearchPlayer(string name); 
  }

  public class MLBLookupServiceClient : IMLBLookupServiceClient
  {
    private const string BASE_URL = "http://lookup-service-prod.mlb.com/json";

    public static String INFO_ENDPOINT = "/named.player_info.bam?";
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
  }
}
