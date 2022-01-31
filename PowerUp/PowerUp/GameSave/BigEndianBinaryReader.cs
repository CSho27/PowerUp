using System;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave
{
  public class BigEndianBinaryReader : IDisposable
  {
    private readonly BinaryReader _reader;

    public BigEndianBinaryReader(Stream stream)
    {
      _reader = new BinaryReader(stream);
    }

    public ushort ReadUInt16()
    {
      var bytes = _reader.ReadBytes(2).Reverse().ToArray();
      return BitConverter.ToUInt16(bytes);
    }
    public byte ReadByte() =>_reader.ReadByte();

    public void Dispose() => _reader.Dispose();
  }
}
