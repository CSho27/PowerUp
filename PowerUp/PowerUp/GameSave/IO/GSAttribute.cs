﻿using System;
using System.Linq;
using System.Reflection;

namespace PowerUp.GameSave.IO
{
  [AttributeUsage(AttributeTargets.Property)]
  public abstract class GSAttribute : Attribute
  {
    public long Offset { get; }

    public GSAttribute(long offset)
    {
      Offset = offset;
    }
  }

  public class GSUIntAttribute : GSAttribute
  {
    public int Bits { get; }
    public int BitOffset { get; }
    public bool StartsOnEven { get; }

    public GSUIntAttribute(long offset, int bits, int bitOffset = 0, bool startsOnEven = false)
      : base(offset)
    {
      Bits = bits;
      BitOffset = bitOffset;
      StartsOnEven = startsOnEven;
    }
  }

  public class GSSIntAttribute : GSAttribute
  {
    public int Bits { get; }
    public int BitOffset { get; }

    public GSSIntAttribute(long offset, int bits, int bitOffset = 0)
      : base(offset)
    {
      Bits = bits;
      BitOffset = bitOffset;
    }
  }

  public class GSBooleanAttribute : GSAttribute
  {
    public int BitOffset { get; }

    public GSBooleanAttribute(long offset, int bitOffset) : base(offset)
    {
      BitOffset = bitOffset;
    }
  }

  public class GSStringAttribute : GSAttribute
  {
    public int StringLength { get; }

    public GSStringAttribute(long offset, int stringLength) : base(offset)
    {
      StringLength = stringLength;
    }
  }

  public class GSBytesAttribute : GSAttribute
  {
    public int NumberOfBytes { get; }

    public GSBytesAttribute(long offset, int numberOfBytes)
      : base(offset)
    {
      NumberOfBytes = numberOfBytes;
    }
  }

  public class GSArrayAttribute : GSAttribute
  {
    public long ItemLength { get; }
    public int ArrayLength { get; }

    public GSArrayAttribute(long offset, long itemLength, int arrayLength)
      : base(offset)
    {
      ItemLength = itemLength;
      ArrayLength = arrayLength;
    }
  }

  public static class GSAttributeExtensions
  {
    public static GSAttribute? GetGSAttribute(this PropertyInfo property) => (GSAttribute?)property
      .GetCustomAttributes(inherit: false)
      .SingleOrDefault(a => typeof(GSAttribute).IsAssignableFrom(a.GetType()));
  }
}
