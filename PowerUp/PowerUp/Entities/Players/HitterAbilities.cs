namespace PowerUp.Entities.Players
{
  public class HitterAbilities
  {
    public int Trajectory { get; set; } = 1;
    public int Contact { get; set; } = 1;
    public int Power { get; set; } = 16;
    public int RunSpeed { get; set; } = 2;
    public int ArmStrength { get; set; } = 7;
    public int Fielding { get; set; } = 6;
    public int ErrorResistance { get; set; } = 5;

    public HotZoneGrid HotZones { get; set; } = new HotZoneGrid();

    public double GetHitterRating()
      => RatingCalculator.CalculateHitterRating(new HitterRatingParameters
      {
        Contact = Contact,
        Power = Power,
        RunSpeed = RunSpeed,
        ArmStrength = ArmStrength,
        Fielding = Fielding,
        ErrorResistance = ErrorResistance
      });
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
