using System.Linq;
using System.Threading.Tasks;
using PowerUp.Fetchers.Algolia;
using PowerUp.Fetchers.MLBStatsApi;
using PowerUp.Fetchers.Statcast;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IMLBLookupServiceClient
  {
    Task<PlayerSearchResults> SearchPlayer(string name);
    Task<PlayerResult> GetPlayerData(long lsPlayerId, int year);
    Task<PlayerInfoResult> GetPlayerInfo(long lsPlayerId);
    Task<HittingStatsResults?> GetHittingStats(long lsPlayerId, int year);
    Task<FieldingStatsResults?> GetFieldingStats(long lsPlayerId, int year);
    Task<PitchingStatsResults?> GetPitchingStats(long lsPlayerId, int year);
    Task<TeamsForYearResults> GetTeamsForYear(int year);
    Task<TeamsForYearResults> GetAllStarTeamsForYear(int year);
  }

  public record PlayerResult(PlayerInfoResult? Info, HittingStatsResults? Hitting, FieldingStatsResults? Fielding, PitchingStatsResults? Pitching);

  public class MLBLookupServiceClient : IMLBLookupServiceClient
  {
    private const string BASE_URL = "http://lookup-service-prod.mlb.com/json";
    private readonly ApiClient _apiClient = new ApiClient();
    private readonly IAlgoliaClient _algoliaClient;
    private readonly IMLBStatsApiClient _mlbStatsApiClient;

    public MLBLookupServiceClient(
      IAlgoliaClient algoliaClient,
      IMLBStatsApiClient mlbStatsApiClient
    )
    {
      _algoliaClient = algoliaClient;
      _mlbStatsApiClient = mlbStatsApiClient;
    }

    public async Task<PlayerSearchResults> SearchPlayer(string name)
    {
      var searchResponse = await _algoliaClient.SearchPlayer(name);
      var totalResults = searchResponse.NbHits;

      var results = await Task.WhenAll(searchResponse.Hits.Select(async r => await GetPlayerInfo(r.PlayerId)));
      return new PlayerSearchResults(totalResults, results);
    }

    public async Task<PlayerResult> GetPlayerData(long lsPlayerId, int year)
    {
      var data = await _mlbStatsApiClient.GetPlayerStatistics(lsPlayerId, year);
      var hittingStats = data.Stats.SingleOrDefault(s => s.Group?.DisplayName == "hitting");
      var fieldingStats = data.Stats.SingleOrDefault(s => s.Group?.DisplayName == "fielding");
      var pitchingStats = data.Stats.SingleOrDefault(s => s.Group?.DisplayName == "pitching");

      return new PlayerResult(
        new PlayerInfoResult(data),
        new HittingStatsResults(hittingStats),
        new FieldingStatsResults(fieldingStats),
        new PitchingStatsResults(pitchingStats)
      );

    }
    public async Task<PlayerInfoResult> GetPlayerInfo(long lsPlayerId)
    {
      var data = await _mlbStatsApiClient.GetPlayerInfo(lsPlayerId);
      return new PlayerInfoResult(data);
    }

    public async Task<HittingStatsResults?> GetHittingStats(long lsPlayerId, int year)
    {
      var data = await GetPlayerData(lsPlayerId, year);
      return data.Hitting;
    }

    public async Task<FieldingStatsResults?> GetFieldingStats(long lsPlayerId, int year)
    {
      var data = await GetPlayerData(lsPlayerId, year);
      return data.Fielding;
    }

    public async Task<PitchingStatsResults?> GetPitchingStats(long lsPlayerId, int year)
    {
      var data = await GetPlayerData(lsPlayerId, year);
      return data.Pitching;
    }

    public async Task<TeamsForYearResults> GetTeamsForYear(int year)
    {
      var teamsResponse = await _mlbStatsApiClient.GetTeams(year);
      var venueIds = teamsResponse.Teams.Select(t => t.Venue?.Id).Where(id => id is not null).Cast<long>();
      var venuesResponse = await _mlbStatsApiClient.GetVenues(venueIds);
      var teamAndVenues = teamsResponse.Teams.Select(t => (t, venuesResponse.Venues.FirstOrDefault(v => v.Id == t.Venue?.Id)));
      return new TeamsForYearResults(teamAndVenues);
    }

    public async Task<TeamsForYearResults> GetAllStarTeamsForYear(int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.team_all_season.bam" },
        new { sport_code = "\'mlb\'", all_star_sw = "\'Y\'", season = $"\'{year}\'" }
      );

      var response = await _apiClient.Get<LSTeamsResponse>(url);
      var results = response!.team_all_season!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSTeamResult>(results.row)!;
      return new TeamsForYearResults(totalResults, deserializedResults);
    }
  }
}
