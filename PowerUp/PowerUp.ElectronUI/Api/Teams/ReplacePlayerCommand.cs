using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class ReplacePlayerCommand : ICommand<ReplacePlayerRequest, ResultResponse>
  {
    private readonly ITeamApi _teamApi;

    public ReplacePlayerCommand(ITeamApi teamApi)
    {
      _teamApi = teamApi;
    }

    public ResultResponse Execute(ReplacePlayerRequest request)
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

  public class ReplacePlayerRequest
  {
    public int TeamId { get; set; }
    public int PlayerToReplaceId { get; set; }
    public int PlayerToInsertId { get; set; }
  }
}
