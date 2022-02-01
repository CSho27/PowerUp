using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerUp.DebugUtils
{
  public static class BinaryUtils
  {
    private const int BYTE_LENGTH = 8;

    public static string ToBitString(this byte @byte)
    {
      var bits = new byte[8];
      for (int i = 0; i < bits.Length; i++)
        bits[i] = @byte.GetBit(i);

      return $"[{string.Join(null, bits)}]";
    }

    public static string ToBitString(this byte[] bytes) => string.Join(" | ", bytes.Select(b => b.ToBitString()));

    public static byte GetBit(this byte @byte, int position)
    {
      var shift = BYTE_LENGTH - position - 1;
      return (byte)((@byte >> shift) & 1);
    }

    public static ushort GetBitsValue(this byte @byte, int start, int length)
    {
      var bits = new byte[length];
      for (int i = 0; i < length; i++)
        bits[i] = GetBit(@byte, i + start);
      return bits.ToUInt16();
    }


    public static ushort ToUInt16(this byte[] bits)
    {
      var value = 0;
      var digits = 0;
      for (int i = bits.Length - 1; i >= 0; i--)
      {
        value += (int)Math.Pow(2, digits) * bits[i];
        digits++;
      }
      return (ushort)value;
    }
  }
}
