using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public LineupWriter(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
    {
      _writer = new GameSaveObjectWriter
        ( characterLibrary
        , fileName
        , format == GameSaveFormat.Wii_2007
          ? ByteOrder.BigEndian
          : ByteOrder.LittleEndian
        );
      _format = format;
    }

    public LineupWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }

    public void Write(int powerProsTeamId, GSLineupDefinition team)
    {
      var teamOffset = LineupOffsetUtils.GetLineupOffset(powerProsTeamId);
      _writer.Write(teamOffset, team);
    }

    public void Dispose() => _writer.Dispose();
  }
}
