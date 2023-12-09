using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    public LineupReader(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
    {
      _reader = new GameSaveObjectReader
        ( characterLibrary
        , fileName
        , format == GameSaveFormat.Wii_2007
            ? ByteOrder.BigEndian
            : ByteOrder.LittleEndian
        );
    }

    public LineupReader(GameSaveObjectReader reader)
    {
      _reader = reader;
    }

    public GSLineupDefinition Read(int powerProsTeamId)
    {
      var lineupOffset = LineupOffsetUtils.GetLineupOffset(powerProsTeamId);
      return _reader.Read<GSLineupDefinition>(lineupOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
