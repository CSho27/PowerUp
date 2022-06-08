using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class CopyPlayerCommand : ICommand<CopyPlayerRequest, PlayerDetailsResponse>
  {
    private readonly IPlayerApi _playerApi;

    public CopyPlayerCommand(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public PlayerDetailsResponse Execute(CopyPlayerRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var playerToCopy = DatabaseConfig.Database.Load<Player>(request.PlayerId)!;
      var newPlayer = _playerApi.CreateCustomCopyOfPlayer(playerToCopy);

      DatabaseConfig.Database.Save(newPlayer);
      tx.Commit();

      return new PlayerDetailsResponse(newPlayer);
    }
  }

  public class CopyPlayerRequest
  {
    public int PlayerId { get; set; }
  }
}
