﻿using System;
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

    public static byte SetBit(this byte @byte, int position, byte newValue)
    {
      if (newValue != 0 && newValue != 1) 
        throw new ArgumentException("New value can only be 0 or 1");
      
      var shift = BYTE_LENGTH - position - 1;
      if (newValue == 1)
        @byte = (byte)(@byte | (1 << shift));
      else
        @byte = (byte)(@byte & ~(1 << shift));

      return @byte;
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

    public static byte[] ToBitArray(this ushort @uint, int numberOfBits)
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

      return bits;
    }
  }
}
