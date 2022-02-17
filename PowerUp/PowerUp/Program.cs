using PowerUp.DebugUtils;
using PowerUp.GameSave;
using PowerUp.Libraries;
using System;

namespace PowerUp
{
  class Program
  {
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const int PLAYER_ID = 20;

    static void Main(string[] args)
    {
      LoadAndRetrieve();
    }

    static void LoadAndRetrieve()
    {
      /*
      var playersPath = SolutionPath.Relative("./PowerUp/data/Players");
      var database = new JsonDatabase<Player>(playersPath);
      database.Save("TestPlayer", new Player() { FirstName = "Steve" });
      var player = database.Load("TestPlayer");
      Console.WriteLine(player.FirstName);
      */
    }

    static void AnalyzeGameSave()
    {
      while (true)
      {
        Console.ReadLine();
        using var loader = new PlayerReader(new CharacterLibrary("C:/Users/short/Documents/PowerUp/data/Character_Library.csv"), GAME_SAVE_PATH);
        var player = loader.Read(PLAYER_ID);
        var bitString = player.TestBytes!.ToBitString();
        var currentTime = DateTime.Now;
        Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString}");
      }
    }
  }
}
