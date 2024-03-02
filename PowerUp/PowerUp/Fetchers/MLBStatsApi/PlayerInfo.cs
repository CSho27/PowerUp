using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class PlayerInfo
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = "";
  }
}
