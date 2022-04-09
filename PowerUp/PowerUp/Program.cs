using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp
{
  class Program
  {
    private const string GAME_SAVE_PATH = "C:/Users/short/OneDrive/Documents/Dolphin Emulator/Wii/title/00010000/524d5045/data/pm2maus.dat";
    private const string DATA_DIRECTORY = "C:/Users/short/Documents/PowerUp/";
    private const int PLAYER_ID = 1;

    static void Main(string[] args)
    {
      var characterLibrary = new CharacterLibrary(Path.Combine(DATA_DIRECTORY, "./data/Character_Library.csv"));

      DatabaseConfig.Initialize(DATA_DIRECTORY);
      AnalyzeGameSave(characterLibrary);
      //PrintAllPlayers(characterLibrary);
      //PrintAllTeams(characterLibrary);
      //PrintAllLineups(characterLibrary);
      //PrintRedsPlayers();
      //BuildPlayerValueLibrary(characterLibrary);
      //FindDuplicatesInLibrary();
      //FindPlayersByLastName();
    }

    static void FindPlayersByLastName()
    {

      var playerDatabase = new PlayerDatabase(DATA_DIRECTORY);

      var time = TimeAction(() =>
      {
        var results = playerDatabase.LoadAll().Where(p => p.LastName == "Pena");
        Console.WriteLine(results.Count());
      });
      Console.WriteLine($"Time: {time}");
    }

    static TimeSpan TimeAction(Action action)
    {
      var startTime = DateTime.Now;
      action();
      return DateTime.Now - startTime;
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
        Console.WriteLine($"Update {currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {bitString} - {player.SkinAndEyes}");
      }
    }

    static void PrintAllPlayers(ICharacterLibrary characterLibrary)
    {
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);

      for (int id = 1; id < 1513; id++)
      {
        var player = playerReader.Read(id);
        var position = (Position)player.PrimaryPosition!;
        var playerString = $"{id} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
        Console.WriteLine($"{playerString}{new string(' ', 38 - playerString.Length)}{player.PitchingForm!.Value}");
      }
    }

    static void PrintAllTeams(ICharacterLibrary characterLibrary)
    {
      var teamReader = new TeamReader(characterLibrary, GAME_SAVE_PATH);
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);

      for (int teamNum = 1; teamNum <= 32; teamNum++)
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

          var playerString = $"{playerNum + 1} - {string.Format("{0:X4}", player.PowerProsId)} {position.GetAbbrev()} {player.LastName}, {player.FirstName}";
          Console.WriteLine(playerString);
          //Console.WriteLine($"{playerString}{new string(' ', 36 - playerString.Length)}{BinaryUtils.ToBitString(player.UnknownBytes_81_88!, formatted: false)}");
        }
        Console.WriteLine();
      }
    }

    static void PrintAllLineups(ICharacterLibrary characterLibrary)
    {
      var lineupReader = new LineupReader(characterLibrary, GAME_SAVE_PATH);

      for (int teamNum = 1; teamNum <= 32; teamNum++)
      {
        var lineup = lineupReader.Read(teamNum);
        Console.WriteLine($"Team {teamNum}");
        Console.WriteLine("No DH:");
        PrintLineup(characterLibrary, lineup.NoDHLineup!);
        Console.WriteLine("DH:");
        PrintLineup(characterLibrary, lineup.DHLineup!);
        Console.WriteLine();
      }
    }

    static void PrintLineup(ICharacterLibrary characterLibrary, IEnumerable<GSLineupPlayer> lineup)
    {
      var playerReader = new PlayerReader(characterLibrary, GAME_SAVE_PATH);
      var lineupPlayers = lineup.ToArray();
      for (int playerNum = 0; playerNum < lineupPlayers.Length; playerNum++)
      {
        var lineupSlot = lineupPlayers[playerNum];
        string? playerString;
        if (lineupSlot.PowerProsPlayerId == 0)
        {
          playerString = "Pitcher";
        }
        else
        {
          var player = playerReader.Read(lineupSlot.PowerProsPlayerId!.Value);
          playerString = $"{player.LastName}, {player.FirstName}";
        }

        var position = lineupSlot.Position!.Value == 0
          ? Position.Pitcher
          : (Position)lineupSlot.Position!.Value;

        Console.WriteLine($"{playerNum+1}. {position.GetAbbrev()} {playerString}");
      }
    }

    static void BuildPlayerValueLibrary(ICharacterLibrary characterLibrary)
    {
      var playerReader = new PlayerReader(characterLibrary, Path.Combine(DATA_DIRECTORY, "./data/BASE.pm2maus.dat"));
      var playersAndValues = new Dictionary<string, int>();
      
      for (int id = 1; id < 1500; id++)
      {
        var player = playerReader.Read(id);
        if(player.PowerProsId != 0 )
        {
          playersAndValues.TryAdd($"{player.FirstName}_{player.LastName}", player.Face!.Value);
        }
      };

      var csvLines = playersAndValues.OrderBy(kvp => kvp.Value).Select(p => $"{p.Value+20} - {p.Key.Replace('_', ' ')},{p.Value}");
      File.WriteAllLines(Path.Combine(DATA_DIRECTORY, "./data/ddddFace_Library.csv"), csvLines);
    }

    static void FindDuplicatesInLibrary()
    {
      var filePathToCheck = Path.Combine(DATA_DIRECTORY, "./data/Face_Library.csv");

      var keyValuePairs = File.ReadAllLines(filePathToCheck)
        .Select(l => l.Split(','))
        .Select(l => new KeyValuePair<string, string>(l[0], l[1]));

      var csvLines = keyValuePairs.GroupBy(p => p.Value).OrderBy(g => int.Parse(g.Key)).Select(g => $"{g.Key}, {string.Join(", ", g)}");
      File.WriteAllLines(Path.Combine(DATA_DIRECTORY, "./data/duplicates.csv"), csvLines);
    }
  }
}
