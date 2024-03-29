﻿using PowerUp.Libraries;
using System;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public class GameSaveFileReader : IDisposable
  {
    private readonly ByteOrderedBinaryReader _reader;
    private readonly ICharacterLibrary _characterLibrary;

    public GameSaveFileReader(ICharacterLibrary characterLibrary, string filePath, ByteOrder byteOrder)
    {
      _reader =  new ByteOrderedBinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read), byteOrder);
      _characterLibrary = characterLibrary;
    }

    public GameSaveFileReader(ICharacterLibrary characterLibrary, Stream stream, ByteOrder byteOrder)
    {
      _reader = new ByteOrderedBinaryReader(stream, byteOrder);
      _characterLibrary = characterLibrary;
    }

    public byte[] ReadBytes(long offset, int numberOfBytes, bool traverseSequentially) 
      => _reader.ReadBytes
          ( offset
          , numberOfBytes
          , translateToStartOfTwoByteChunk: false
          , twoByteCheckStartsAtEvenOffset: false
          , traverseSequentially: traverseSequentially
          );
    public string ReadString(long offset, int stringLength)
    {
      var chars = Enumerable.Empty<char>();
      for (int i = 0; i < stringLength; i++)
        chars = chars.Append(ReadChar(offset + 2 * i));

      return new string(chars.ToArray()).TrimEnd();
    }

    public ushort ReadUInt(long offset, int bitOffset, int numberOfBits, bool translateToStartOfTwoByteChunk, bool twoByteChunkStartsAtEvenOffset)
    {
      var numberOfBytesToRead = UIntInterpret.GetNumberOfBytesNeeded(bitOffset, numberOfBits);
      var bytesToReadFrom = _reader.ReadBytes(offset, numberOfBytesToRead, translateToStartOfTwoByteChunk, twoByteChunkStartsAtEvenOffset, traverseSequentially: false);
      var valueBits = UIntInterpret.GetValueBits(bytesToReadFrom, bitOffset, numberOfBits);
      return valueBits.ToUInt16();
    }

    public short ReadSInt(long offset, int bitOffset, int numberOfBits, bool translateToStartOfTwoByteChunk, bool twoByteChunkStartsAtEvenOffset)
    {
      var isNegative = ReadBool(offset, bitOffset);
      var value = ReadUInt(offset, bitOffset + 1, numberOfBits - 1, translateToStartOfTwoByteChunk, twoByteChunkStartsAtEvenOffset);
      return isNegative
        ? (short)(value * -1)
        : (short)value;
    }

    public char ReadChar(long offset) => _characterLibrary[ReadUInt(offset, 0, 16, translateToStartOfTwoByteChunk: true, twoByteChunkStartsAtEvenOffset: false)];
    public bool ReadBool(long offset, int bitOffset) => ReadUInt(offset, bitOffset, 1, translateToStartOfTwoByteChunk: false, twoByteChunkStartsAtEvenOffset: false) == 1;

    public void Dispose() => _reader.Dispose();
  }
}
