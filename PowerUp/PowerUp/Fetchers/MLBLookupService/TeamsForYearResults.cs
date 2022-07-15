using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public class TeamsForYearResults
  {
    public long TotalResults { get; }
    public IEnumerable<TeamResult> Results { get; }

    public TeamsForYearResults(int totalResults, IEnumerable<LSTeamResult> results)
    {
      TotalResults = totalResults;
      Results = results.Select(r => new TeamResult(r));
    }
  }

  public class TeamResult
  {
    public int LSTeamId { get; }
    public int Year { get; }
    public string LocationName { get; }
    public string TeamName { get; }
    public string FullName { get; }
    public string State { get; set; }
    public string City { get; set; }
    public string Venue { get; set; }
    public string League { get; set; }
    public string? Division { get; set; }

    public TeamResult(LSTeamResult result)
    {
      LSTeamId = int.Parse(result.team_id!);
      Year = int.Parse(result.season!);
      LocationName = result.name_short!;
      TeamName = result.name!;
      FullName = result.name_display_full!;
      State = result.state!;
      City = result.city!;
      Venue = result.venue_name!;
      League = result.league!;
      Division = result.division.StringIfNotEmpty();
    }
  }
}
