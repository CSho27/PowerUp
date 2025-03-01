using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.AppSettings;
using PowerUp.GameSave.GameSaveManagement;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class RenameGameSaveCommand : ICommand<RenameGameSaveRequest, ResultResponse>
  {
    private readonly IGameSaveManager _gameSaveManager;

    public RenameGameSaveCommand(IGameSaveManager gameSaveManager)
    {
      _gameSaveManager = gameSaveManager;
    }

    public Task<ResultResponse> Execute(RenameGameSaveRequest request)
    {
      var settings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if (settings == null || settings.GameSaveManagerDirectoryPath == null)
        throw new InvalidOperationException("Game Save Manager has not been initlialized");

      var success = _gameSaveManager.RenameGameSave(settings.GameSaveManagerDirectoryPath, request.GameSaveId, request.Name);
      return Task.FromResult(new ResultResponse(success));
    }
  }

  public class RenameGameSaveRequest
  {
    public int GameSaveId { get; set; }
    public string? Name { get; set; }
  }
}
