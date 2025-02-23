using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class Sport
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";

    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; } = "";
  }
}
