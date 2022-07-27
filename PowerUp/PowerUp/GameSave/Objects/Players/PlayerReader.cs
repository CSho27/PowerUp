using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerReader : IDisposable
  {
    private readonly GameSaveObjectReader _reader;
    private readonly GameSaveFormat _format;

    // Now used solely for testing
    internal PlayerReader(ICharacterLibrary characterLibrary, string fileName)
    {
      _reader = new GameSaveObjectReader(characterLibrary, fileName);
      _format = GameSaveFormat.Wii;

    }

    public PlayerReader(GameSaveObjectReader reader, GameSaveFormat format)
    {
      _reader = reader;
      _format = format;
    }

    public GSPlayer Read(int powerProsId)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(_format, powerProsId);
      return _reader.Read<GSPlayer>(playerOffset);
    }

    public void Dispose() => _reader.Dispose();
  }
}
