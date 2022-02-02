using PowerUp.DebugUtils;
using System;
using System.IO;
using System.Linq;

namespace PowerUp.GameSave
{
  public class GameSaveReader : IDisposable
  {
    private readonly Stream _stream;

    public GameSaveReader(string filePath)
    {
      _stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
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
        chars = chars.Append(ReadChar(offset + 2*i));

      return new string(chars.ToArray()).TrimEnd();
    }

    public char ReadChar(long offset) => GetChar(ReadUInt16(offset));
    public ushort ReadUInt16(long offset) => GetReaderFor(offset).ReadUInt16();
    public ushort ReadUInt8(long offset) => GetReaderFor(offset).ReadByte().GetBitsValue(0, 8);
    public ushort ReadUInt4(long offset, int bitOffset) => GetReaderFor(offset).ReadByte().GetBitsValue(bitOffset, 4);
    public ushort ReadUInt5(long offset, int bitOffset) => GetReaderFor(offset).ReadByte().GetBitsValue(bitOffset, 5);
    public ushort ReadUInt3(long offset, int bitOffset) => GetReaderFor(offset).ReadByte().GetBitsValue(bitOffset, 3);
    public ushort ReadUInt2(long offset, int bitOffset) => GetReaderFor(offset).ReadByte().GetBitsValue(bitOffset, 2);
    public bool ReadBool(long offset, int bitOffset) => GetReaderFor(offset).ReadByte().GetBitsValue(bitOffset, 1) == 1;

    private BigEndianBinaryReader GetReaderFor(long offset)
    {
      _stream.Seek(offset, SeekOrigin.Begin);
      return new BigEndianBinaryReader(_stream);
    }

    public void Dispose() => _stream.Dispose();

    private char GetChar(ushort charNum) => charNum switch
    {
      0 => ' ',
      4 => '.',
      7 => ';',
      8 => '?',
      9 => '!',
      15 => '^',
      17 => '_',
      29 => '-',
      30 => '/',
      34 => '|',
      38 => '\'',
      41 => '(',
      42 => ')',
      45 => '[',
      46 => ']',
      47 => '{',
      48 => '}',
      59 => '+',
      60 => '-',
      64 => '=',
      66 => '<',
      67 => '>',
      203 => '0',
      204 => '1',
      205 => '2',
      206 => '3',
      207 => '4',
      208 => '5',
      209 => '6',
      210 => '7',
      211 => '8',
      212 => '9',
      220 => 'A',
      221 => 'B',
      222 => 'C',
      223 => 'D',
      224 => 'E',
      225 => 'F',
      226 => 'G',
      227 => 'H',
      228 => 'I',
      229 => 'J',
      230 => 'K',
      231 => 'L',
      232 => 'M',
      233 => 'N',
      234 => 'O',
      235 => 'P',
      236 => 'Q',
      237 => 'R',
      238 => 'S',
      239 => 'T',
      240 => 'U',
      241 => 'V',
      242 => 'W',
      243 => 'X',
      244 => 'Y',
      245 => 'Z',
      252 => 'a',
      253 => 'b',
      254 => 'c',
      255 => 'd',
      256 => 'e',
      257 => 'f',
      258 => 'g',
      259 => 'h',
      260 => 'i',
      261 => 'j',
      262 => 'k',
      263 => 'l',
      264 => 'm',
      265 => 'n',
      266 => 'o',
      267 => 'p',
      268 => 'q',
      269 => 'r',
      270 => 's',
      271 => 't',
      272 => 'u',
      273 => 'v',
      274 => 'w',
      275 => 'x',
      276 => 'y',
      277 => 'z',
      _ => LogAndReturnStar(charNum)
    };

    private char LogAndReturnStar(ushort charNum) 
    { 
      Console.WriteLine($"Unkown char value: {charNum}"); 
      return '*';
    }
  }
}
