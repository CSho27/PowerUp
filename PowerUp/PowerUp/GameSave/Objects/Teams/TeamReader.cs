using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    public TeamReader(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
    {
      _reader = new GameSaveObjectReader
        ( characterLibrary
        , fileName
        , format == GameSaveFormat.Wii_2007
            ? ByteOrder.BigEndian
            : ByteOrder.LittleEndian
        );
      _format = format;
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
