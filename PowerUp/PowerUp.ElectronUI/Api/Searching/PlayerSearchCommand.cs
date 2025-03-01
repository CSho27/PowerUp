using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Queries;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Searching
{
  public class PlayerSearchCommand : ICommand<PlayerSearchRequest, PlayerSearchResponse>
  {
    public Task<PlayerSearchResponse> Execute(PlayerSearchRequest request)
    {
      if(request.SearchText == null)
        return Task.FromResult(PlayerSearchResponse.Empty());

      var results = new PlayerSearchQuery(request.SearchText).Execute();
      return Task.FromResult(new PlayerSearchResponse(results));
    }
  }

  public class PlayerSearchRequest
  {
    public string? SearchText { get; set; }
  }

  public class PlayerSearchResponse
  {
    public IEnumerable<PlayerSearchResultDto> Results { get; set; } = Enumerable.Empty<PlayerSearchResultDto>();

    public PlayerSearchResponse(IEnumerable<Player> results)
    {
      Results = results.Select(r => new PlayerSearchResultDto(r));
    }

    public static PlayerSearchResponse Empty() => new PlayerSearchResponse(Enumerable.Empty<Player>());
  }

  public class PlayerSearchResultDto
  {
    public int PlayerId { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public bool CanEdit => SourceType.CanEdit();
    public string UniformNumber { get; set; }
    public string SavedName { get; set; }
    public string FormalDisplayName { get; set; }
    public string InformalDisplayName { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Position Position { get; set; }
    public string PositionAbbreviation => Position.GetAbbrev();
    public string BatsAndThrows { get; set; }
    public int Overall { get; set; }

    public PlayerSearchResultDto(Player result)
    {
      PlayerId = result.Id!.Value;
      SourceType = result.SourceType;
      UniformNumber = result.UniformNumber!;
      SavedName = result.SavedName!;
      FormalDisplayName = result.FormalDisplayName!;
      InformalDisplayName = result.InformalDisplayName!;
      Position = result.PrimaryPosition;
      BatsAndThrows = result.BatsAndThrows!;
      Overall = result.Overall.RoundDown();
    }
  }
}
