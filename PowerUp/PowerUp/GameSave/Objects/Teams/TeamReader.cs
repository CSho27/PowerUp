using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    internal TeamReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
    }

    public TeamReader(GameSaveObjectReader reader, GameSaveFormat format)
    {
      _reader = reader;
      _format = format;
    }

    public GSTeam Read(int powerProsTeamId)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(_format, powerProsTeamId);
      return _reader.Read<GSTeam>(teamOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
