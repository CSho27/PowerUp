using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    public PlayerReader(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
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

    public PlayerReader(GameSaveObjectReader reader)
    {
      _reader = reader;
    }

    public IGSPlayer Read(int powerProsId)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId, _format);
      return _format switch
      {
        GameSaveFormat.Wii_2007 => _reader.Read<GSPlayer>(playerOffset),
        GameSaveFormat.Ps2_2007 => _reader.Read<Ps2GSPlayer>(playerOffset),
        _ => throw new NotImplementedException()
      };
    }

    public void Dispose() => _reader.Dispose();
  }
}
