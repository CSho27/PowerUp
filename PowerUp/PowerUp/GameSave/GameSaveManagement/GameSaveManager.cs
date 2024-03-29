﻿using PowerUp.GameSave.IO;
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
    public string MoveDirectoryIfNeeded(string currentDirectoryPath);
    public (string gameSavePath, int gameSaveId) CreateNewGameSave(string directoryPath, string? sourceGameSavePath, string rosterName);
    public GameSaveManagerState GetCurrentState(string directoryPath);
    public void ActivateGameSave(string directoryPath, int gameSaveId);
    public bool RenameGameSave(string directoryPath, int gameSaveId, string? newName);
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
    private readonly IBaseGameSavePathProvider _gameSavePathProvider;

    public GameSaveManager(ICharacterLibrary characterLibrary, IBaseGameSavePathProvider gameSavePathProvider)
    {
      _characterLibrary = characterLibrary;
      _gameSavePathProvider = gameSavePathProvider;
    }
    
    public bool Initialize(string directoryPath)
    {
      if (!Directory.Exists(directoryPath))
        return false;

      DirectoryFactory.CreateDirectoriesForPathIfNeeded(Path.Combine(directoryPath, "data"));

      var originalGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath, forDataFolder: true);
      var existingOptions = GetGameSaveOptions(directoryPath);
      if (File.Exists(originalGameSavePath) && !existingOptions.Any(o => o.GameSaveId == 0))
      {
        var originalFolderPath = GameSavePathBuilder.GetPowerUpDirectoryForNewGameSave(directoryPath, "Original");
        DirectoryFactory.CreateDirectoriesForPathIfNeeded(originalFolderPath);
        File.Copy(originalGameSavePath, GameSavePathBuilder.GetGameSavePath(originalFolderPath));
        File.Copy(originalGameSavePath, GameSavePathBuilder.GetGameSavePath(originalFolderPath, forBackup: true));
      }
      else
      {
        var originalFolderPath = GameSavePathBuilder.GetPowerUpGameSavesWiiDirectory(directoryPath);
        DirectoryFactory.CreateDirectoriesForPathIfNeeded(originalFolderPath);
      }

      return true;
    }

    // This is here specifically to avoid PowerPros thinking it's out of memory to use
    public string MoveDirectoryIfNeeded(string directoryPath)
    {
      if (Path.GetFileName(directoryPath) != "data" || Path.GetDirectoryName(directoryPath) == "524d5045")
        return directoryPath;

      var currentGameSavesPath = GameSavePathBuilder.GetPowerUpGameSavesDirectory(directoryPath);
      var newDirectoryPath = Path.Combine(directoryPath, "../");
      var newGameSavesPath = GameSavePathBuilder.GetPowerUpGameSavesDirectory(newDirectoryPath);
      FileSystemUtils.CopyDirectoryRecursively(currentGameSavesPath, newGameSavesPath);
      Directory.Delete(currentGameSavesPath, recursive: true);
      return newDirectoryPath;
    }

    public (string gameSavePath, int gameSaveId) CreateNewGameSave(string directoryPath, string? sourceGameSave, string rosterName)
    {
      var nextGameSaveId = GetGameSaveOptions(directoryPath).Select(o => o.GameSaveId).MaxOrDefault() + 1;

      var newGameSaveDirPath = GameSavePathBuilder.GetPowerUpDirectoryForNewGameSave(directoryPath, rosterName);
      DirectoryFactory.CreateDirectoriesForPathIfNeeded(newGameSaveDirPath);

      var newGameSavePath = GameSavePathBuilder.GetGameSavePath(newGameSaveDirPath);

      File.Copy(sourceGameSave ?? _gameSavePathProvider.GetPath(), newGameSavePath);
      return (newGameSavePath, nextGameSaveId);
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

      var activeGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath, forDataFolder: true);
      var activeGameSaveBackupPath = GetGameSavePathForId(directoryPath, gameSaveId);
      if(activeGameSaveBackupPath != null)
        File.Copy(activeGameSaveBackupPath, activeGameSavePath, overwrite: true);
    }
    
    public bool RenameGameSave(string directoryPath, int gameSaveId, string? newName)
    {
      if (string.IsNullOrWhiteSpace(newName))
        return false;

      var gameSaveOptions = GetGameSaveOptions(directoryPath);
      var optionToRename = gameSaveOptions.Single(o => o.GameSaveId == gameSaveId);
      var otherGameSaveOptions = gameSaveOptions.Where(o => o.GameSaveId != gameSaveId);

      var scrubbedNewName = GameSavePathBuilder.ScrubForFileName(newName);
      if (otherGameSaveOptions.Any(o => o.Name == scrubbedNewName))
        return false;

      var currentGameSaveDirectoryPath = optionToRename.DirectoryPath;
      var newGameSaveDirectoryPath = Path.Combine(Path.GetDirectoryName(currentGameSaveDirectoryPath)!, scrubbedNewName);
      Directory.Move(currentGameSaveDirectoryPath, newGameSaveDirectoryPath);
      return true;
    }

    private IEnumerable<GameSaveOption> GetGameSaveOptions(string directoryPath)
    {
      var gameSaveDirectory = GameSavePathBuilder.GetPowerUpGameSavesWiiDirectory(directoryPath);
      if(!Directory.Exists(gameSaveDirectory))
        return Enumerable.Empty<GameSaveOption>();
      var gameSaveDirectories = Directory.GetDirectories(gameSaveDirectory);
      return gameSaveDirectories.Select(x => GetGameSaveOptionForFolder(x)).Where(o => o != null).Cast<GameSaveOption>().ToList();
    }

    private GameSaveOption? GetGameSaveOptionForFolder(string directoryPath)
    {
      var gameSaveFilePath = GameSavePathBuilder.GetGameSavePath(directoryPath);
      var gameSaveId = GetGameSaveIdForFile(gameSaveFilePath);
      if (!gameSaveId.HasValue)
        return null;
      var gameSaveName = Path.GetFileName(directoryPath)!;
      return new GameSaveOption(gameSaveId.Value, gameSaveName, directoryPath, gameSaveFilePath);
    }

    private int? GetActiveGameSaveId(string directoryPath)
    {
      var activeGameSavePath = GameSavePathBuilder.GetGameSavePath(directoryPath, forDataFolder: true);
      return File.Exists(activeGameSavePath)
        ? GetGameSaveIdForFile(activeGameSavePath)
        : null;
    }

    private int? GetGameSaveIdForFile(string filePath)
    {
      if (!File.Exists(filePath))
        return null;

      using var reader = new GameSaveObjectReader(_characterLibrary, new FileStream(filePath, FileMode.Open, FileAccess.Read), ByteOrder.BigEndian);
      return reader.ReadInt(GSGameSave.PowerUpIdOffset);
    }

    private string? GetGameSavePathForId(string directoryPath, int gameSaveId)
    {
      var options = GetGameSaveOptions(directoryPath);
      var activeGameSave = options.FirstOrDefault(o => o.GameSaveId == gameSaveId);
      return activeGameSave?.GameSaveFilePath;
    }

    private void SyncActiveGameSave(string directoryPath, int? activeGameSaveId)
    {
      if (!activeGameSaveId.HasValue)
        return;

      var activeGameSave = GameSavePathBuilder.GetGameSavePath(directoryPath, forDataFolder: true);
      var activeGameBackupPath = GetGameSavePathForId(directoryPath, activeGameSaveId!.Value);
      if (activeGameBackupPath != null)
        File.Copy(activeGameSave, activeGameBackupPath, overwrite: true);
    }
  }
}
