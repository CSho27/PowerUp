using System;
using System.IO;
using System.Linq;

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
      var scrubbedGameSaveName = ScrubForFileName(gameSaveName);

      bool dirExists = true;
      string gameSaveDir = "";
      for (var i = 0; dirExists; i++)
      {
        var numString = i == 0
          ? ""
          : $"({i})";
        gameSaveDir = Path.Combine(powerUpDirectory, $"./{scrubbedGameSaveName}{numString}");
        dirExists = Directory.Exists(gameSaveDir);
      }

      return gameSaveDir;
    }
    public static string GetGameSavePath(string gameSaveDirectory, bool forBackup = false) => forBackup
      ? Path.Combine(gameSaveDirectory, "./backup_pm2maus.dat")
      : Path.Combine(gameSaveDirectory, "./pm2maus.dat");
    public static string ScrubForFileName(string fileName)
    {
      var scrubbedFileName = fileName;
      var invalidChars = Path.GetInvalidFileNameChars().Append('/').Append('\\');
      foreach (var c in invalidChars)
        scrubbedFileName = scrubbedFileName.Replace($"{c}", "");
      
      return scrubbedFileName;
    }
  }
}
