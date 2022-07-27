using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public LineupWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
      _format = GameSaveFormat.Wii;
    }

    public LineupWriter(GameSaveObjectWriter writer, GameSaveFormat format)
    {
      _writer = writer;
      _format = format;
    }

    public void Write(int powerProsTeamId, GSLineupDefinition team)
    {
      var teamOffset = LineupOffsetUtils.GetLineupOffset(_format, powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
