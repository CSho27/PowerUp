using System;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public interface IMLBStatsApiClient
  {
    public Task<Person> GetPlayerInfo(long lsPlayerId);
    public Task<Person> GetPlayerStatistics(long lsPlayerId, int year);
  }

  public class MLBStatsApiClient : IMLBStatsApiClient
  {
    private const string BASE_URL = "https://statsapi.mlb.com/api/v1";
    private readonly ApiClient _client = new ApiClient();

    public Task<Person> GetPlayerInfo(long lsPlayerId)
    {
      return GetPlayerData(lsPlayerId, null);
    }

    public Task<Person> GetPlayerStatistics(long lsPlayerId, int year)
    {
      return GetPlayerData(lsPlayerId, year);
    }

    private async Task<Person> GetPlayerData(long lsPlayerId, int? year)
    {
      var group = "[hitting,pitching,fielding]";
      var type = "season";
      var sportId = 1;
      var hydration = year.HasValue
        ? $"stats(group={group},type={type},sportId={sportId},season={year}),currentTeam"
        : "currentTeam";

      var url = UrlBuilder.Build(
        new[] { BASE_URL, "people" },
        new { personIds = lsPlayerId, hydrate = hydration }
      );

      var response = await _client.Get<PeopleResults>(url);
      var person = response.People.SingleOrDefault();
      if (person is null)
        throw new InvalidOperationException("No player info found for this id");

      return person;
    }
  }
}
