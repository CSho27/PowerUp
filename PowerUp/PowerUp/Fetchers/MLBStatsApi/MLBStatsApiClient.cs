using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public interface IMLBStatsApiClient
  {
    Task<RosterResult> GetTeamRoster(long mlbTeamId, int year);
    public Task<Person> GetPlayerInfo(long mlbPlayerId);
    public Task<Person> GetPlayerStatistics(long mlbPlayerId, int year);
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
  }
}
