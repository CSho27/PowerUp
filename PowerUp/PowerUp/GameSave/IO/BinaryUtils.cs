using System;
using System.Linq;

namespace PowerUp.GameSave.IO
{
  public static class BinaryUtils
  {
    public const int BYTE_LENGTH = 8;

    public static string ToBitString(this byte @byte, bool formatted = true)
    {
      var bits = new byte[8];
      for (int i = 0; i < bits.Length; i++)
        bits[i] = @byte.GetBit(i);

      var bitString = string.Join(null, bits);
      return formatted
        ? $"[{bitString}]"
        : bitString;
    }

    public static string ToBitString(this byte[] bytes, bool formatted = true) => string.Join(formatted ? " | " : "", bytes.Select(b => b.ToBitString(formatted)));

    public static byte GetBit(this byte @byte, int position, bool isLittleEndian = false)
    {
      var shift = isLittleEndian
        ? position
        : BYTE_LENGTH - position - 1;
      return (byte)(@byte >> shift & 1);
    }

    public static byte SetBit(this byte @byte, int position, byte newValue, bool isLittleEndian = false)
    {
      if (newValue != 0 && newValue != 1)
        throw new ArgumentException("New value can only be 0 or 1");

      var shift = isLittleEndian
        ? position
        : BYTE_LENGTH - position - 1;
      @byte ^= (byte)((-newValue ^ @byte) & 1 << shift);
      return @byte;
    }

    public static ushort ToUInt16(this byte[] bits, bool isLittleEndian = false)
    {
      if (bits.Length > 16)
        throw new ArgumentException("Number of bits exceeded 16 bit maximum");

      var bitArray = isLittleEndian
        ? bits.Reverse().ToArray()
        : bits;

      var value = 0;
      var digits = 0;
      for (int i = bitArray.Length - 1; i >= 0; i--)
      {
        value += (int)Math.Pow(2, digits) * bitArray[i];
        digits++;
      }
      return (ushort)value;
    }

    public static byte[] ToBitArray(this ushort @uint, int numberOfBits, bool isLittleEndian = false)
    {
      var bits = new byte[numberOfBits];
      var currentValue = 0;

      for (int i = 0; i < numberOfBits; i++)
      {
        var positionValue = (int)Math.Pow(2, numberOfBits - i - 1);
        if (currentValue + positionValue <= @uint)
        {
          bits[i] = 1;
          currentValue += positionValue;
        }
        else
          bits[i] = 0;
      }

      if (currentValue < @uint)
        throw new ArgumentException("uint value is too large for the number of bits specified", nameof(@uint));

      return isLittleEndian
        ? bits.Reverse().ToArray()
        : bits;
    }
  }
}
