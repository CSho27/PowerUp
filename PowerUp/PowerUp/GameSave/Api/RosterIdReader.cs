using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;

namespace PowerUp.GameSave.Api
{
  public class RosterIdReader
  {
    private ICharacterLibrary _characterLibrary;

    public RosterIdReader(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
    }

    public short Read(string filePath)
    {
      using var reader = new GameSaveObjectReader(_characterLibrary, filePath);
      return reader.ReadInt(GSGameSave.PowerUpIdOffset);
    }
  }
}
