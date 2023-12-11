using PowerUp.Fetchers.MLBLookupService;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public interface IMLBStatsApiClient
  {
    Task<FieldingStatsResults> GetFieldingStats(long lsPlayerId, int year);
  }

  public class MLBStatsApiClient : IMLBStatsApiClient
  {
    private const string BASE_URL = "https://statsapi.mlb.com/api/v1";
    private readonly ApiClient _client = new ApiClient();

    public async Task<FieldingStatsResults> GetFieldingStats(long lsPlayerId, int year)
    {
      var group = "[hitting,pitching,fielding]";
      var type = "season";
      var sportId = 1;

      var url = UrlBuilder.Build(
        new[] { BASE_URL, "people" },
        new { personIds = lsPlayerId, hydrate = $"stats(group={group},type={type},sportId={sportId},season={year}),currentTeam" }
      );

      var response = await _client.Get<PeopleResults>(url);
      var fieldingStats = response.People.Single().Stats.Single(s => s.Group?.DisplayName == "fielding"); 
      return new FieldingStatsResults(fieldingStats);
    }
  }
}
