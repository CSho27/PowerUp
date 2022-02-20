using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;

    public TeamWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
    }

    public TeamWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }
    public void Write(int powerProsTeamId, GSTeam team)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
