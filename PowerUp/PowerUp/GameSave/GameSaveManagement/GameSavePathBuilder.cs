using System;
using System.IO;

namespace PowerUp.GameSave.GameSaveManagement
{
  public class GameSavePathBuilder
  {
    private const string GAME_SAVE_FOLDER_PATH = "./PowerUp Game Saves";
    private const string WII_FOLDER_PATH = "./Wii";

    public static string GetPowerUpGameSavesDirectory(string baseDirectory) => Path.Combine(baseDirectory, GAME_SAVE_FOLDER_PATH, WII_FOLDER_PATH);
    public static string GetPowerUpDirectoryForNewGameSave(string baseDirectory, string gameSaveName)
    {
      var powerUpDirectory = GetPowerUpGameSavesDirectory(baseDirectory);
      if (!Directory.Exists(powerUpDirectory))
        throw new InvalidOperationException($"PowerUp GameSave Folder is not configured correctly. No directory found at {powerUpDirectory}");

      bool dirExists = true;
      string gameSaveDir = "";
      for (var i = 0; dirExists; i++)
      {
        var numString = i == 0
          ? ""
          : $"({i})";
        gameSaveDir = Path.Combine(powerUpDirectory, $"./{gameSaveName}{numString}");
        dirExists = Directory.Exists(gameSaveDir);
      }

      return gameSaveDir;
    }
    public static string GetGameSavePath(string gameSaveDirectory) => Path.Combine(gameSaveDirectory, "./pm2maus.dat");
  }
}
