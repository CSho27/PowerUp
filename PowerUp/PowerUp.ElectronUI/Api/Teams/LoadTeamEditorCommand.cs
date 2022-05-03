using PowerUp.Databases;
using PowerUp.Entities.Teams;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class LoadTeamEditorCommand : ICommand<LoadTeamEditorRequest, LoadTeamEditorResponse>
  {
    public LoadTeamEditorResponse Execute(LoadTeamEditorRequest request)
    {
      var team = DatabaseConfig.Database.Load<Team>(request.TeamId);
      return new LoadTeamEditorResponse(team!);
    }
  }

  public class LoadTeamEditorRequest
  {
    public int TeamId { get; set; }
  }
}
