using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveObjectWriter<GSPlayer> _writer;

    public PlayerWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter<GSPlayer>(characterLibrary, fileName);
    }

    public void Write(int powerProsId, GSPlayer player)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId);
      _writer.Write(playerOffset, player);
    }

    public void Dispose() => _writer.Dispose();
  }
}
