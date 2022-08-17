using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.GameSaveManagement
{
  public interface IGameSaveManager
  {
    public bool Initialize(string directoryPath);
    public GameSaveManagerState GetCurrentState(string directoryPath);
    public void ActivateGameSave(string directoryPath, int gameSaveId);
  }

  public class GameSaveManagerState
  {
    public int? ActiveGameSaveId { get; }
    public IEnumerable<GameSaveOption> GameSaveOptions { get; }

    public GameSaveManagerState(int? activeGameSaveId, IEnumerable<GameSaveOption> gameSaveOptions)
    {
      ActiveGameSaveId = activeGameSaveId;
      GameSaveOptions = gameSaveOptions;
    }
  }

  public class GameSaveOption
  {
    public int GameSaveId { get; }
    public string Name { get; }
    public string DirectoryPath { get; }
    public string GameSaveFilePath { get; }


    public GameSaveOption(int id, string name, string directoryPath, string gameSaveFilePath)
    {
      GameSaveId = id;
      Name = name;
      DirectoryPath = directoryPath;
      GameSaveFilePath = gameSaveFilePath;
    }
  }

  public class GameSaveManager : IGameSaveManager
  {
    private readonly ICharacterLibrary _characterLibrary;

    public GameSaveManager(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
    }
    
    public bool Initialize(string directoryPath)
    {
      var originalGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      if (!File.Exists(originalGameSavePath))
        return false;

      var originalFolderPath = GameSavePathBuilder.GetPowerUpDirectoryForNewGameSave(directoryPath, "Original");
      DirectoryFactory.CreateDirectoriesForPathIfNeeded(originalFolderPath);
      File.Copy(originalGameSavePath, GameSavePathBuilder.GetGameSavePath(originalFolderPath));
      return true;
    }

    public GameSaveManagerState GetCurrentState(string directoryPath)
    {
      var activeGameSaveId = GetActiveGameSaveId(directoryPath);
      SyncActiveGameSave(directoryPath, activeGameSaveId);
      var gameSaves = GetGameSaveOptions(directoryPath);
      return new GameSaveManagerState(activeGameSaveId, gameSaves);
    }

    public void ActivateGameSave(string directoryPath, int gameSaveId)
    {
      var activeGameSaveId = GetActiveGameSaveId(directoryPath);
      SyncActiveGameSave(directoryPath, activeGameSaveId);

      var activeGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      var activeGameSaveBackupPath = GetGameSavePathForId(directoryPath, gameSaveId);
      File.Copy(activeGameSaveBackupPath, activeGameSavePath, overwrite: true);
    }

    private IEnumerable<GameSaveOption> GetGameSaveOptions(string directoryPath)
    {
      var gameSaveDirectories = Directory.GetDirectories(GameSavePathBuilder.GetPowerUpGameSavesDirectory(directoryPath));
      return gameSaveDirectories.Select(x => GetGameSaveOptionForFolder(x)).Where(o => o != null).Cast<GameSaveOption>();
    }

    private GameSaveOption? GetGameSaveOptionForFolder(string directoryPath)
    {
      var gameSaveFilePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      var gameSaveId = GetGameSaveIdForFile(gameSaveFilePath);
      var gameSaveName = directoryPath.Split(Path.PathSeparator).Last();
      return new GameSaveOption(gameSaveId, gameSaveName, directoryPath, gameSaveFilePath);
    }

    private int? GetActiveGameSaveId(string directoryPath)
    {
      var activeGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      return File.Exists(activeGameSavePath)
        ? GetGameSaveIdForFile(activeGameSavePath)
        : null;
    }

    private int GetGameSaveIdForFile(string directoryPath)
    {
      var reader = new GameSaveObjectReader(_characterLibrary, new FileStream(directoryPath, FileMode.Open, FileAccess.Read));
      return reader.ReadInt(GSGameSave.PowerUpIdOffset);
    }

    private string GetGameSavePathForId(string directoryPath, int gameSaveId)
    {
      var options = GetGameSaveOptions(directoryPath);
      var activeGameSave = options.Single(o => o.GameSaveId == gameSaveId);
      return activeGameSave.GameSaveFilePath;
    }

    private void SyncActiveGameSave(string directoryPath, int? activeGameSaveId)
    {
      if (!activeGameSaveId.HasValue)
        return;

      var activeGameSave = GameSavePathBuilder.GetGameSavePath(directoryPath);
      var activeGameBackupPath = GetGameSavePathForId(directoryPath, activeGameSaveId!.Value);
      File.Copy(activeGameSave, activeGameBackupPath, overwrite: true);
    }
  }
}
