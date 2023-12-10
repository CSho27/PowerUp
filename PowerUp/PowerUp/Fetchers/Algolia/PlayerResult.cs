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
        public long TeamCode { get; set; }

        [JsonPropertyName("teamFileCode")]
        public string[] TeamFileCode { get; set; } = [];

        [JsonPropertyName("teamName")]
        public string[] TeamName { get; set; } = [];

        [JsonPropertyName("playerName")]
        public string[] PlayerName { get; set; } = [];

        [JsonPropertyName("homeTeamCode")]
        public string? HomeTeamCode { get; set; }

        [JsonPropertyName("awayTeamCode")]
        public string? AwayTeamCode { get; set; }

        [JsonPropertyName("gameDateTime")]
        public DateTime? GameDateTime { get; set; }

        [JsonPropertyName("gamePk")]
        public string? GamePk { get; set; }

        [JsonPropertyName("gameTitle")]
        public string? GameTitle { get; set; }

        [JsonPropertyName("taxonomy")]
        public string[] Taxonomy { get; set; } = [];

        [JsonPropertyName("entityId")]
        public Guid EntityId { get; set; }

        [JsonPropertyName("thumbnailUrl")]
        public Uri? ThumbnailUrl { get; set; }

        [JsonPropertyName("templateUrl")]
        public string? TemplateUrl { get; set; }

        [JsonPropertyName("playerId")]
        public long PlayerId { get; set; }

        [JsonPropertyName("biography")]
        public string? Biography { get; set; }

        [JsonPropertyName("highlight")]
        public string? Highlight { get; set; }

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
