using System;

namespace PowerUp.Entities
{
  public enum MLBPPTeam
  {
    /// <summary>
    /// Note: The National League All-Stars have the Id 0, but are last in the list of team definitions
    /// </summary>
    [Abbrev("NL"), DisplayName("National League All-Stars"), Division(MLBPPDivision.AllStars)]
    NationalLeagueAllStars,
    [Abbrev("Bal"), TeamLocation("Baltimore"), Division(MLBPPDivision.ALEast)]
    Orioles = 1,
    [Abbrev("Bos"), TeamLocation("Boston"), DisplayName("Red Sox"), Division(MLBPPDivision.ALEast)]
    RedSox,
    [Abbrev("NYY"), TeamLocation("New York"), Division(MLBPPDivision.ALEast)]
    Yankees,
    [Abbrev("TB"), TeamLocation("Tampa Bay"), Division(MLBPPDivision.ALEast)]
    Rays,
    [Abbrev("Tor"), TeamLocation("Toronto"), DisplayName("Blue Jays"), Division(MLBPPDivision.ALEast)]
    BlueJays,
    [Abbrev("CHW"), TeamLocation("Chicago"), DisplayName("White Sox"), Division(MLBPPDivision.ALCentral)]
    WhiteSox,
    [Abbrev("Cle"), TeamLocation("Cleveland"), Division(MLBPPDivision.ALCentral)]
    Indians,
    [Abbrev("Det"), TeamLocation("Detroit"), Division(MLBPPDivision.ALCentral)]
    Tigers,
    [Abbrev("KC"), TeamLocation("Kansas City"), Division(MLBPPDivision.ALCentral)]
    Royals,
    [Abbrev("Min"), TeamLocation("Minnesota"), Division(MLBPPDivision.ALCentral)]
    Twins,
    [Abbrev("LAA"), TeamLocation("Anaheim"), Division(MLBPPDivision.ALWest)]
    Angels,
    [Abbrev("Oak"), TeamLocation("Oakland"), Division(MLBPPDivision.ALWest)]
    Athletics,
    [Abbrev("Sea"), TeamLocation("Seattle"), Division(MLBPPDivision.ALWest)]
    Mariners,
    [Abbrev("TEX"), TeamLocation("Texas"), Division(MLBPPDivision.ALWest)]
    Rangers,
    [Abbrev("Atl"), TeamLocation("Atlanta"), Division(MLBPPDivision.NLEast)]
    Braves,
    [Abbrev("Fl"), TeamLocation("Florida"), Division(MLBPPDivision.NLEast)]
    Marlins,
    [Abbrev("NYM"), TeamLocation("New York"), Division(MLBPPDivision.NLEast)]
    Mets,
    [Abbrev("Phi"), TeamLocation("Philadelphia"), Division(MLBPPDivision.NLEast)]
    Phillies,
    [Abbrev("WSH"), TeamLocation("Washington"), Division(MLBPPDivision.NLEast)]
    Nationals,
    [Abbrev("CHC"), TeamLocation("Chicago"), Division(MLBPPDivision.NLCentral)]
    Cubs,
    [Abbrev("Cin"), TeamLocation("Cincinnati"), Division(MLBPPDivision.NLCentral)]
    Reds,
    [Abbrev("Hou"), TeamLocation("Houston"), Division(MLBPPDivision.NLCentral)]
    Astros,
    [Abbrev("Mil"), TeamLocation("Milwaukee"), Division(MLBPPDivision.NLCentral)]
    Brewers,
    [Abbrev("Pit"), TeamLocation("Pittsburgh"), Division(MLBPPDivision.NLCentral)]
    Pirates,
    [Abbrev("STL"), TeamLocation("St. Louis"), Division(MLBPPDivision.NLCentral)]
    Cardinals,
    [Abbrev("Ari"), TeamLocation("Arizona"), Division(MLBPPDivision.NLWest)]
    DBacks,
    [Abbrev("Col"), TeamLocation("Colorado"), Division(MLBPPDivision.NLWest)]
    Rockies,
    [Abbrev("LAD"), TeamLocation("Los Angeles"), Division(MLBPPDivision.NLWest)]
    Dodgers,
    [Abbrev("SD"), TeamLocation("San Diego"), Division(MLBPPDivision.NLWest)]
    Padres,
    [Abbrev("SF"), TeamLocation("San Francisco"), Division(MLBPPDivision.NLWest)]
    Giants,
    [Abbrev("AL"), DisplayName("American League All-Stars"), Division(MLBPPDivision.AllStars)]
    AmericanLeagueAllStars,
  }

  public enum MLBPPDivision
  {
    [DisplayName("AL East")]
    ALEast,
    [DisplayName("AL Central")]
    ALCentral,
    [DisplayName("AL West")]
    ALWest,
    [DisplayName("NL East")]
    NLEast,
    [DisplayName("NL Central")]
    NLCentral,
    [DisplayName("NL West")]
    NLWest,
    [DisplayName("All-Stars")]
    AllStars
  }

  public static class MLBPPTeamExtensions
  {
    public static string? GetTeamLocation(this MLBPPTeam value) 
      => value.GetEnumAttribute<TeamLocationAttribute>()?.TeamLocation;
    public static MLBPPDivision GetDivision(this MLBPPTeam value)
      => value.GetEnumAttribute<DivisionAttribute>()!.Division;
    public static string GetFullDisplayName(this MLBPPTeam value)
    {
      var teamLocation = value.GetTeamLocation();
      return teamLocation != null
        ? $"{teamLocation} {value.GetDisplayName()}"
        : value.GetDisplayName();
    }

  }

  public class TeamLocationAttribute : Attribute
  {
    public string TeamLocation { get; set; }

    public TeamLocationAttribute(string teamLocation)
    {
      TeamLocation = teamLocation;
    }
  }

  public class DivisionAttribute : Attribute
  {
    public MLBPPDivision Division { get; set; }

    public DivisionAttribute(MLBPPDivision division)
    {
      Division = division;
    }
  }
}
