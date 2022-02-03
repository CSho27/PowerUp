using System.IO;

namespace PowerUp.GameSave
{
  public class PeekingBinaryWriter
  {
    private readonly Stream _stream;
    private readonly BinaryReader _reader;
    private readonly BinaryWriter _writer;

    public PeekingBinaryWriter(Stream stream)
    {
      _stream = stream;
      _reader = new BinaryReader(stream);
      _writer = new BinaryWriter(stream);
    }

    public byte PeekByte()
    {
      var @byte = _reader.ReadByte();
      _stream.Seek(-1, SeekOrigin.Current);
      return @byte;
    }

    public void Write(byte @byte) => _writer.Write(@byte);
  }
}
