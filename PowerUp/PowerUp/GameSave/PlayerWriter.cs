using System;

namespace PowerUp.GameSave
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveWriter _writer;

    public PlayerWriter(string fileName)
    {
      _writer = new GameSaveWriter(fileName);
    }

    public void Write(int powerProsId, GSPlayer player)
    {
      var playerOffset = OffsetUtils.GetPlayerOffset(powerProsId);
      foreach(var property in typeof(GSPlayer).GetProperties())
      {
        var gameSaveAttribute = property.GetGSAttribute();
        var propertyValue = property.GetValue(player);
        if (gameSaveAttribute == null || propertyValue == null)
          continue;

        if (gameSaveAttribute is GSUIntAttribute uintAttr)
          _writer.WriteUInt(playerOffset + uintAttr.Offset, uintAttr.BitOffset, uintAttr.Bits, (ushort)propertyValue);
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
