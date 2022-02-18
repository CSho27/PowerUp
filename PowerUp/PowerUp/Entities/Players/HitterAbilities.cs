namespace PowerUp.Entities.Players
{
  public class HitterAbilities
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }

    public HotZoneGrid HotZones { get; set; } = new HotZoneGrid();
  }

  public class HotZoneGrid
  {
    public HotZonePreference UpAndIn { get; set; }
    public HotZonePreference Up { get; set; }
    public HotZonePreference UpAndAway { get; set; }
    public HotZonePreference MiddleIn { get; set; }
    public HotZonePreference Middle { get; set; }
    public HotZonePreference MiddleAway { get; set; }
    public HotZonePreference DownAndIn { get; set; }
    public HotZonePreference Down { get; set; }
    public HotZonePreference DownAndAway { get; set; }
  }

  public enum HotZonePreference
  {
    Neutral,
    Hot,
    Cold = 3
  }
}
