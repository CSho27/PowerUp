using System;
using System.Text.Json;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSFieldingStatsResponse
  {
    public LSFieldingStatsTm? sport_fielding_tm { get; set; }
  }

  public class LSFieldingStatsTm
  {
    public string? copyRight { get; set; }
    public LSFieldingStatsQueryResults? queryResults { get; set; }
  }

  public class LSFieldingStatsQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSFieldingStatsResult
  {
    public string? player_id { get; set; }
    public string? season { get; set; }
    public string? team_id { get; set; }
    public string? team_abbrev { get; set; }
    public string? team_short { get; set; }
    public string? team_full { get; set; }
    public string? team_seq { get; set; }
    public string? league { get; set; }
    public string? league_short { get; set; }
    public string? league_full { get; set; }

    public string? position { get; set; }
    public string? g { get; set; }
    public string? gs { get; set; }
    public string? inn { get; set; }
    public string? tc { get; set; }
    public string? e { get; set; }
    public string? a { get; set; }
    public string? po { get; set; }
    public string? dp { get; set; }
    public string? rf { get; set; }
    public string? fpct { get; set; }
    public string? cs { get; set; }
    public string? sb { get; set; }
    public string? pb { get; set; }
    public string? cwp { get; set; }
  }
}
