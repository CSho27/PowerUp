using PowerUp.CSV;
using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Rosters;
using System.Net.Mime;

namespace PowerUp.ElectronUI.Api.Rosters
{
  public class ExportCsvCommand(IPlayerCsvService csvService) : ICommand<ExportCsvRequest, FileResponse>
  {
    public async Task<FileResponse> Execute(ExportCsvRequest request)
    {
      var csvStream = new MemoryStream();
      var roster = DatabaseConfig.Database.Load<Roster>(request.RosterId);
      if (roster is null)
        throw new Exception($"No Roster found for {request.RosterId}");
      await csvService.ExportRoster(csvStream, roster);
      return new FileResponse($"{roster!.Name}.csv", csvStream, MediaTypeNames.Text.Csv);
    }
  }

  public record ExportCsvRequest(int RosterId);
}
