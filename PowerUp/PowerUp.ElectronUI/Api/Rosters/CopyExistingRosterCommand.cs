using PowerUp.Databases;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class CopyExistingRosterCommand : ICommand<CopyExistingRosterRequest, CopyExistingRosterResponse>
  {
    private readonly IRosterApi _rosterApi;

    public CopyExistingRosterCommand(IRosterApi rosterApi)
    {
      _rosterApi = rosterApi;
    }

    public CopyExistingRosterResponse Execute(CopyExistingRosterRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      var rosterCopy = _rosterApi.CreateCustomCopyOfRoster(roster);

      DatabaseConfig.Database.Save(rosterCopy);
      tx.Commit();

      return new CopyExistingRosterResponse { RosterId = rosterCopy.Id!.Value };
    }
  }

  public class CopyExistingRosterRequest
  {
    public int RosterId { get; set; }
  }

  public class CopyExistingRosterResponse
  {
    public int RosterId { get; set; }
  }
}
