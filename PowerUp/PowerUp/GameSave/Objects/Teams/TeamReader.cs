using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;

    public TeamReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
    }

    public TeamReader(GameSaveObjectReader reader)
    {
      _reader = reader;
    }

    public GSTeam Read(int powerProsTeamId)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(powerProsTeamId);
      return _reader.Read<GSTeam>(teamOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
