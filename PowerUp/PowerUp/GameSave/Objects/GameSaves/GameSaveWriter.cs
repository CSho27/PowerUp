using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Players;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Libraries;
using System;
using System.Linq;

namespace PowerUp.GameSave.Objects.GameSaves
{
  public class GameSaveWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;

    public GameSaveWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
    }

    public void Write(GSGameSave gameSave)
    {
      var playerWriter = new PlayerWriter(_writer);
      var players = gameSave.Players.ToList();
      for (int i = 0; i < players.Count; i++)
        playerWriter.Write(i + 1, players[i]);

      var teamWriter = new TeamWriter(_writer);
      var lineupWriter = new LineupWriter(_writer);
      var teams = gameSave.Teams.ToList();
      var lineups = gameSave.Lineups.ToList();

      if (teams.Count != lineups.Count)
        throw new ArgumentException("Number of teams and lineups must match");
      
      for(int i = 0; i < teams.Count; i++)
      {
        teamWriter.Write(i + 1, teams[i]);
        lineupWriter.Write(i + 1, lineups[i]);
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
