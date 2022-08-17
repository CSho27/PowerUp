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
      var existingSettings = DatabaseConfig.Database.LoadOnly<AppSettings>();
      if(existingSettings != null && existingSettings.GameSaveManagerDirectoryPath != null)
        return new InitializeGameSaveResponse { Success = true };

      var defaultDirExists = _gameSaveManager.Initialize(DEFAULT_DOLPHIN_POWER_PROS_DIR);
      if (!defaultDirExists)
        return new InitializeGameSaveResponse { Success = false };

      var newSettings = new AppSettings() { GameSaveManagerDirectoryPath = DEFAULT_DOLPHIN_POWER_PROS_DIR };
      DatabaseConfig.Database.Save(newSettings);
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
