using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class PositionInfo
  {
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; } = "";
  }
}
