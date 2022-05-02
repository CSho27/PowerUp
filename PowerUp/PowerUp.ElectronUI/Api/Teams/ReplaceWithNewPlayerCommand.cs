using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class ReplaceWithNewPlayerCommand : ICommand<ReplaceWithNewPlayerRequest, ResultResponse>
  {
    private readonly IPlayerApi _playerApi;
    private readonly ITeamApi _teamApi;

    public ReplaceWithNewPlayerCommand(IPlayerApi playerApi, ITeamApi teamApi)
    {
      _playerApi = playerApi;
      _teamApi = teamApi;
    }

    public ResultResponse Execute(ReplaceWithNewPlayerRequest request)
    {

      using var tx = DatabaseConfig.Database.BeginTransaction();
      
      var playerToRemove = DatabaseConfig.Database.Load<Player>(request.PlayerToReplaceId);
      var playerToInsert = _playerApi.CreateDefaultPlayer(EntitySourceType.Custom, playerToRemove!.PrimaryPosition == Position.Pitcher);

      DatabaseConfig.Database.Save(playerToInsert);

      var team = DatabaseConfig.Database.Load<Team>(request.TeamId);
      _teamApi.ReplacePlayer(team!, playerToRemove, playerToInsert);

      DatabaseConfig.Database.Save(team!);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class ReplaceWithNewPlayerRequest
  {
    public int TeamId { get; set; }
    public int PlayerToReplaceId { get; set; }
  }
}
