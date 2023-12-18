using System;
using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.Algolia
{
  public class PlayerResult
  {
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("lastUpdatedDate")]
    public long LastUpdatedDate { get; set; }

    [JsonPropertyName("contentDate")]
    public long ContentDate { get; set; }

    [JsonPropertyName("teamCode")]
    public object? TeamCode { get; set; }

    [JsonPropertyName("teamFileCode")]
    public object[] TeamFileCode { get; set; } = [];

    [JsonPropertyName("teamName")]
    public object[] TeamName { get; set; } = [];

    [JsonPropertyName("playerName")]
    public string[] PlayerName { get; set; } = [];

    [JsonPropertyName("homeTeamCode")]
    public object? HomeTeamCode { get; set; }

    [JsonPropertyName("awayTeamCode")]
    public object? AwayTeamCode { get; set; }

    [JsonPropertyName("gameDateTime")]
    public object? GameDateTime { get; set; }

    [JsonPropertyName("gamePk")]
    public object? GamePk { get; set; }

    [JsonPropertyName("gameTitle")]
    public object? GameTitle { get; set; }

    [JsonPropertyName("taxonomy")]
    public object[] Taxonomy { get; set; } = [];

    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; }

    [JsonPropertyName("entityId")]
    public Guid EntityId { get; set; }

    [JsonPropertyName("culture")]
    public string? Culture { get; set; }

    [JsonPropertyName("thumbnailUrl")]
    public Uri? ThumbnailUrl { get; set; }

    [JsonPropertyName("templateUrl")]
    public string? TemplateUrl { get; set; }

    [JsonPropertyName("playerId")]
    public long PlayerId { get; set; }

    [JsonPropertyName("biography")]
    public object? Biography { get; set; }

    [JsonPropertyName("playerType")]
    public string? PlayerType { get; set; }

    [JsonPropertyName("highlight")]
    public object? Highlight { get; set; }

    [JsonPropertyName("position")]
    public string? Position { get; set; }

    [JsonPropertyName("drafted")]
    public string? Drafted { get; set; }

    [JsonPropertyName("prospectBio")]
    public ProspectBio[] ProspectBio { get; set; } = [];

    [JsonPropertyName("objectID")]
    public Guid ObjectId { get; set; }

    [JsonPropertyName("_highlightResult")]
    public HighlightResult? HighlightResult { get; set; }
  }
}
