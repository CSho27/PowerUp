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

    public LoadablePlayer Load(int playerId)
    {
      var playerOffset = PLAYER_START_OFFSET + PLAYER_SIZE * (playerId - 1);
      var loadedPlayer = new LoadablePlayer();
      foreach(var property in typeof(LoadablePlayer).GetProperties())
      {
        var gameSaveAttribute = property
          .GetCustomAttributes(inherit: false)
          .SingleOrDefault(a => typeof(GSAttribute).IsAssignableFrom(a.GetType()));

        if (gameSaveAttribute == null)
          break;

        if (gameSaveAttribute is GSBooleanAttribute boolAttr)
          property.SetValue(loadedPlayer, _reader.ReadBool(playerOffset + boolAttr.Offset, boolAttr.BitOffset));
        else if (gameSaveAttribute is GSUInt16Attribute uint16Attr)
          property.SetValue(loadedPlayer, _reader.ReadUInt16(playerOffset + uint16Attr.Offset));
        else if (gameSaveAttribute is GSStringAttribute stringAttribute)
          property.SetValue(loadedPlayer, _reader.ReadString(playerOffset + stringAttribute.Offset, stringAttribute.StringLength));
      }

      return loadedPlayer;
    }

    public void Dispose() => _reader.Dispose();
  }
}
