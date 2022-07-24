using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.FreeAgents;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using System;
using System.Collections.Generic;
using System.IO;

namespace PowerUp.GameSave.Objects.GameSaves
{
  public class GameSaveReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;

    public GameSaveReader(ICharacterLibrary characterLibrary, string filePath)
    {
      _reader = new GameSaveObjectReader(characterLibrary, filePath);
    }

    public GameSaveReader(ICharacterLibrary characterLibrary, Stream stream)
    {
      _reader = new GameSaveObjectReader(characterLibrary, stream);
    }

    /// <summary>
    /// Reads GameSave file into data objects
    /// </summary>
    /// <returns></returns>
    public GSGameSave Read()
    {
      var playerReader = new PlayerReader(_reader);
      var gsPlayers = new List<GSPlayer>();
      for (int i = 1; i <= 1500; i++)
        gsPlayers.Add(playerReader.Read(i));

      var teamReader = new TeamReader(_reader);
      var lineupReader = new LineupReader(_reader);
      var gsTeams = new List<GSTeam>();
      var gsLineups = new List<GSLineupDefinition>();
      for (int i = 0; i < 32; i++)
      {
        gsTeams.Add(teamReader.Read(i));
        gsLineups.Add(lineupReader.Read(i));
      }
      var freeAgrents = new FreeAgentListReader(_reader).Read();

      return new GSGameSave { Players = gsPlayers, Teams = gsTeams, Lineups = gsLineups, FreeAgents = freeAgrents };  
    }

    public void Dispose() => _reader.Dispose();
  }
}
