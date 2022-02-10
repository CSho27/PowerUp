using System;
using System.Linq;

namespace PowerUp.GameSave
{
  public class GameSaveObjectReader<TGameSaveObject> : IDisposable where TGameSaveObject : class
  {
    private readonly GameSaveReader _reader;

    public GameSaveObjectReader(string fileName)
    {
      _reader = new GameSaveReader(fileName);
    }

    public TGameSaveObject Read(long offset)
    {
      var gsObject = (TGameSaveObject)typeof(TGameSaveObject).GetConstructors().First().Invoke(null);
      foreach (var property in typeof(GSPlayer).GetProperties())
      {
        var gameSaveAttribute = property.GetGSAttribute();
        if (gameSaveAttribute == null)
          continue;

        if (gameSaveAttribute is GSBooleanAttribute boolAttr)
          property.SetValue(gsObject, _reader.ReadBool(offset + boolAttr.Offset, boolAttr.BitOffset));
        else if (gameSaveAttribute is GSUIntAttribute uintAttr)
          property.SetValue(gsObject, _reader.ReadUInt(offset + uintAttr.Offset, uintAttr.BitOffset, uintAttr.Bits));
        else if (gameSaveAttribute is GSSIntAttribute sintAttr)
          property.SetValue(gsObject, _reader.ReadSInt(offset + sintAttr.Offset, sintAttr.BitOffset, sintAttr.Bits));
        else if (gameSaveAttribute is GSStringAttribute stringAttr)
          property.SetValue(gsObject, _reader.ReadString(offset + stringAttr.Offset, stringAttr.StringLength));
        else if (gameSaveAttribute is GSBytesAttribute bytesAttr)
          property.SetValue(gsObject, _reader.ReadBytes(offset + bytesAttr.Offset, bytesAttr.NumberOfBytes));
      }

      return gsObject;
    }
    
    public void Dispose() => _reader.Dispose();
  }
}
