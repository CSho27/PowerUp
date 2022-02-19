using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerReader : IDisposable
  {
    private readonly GameSaveObjectReader<GSPlayer> _reader;

    public PlayerReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader<GSPlayer>(characterLibrary, fileName);
    }

    public GSPlayer Read(int powerProsId)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId);
      return _reader.Read(playerOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
