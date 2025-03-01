
using PowerUp.CSV;
using PowerUp.Databases;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ImportCsvCommand(IPlayerCsvService csvService) : IFileRequestCommand<ImportCsvRequest, ImportCsvResponse>
  {
    public async Task<ImportCsvResponse> Execute(ImportCsvRequest request, IFormFile? file)
    {
      using var stream = file?.OpenReadStream();
      var roster = await csvService.ImportRoster(stream!, request.ImportSource);
      if (roster is not null)
        DatabaseConfig.Database.Save(roster);
      return new ImportCsvResponse(roster!.Id!.Value);
    }
  }

  public record ImportCsvRequest(string ImportSource);
  public record ImportCsvResponse(int RosterId);
}
