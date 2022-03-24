using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.GameSave.Api;
using PowerUp.Libraries;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ExportRosterCommand : ICommand<ExportRosterRequest, ResultResponse>
  {
    private readonly IRosterExportApi _rosterExportApi;

    public ExportRosterCommand(IRosterExportApi rosterExportApi)
    {
      _rosterExportApi = rosterExportApi;
    }

    public ResultResponse Execute(ExportRosterRequest request)
    {
      if(request.DirectoryPath == null)
        throw new ArgumentNullException(nameof(request.DirectoryPath));

      var roster = DatabaseConfig.RosterDatabase.Load(request.RosterId);

      var parameters = new RosterExportParameters 
      {
        ExportDirectory = request.DirectoryPath,
        Roster = roster
      };

      _rosterExportApi.ExportRoster(parameters);

      return ResultResponse.Succeeded();
    }
  }

  public class ExportRosterRequest
  {
    public int RosterId { get; set; }
    public string? DirectoryPath { get; set; }
  }
}
