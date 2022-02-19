using PowerUp.GameSave.Objects.Players;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.IO
{
  public class GameSaveObjectWriter<TGameSaveObject> : IDisposable where TGameSaveObject : class
  {
    private readonly GameSaveWriter _writer;

    public GameSaveObjectWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveWriter(characterLibrary, fileName);
    }

    public void Write(long offset, TGameSaveObject gsObject)
    {
      foreach (var property in typeof(TGameSaveObject).GetProperties())
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
      }
    }

    public void Dispose() => _writer.Dispose();
  }
}
