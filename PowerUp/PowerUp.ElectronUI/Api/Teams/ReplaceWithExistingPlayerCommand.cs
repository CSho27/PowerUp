using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class ReplaceWithExistingPlayerCommand : ICommand<ReplaceWithExistingPlayerRequest, ResultResponse>
  {
    private readonly IPlayerApi _playerApi;
    private readonly ITeamApi _teamApi;

    public ReplaceWithExistingPlayerCommand(IPlayerApi playerApi, ITeamApi teamApi)
    {
      _playerApi = playerApi;
      _teamApi = teamApi;
    }

    public ResultResponse Execute(ReplaceWithExistingPlayerRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var playerToRemove = DatabaseConfig.Database.Load<Player>(request.PlayerToReplaceId)!;
      var playerToInsert = DatabaseConfig.Database.Load<Player>(request.PlayerToInsertId)!;
      var team = DatabaseConfig.Database.Load<Team>(request.TeamId);
      _teamApi.ReplacePlayer(team!, playerToRemove, playerToInsert);

      DatabaseConfig.Database.Save(team!);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class ReplaceWithExistingPlayerRequest
  {
    public int TeamId { get; set; }
    public int PlayerToReplaceId { get; set; }
    public int PlayerToInsertId { get; set; }
  }
}
