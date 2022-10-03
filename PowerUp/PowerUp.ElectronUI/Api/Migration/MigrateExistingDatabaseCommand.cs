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
      _migrationApi.MigrateDataFrom("./Data");
      return ResultResponse.Succeeded();
    }
  }

  public class MigrateExistingDatabaseRequest
  {
    public string? PowerUpDirectory { get; set; }
  }
}
