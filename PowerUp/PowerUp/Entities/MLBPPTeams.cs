using System;

namespace PowerUp.Entities
{
  public enum MLBPPTeam
  {
    [Abbrev("Bal"), TeamLocation("Baltimore")]
    Orioles = 1,
    [Abbrev("Bos"), TeamLocation("Boston"), DisplayName("Red Sox")]
    RedSox,
    [Abbrev("NYY"), TeamLocation("New York")]
    Yankees,
    [Abbrev("TB"), TeamLocation("Tampa Bay")]
    Rays,
    [Abbrev("Tor"), TeamLocation("Toronto"), DisplayName("Blue Jays")]
    BlueJays,
    [Abbrev("CHW"), TeamLocation("Chicago"), DisplayName("White Sox")]
    WhiteSox,
    [Abbrev("Cle"), TeamLocation("Cleveland")]
    Indians,
    [Abbrev("Det"), TeamLocation("Detroit")]
    Tigers,
    [Abbrev("KC"), TeamLocation("Kansas City")]
    Royals,
    [Abbrev("Min"), TeamLocation("Minnesota")]
    Twins,
    [Abbrev("LAA"), TeamLocation("Anaheim")]
    Angels,
    [Abbrev("Oak"), TeamLocation("Oakland")]
    Athletics,
    [Abbrev("Sea"), TeamLocation("Seattle")]
    Mariners,
    [Abbrev("TEX"), TeamLocation("Texas")]
    Rangers,
    [Abbrev("Atl"), TeamLocation("Atlanta")]
    Braves,
    [Abbrev("Fl"), TeamLocation("Florida")]
    Marlins,
    [Abbrev("NYM"), TeamLocation("New York")]
    Mets,
    [Abbrev("Phi"), TeamLocation("Philadelphia")]
    Phillies,
    [Abbrev("WSH"), TeamLocation("Washington")]
    Nationals,
    [Abbrev("CHC"), TeamLocation("Chicago")]
    Cubs,
    [Abbrev("Cin"), TeamLocation("Cincinnati")]
    Reds,
    [Abbrev("Hou"), TeamLocation("Houston")]
    Astros,
    [Abbrev("Mil"), TeamLocation("Milwaukee")]
    Brewers,
    [Abbrev("Pit"), TeamLocation("Pittsburgh")]
    Pirates,
    [Abbrev("STL"), TeamLocation("St. Louis")]
    Cardinals,
    [Abbrev("Ari"), TeamLocation("Arizona")]
    DBacks,
    [Abbrev("Col"), TeamLocation("Colorado")]
    Rockies,
    [Abbrev("LAD"), TeamLocation("Los Angeles")]
    Dodgers,
    [Abbrev("SD"), TeamLocation("San Diego")]
    Padres,
    [Abbrev("SF"), TeamLocation("San Francisco")]
    Giants,
    [Abbrev("AL"), DisplayName("American League All-Stars")]
    AmericanLeagueAllStars,
    [Abbrev("NL"), DisplayName("National League All-Stars")]
    NationalLeagueAllStars
  }

  public static class TeamLocationExtensions
  {
    public static string? GetTeamLocation(this MLBPPTeam value) 
      => value.GetEnumAttribute<TeamLocationAttribute>()?.TeamLocation;
    public static string? GetFullDisplayName(this MLBPPTeam value)
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
}
