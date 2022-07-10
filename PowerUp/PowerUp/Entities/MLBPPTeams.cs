using System;

namespace PowerUp.Entities
{
  public enum MLBPPTeam
  {
    /// <summary>
    /// Note: The National League All-Stars have the Id 0, but are last in the list of team definitions
    /// </summary>
    [Abbrev("NL"), DisplayName("National League All-Stars"), Division(MLBPPDivision.AllStars), LSTeamId(160)]
    NationalLeagueAllStars,
    [Abbrev("Bal"), TeamLocation("Baltimore"), Division(MLBPPDivision.ALEast), LSTeamId(110)]
    Orioles = 1,
    [Abbrev("Bos"), TeamLocation("Boston"), DisplayName("Red Sox"), Division(MLBPPDivision.ALEast), LSTeamId(111)]
    RedSox,
    [Abbrev("NYY"), TeamLocation("New York"), Division(MLBPPDivision.ALEast), LSTeamId(147)]
    Yankees,
    [Abbrev("TB"), TeamLocation("Tampa Bay"), Division(MLBPPDivision.ALEast), LSTeamId(139)]
    Rays,
    [Abbrev("Tor"), TeamLocation("Toronto"), DisplayName("Blue Jays"), Division(MLBPPDivision.ALEast), LSTeamId(141)]
    BlueJays,
    [Abbrev("CHW"), TeamLocation("Chicago"), DisplayName("White Sox"), Division(MLBPPDivision.ALCentral), LSTeamId(145)]
    WhiteSox,
    [Abbrev("Cle"), TeamLocation("Cleveland"), Division(MLBPPDivision.ALCentral), LSTeamId(114)]
    Indians,
    [Abbrev("Det"), TeamLocation("Detroit"), Division(MLBPPDivision.ALCentral), LSTeamId(116)]
    Tigers,
    [Abbrev("KC"), TeamLocation("Kansas City"), Division(MLBPPDivision.ALCentral), LSTeamId(118)]
    Royals,
    [Abbrev("Min"), TeamLocation("Minnesota"), Division(MLBPPDivision.ALCentral), LSTeamId(142)]
    Twins,
    [Abbrev("LAA"), TeamLocation("Anaheim"), Division(MLBPPDivision.ALWest), LSTeamId(108)]
    Angels,
    [Abbrev("Oak"), TeamLocation("Oakland"), Division(MLBPPDivision.ALWest), LSTeamId(133)]
    Athletics,
    [Abbrev("Sea"), TeamLocation("Seattle"), Division(MLBPPDivision.ALWest), LSTeamId(136)]
    Mariners,
    [Abbrev("TEX"), TeamLocation("Texas"), Division(MLBPPDivision.ALWest), LSTeamId(140)]
    Rangers,
    [Abbrev("Atl"), TeamLocation("Atlanta"), Division(MLBPPDivision.NLEast), LSTeamId(144)]
    Braves,
    [Abbrev("Fl"), TeamLocation("Florida"), Division(MLBPPDivision.NLEast), LSTeamId(146)]
    Marlins,
    [Abbrev("NYM"), TeamLocation("New York"), Division(MLBPPDivision.NLEast), LSTeamId(121)]
    Mets,
    [Abbrev("Phi"), TeamLocation("Philadelphia"), Division(MLBPPDivision.NLEast), LSTeamId(143)]
    Phillies,
    [Abbrev("WSH"), TeamLocation("Washington"), Division(MLBPPDivision.NLEast), LSTeamId(120)]
    Nationals,
    [Abbrev("CHC"), TeamLocation("Chicago"), Division(MLBPPDivision.NLCentral), LSTeamId(112)]
    Cubs,
    [Abbrev("Cin"), TeamLocation("Cincinnati"), Division(MLBPPDivision.NLCentral), LSTeamId(113)]
    Reds,
    [Abbrev("Hou"), TeamLocation("Houston"), Division(MLBPPDivision.NLCentral), LSTeamId(117)]
    Astros,
    [Abbrev("Mil"), TeamLocation("Milwaukee"), Division(MLBPPDivision.NLCentral), LSTeamId(158)]
    Brewers,
    [Abbrev("Pit"), TeamLocation("Pittsburgh"), Division(MLBPPDivision.NLCentral), LSTeamId(134)]
    Pirates,
    [Abbrev("STL"), TeamLocation("St. Louis"), Division(MLBPPDivision.NLCentral), LSTeamId(138)]
    Cardinals,
    [Abbrev("Ari"), TeamLocation("Arizona"), Division(MLBPPDivision.NLWest), LSTeamId(109)]
    DBacks,
    [Abbrev("Col"), TeamLocation("Colorado"), Division(MLBPPDivision.NLWest), LSTeamId(115)]
    Rockies,
    [Abbrev("LAD"), TeamLocation("Los Angeles"), Division(MLBPPDivision.NLWest), LSTeamId(119)]
    Dodgers,
    [Abbrev("SD"), TeamLocation("San Diego"), Division(MLBPPDivision.NLWest), LSTeamId(135)]
    Padres,
    [Abbrev("SF"), TeamLocation("San Francisco"), Division(MLBPPDivision.NLWest), LSTeamId(137)]
    Giants,
    [Abbrev("AL"), DisplayName("American League All-Stars"), Division(MLBPPDivision.AllStars), LSTeamId(159)]
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
    public static long GetLSTeamId(this MLBPPTeam value)
      => value.GetEnumAttribute<LSTeamIdAttribute>()!.LSTeamId;

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

  public class LSTeamIdAttribute : Attribute
  {
    public long LSTeamId { get; set; }

    public LSTeamIdAttribute(long lsTeamId)
    {
      LSTeamId = lsTeamId;
    }
  }
}
