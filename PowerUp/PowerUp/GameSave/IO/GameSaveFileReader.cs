using PowerUp.Libraries;
using System;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public class GameSaveFileReader : IDisposable
  {
    private readonly ByteOrderedBinaryReader _reader;
    private readonly ICharacterLibrary _characterLibrary;

    public GameSaveFileReader(ICharacterLibrary characterLibrary, string filePath)
    {
      _reader =  new ByteOrderedBinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read), ByteOrder.BigEndian);
      _characterLibrary = characterLibrary;
    }

    public GameSaveFileReader(ICharacterLibrary characterLibrary, Stream stream)
    {
      _reader = new ByteOrderedBinaryReader(stream, ByteOrder.BigEndian);
      _characterLibrary = characterLibrary;
    }

    public byte[] ReadBytes(long offset, int numberOfBytes) => _reader.ReadBytes(offset, numberOfBytes);
    public string ReadString(long offset, int stringLength)
    {
      var chars = Enumerable.Empty<char>();
      for (int i = 0; i < stringLength; i++)
        chars = chars.Append(ReadChar(offset + 2 * i));

      return new string(chars.ToArray()).TrimEnd();
    }

    public ushort ReadUInt(long offset, int bitOffset, int numberOfBits)
    {
      var numberOfBytesToRead = UIntInterpret.GetNumberOfBytesNeeded(bitOffset, numberOfBits);
      var bytesToReadFrom = _reader.ReadBytes(offset, numberOfBytesToRead);
      var valueBits = UIntInterpret.GetValueBits(bytesToReadFrom.ToArray(), bitOffset, numberOfBits);
      return valueBits.ToUInt16();
    }

    public short ReadSInt(long offset, int bitOffset, int numberOfBits)
    {
      var isNegative = ReadBool(offset, bitOffset);
      var value = ReadUInt(offset, bitOffset + 1, numberOfBits - 1);
      return isNegative
        ? (short)(value * -1)
        : (short)value;
    }

    public char ReadChar(long offset) => _characterLibrary[ReadUInt(offset, 0, 16)];
    public bool ReadBool(long offset, int bitOffset) => ReadUInt(offset, bitOffset, 1) == 1;

    public void Dispose() => _reader.Dispose();
  }
}
