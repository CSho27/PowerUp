using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class LoadExistingRosterOptionsCommand : ICommand<LoadRosterOptionsRequest, IEnumerable<SimpleCode>>
  {
    public IEnumerable<SimpleCode> Execute(LoadRosterOptionsRequest request) => DatabaseConfig.RosterDatabase.LoadAll()
      .Select(r => new SimpleCode(r.Id!.Value, $"{r.Name} ({r.SourceType})"));
  }

  public class LoadRosterOptionsRequest
  {
    public int RosterId { get; set; }
  }
}
