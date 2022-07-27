using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;
using System;

namespace PowerUp.GameSave.Objects.Players
{
  public class PlayerWriter : IDisposable
  {
    private readonly GameSaveObjectWriter _writer;
    private readonly GameSaveFormat _format;

    // Now used only for testing
    internal PlayerWriter(ICharacterLibrary characterLibrary, string fileName)
    {
      _writer = new GameSaveObjectWriter(characterLibrary, fileName);
      _format = GameSaveFormat.Wii;
    }

    public PlayerWriter(GameSaveObjectWriter writer, GameSaveFormat format)
    {
      _writer = writer;
      _format = format;
    }

    public void Write(int powerProsId, GSPlayer player)
    {
      var playerOffset = PlayerOffsetUtils.GetPlayerOffset(_format, powerProsId);
      _writer.Write(playerOffset, player);
    }

    public void Dispose() => _writer.Dispose();
  }
}
