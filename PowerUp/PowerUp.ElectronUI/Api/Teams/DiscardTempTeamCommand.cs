using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Teams;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class DiscardTempTeamCommand : ICommand<DiscardTempTeamRequest, ResultResponse>
  {
    public Task<ResultResponse> Execute(DiscardTempTeamRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      DatabaseConfig.Database.DeleteWhere<TempTeam>(t => 
        (t.Team!.Id == request.TeamId && t.Id == request.TempTeamId) ||
        t.CreatedOn.AddDays(1) < DateTime.Now
      );

      tx.Commit();
      return Task.FromResult(ResultResponse.Succeeded());
    }
  }

  public class DiscardTempTeamRequest
  {
    public int TeamId { get; set; }
    public int TempTeamId { get; set; }
  }
}
