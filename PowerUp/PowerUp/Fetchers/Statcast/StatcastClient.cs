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
