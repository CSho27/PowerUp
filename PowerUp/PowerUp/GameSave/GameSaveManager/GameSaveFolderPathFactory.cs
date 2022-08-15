using System.IO;

namespace PowerUp.GameSave.GameSaveManager
{
  public class GameSaveFolderPathFactory
  {
    private const string GAME_SAVE_FOLDER_PATH = "./PowerUp Game Saves";
    private const string WII_FOLDER_PATH = "./Wii";

    public static string BuildFor(string baseDirectory, string rosterName)
    {
      var gameSaveFolderPath = CombineAndCreateIfNotExists(baseDirectory, GAME_SAVE_FOLDER_PATH);
      var wiiSavesPath = CombineAndCreateIfNotExists(gameSaveFolderPath, WII_FOLDER_PATH);

      bool fileExists = true;
      string rosterFilePath = "";
      for(var i = 0; fileExists; i++)
      {
        var numString = i == 0
          ? ""
          : $"({i})";
        rosterFilePath = Path.Combine(wiiSavesPath, $"./{rosterName}{numString}");
        fileExists = Directory.Exists(rosterFilePath);
      }

      CreateIfNotExists(rosterFilePath);
      return rosterFilePath;
    }

    private static string CombineAndCreateIfNotExists(string basePath, string relativePath)
    {
      var directoryPath = Path.Combine(basePath, relativePath);
      CreateIfNotExists(directoryPath);
      return directoryPath;
    }

    private static void CreateIfNotExists(string directoryPath)
    {
      if (!Directory.Exists(directoryPath))
        Directory.CreateDirectory(directoryPath);
    }
  }
}
