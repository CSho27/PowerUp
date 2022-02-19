using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;

    public LineupWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
    }

    public void Write(int powerProsTeamId, GSLineupDefinition team)
    {
      var teamOffset = LineupOffsetUtils.GetLineupOffset(powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
