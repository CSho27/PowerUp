using PowerUp.Fetchers.MLBLookupService;

namespace PowerUp.ElectronUI.Api.Generation
{
  public class GetPlayerInfoCommand : ICommand<PlayerInfoRequest, PlayerInfoResponse>
  {
    private readonly IMLBLookupServiceClient _mlbLookupServiceClient;

    public GetPlayerInfoCommand(IMLBLookupServiceClient mlbLookupServiceClient)
    {
      _mlbLookupServiceClient = mlbLookupServiceClient;
    }

    public async Task<PlayerInfoResponse> Execute(PlayerInfoRequest request)
    {
      var result = await _mlbLookupServiceClient.GetPlayerInfo(request.LSPlayerId);
      return new PlayerInfoResponse(result);
    }
  } 

  public class PlayerInfoRequest
  {
    public long LSPlayerId { get; set; }
  }

  public class PlayerInfoResponse
  {
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }

    public PlayerInfoResponse(PlayerInfoResult result)
    {
      StartYear = result.ProDebutDate?.Year;
      EndYear = result.EndDate?.Year;
    }
  }
}
