using PowerUp.Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public class GameSaveFileReader : IDisposable
  {
    private readonly Stream _stream;
    private readonly ICharacterLibrary _characterLibrary;

    public GameSaveFileReader(ICharacterLibrary characterLibrary, string filePath)
    {
      _stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
      _characterLibrary = characterLibrary;
    }

    public GameSaveFileReader(ICharacterLibrary characterLibrary, Stream stream)
    {
      _stream = stream;
      _characterLibrary = characterLibrary;
    }

    public byte[] ReadBytes(long offset, int numberOfBytes)
    {
      var reader = GetReaderFor(offset);
      var bytes = Enumerable.Empty<byte>();
      for (int i = 0; i < numberOfBytes; i++)
        bytes = bytes.Append(reader.ReadByte());

      return bytes.ToArray();
    }

    public string ReadString(long offset, int stringLength, ByteOrder byteOrder)
    {
      var chars = Enumerable.Empty<char>();
      for (int i = 0; i < stringLength; i++)
        chars = chars.Append(ReadChar(offset + 2 * i, byteOrder));

      return new string(chars.ToArray()).TrimEnd();
    }

    public ushort ReadUInt(long offset, int bitOffset, int numberOfBits, ByteOrder byteOrder)
    {
      var (offsetToStartAt, numberOfBytesToRead) = UIntInterpret.GetBytesToRead(offset, bitOffset, numberOfBits, byteOrder);
      var reader = GetReaderFor(offsetToStartAt);
      var bytesToReadFrom = new List<byte>();
      for(int i = 0; i<= numberOfBytesToRead; i++)
        bytesToReadFrom.Add(reader.ReadByte());

      var valueBits = UIntInterpret.GetValueBits(offset, bytesToReadFrom.ToArray(), bitOffset, numberOfBits, byteOrder);
      return valueBits.ToUInt16();
    }

    public short ReadSInt(long offset, int bitOffset, int numberOfBits, ByteOrder byteOrder)
    {
      var isNegative = ReadBool(offset, bitOffset, byteOrder);
      var value = ReadUInt(offset, bitOffset + 1, numberOfBits - 1, byteOrder);
      return isNegative
        ? (short)(value * -1)
        : (short)value;
    }

    public char ReadChar(long offset, ByteOrder byteOrder) => _characterLibrary[ReadUInt(offset, 0, 16, byteOrder)];
    public bool ReadBool(long offset, int bitOffset, ByteOrder byteOrder) => ReadUInt(offset, bitOffset, 1, byteOrder) == 1;

    private BinaryReader GetReaderFor(long offset)
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      return new BinaryReader(_stream);
    }

    public void Dispose() => _stream.Dispose();
  }
}
