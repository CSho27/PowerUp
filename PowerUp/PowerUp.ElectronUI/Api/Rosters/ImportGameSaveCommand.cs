
using PowerUp.GameSave.Api;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ImportGameSaveCommand(IRosterImportApi rosterImportApi) : IFileRequestCommand<ImportGameSaveRequest, ImportGameSaveResponse>
  {
    public Task<ImportGameSaveResponse> Execute(ImportGameSaveRequest request, IFormFile? file)
    {
      var parameters = new RosterImportParameters
      {
        Stream = file?.OpenReadStream(),
        ImportSource = request.ImportSource,
      };
      var result = rosterImportApi.ImportRoster(parameters);
      return Task.FromResult(new ImportGameSaveResponse(result.Roster!.Id!.Value));
    }
  }

  public record ImportGameSaveRequest(string ImportSource);
  public record ImportGameSaveResponse(int RosterId);
}
