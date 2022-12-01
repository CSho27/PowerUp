using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    public PlayerWriter(ICharacterLibrary characterLibrary, string fileName, GameSaveFormat format)
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

    public PlayerWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }

    public void Write(int powerProsId, IGSPlayer player)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId, _format);
      switch(_format)
      {
        case GameSaveFormat.Wii_2007:
          _writer.Write(playerOffset, (GSPlayer)player);
          break;
        case GameSaveFormat.Ps2_2007:
          _writer.Write(playerOffset, (Ps2GSPlayer)player);
          break;
        default:
          throw new NotImplementedException();
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
