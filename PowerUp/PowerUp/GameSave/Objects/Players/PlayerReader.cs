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

    public GSPlayer Read(int powerProsId)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId, _format);
      return _reader.Read<GSPlayer>(playerOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
