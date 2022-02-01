using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerUp.GameSave
{
  public class PlayerLoader : IDisposable
  {
    private const long PLAYER_START_OFFSET = 0x68c74;
    private const long PLAYER_SIZE = 0xb0;
    private readonly GameSaveReader _reader;

    public PlayerLoader(string fileName)
    {
      _reader = new GameSaveReader(fileName);
    }

    public GSPlayer Load(int playerId)
    {
      var playerOffset = PLAYER_START_OFFSET + PLAYER_SIZE * (playerId - 1);
      var loadedPlayer = new GSPlayer();
      foreach(var property in typeof(GSPlayer).GetProperties())
      {
        var gameSaveAttribute = property
          .GetCustomAttributes(inherit: false)
          .SingleOrDefault(a => typeof(GSAttribute).IsAssignableFrom(a.GetType()));

        if (gameSaveAttribute == null)
          continue;

        var type = property.PropertyType;

        

        if (gameSaveAttribute is GSBooleanAttribute boolAttr)
          property.SetValue(loadedPlayer, _reader.ReadBool(playerOffset + boolAttr.Offset, boolAttr.BitOffset));
        else if (gameSaveAttribute is GSUInt2Attribute uint2Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt2(playerOffset + uint2Attr.Offset, uint2Attr.BitOffset));
        else if (gameSaveAttribute is GSUInt3Attribute uint3Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt3(playerOffset + uint3Attr.Offset, uint3Attr.BitOffset));
        else if (gameSaveAttribute is GSUInt4Attribute uint4Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt4(playerOffset + uint4Attr.Offset, uint4Attr.BitOffset));
        else if (gameSaveAttribute is GSUInt8Attribute uint8Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt8(playerOffset + uint8Attr.Offset));
        else if (gameSaveAttribute is GSUInt16Attribute uint16Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt16(playerOffset + uint16Attr.Offset));
        else if (gameSaveAttribute is GSStringAttribute stringAttr)
          property.SetValue(loadedPlayer, _reader.ReadString(playerOffset + stringAttr.Offset, stringAttr.StringLength));
        else if(gameSaveAttribute is GSBytesAttribute bytesAttr)
          property.SetValue(loadedPlayer, _reader.ReadBytes(playerOffset + bytesAttr.Offset, bytesAttr.NumberOfBytes));

      }

      return loadedPlayer;
    }

    public void Dispose() => _reader.Dispose();
  }
}
