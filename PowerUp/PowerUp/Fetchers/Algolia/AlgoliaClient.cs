using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.Algolia
{
  public class AlgoliaClient
    {
        private const string BASE_URL = "https://yvo49oxzy7-dsn.algolia.net/1/indexes/*/queries";
        private const string AGENT = "Algolia for JavaScript (4.8.2); Browser (lite)";
        private const string API_KEY = "2305f7af47eda36d30e1fa05f9986e56";
        private const string APPLICATION_ID = "YVO49OXZY7";

        private readonly ApiClient _client = new ApiClient();

        public async Task<IEnumerable<PlayerResult>> SearchPlayer(string name)
        {
            var url = UrlBuilder.Build(
              BASE_URL,
              new Dictionary<string, string>
              {
                { "x-algolia-agent",  AGENT },
                { "x-algolia-api-key", API_KEY },
                { "x-algolia-application-id", APPLICATION_ID },
              }
            );

            var request = new
            {
                requests = new[]
                {
                  new AlgoliaSearchRequest
                  {
                    IndexName = "mlb-players",
                    Parameters = $"filters=culture%3Aen-us&query={name}"
                  }
                }
            };

            var response = await _client.Post<AlgoliaSearchResults<PlayerResult>>(url, request);
            return response.Results.Single().Hits;
        }
    }
}
