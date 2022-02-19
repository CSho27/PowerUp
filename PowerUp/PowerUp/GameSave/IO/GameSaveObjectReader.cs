using PowerUp.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public class GameSaveObjectReader: IDisposable 
  {
    private readonly GameSaveReader _reader;

    public GameSaveObjectReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveReader(characterLibrary, fileName);
    }

    public TGameSaveObject Read<TGameSaveObject>(long offset) where TGameSaveObject : class
      => (TGameSaveObject)Read(typeof(TGameSaveObject), offset);

    public object Read(Type type, long offset)
    {
      var gsObject = type.GetConstructors().First().Invoke(null);
      foreach (var property in type.GetProperties())
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
        else if (gameSaveAttribute is GSArrayAttribute arrayAttr)
        {
          var arrayType = property.PropertyType.GenericTypeArguments[0];
          var array = Array.CreateInstance(arrayType, arrayAttr.ArrayLength);
          for (int i = 0; i < arrayAttr.ArrayLength; i++)
            array.SetValue(Read(arrayType, offset + i * arrayAttr.ItemLength), i);

          property.SetValue(gsObject, array);
        }

      }

      return gsObject;
    }

    public void Dispose() => _reader.Dispose();
  }
}
