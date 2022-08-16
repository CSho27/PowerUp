using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;
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

    public GameSaveOption(int id, string name)
    {
      GameSaveId = id;
      Name = name;
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
      var gameSaveDirectories = Directory.GetDirectories(GameSavePathBuilder.GetPowerUpGameSavesDirectory(directoryPath));
      var gameSaves = gameSaveDirectories.Select(x => GetGameSaveOptionForFolder(x)).Where(o =>  o != null).Cast<GameSaveOption>();
      var activeGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      int? activeGameSaveId = null;
      if (File.Exists(activeGameSavePath))
        activeGameSaveId = GetGameSaveIdForFile(activeGameSavePath);

      return new GameSaveManagerState(activeGameSaveId, gameSaves);
    }

    private GameSaveOption? GetGameSaveOptionForFolder(string directoryPath)
    {
      var gameSaveId = GetGameSaveIdForFile(GameSavePathBuilder.GetGameSavePath(directoryPath));
      var gameSaveName = directoryPath.Split(Path.PathSeparator).Last();
      return new GameSaveOption(gameSaveId, gameSaveName);
    }

    private int GetGameSaveIdForFile(string directoryPath)
    {
      var reader = new GameSaveObjectReader(_characterLibrary, new FileStream(directoryPath, FileMode.Open, FileAccess.Read));
      return reader.ReadInt(GSGameSave.PowerUpIdOffset);
    }

    public void ActivateGameSave(string filePath, int gameSaveId)
    {
      throw new NotImplementedException();
    }
  }
}
