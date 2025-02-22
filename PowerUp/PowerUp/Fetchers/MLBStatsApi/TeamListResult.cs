using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class TeamListResult
  {
    [JsonPropertyName("copyright")]
    public string Copyright { get; set; } = "";

    [JsonPropertyName("teams")]
    public TeamEntry[] Teams { get; set; } = [];
  }

  public class TeamEntry
  {
    [JsonPropertyName("springLeague")]
    public EntityInfo? SpringLeague { get; set; }

    [JsonPropertyName("allStarStatus")]
    public string? AllStarStatus { get; set; }

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("season")]
    public long Season { get; set; }

    [JsonPropertyName("venue")]
    public EntityInfo? Venue { get; set; }

    [JsonPropertyName("springVenue")]
    public SpringVenue? SpringVenue { get; set; }

    [JsonPropertyName("teamCode")]
    public string? TeamCode { get; set; }

    [JsonPropertyName("fileCode")]
    public string? FileCode { get; set; }

    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    [JsonPropertyName("teamName")]
    public string TeamName { get; set; } = "";

    [JsonPropertyName("locationName")]
    public string LocationName { get; set; } = "";

    [JsonPropertyName("firstYearOfPlay")]
    public string? RawFirstYearOfPlay { get; set; }

    public long? FirstYearOfPlay => long.TryParse(RawFirstYearOfPlay, out var firstYear)
      ? firstYear
      : null;

    [JsonPropertyName("league")]
    public EntityInfo? League { get; set; }

    [JsonPropertyName("division")]
    public EntityInfo? Division { get; set; }

    [JsonPropertyName("sport")]
    public EntityInfo? Sport { get; set; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }

    [JsonPropertyName("franchiseName")]
    public string? FranchiseName { get; set; }

    [JsonPropertyName("clubName")]
    public string? ClubName { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
  }

  public class EntityInfo
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }
  }

  public class SpringVenue
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }
  }
}
