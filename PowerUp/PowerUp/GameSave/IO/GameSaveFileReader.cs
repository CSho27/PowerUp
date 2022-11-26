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

    public string ReadString(long offset, int stringLength)
    {
      var chars = Enumerable.Empty<char>();
      for (int i = 0; i < stringLength; i++)
        chars = chars.Append(ReadChar(offset + 2 * i));

      return new string(chars.ToArray()).TrimEnd();
    }

    public ushort ReadUInt(long offset, int bitOffset, int numberOfBits)
    {
      var reader = GetReaderFor(offset);
      var valueBits = new byte[numberOfBits];

      var numberOfBytesToRead = UIntInterpret.GetNumberOfBytesNeeded(bitOffset, numberOfBits);
      var bytesToReadFrom = new List<byte>();
      for(int i = 0; i<= numberOfBytesToRead; i++)
        bytesToReadFrom.Add(reader.ReadByte());

      int byteIndex = 0;
      int bitsRead = 0;
      int bitOfCurrentByte = bitOffset;
      byte currentByte = bytesToReadFrom[0];
      while (bitsRead < numberOfBits)
      {
        if (bitOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          currentByte = bytesToReadFrom[++byteIndex];
          bitOfCurrentByte = 0;
        }

        valueBits[bitsRead] = currentByte.GetBit(bitOfCurrentByte);

        bitsRead++;
        bitOfCurrentByte++;
      }

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

    private BinaryReader GetReaderFor(long offset)
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      return new BinaryReader(_stream);
    }

    public void Dispose() => _stream.Dispose();
  }
}
