using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.Statcast
{
  public interface IStatcastClient
  {
    Task<IEnumerable<StatcastPlayerSearchResult>> SearchPlayer(string name);
  }

  public class StatcastClient : IStatcastClient
  {
    private const string URL = "https://yvo49oxzy7-dsn.algolia.net/1/indexes/*/queries?x-algolia-agent=Algolia%20for%20JavaScript%20(4.8.2)%3B%20Browser%20(lite)&x-algolia-api-key=2305f7af47eda36d30e1fa05f9986e56&x-algolia-application-id=YVO49OXZY7";
    private const string BASE_URL = "https://baseballsavant.mlb.com";
    private readonly ApiClient _client = new ApiClient();

    public async Task<IEnumerable<StatcastPlayerSearchResult>> SearchPlayer(string name)
    {
      var url = UrlBuilder.Build(
        new[] { BASE_URL, "player", "search-all" },
        new { search = name }
      );

      var response = await _client.Get<IEnumerable<StatcastPlayerSearchResult>>(url);
      return response;
    }
  }
}
