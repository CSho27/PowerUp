using System;
using System.IO;

namespace PowerUp.GameSave.IO
{
  public class PeekingBinaryWriter : IDisposable
  {
    private readonly Stream _stream;
    private readonly BinaryReader _reader;
    private readonly BinaryWriter _writer;
    private readonly ByteOrder _byteOrder;

    public PeekingBinaryWriter(Stream stream, ByteOrder byteOrder)
    {
      _stream = stream;
      _reader = new BinaryReader(stream);
      _writer = new BinaryWriter(stream);
      _byteOrder = byteOrder;
    }

    public byte PeekByte()
    {
      var @byte = _reader.ReadByte();
      _stream.Seek(-1, SeekOrigin.Current);
      return @byte;
    }

    public void Write(byte @byte, bool startsOnEven)
    {
      var nextByteOffset = ByteOrderInterpreter.GetNextByteOffset(_stream.Position, _byteOrder, startsOnEven, traverseSequentially: false);
      _writer.Write(@byte);
      _stream.Seek(nextByteOffset, SeekOrigin.Begin);
    }

    public void Dispose()
    {
      _reader.Dispose();
      _writer.Dispose();
      _stream.Dispose();
    }
  }
}
