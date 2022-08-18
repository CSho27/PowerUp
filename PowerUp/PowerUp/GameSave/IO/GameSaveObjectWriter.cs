using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public class GameSaveObjectWriter: IDisposable 
  {
    public GameSaveFileWriter _writer;

    public GameSaveObjectWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveFileWriter(characterLibrary, fileName);
    }

    public void Write<TGameSaveObject>(long offset, TGameSaveObject gsObject) where TGameSaveObject : class
      => Write(typeof(TGameSaveObject), offset, gsObject);

    public void WriteInt(long offset, short @int) => _writer.WriteSInt(offset, 0, 16, @int);

    public void Write(Type type, long offset, object gsObject)
    {
      foreach (var property in type.GetProperties())
      {
        var gameSaveAttribute = property.GetGSAttribute();
        var propertyValue = property.GetValue(gsObject);
        if (gameSaveAttribute == null || propertyValue == null)
          continue;

        if (gameSaveAttribute is GSBooleanAttribute boolAttr)
          _writer.WriteBool(offset + boolAttr.Offset, boolAttr.BitOffset, (bool)propertyValue);
        else if (gameSaveAttribute is GSUIntAttribute uintAttr)
          _writer.WriteUInt(offset + uintAttr.Offset, uintAttr.BitOffset, uintAttr.Bits, (ushort)propertyValue);
        else if (gameSaveAttribute is GSSIntAttribute sintAttr)
          _writer.WriteSInt(offset + sintAttr.Offset, sintAttr.BitOffset, sintAttr.Bits, (short)propertyValue);
        else if (gameSaveAttribute is GSStringAttribute stringAttr)
          _writer.WriteString(offset + stringAttr.Offset, stringAttr.StringLength, (string)propertyValue);
        else if (gameSaveAttribute is GSArrayAttribute arrayAttr)
        {
          var arrayType = property.PropertyType.GenericTypeArguments[0];
          var objectArray = ((IEnumerable<object>)propertyValue).ToArray();

          for (int i = 0; i < arrayAttr.ArrayLength && i < objectArray.Length; i++)
          {
            var elementValue = objectArray[i];
            Write(arrayType, offset + arrayAttr.Offset + i * arrayAttr.ItemLength, elementValue);
          }
        }
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
