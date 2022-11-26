using System;
using System.IO;
using System.Linq;

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

    public byte[] ReadBytes(long offset, int numberOfBytes) 
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      var bytes = new byte[numberOfBytes];
      for (int i = 0; i < numberOfBytes; i++)
        bytes[i] = ReadNextByte();

      return bytes.ToArray();
    }

    private byte ReadNextByte()
    {
      _stream.Seek(ByteOrderInterpreter.GetNextByteOffset(_stream.Position, _byteOrder), SeekOrigin.Begin);
      return _reader.ReadByte();
    }

    public void Dispose()
    {
      _reader.Dispose();
      _stream.Dispose();
    }
  }
}
