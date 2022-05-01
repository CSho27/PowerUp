using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class ReplacePlayerWithCopyCommand : ICommand<ReplacePlayerWithCopyRequest, ResultResponse>
  {
    private readonly IPlayerApi _playerApi;
    private readonly ITeamApi _teamApi;

    public ReplacePlayerWithCopyCommand(IPlayerApi playerApi, ITeamApi teamApi)
    {
      _playerApi = playerApi;
      _teamApi = teamApi;
    }

    public ResultResponse Execute(ReplacePlayerWithCopyRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var playerToCopy = DatabaseConfig.Database.Load<Player>(request.PlayerId)!;
      var playerToInsert = _playerApi.CreateCustomCopyOfPlayer(playerToCopy);

      DatabaseConfig.Database.Save(playerToInsert);

      var team = DatabaseConfig.Database.Load<Team>(request.TeamId);
      _teamApi.ReplacePlayer(team!, playerToCopy, playerToInsert);

      DatabaseConfig.Database.Save(team!);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class ReplacePlayerWithCopyRequest
  {
    public int TeamId { get; set; }
    public int PlayerId { get; set; }
  }
}
