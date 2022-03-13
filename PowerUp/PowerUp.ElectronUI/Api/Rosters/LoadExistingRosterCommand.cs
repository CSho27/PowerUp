using PowerUp.Databases;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadExistingRosterCommand : ICommand<LoadExistingRosterRequest, RosterEditorResponse>
  {
    public RosterEditorResponse Execute(LoadExistingRosterRequest request)
    {
      var roster = DatabaseConfig.RosterDatabase.Load(request.RosterId);
      var rosterDetails = RosterDetails.FromRoster(roster!);
      return new RosterEditorResponse(rosterDetails);
    }
  }

  public class LoadExistingRosterRequest 
  { 
    public int RosterId { get; set; }
  }
}
