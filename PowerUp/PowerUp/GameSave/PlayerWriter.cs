using System;

namespace PowerUp.GameSave
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveObjectWriter<GSPlayer> _writer;

    public PlayerWriter(string fileName)
    {
      _writer = new GameSaveObjectWriter<GSPlayer>(fileName);
    }

    public void Write(int powerProsId, GSPlayer player)
    {
      var playerOffset = OffsetUtils.GetPlayerOffset(powerProsId);
      _writer.Write(playerOffset, player);
    }

    public void Dispose() => _writer.Dispose();
  }
}
