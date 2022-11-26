using PowerUp.GameSave.IO;
using PowerUp.GameSave.Objects.GameSaves;
using PowerUp.Libraries;

namespace PowerUp.GameSave.Api
{
  public class RosterIdReader
  {
    private readonly ICharacterLibrary _characterLibrary;
    private readonly ByteOrder _byteOrder;

    public RosterIdReader(ICharacterLibrary characterLibrary)
    {
      _characterLibrary = characterLibrary;
    }

    public short Read(string filePath)
    {
      using var reader = new GameSaveObjectReader(_characterLibrary, filePath, ByteOrder.BigEndian);
      return reader.ReadInt(GSGameSave.PowerUpIdOffset);
    }
  }
}
