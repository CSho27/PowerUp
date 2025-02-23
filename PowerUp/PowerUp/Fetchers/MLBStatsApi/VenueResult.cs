using PowerUp.Fetchers.MLBLookupService;
using System.Text.Json.Serialization;

namespace PowerUp.Fetchers.MLBStatsApi
{
  public class VenueResult
  {
    [JsonPropertyName("copyright")]
    public string Copyright { get; set; } = "";

    [JsonPropertyName("venues")]
    public VenueEntry[] Venues { get; set; } = [];
  }

  public class VenueEntry
  {
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("link")]
    public string Link { get; set; } = "";

    [JsonPropertyName("location")]
    public Location? Location { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("season")]
    public string? RawSeason { get; set; }

    public long? Season => RawSeason?.TryParseInt();
  }

  public partial class Location
  {
    [JsonPropertyName("address1")]
    public string Address1 { get; set; } = "";

    [JsonPropertyName("city")]
    public string City { get; set; } = "";

    [JsonPropertyName("state")]
    public string State { get; set; } = "";

    [JsonPropertyName("stateAbbrev")]
    public string StateAbbrev { get; set; } = "";

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = "";

    [JsonPropertyName("defaultCoordinates")]
    public Coordinates? DefaultCoordinates { get; set; }

    [JsonPropertyName("azimuthAngle")]
    public double? AzimuthAngle { get; set; }

    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; } = "";

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = "";
  }

  public partial class Coordinates
  {
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
  }
}
