using System;
using System.Text.Json;
using System.Threading.Tasks;

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

  public partial class MLBLookupServiceClient : IMLBLookupServiceClient
  {
    private const string BASE_URL = "http://lookup-service-prod.mlb.com/json";
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

    public async Task<PlayerInfoResult> GetPlayerInfo(long lsPlayerId)
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
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "named.sport_fielding_tm.bam" },
        new { league_list_id = "\'mlb\'", game_type = "\'R\'", player_id = $"\'{lsPlayerId}\'", season = $"\'{year}\'" }
      );

      var response = await _apiClient.Get<LSFieldingStatsResponse>(url);
      var results = response!.sport_fielding_tm!.queryResults!;
      var totalResults = int.Parse(results.totalSize!);
      var deserializedResults = Deserialization.SingleArrayOrNullToEnumerable<LSFieldingStatsResult>(results.row)!;
      return new FieldingStatsResults(totalResults, deserializedResults);
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
