using PowerUp.Libraries;
using System;
using System.IO;

namespace PowerUp.GameSave.IO
{
  public class GameSaveFileWriter : IDisposable
  {
    private readonly Stream _stream;
    private readonly ICharacterLibrary _characterLibrary;
    private readonly ByteOrder _byteOrder;

    public GameSaveFileWriter(ICharacterLibrary characterLibrary, string filePath, ByteOrder byteOrder)
    {
      _stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
      _characterLibrary = characterLibrary;
      _byteOrder = byteOrder;
    }

    public void WriteString(long offset, int stringLength, string @string)
    {
      var sanitizedString = @string.RemoveAccents();
      if (sanitizedString.Length > stringLength)
        throw new ArgumentException("String longer than specified length", nameof(sanitizedString));

      for (int i = 0; i < stringLength; i++)
        WriteChar(offset + 2 * i, i < sanitizedString.Length ? sanitizedString[i] : ' ');
    }

    public void WriteUInt(
      long offset,
      int bitOffset,
      int numberOfBits,
      bool translateToStartOfTwoByteChunk,
      bool twoByteCheckStartsAtEvenOffset,
      ushort @uint
    )
    {
      var offsetToStartAt = translateToStartOfTwoByteChunk
        ? ByteOrderInterpreter.TranslateOffset(offset, _byteOrder, twoByteCheckStartsAtEvenOffset)
        : offset;
      _stream.Seek(offsetToStartAt, SeekOrigin.Begin);
      var writer = new PeekingBinaryWriter(_stream, _byteOrder);
      var valueBits = @uint.ToBitArray(numberOfBits);

      int bitsWritten = 0;
      int bitsOfCurrentByte = bitOffset;
      byte currentByte = writer.PeekByte();
      while (bitsWritten < numberOfBits)
      {
        if (bitsOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          writer.Write(currentByte, twoByteCheckStartsAtEvenOffset);
          currentByte = writer.PeekByte();
          bitsOfCurrentByte = 0;
        }

        currentByte = currentByte.SetBit(bitsOfCurrentByte, valueBits[bitsWritten]);

        bitsWritten++;
        bitsOfCurrentByte++;
      }
      writer.Write(currentByte, twoByteCheckStartsAtEvenOffset);
    }

    public void WriteSInt(
      long offset,
      int bitOffset,
      int numberOfBits,
      bool translateToStartOfTwoByteChunk,
      bool twoByteCheckStartsAtEvenOffset,
      short sint
    )
    {
      var offsetToStartAt = translateToStartOfTwoByteChunk
        ? ByteOrderInterpreter.TranslateOffset(offset, _byteOrder, twoByteCheckStartsAtEvenOffset)
        : offset;
      var isNegative = sint < 0;
      WriteBool(offsetToStartAt, bitOffset, isNegative);
      WriteUInt(
        offsetToStartAt,
        bitOffset + 1,
        numberOfBits - 1,
        translateToStartOfTwoByteChunk,
        twoByteCheckStartsAtEvenOffset,
        (ushort)Math.Abs(sint)
      );
    }

    public void WriteChar(long offset, char @char)
    {
      var charNum = _characterLibrary[@char];
      WriteUInt(
        offset, 
        0, 
        16,
        translateToStartOfTwoByteChunk: true,
        twoByteCheckStartsAtEvenOffset: false,
        charNum
      );
    }

    public void WriteBool(long offset, int bitOffset, bool @bool) 
      => WriteUInt(
          offset,
          bitOffset,
          1,
          translateToStartOfTwoByteChunk: false,
          twoByteCheckStartsAtEvenOffset: false,
          (ushort)(@bool ? 1 : 0)
         );

    public void Dispose() => _stream.Dispose();
  }
}
