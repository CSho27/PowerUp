﻿using PowerUp.Entities;
using PowerUp.Entities.Players.Queries;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Searching
{
  public class PlayerSearchCommand : ICommand<PlayerSearchRequest, PlayerSearchResponse>
  {
    public PlayerSearchResponse Execute(PlayerSearchRequest request)
    {
      if(request.SearchText == null)
        return PlayerSearchResponse.Empty();

      var results = new PlayerSearchQuery(request.SearchText).Execute();
      return new PlayerSearchResponse(results);
    }
  }

  public class PlayerSearchRequest
  {
    public string? SearchText { get; set; }
  }

  public class PlayerSearchResponse
  {
    public IEnumerable<PlayerSearchResultDto> Results { get; set; } = Enumerable.Empty<PlayerSearchResultDto>();

    public PlayerSearchResponse(IEnumerable<PlayerSearchResult> results)
    {
      Results = results.Select(r => new PlayerSearchResultDto(r));
    }

    public static PlayerSearchResponse Empty() => new PlayerSearchResponse(Enumerable.Empty<PlayerSearchResult>());
  }

  public class PlayerSearchResultDto
  {
    public int PlayerId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; set; }
    public string UniformNumber { get; set; }
    public string FormalDisplayName { get; set; }
    public string InformalDisplayName { get; set; }
    public string Position { get; set; }
    public string BatsAndThrows { get; set; }
    public int Overall { get; set; }

    public PlayerSearchResultDto(PlayerSearchResult result)
    {
      PlayerId = result.Id;
      SourceType = result.SourceType;
      UniformNumber = result.UniformNumber!;
      FormalDisplayName = result.FormalDisplayName!;
      InformalDisplayName = result.InformalDisplayName!;
      Position = result.PrimaryPosition.GetAbbrev();
      BatsAndThrows = $"{result.BattingSide.GetAbbrev()}/{result.ThrowingArm.GetAbbrev()}";
      Overall = result.Overall.RoundDown();
    }
  }
}
