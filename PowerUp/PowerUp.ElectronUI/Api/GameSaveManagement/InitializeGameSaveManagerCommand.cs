using PowerUp.Databases;
using PowerUp.Entities.AppSettings;
using PowerUp.GameSave.GameSaveManagement;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class InitializeGameSaveManagerCommand : ICommand<InitializeGameSaveManagerRequest, InitializeGameSaveResponse>
  {
    private const string DEFAULT_DOLPHIN_POWER_PROS_DIR = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045";
    private readonly IGameSaveManager _gameSaveManager;

    public InitializeGameSaveManagerCommand(IGameSaveManager gameSaveManager)
    {
      _gameSaveManager = gameSaveManager;
    }

    public Task<InitializeGameSaveResponse> Execute(InitializeGameSaveManagerRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();
      var settings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if(settings == null)
      {
        settings = new AppSettings();
        settings.GameSaveManagerDirectoryPath = request.GameSaveManagerDirectoryPath ?? DEFAULT_DOLPHIN_POWER_PROS_DIR;
      }
      else
      {
        settings.GameSaveManagerDirectoryPath = request.GameSaveManagerDirectoryPath != null
          ? Path.GetFullPath(request.GameSaveManagerDirectoryPath) == Path.GetFullPath(Path.Combine(DEFAULT_DOLPHIN_POWER_PROS_DIR, "data"))
             ? DEFAULT_DOLPHIN_POWER_PROS_DIR
             : request.GameSaveManagerDirectoryPath
          : _gameSaveManager.MoveDirectoryIfNeeded(settings.GameSaveManagerDirectoryPath!);
      }
      DatabaseConfig.Database.Save(settings);
      
      var dirExists = _gameSaveManager.Initialize(settings.GameSaveManagerDirectoryPath);
      if (!dirExists)
        return Task.FromResult(new InitializeGameSaveResponse { Success = false });
      
      tx.Commit();
      return Task.FromResult(new InitializeGameSaveResponse { Success = true });
    }
  }

  public class InitializeGameSaveManagerRequest
  {
    public string? GameSaveManagerDirectoryPath { get; set; }
  }

  public class InitializeGameSaveResponse 
  {
    public bool Success { get; set; }
  }
}
