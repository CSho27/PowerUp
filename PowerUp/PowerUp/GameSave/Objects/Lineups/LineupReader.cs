using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    public LineupReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
      _format = GameSaveFormat.Wii;
    }

    public LineupReader(GameSaveObjectReader reader, GameSaveFormat format)
    {
      _reader = reader;
      _format = format;
    }

    public GSLineupDefinition Read(int powerProsTeamId)
    {
      var lineupOffset = LineupOffsetUtils.GetLineupOffset(_format, powerProsTeamId);
      return _reader.Read<GSLineupDefinition>(lineupOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
