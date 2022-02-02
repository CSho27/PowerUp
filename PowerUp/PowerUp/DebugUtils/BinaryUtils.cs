using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerUp.DebugUtils
{
  public static class BinaryUtils
  {
    public const int BYTE_LENGTH = 8;

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

    public static ushort ToUInt16(this byte[] bits)
    {
      if (bits.Length > 16) 
        throw new ArgumentException("Number of bits exceeded 16 bit maximum");

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
