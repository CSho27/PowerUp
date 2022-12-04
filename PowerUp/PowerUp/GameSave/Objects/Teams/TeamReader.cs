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

    public IGSTeam Read(int powerProsTeamId)
    {
      var teamOffset = TeamOffsetUtils.GetTeamOffset(powerProsTeamId, _format);
      return _format switch
      {
        GameSaveFormat.Wii_2007 => _reader.Read<GSTeam>(teamOffset),
        GameSaveFormat.Ps2_2007 => _reader.Read<Ps2GSTeam>(teamOffset),
        _ => throw new NotImplementedException()
      };
    }

    public void Dispose() => _reader.Dispose();
  }
}
