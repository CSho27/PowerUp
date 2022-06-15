using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSPlayerInfoResponse
  {
    public LSPlayerInfo? player_info { get; set; }
  }

  public class LSPlayerInfo
  {
    public string? copyRight { get; set; }
    public LSPlayerSearchQueryResults? queryResults { get; set; }
  }

  public class LSPlayerInfoQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSPlayerInfoResult
  {
    public string? player_id { get; set; }
    public string? primary_position { get; set; }
    public string? primary_position_txt { get; set; }
    public string? name_prefix { get; set; }
    public string? name_first { get; set; }
    public string? name_middle { get; set; }
    public string? name_last { get; set; }
    public string? name_use { get; set; }
    public string? name_nick { get; set; }
    public string? name_display_first_last { get; set; }
    public string? name_display_last_first { get; set; }
    public string? name_display_roster { get; set; }
    public string? jersey_number { get; set; }
    public string? bats { get; set; }
    public string? throws { get; set; }
    public string? gender { get; set; }
    public string? weight { get; set; }
    public string? height_feet { get; set; }
    public string? height_inches { get; set; }
    public string? birth_date { get; set; }
    public string? birth_country { get; set; }
    public string? birth_state { get; set; }
    public string? birth_city { get; set; }
    public string? death_date { get; set; }
    public string? death_country { get; set; }
    public string? death_state { get; set; }
    public string? death_city { get; set; }
    public string? age { get; set; }
    public string? primary_sport_code { get; set; }
    public string? primary_stat_type { get; set; }
    public string? high_school { get; set; }
    public string? college { get; set; }
    public string? pro_debut_date { get; set; }
    public string? start_date { get; set; }
    public string? end_date { get; set; }
    public string? service_years { get; set; }
    public string? status { get; set; }
    public string? status_code { get; set; }
    public string? status_date { get; set; }
    public string? team_id { get; set; }
    public string? team_code { get; set; }
    public string? team_abbrev { get; set; }
    public string? team_name { get; set; }
    public string? league { get; set; }
    public string? twitter_id { get; set; }
  }
}
