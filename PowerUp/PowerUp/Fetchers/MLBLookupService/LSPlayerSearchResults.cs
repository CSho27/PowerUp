using System;
using System.Text.Json;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSPlayerSearchResults
  {
    public LSSearchPlayerAll? search_player_all { get; set; }
  }

  public class LSSearchPlayerAll
  {
    public string? copyRight { get; set; }
    public LSPlayerSearchQueryResults? queryResults { get; set; }
  }

  public class LSPlayerSearchQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSPlayerSearchResult
  {
    public string? player_id { get; set; }
    public string? position_id { get; set; }
    public string? position { get; set; }
    public string? name_first { get; set; }
    public string? name_last { get; set; }
    public string? name_use { get; set; }
    public string? name_display_first_last { get; set; }
    public string? name_dislpay_last_first { get; set; }
    public string? name_display_roster { get; set; }
    public string? bats { get; set; }
    public string? throws { get; set; }
    public string? weight { get; set; }
    public string? height_feet { get; set; }
    public string? height_inches { get; set; }
    public string? birth_date { get; set; }
    public string? birth_country { get; set; }
    public string? birth_state { get; set; }
    public string? birth_city { get; set; }
    public string? sport_code { get; set; }
    public string? high_school { get; set; }
    public string? college { get; set; }
    public string? pro_debut_date { get; set; }
    public string? service_years { get; set; }
    public string? active_sw { get; set; }
    public string? team_id { get; set; }
    public string? team_code { get; set; }
    public string? team_abbrev { get; set; }
    public string? team_full { get; set; }
    public string? league { get; set; }
  }
}
