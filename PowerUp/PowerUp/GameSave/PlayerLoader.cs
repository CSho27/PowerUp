﻿using System;
using System.Linq;

namespace PowerUp.GameSave
{
  public class PlayerLoader : IDisposable
  {
    private readonly GameSaveReader _reader;

    public PlayerLoader(string fileName)
    {
      _reader = new GameSaveReader(fileName);
    }

    public GSPlayer Load(int powerProsId)
    {
      var playerOffset = OffsetUtils.GetPlayerOffset(powerProsId);
      var loadedPlayer = new GSPlayer();
      foreach(var property in typeof(GSPlayer).GetProperties())
      {
        var gameSaveAttribute = property.GetGSAttribute();
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
