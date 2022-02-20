using PowerUp.GameSave.IO;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;

    public PlayerReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
    }

    public PlayerReader(GameSaveObjectReader reader)
    {
      _reader = reader;
    }

    public GSPlayer Read(int powerProsId)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(powerProsId);
      return _reader.Read<GSPlayer>(playerOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
