using PowerUp.Entities.Players;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class HotZoneGridDto
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference UpAndIn { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference Up { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference UpAndAway { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference MiddleIn { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference Middle { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference MiddleAway { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference DownAndIn { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference Down { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HotZonePreference DownAndAway { get; set; }

    public HotZoneGridDto() { }

    public HotZoneGridDto(HotZoneGrid grid)
    {
      UpAndIn = grid.UpAndIn;
      Up = grid.Up;
      UpAndAway = grid.UpAndAway;
      MiddleIn = grid.MiddleIn;
      Middle = grid.Middle;
      MiddleAway = grid.MiddleAway;
      DownAndIn = grid.DownAndIn;
      Down = grid.Down;
      DownAndAway = grid.DownAndAway;
    }
  }
}
