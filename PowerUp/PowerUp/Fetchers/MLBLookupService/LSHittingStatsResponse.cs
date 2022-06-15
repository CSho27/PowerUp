using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class LSHittingStatsResponse
  {
    public LSHittingStatsTm? sport_hitting_tm { get; set; }
  }

  public class LSHittingStatsTm
  {
    public string? copyRight { get; set; }
    public LSHittingStatsQueryResults? queryResults { get; set; }
  }

  public class LSHittingStatsQueryResults
  {
    public DateTime? created { get; set; }
    public string? totalSize { get; set; }
    public JsonElement? row { get; set; }
  }

  public class LSHittingStatsResult
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

    public string? g { get; set; }
    public string? ab { get; set; }
    public string? tpa { get; set; }
    public string? h { get; set; }
    public string? d { get; set; }
    public string? t { get; set; }
    public string? hr { get; set; }
    public string? xbh { get; set; }
    public string? tb { get; set; }
    public string? bb { get; set; }
    public string? ibb { get; set; }
    public string? hbp { get; set; }
    public string? rbi { get; set; }
    public string? lob { get; set; }
    public string? r { get; set; }
    public string? so { get; set; }
    public string? go { get; set; }
    public string? ao { get; set; }
    public string? hgnd { get; set; }
    public string? hldr { get; set; }
    public string? hfly { get; set; }
    public string? hpop { get; set; }
    public string? gidp { get; set; }
    public string? sf { get; set; }
    public string? sac { get; set; }
    public string? roe { get; set; }
    public string? avg { get; set; }
    public string? slg { get; set; }
    public string? obp { get; set; }
    public string? ops { get; set; }
    public string? babip { get; set; }
    public string? ppa { get; set; }
    public string? sb { get; set; }
    public string? cs { get; set; }
  }
}
