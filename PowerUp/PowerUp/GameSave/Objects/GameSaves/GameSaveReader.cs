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
    private readonly GameSaveFormat _format;

    internal GameSaveReader(ICharacterLibrary characterLibrary, string filePath)
    {
      _reader = new GameSaveObjectReader(characterLibrary, filePath);
      _format = GameSaveFormat.Wii;
    }

    public GameSaveReader(ICharacterLibrary characterLibrary, Stream stream, GameSaveFormat format)
    {
      _reader = new GameSaveObjectReader(characterLibrary, stream, format == GameSaveFormat.Ps2);
      _format = format;
    }

    /// <summary>
    /// Reads GameSave file into data objects
    /// </summary>
    /// <returns></returns>
    public GSGameSave Read()
    {
      var playerReader = new PlayerReader(_reader, _format);
      var gsPlayers = new List<GSPlayer>();
      for (int i = 1; i <= 1500; i++)
        gsPlayers.Add(playerReader.Read(i));

      var teamReader = new TeamReader(_reader, _format);
      var lineupReader = new LineupReader(_reader, _format);
      var gsTeams = new List<GSTeam>();
      var gsLineups = new List<GSLineupDefinition>();
      for (int i = 0; i < 32; i++)
      {
        gsTeams.Add(teamReader.Read(i));
        gsLineups.Add(lineupReader.Read(i));
      }
      var freeAgrents = new FreeAgentListReader(_reader, _format).Read();

      return new GSGameSave { Players = gsPlayers, Teams = gsTeams, Lineups = gsLineups, FreeAgents = freeAgrents };  
    }

    public void Dispose() => _reader.Dispose();
  }
}
