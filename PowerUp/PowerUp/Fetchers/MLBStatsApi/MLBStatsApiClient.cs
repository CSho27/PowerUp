using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public interface IMLBStatsApiClient
  {
    Task<TeamListResult> GetTeams(int year);
    Task<TeamListResult> GetTeam(int teamId, int? year = null);
    Task<RosterResult> GetTeamRoster(long mlbTeamId, int year);
    Task<VenueResult> GetVenues(IEnumerable<long> venueIds, int? year = null);
    Task<Person> GetPlayerInfo(long mlbPlayerId);
    Task<Person> GetPlayerStatistics(long mlbPlayerId, int year);
  }

  public class MLBStatsApiClient : IMLBStatsApiClient
  {
    private const string BASE_URL = "https://statsapi.mlb.com/api/v1";
    private readonly ApiClient _client = new ApiClient();

    public async Task<RosterResult> GetTeamRoster(long mlbTeamId, int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "teams", mlbTeamId.ToString(), "roster" },
        // What are the types of rosterType
        // What is the 'date' parameter for?
        new { rosterType = "active", season = year.ToString() }
      );
      return await _client.Get<RosterResult>(url);
    }

    public async Task<TeamListResult> GetTeams(int year)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "teams" },
        new { sportId = 1, season = year.ToString(), leagueIds = "103,104,106" }
      );
      return await _client.Get<TeamListResult>(url);
    }

    public async Task<TeamListResult> GetTeam(int teamId, int? year = null)
    {
      var parameters = new Dictionary<string, string>();
      if (year.HasValue) parameters.Add("season", year.Value.ToString());
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "teams", teamId.ToString() },
        parameters
      );
      return await _client.Get<TeamListResult>(url);
    }

    public Task<Person> GetPlayerInfo(long mlbPlayerId)
    {
      return GetPlayerData(mlbPlayerId, null);
    }

    public Task<Person> GetPlayerStatistics(long mlbPlayerId, int year)
    {
      return GetPlayerData(mlbPlayerId, year);
    }

    private async Task<Person> GetPlayerData(long mlbPlayerId, int? year)
    {
      var group = "[hitting,pitching,fielding]";
      var type = "season";
      var sportId = 1;
      var hydration = year.HasValue
        ? $"stats(group={group},type={type},sportId={sportId},season={year}),currentTeam"
        : "currentTeam";

      var url = UrlBuilder.Build(
        new[] { BASE_URL, "people" },
        new { personIds = mlbPlayerId, hydrate = hydration }
      );

      var response = await _client.Get<PeopleResults>(url);
      var person = response.People.SingleOrDefault();
      if (person is null)
        throw new InvalidOperationException("No player info found for this id");

      return person;
    }

    public async Task<VenueResult> GetVenues(IEnumerable<long> venueIds, int? year = null)
    {
      var parameters = new Dictionary<string, string>
      {
        { "venueIds", venueIds.StringJoin(",") },
        { "hydrate", "location" }
      };
      if (year.HasValue) parameters.Add("season", year.Value.ToString());
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "venues" },
        parameters
      );

      return await _client.Get<VenueResult>(url);
    }
  }
}
