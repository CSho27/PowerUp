using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.Algolia
{
  public class AlgoliaSearchRequest
    {
        [JsonPropertyName("indexName")]
        public string IndexName { get; init; } = "";


        [JsonPropertyName("params")]
        public string Parameters { get; init; } = "";
    }
}
