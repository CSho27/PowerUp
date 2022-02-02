using PowerUp.DebugUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PowerUp.GameSave
{
  public class GameSaveWriter : IDisposable
  {
    private readonly Stream _stream;

    public GameSaveWriter(string filePath)
    {
      _stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
    }

    public void WriteString(long offset, string @string)
    {
      
    }

    public void WriteUInt(long offset, int bitOffset, int numberOfBits, ushort @uint)
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      var reader = new BinaryReader(_stream);
      var writer = new BinaryWriter(_stream);
      var valueBits = @uint.ToBitArray(numberOfBits);

      int bitsWritten = 0;
      int bitsOfCurrentByte = bitOffset;
      byte currentByte = reader.ReadByte();
      while(bitsWritten < numberOfBits)
      {
        if(bitsOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          writer.Write(currentByte);
          currentByte = reader.ReadByte();
          bitsOfCurrentByte = 0;
        }

        currentByte.SetBit(bitsOfCurrentByte, valueBits[bitsWritten]);

        bitsWritten++;
        bitsOfCurrentByte++;
      }
    }

    public void Dispose() => _stream.Dispose();
  }
}
