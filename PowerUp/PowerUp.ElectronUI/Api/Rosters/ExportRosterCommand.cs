using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Rosters;
using PowerUp.GameSave.Api;
using System.Net.Mime;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ExportRosterCommand : IFileRequestCommand<ExportRosterRequest, FileResponse>
  {
    private readonly IRosterExportApi _rosterExportApi;

    public ExportRosterCommand(IRosterExportApi rosterExportApi)
    {
      _rosterExportApi = rosterExportApi;
    }

    public FileResponse Execute(ExportRosterRequest request, IFormFile? file)
    {
      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId);
      using var fileStream = file?.OpenReadStream();
      var parameters = new RosterExportParameters 
      {
        Roster = roster,
        SourceGameSave = fileStream,
      };

      var rosterFile = _rosterExportApi.ExportRoster(parameters);
      return new FileResponse($"{roster!.Name}.dat", rosterFile, MediaTypeNames.Multipart.FormData);
    }
  }

  public class ExportRosterRequest
  {
    public int RosterId { get; set; }
    public IFormFile? SourceGameSave { get; set; }
  }
}
