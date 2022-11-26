using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;

    public PlayerWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
    }

    public PlayerWriter(GameSaveObjectWriter writer)
    {
      _writer = writer;
    }

    public void Write(int powerProsId, GSPlayer player)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId, GameSaveFormat.Wii_2007);
      _writer.Write(playerOffset, player);
    }

    public void Dispose() => _writer.Dispose();
  }
}
