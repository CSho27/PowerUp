using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class CreatePlayerCommand : ICommand<CreatePlayerRequest, PlayerDetailsResponse>
  {
    private readonly IPlayerApi _playerApi;

    public CreatePlayerCommand(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public PlayerDetailsResponse Execute(CreatePlayerRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var newPlayer = _playerApi.CreateDefaultPlayer(EntitySourceType.Custom, request.IsPitcher);

      DatabaseConfig.Database.Save(newPlayer);
      tx.Commit();

      return new PlayerDetailsResponse(newPlayer);
    }
  }

  public class CreatePlayerRequest 
  {
    public bool IsPitcher { get; set; }
  }
}
