using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class RosterResult
  {
    [JsonPropertyName("copyright")]
    public string Copyright { get; set; } = "";

    [JsonPropertyName("roster")]
    public RosterEntry[] Roster { get; set; } = [];

    [JsonPropertyName("teamId")]
    public long TeamId { get; set; }

    [JsonPropertyName("rosterType")]
    public string RosterType { get; set; } = "";
  }

  public class RosterEntry
  {
    [JsonPropertyName("person")]
    public PlayerInfo? Person { get; set; }

    [JsonPropertyName("jerseyNumber")]
    public string? JerseyNumber { get; set; }

    [JsonPropertyName("position")]
    public PositionInfo? Position { get; set; }

    [JsonPropertyName("status")]
    public StatusInfo? Status { get; set; }
  }
}
