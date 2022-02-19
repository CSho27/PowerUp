using PowerUp.DebugUtils;
using PowerUp.Entities.Players;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using System;
using System.Linq;

namespace PowerUp
{
  class Program
  {
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const int PLAYER_ID = 20;

    static void Main(string[] args)
    {
      var characterLibrary = new CharacterLibrary("C:/Users/short/Documents/PowerUp/data/Character_Library.csv");
      var teamReader = new TeamReader(characterLibrary, GAME_SAVE_PATH);
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);

      for(int teamNum = 1; teamNum <= 30; teamNum++)
      {
        var team = teamReader.Read(teamNum);
        var playerEntries = team.PlayerEntries!.ToArray();
        Console.WriteLine($"Team {playerEntries.First().PowerProsTeamId}");
        for (int playerNum = 0; playerNum < playerEntries.Length; playerNum++)
        {
          var pe = playerEntries[playerNum];
          if (pe.PowerProsPlayerId == 0)
            continue;

          var player = playerReader.Read(pe.PowerProsPlayerId!.Value);
          var position = (Position)player.PrimaryPosition!;
          var league = pe.IsMLB!.Value
            ? "MLB"
            : pe.IsAAA!.Value
              ? "AAA"
              : "ERR";
          var playerString = $"{playerNum+1} {league} {position.GetAbbrev()} {pe.PitcherRole}: {player.LastName}, {player.FirstName}";
          if (pe.IsPinchHitter!.Value)
            playerString += " PH";
          if (pe.IsPinchRunner!.Value)
            playerString += " PR";
          if (pe.IsDefensiveReplacement!.Value)
            playerString += " DEF";
          if (pe.IsDefensiveLiability!.Value)
            playerString += " SUB";

          Console.WriteLine(playerString);
        }
        Console.WriteLine();
      }
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

    static void AnalyzeGameSave(ICharacterLibrary characterLibrary)
    {
      while (true)
      {
        Console.ReadLine();
        using var loader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);
        var player = loader.Read(PLAYER_ID);
        var bitString = player.TestBytes!.ToBitString();
        var currentTime = DateTime.Now;
        Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString}");
      }
    }
  }
}
