using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class Laterality
  {
    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";
  }
}
