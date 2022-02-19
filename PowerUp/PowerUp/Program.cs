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
    private const int PLAYER_ID = 370;

    static void Main(string[] args)
    {
      var characterLibrary = new CharacterLibrary("C:/Users/short/Documents/PowerUp/data/Character_Library.csv");
      //PrintAllPlayers(characterLibrary);
      PrintAllTeams(characterLibrary);
      //AnalyzeGameSave(characterLibrary);
    }

    static void PrintAllPlayers(ICharacterLibrary characterLibrary)
    {
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);

      for (int id = 1; id < 1513; id++)
      {
        var player = playerReader.Read(id);
        var position = (Position)player.PrimaryPosition!;
        var playerString = $"{id} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
        Console.WriteLine($"{playerString}{new string(' ', 38 - playerString.Length)}{BinaryUtils.ToBitString(player.EmptyPlayerBytes!)}");
      }
    }

    static void PrintAllTeams(ICharacterLibrary characterLibrary)
    {
      var teamReader = new TeamReader(characterLibrary, GAME_SAVE_PATH);
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);

      for (int teamNum = 1; teamNum <= 30; teamNum++)
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
          /*
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
          */

          var playerString = $"{playerNum + 1} - {string.Format("{0:X4}", player.PowerProsId)} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
          Console.WriteLine($"{playerString}{new string(' ', 36 - playerString.Length)}{BinaryUtils.ToBitString(player.UnknownBytes_81_88!, formatted: false)}");
        }
        Console.WriteLine();
      }
    }

    static void AnalyzeGameSave(ICharacterLibrary characterLibrary)
    {
      while (true)
      {
        Console.ReadLine();
        using var loader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);
        var player = loader.Read(PLAYER_ID);
        var bitString = player.UnknownBytes_81_88!.ToBitString();
        var currentTime = DateTime.Now;
        Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString}");
      }
    }
  }
}
