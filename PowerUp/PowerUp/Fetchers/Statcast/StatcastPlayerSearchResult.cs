using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.Statcast
{
  public class StatcastPlayerSearchResult
  {
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("is_player")]
    public int IsPlayer { get; set; }

    [JsonPropertyName("mlb")]
    public int Mlb { get; set; }

    [JsonPropertyName("league")]
    public string? League { get; set; }

    [JsonPropertyName("first")]
    public string? First { get; set; }

    [JsonPropertyName("is_prospect")]
    public int IsProspect { get; set; }

    [JsonPropertyName("pos")]
    public string? Pos { get; set; }

    [JsonPropertyName("last_year")]
    public string? LastYear { get; set; }

    [JsonPropertyName("name_display_club")]
    public string? NameDisplayClub { get; set; }
  }
}
