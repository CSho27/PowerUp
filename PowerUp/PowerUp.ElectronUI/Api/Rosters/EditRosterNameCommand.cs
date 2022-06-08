using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class EditRosterNameCommand : ICommand<EditRosterNameRequest, ResultResponse>
  {
    private readonly IRosterApi _rosterApi;

    public EditRosterNameCommand(IRosterApi rosterApi)
    {
      _rosterApi = rosterApi;
    }

    public ResultResponse Execute(EditRosterNameRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId)!;
      _rosterApi.EditRosterName(roster, request.RosterName!);

      DatabaseConfig.Database.Save(roster);
      tx.Commit();

      return ResultResponse.Succeeded();
    }
  }

  public class EditRosterNameRequest
  {
    public int RosterId { get; set; }
    public string? RosterName { get; set; }
  }
}
