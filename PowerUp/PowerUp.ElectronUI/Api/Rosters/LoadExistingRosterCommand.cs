using PowerUp.Databases;
using PowerUp.Entities.Rosters;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadExistingRosterCommand : ICommand<LoadExistingRosterRequest, RosterEditorResponse>
  {
    public Task<RosterEditorResponse> Execute(LoadExistingRosterRequest request)
    {
      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId);
      var rosterDetails = RosterDetails.FromRoster(roster!);
      return Task.FromResult(new RosterEditorResponse(rosterDetails));
    }
  }

  public class LoadExistingRosterRequest 
  { 
    public int RosterId { get; set; }
  }
}
