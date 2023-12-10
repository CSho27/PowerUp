using PowerUp.Fetchers.MLBLookupService;
using PowerUp.Fetchers.Statcast;

namespace PowerUp.ElectronUI.Api.Searching
{
  public class PlayerLookupCommand : ICommand<PlayerLookupRequest, PlayerLookupResponse>
  {
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;

    public PlayerLookupCommand(IMLBLookupServiceClient mlbLookupServiceClient)
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
    }

    public PlayerLookupResponse Execute(PlayerLookupRequest request)
    {
      var response = Task.Run(() => _mlbLookupServiceClient.SearchPlayer(request.SearchText!)).GetAwaiter().GetResult();
      return new PlayerLookupResponse(response.Results);
    }
  }

  public class PlayerLookupRequest
  {
    public string? SearchText { get; set; }
  }

  public class PlayerLookupResponse
  {
    public IEnumerable<PlayerLookupResultDto> Results { get; set; }

    public PlayerLookupResponse(IEnumerable<PlayerSearchResult> results)
    {
      Results = results.Select(r => new PlayerLookupResultDto(r));
    }

    public static PlayerLookupResponse Empty() => new PlayerLookupResponse(Enumerable.Empty<PlayerSearchResult>());
  }

  public class PlayerLookupResultDto
  {
    public long LSPlayerId { get; set; }
    public string InformalDisplayName { get; set; }
    public string Position { get; set; }
    public string? MostRecentTeam { get; set; }
    public int? DebutYear { get; set; }

    public PlayerLookupResultDto(PlayerSearchResult result)
    {
      LSPlayerId = result.LSPlayerId;
      InformalDisplayName = result.InformalDisplayName;
      Position = result.Position.GetAbbrev();
      MostRecentTeam = result.TeamName;
      DebutYear = result.ProDebutDate?.Year;
    }
  }
}
