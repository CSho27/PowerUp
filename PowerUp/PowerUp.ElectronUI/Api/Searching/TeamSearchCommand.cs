using PowerUp.Entities;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Queries;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Searching
{
  public class TeamSearchCommand : ICommand<TeamSearchRequest, TeamSearchResponse>
  {
    public TeamSearchResponse Execute(TeamSearchRequest request)
    {
      if (request.SearchText == null)
        return TeamSearchResponse.Empty();

      var results = new TeamSearchQuery(request.SearchText).Execute();
      return new TeamSearchResponse(results);
    }
  }

  public class TeamSearchRequest
  {
    public string? SearchText { get; set; }
  }

  public class TeamSearchResponse
  {
    public IEnumerable<TeamSearchResultDto> Results { get; set; }

    public TeamSearchResponse(IEnumerable<Team> results)
    {
      Results = results.Select(r => new TeamSearchResultDto(r));
    }

    public static TeamSearchResponse Empty() => new TeamSearchResponse(Enumerable.Empty<Team>());
  }

  public class TeamSearchResultDto
  {
    public int TeamId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; }
    public int Hitting { get; set; }
    public int Pitching { get; set; }
    public int Overall { get; set; }

    public TeamSearchResultDto(Team result)
    {
      TeamId = result.Id!.Value;
      SourceType = result.SourceType;
      Name = result.Name!;
      Hitting = result.GetHittingRating().RoundDown();
      Pitching = result.GetPitchingRating().RoundDown();
      Overall = result.GetOverallRating().RoundDown();
    }

  }
}
