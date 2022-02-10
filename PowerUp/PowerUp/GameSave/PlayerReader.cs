using System;

namespace PowerUp.GameSave
{
  public class PlayerReader : IDisposable
  {
    private readonly GameSaveObjectReader<GSPlayer> _reader;


    public PlayerReader(string fileName)
    {
      _reader = new GameSaveObjectReader<GSPlayer>(fileName);
    }

    public GSPlayer Read(int powerProsId)
    {
      var playerOffset = OffsetUtils.GetPlayerOffset(powerProsId);
      return _reader.Read(playerOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
