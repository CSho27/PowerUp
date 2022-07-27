using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public TeamWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
      _format = GameSaveFormat.Wii;
    }

    public TeamWriter(GameSaveObjectWriter writer, GameSaveFormat format)
    {
      _writer = writer;
      _format = format;
    }
    public void Write(int powerProsTeamId, GSTeam team)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(_format, powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
