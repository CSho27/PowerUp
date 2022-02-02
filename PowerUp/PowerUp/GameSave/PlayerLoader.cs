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

        if (gameSaveAttribute is GSBooleanAttribute boolAttr)
          property.SetValue(loadedPlayer, _reader.ReadBool(playerOffset + boolAttr.Offset, boolAttr.BitOffset));
        else if (gameSaveAttribute is GSUIntAttribute uintAttr)
          property.SetValue(loadedPlayer, _reader.ReadUInt(playerOffset + uintAttr.Offset, uintAttr.BitOffset, uintAttr.Bits));
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
