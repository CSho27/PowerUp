using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Migrations;

namespace PowerUp.ElectronUI.Api.Migration
{
  public class MigrateExistingDatabaseCommand : ICommand<MigrateExistingDatabaseRequest, ResultResponse>
  {
    private readonly IMigrationApi _migrationApi;

    public MigrateExistingDatabaseCommand(IMigrationApi migrationApi)
    {
      _migrationApi = migrationApi;
    }

    public ResultResponse Execute(MigrateExistingDatabaseRequest request)
    {
      var dataDirectoryPath = Path.Combine(Path.GetDirectoryName(request.PowerUpDirectory)!, "./resources/bin/Data");
      if (!Directory.Exists(dataDirectoryPath))
        return ResultResponse.Failed();

      _migrationApi.MigrateDataFrom(dataDirectoryPath);
      return ResultResponse.Succeeded();
    }
  }

  public class MigrateExistingDatabaseRequest
  {
    public string? PowerUpDirectory { get; set; }
  }
}
