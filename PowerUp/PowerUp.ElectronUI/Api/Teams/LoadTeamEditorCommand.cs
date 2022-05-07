using PowerUp.Databases;
using PowerUp.Entities.Teams;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class LoadTeamEditorCommand : ICommand<LoadTeamEditorRequest, LoadTeamEditorResponse>
  {
    public LoadTeamEditorResponse Execute(LoadTeamEditorRequest request)
    {
      if (request.TempTeamId.HasValue)
      {
        var tempTeam = DatabaseConfig.Database.Load<TempTeam>(request.TempTeamId.Value)!;
        if (tempTeam.Team!.Id != request.TeamId)
          throw new InvalidOperationException("Mismatching TeamId and TempTeamId");

        return new LoadTeamEditorResponse(tempTeam);
      } 
      else
      {
        var team = DatabaseConfig.Database.Load<Team>(request.TeamId)!;
        var tempTeam = new TempTeam(team);
        DatabaseConfig.Database.Save(tempTeam);
        return new LoadTeamEditorResponse(tempTeam);
      }
    }
  }

  public class LoadTeamEditorRequest
  {
    public int TeamId { get; set; }
    public int? TempTeamId { get; set; }
  }
}
