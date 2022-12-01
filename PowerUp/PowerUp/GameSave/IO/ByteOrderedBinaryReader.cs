using System;
using System.IO;

namespace PowerUp.GameSave.IO
{
  public class ByteOrderedBinaryReader : IDisposable
  {
    private readonly Stream _stream;
    private readonly BinaryReader _reader;
    private readonly ByteOrder _byteOrder;

    public ByteOrderedBinaryReader(Stream stream, ByteOrder byteOrder)
    {
      _stream = stream;
      _reader = new BinaryReader(stream);
      _byteOrder = byteOrder;
    }

    public byte[] ReadBytes(long offset, int numberOfBytes, bool translateToStartOfTwoByteChunk, bool twoByteCheckStartsAtEvenOffset, bool traverseSequentially) 
    {
      var offsetToStartAt = translateToStartOfTwoByteChunk
        ? ByteOrderInterpreter.TranslateOffset(offset, _byteOrder, twoByteCheckStartsAtEvenOffset)
        : offset;
      _stream.Seek(offsetToStartAt, SeekOrigin.Begin);
      var bytes = new byte[numberOfBytes];
      for (int i = 0; i < numberOfBytes; i++)
        bytes[i] = ReadNextByte(twoByteCheckStartsAtEvenOffset, traverseSequentially);

      return bytes;
    }

    private byte ReadNextByte(bool startsOnEven, bool traverseSequentially)
    {
      var nextByteOffset = ByteOrderInterpreter.GetNextByteOffset(_stream.Position, _byteOrder, startsOnEven, traverseSequentially);
      var @byte = _reader.ReadByte();
      _stream.Seek(nextByteOffset, SeekOrigin.Begin);
      return @byte;
    }

    public void Dispose()
    {
      _reader.Dispose();
      _stream.Dispose();
    }
  }
}
