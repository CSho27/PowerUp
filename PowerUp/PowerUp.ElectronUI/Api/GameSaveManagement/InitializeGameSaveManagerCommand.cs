using PowerUp.Databases;
using PowerUp.Entities.AppSettings;
using PowerUp.GameSave.GameSaveManagement;

namespace PowerUp.ElectronUI.Api.GameSaveManagement
{
  public class InitializeGameSaveManagerCommand : ICommand<InitializeGameSaveManagerRequest, InitializeGameSaveResponse>
  {
    private const string DEFAULT_DOLPHIN_POWER_PROS_DIR = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data";

    private readonly IGameSaveManager _gameSaveManager;

    public InitializeGameSaveManagerCommand(IGameSaveManager gameSaveManager)
    {
      _gameSaveManager = gameSaveManager;
    }

    public InitializeGameSaveResponse Execute(InitializeGameSaveManagerRequest request)
    {
      using var tx = DatabaseConfig.Database.BeginTransaction();

      var directoryPath = request.GameSaveManagerDirectoryPath ?? DEFAULT_DOLPHIN_POWER_PROS_DIR;
      var settings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if(request.GameSaveManagerDirectoryPath == null && settings != null && settings.GameSaveManagerDirectoryPath != null)
        return new InitializeGameSaveResponse { Success = true };

      var dirExists = _gameSaveManager.Initialize(directoryPath);
      if (!dirExists)
        return new InitializeGameSaveResponse { Success = false };
      
      if(settings == null)
        settings = new AppSettings();

      settings.GameSaveManagerDirectoryPath = directoryPath;
      
      DatabaseConfig.Database.Save(settings);
      tx.Commit();

      return new InitializeGameSaveResponse { Success = true };
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
