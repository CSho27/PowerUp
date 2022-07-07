using System;
using System.Text.Json;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSTeamRosterResponse
  {
    public LSTeamRosterResult? roster_team_alltime { get; set; }
  }

  public class LSTeamRosterResult
  {
    public string? copyRight { get; set; }
    public LSTeamRosterQueryResults? queryResults { get; set; }
  }

  public class LSTeamRosterQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSTeamRosterPlayerResult
  {
    public string? player_id { get; set; }
    public string? team_id { get; set; }
    public string? jersey_number { get; set; }
    public string? name_last_first { get; set; }
    public string? status_short { get; set; }
    public string? primary_position_cd { get; set; }
    public string? primary_position { get; set; }
    public string? bats { get; set; }
    public string? throws { get; set; }
  }
}
