using System;
using System.Text.Json;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSTeamsResponse
  {
    public LSTeamsResult? team_all_season { get; set; }
  }

  public class LSTeamsResult
  {
    public string? copyRight { get; set; }
    public LSTeamsQueryResults? queryResults { get; set; }
  }

  public class LSTeamsQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSTeamResult
  {
    public string? team_id { get; set; }
    public string? season { get; set; }

    public string? state { get; set; }
    public string? city { get; set; }

    public string? name { get; set; }
    public string? name_short { get; set; }
    public string? name_display_short { get; set; }
    public string? name_display_long { get; set; }
    public string? name_display_brief { get; set; }
    public string? name_display_full { get; set; }
    public string? name_abbrev { get; set; }

    public string? venue_id { get; set; }
    public string? venue_short { get; set; }
    public string? venue_name { get; set; }
    public string? league { get; set; }
    public string? league_abbrev { get; set; }
    public string? league_id { get; set; }
    public string? league_full { get; set; }
    public string? mlb_org_id { get; set; }
    public string? mlb_org { get; set; }
    public string? mlb_org_abbrev { get; set; }
    public string? mlb_org_short { get; set; }
    public string? mlb_org_brief { get; set; }

    public string? first_year_of_play { get; set; }
    public string? last_year_of_play { get; set; }

    public string? division_id { get; set; }
    public string? division { get; set; }
    public string? division_abbrev { get; set; }
    public string? division_full { get; set; }
  }
}
