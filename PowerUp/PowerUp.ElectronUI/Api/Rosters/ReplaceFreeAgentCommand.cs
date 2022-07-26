using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ReplaceFreeAgentCommand : ICommand<ReplaceFreeAgentRequest, ResultResponse>
  {
    private readonly IRosterApi _rosterApi;

    public ReplaceFreeAgentCommand(IRosterApi rosterApi)
    {
      _rosterApi = rosterApi;
    }

    public ResultResponse Execute(ReplaceFreeAgentRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var playerToRemove = DatabaseConfig.Database.Load<Player>(request.PlayerToReplaceId)!;
      var playerToInsert = DatabaseConfig.Database.Load<Player>(request.PlayerToInsertId)!;
      var team = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      _rosterApi.ReplaceFreeAgent(team!, playerToRemove, playerToInsert);

      DatabaseConfig.Database.Save(team!);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class ReplaceFreeAgentRequest
  {
    public int RosterId { get; set; }
    public int PlayerToReplaceId { get; set; }
    public int PlayerToInsertId { get; set; }
  }
}
