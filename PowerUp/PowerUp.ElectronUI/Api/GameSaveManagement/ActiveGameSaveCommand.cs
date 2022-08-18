using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.AppSettings;
using PowerUp.GameSave.GameSaveManagement;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class ActiveGameSaveCommand : ICommand<ActivateGameSaveRequest, ResultResponse>
  {
    private readonly IGameSaveManager _gameSaveManager;

    public ActiveGameSaveCommand(IGameSaveManager gameSaveManager)
    {
      _gameSaveManager = gameSaveManager;
    }

    public ResultResponse Execute(ActivateGameSaveRequest request)
    {
      var settings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if (settings == null || settings.GameSaveManagerDirectoryPath == null)
        throw new InvalidOperationException("Game Save Manager has not been initlialized");

      _gameSaveManager.ActivateGameSave(settings.GameSaveManagerDirectoryPath, request.GameSaveId);
      return ResultResponse.Succeeded();
    }
  }

  public class ActivateGameSaveRequest
  {
    public int GameSaveId { get; set; }
  }
}
