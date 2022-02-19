using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;

    public LineupReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
    }

    public GSLineupDefinition Read(int powerProsTeamId)
    {
      var lineupOffset = LineupOffsetUtils.GetLineupOffset(powerProsTeamId);
      return _reader.Read<GSLineupDefinition>(lineupOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
