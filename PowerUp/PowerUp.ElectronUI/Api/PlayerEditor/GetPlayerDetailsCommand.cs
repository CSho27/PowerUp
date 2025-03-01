using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class GetPlayerDetailsCommand : ICommand<GetPlayerDetilsRequest, PlayerDetailsResponse>
  {
    public Task<PlayerDetailsResponse> Execute(GetPlayerDetilsRequest request)
    {
      var player = DatabaseConfig.Database.Load<Player>(request.PlayerId)!;
      return Task.FromResult(new PlayerDetailsResponse(player));
    }
  }

  public class GetPlayerDetilsRequest
  {
    public int PlayerId { get; set; }
  }
}
