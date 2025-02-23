using System.Collections;
using System.Collections.Generic;
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
      return await GetResultsForTeams(teamsResponse.Teams, year);
    }

    public async Task<TeamsForYearResults> GetAllStarTeamsForYear(int year)
    {
      var alAllStars = await _mlbStatsApiClient.GetTeam(159, year);
      var nlAllStars = await _mlbStatsApiClient.GetTeam(160, year);
      var allStarTeams = alAllStars.Teams.Concat(nlAllStars.Teams).ToList();
      return await GetResultsForTeams(allStarTeams, year);
    }

    private async Task<TeamsForYearResults> GetResultsForTeams(IEnumerable<TeamEntry> teams, int year)
    {
      var venueIds = teams.Select(t => t.Venue?.Id).Where(id => id is not null).Distinct().Cast<long>();
      var venuesResponse = await _mlbStatsApiClient.GetVenues(venueIds, year);
      var teamAndVenues = teams.Select(t => (t, venuesResponse.Venues.FirstOrDefault(v => v.Id == t.Venue?.Id)));
      return new TeamsForYearResults(teamAndVenues);
    }
  }
}
