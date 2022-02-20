using PowerUp.DebugUtils;
using PowerUp.Libraries;
using System;
using System.IO;

namespace PowerUp.GameSave.IO
{
  public class GameSaveFileWriter : IDisposable
  {
    private readonly Stream _stream;
    private readonly ICharacterLibrary _characterLibrary;

    public GameSaveFileWriter(ICharacterLibrary characterLibrary, string filePath)
    {
      _stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
      _characterLibrary = characterLibrary;
    }

    public void WriteString(long offset, int stringLength, string @string)
    {
      if (@string.Length > stringLength)
        throw new ArgumentException("String longer than specified length", nameof(@string));

      for (int i = 0; i < stringLength; i++)
        WriteChar(offset + 2 * i, i < @string.Length ? @string[i] : ' ');
    }

    public void WriteUInt(long offset, int bitOffset, int numberOfBits, ushort @uint)
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      var writer = new PeekingBinaryWriter(_stream);
      var valueBits = @uint.ToBitArray(numberOfBits);

      int bitsWritten = 0;
      int bitsOfCurrentByte = bitOffset;
      byte currentByte = writer.PeekByte();
      while (bitsWritten < numberOfBits)
      {
        if (bitsOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          writer.Write(currentByte);
          currentByte = writer.PeekByte();
          bitsOfCurrentByte = 0;
        }

        currentByte = currentByte.SetBit(bitsOfCurrentByte, valueBits[bitsWritten]);

        bitsWritten++;
        bitsOfCurrentByte++;
      }
      writer.Write(currentByte);
    }

    public void WriteSInt(long offset, int bitOffset, int numberOfBits, short sint)
    {
      var isNegative = sint < 0;
      WriteBool(offset, bitOffset, isNegative);
      WriteUInt(offset, bitOffset + 1, numberOfBits - 1, (ushort)Math.Abs(sint));
    }

    public void WriteChar(long offset, char @char)
    {
      var charNum = _characterLibrary[@char];
      if (charNum.HasValue)
        WriteUInt(offset, 0, 16, charNum.Value);
    }

    public void WriteBool(long offset, int bitOffset, bool @bool) => WriteUInt(offset, bitOffset, 1, (ushort)(@bool ? 1 : 0));

    public void Dispose() => _stream.Dispose();
  }
}
