using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using PowerUp.Fetchers.Algolia;
using PowerUp.Fetchers.MLBStatsApi;
using PowerUp.Fetchers.Statcast;

namespace PowerUp.Fetchers.MLBLookupService
{
  public interface IMLBLookupServiceClient
  {
    Task<PlayerSearchResults> SearchPlayer(string name);
    Task<PlayerInfoResult> GetPlayerInfo(long lsPlayerId);
    Task<HittingStatsResults> GetHittingStats(long lsPlayerId, int year);
    Task<FieldingStatsResults> GetFieldingStats(long lsPlayerId, int year);
    Task<PitchingStatsResults> GetPitchingStats(long lsPlayerId, int year);
    Task<TeamsForYearResults> GetTeamsForYear(int year);
    Task<TeamsForYearResults> GetAllStarTeamsForYear(int year);
    Task<TeamRosterResult> GetTeamRosterForYear(long lsTeamId, int year);
  }

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
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.search_player_all.bam" },
        new { sport_code = "\'mlb\'", name_part = $"\'{name}%\'" }
      );

      var searchResponse = await _algoliaClient.SearchPlayer(name);
      var totalResults = searchResponse.NbHits;

      var results = await Task.WhenAll(searchResponse.Hits.Select(async r => await GetPlayerInfo(r.PlayerId)));
      return new PlayerSearchResults(totalResults, results);
    }

    public async Task<PlayerInfoResult> GetPlayerInfo(long lsPlayerId)
    {
      var result = await _mlbStatsApiClient.GetPlayerInfo(lsPlayerId);
      if (result is null)
        throw new InvalidOperationException("No player info found for this id");

      return result;
    }

    public async Task<HittingStatsResults> GetHittingStats(long lsPlayerId, int year)
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

    public async Task<FieldingStatsResults> GetFieldingStats(long lsPlayerId, int year)
    {
      return await _mlbStatsApiClient.GetFieldingStats(lsPlayerId, year);
    }

    public async Task<PitchingStatsResults> GetPitchingStats(long lsPlayerId, int year)
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

    public async Task<TeamsForYearResults> GetTeamsForYear(int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.team_all_season.bam" },
        new { sport_code = "\'mlb\'", all_star_sw = "\'N\'", season = $"\'{year}\'" }
      );

      var response = await _apiClient.Get<LSTeamsResponse>(url);
      var results = response!.team_all_season!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSTeamResult>(results.row)!;
      return new TeamsForYearResults(totalResults, deserializedResults);
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

    public async Task<TeamRosterResult> GetTeamRosterForYear(long lsTeamId, int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.roster_team_alltime.bam" },
        new { start_season = $"\'{year}\'", end_season = $"\'{year}\'", team_id = $"\'{lsTeamId}\'" }
      );

      var response = await _apiClient.Get<LSTeamRosterResponse>(url);
      var results = response!.roster_team_alltime!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSTeamRosterPlayerResult>(results.row)!;
      return new TeamRosterResult(totalResults, deserializedResults);
    }
  }
}
