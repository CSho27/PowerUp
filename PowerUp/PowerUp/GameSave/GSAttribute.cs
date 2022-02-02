using System;

namespace PowerUp.GameSave
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

  public abstract class GSPartialByteAttribute : GSAttribute
  {
    public int BitOffset { get; }
    public GSPartialByteAttribute(long offset, int bitOffset) 
      : base(offset) 
    {
      BitOffset = bitOffset;
    }
  }

  public class GSBooleanAttribute : GSPartialByteAttribute
  {
    public GSBooleanAttribute(long offset, int bitOffset) : base(offset, bitOffset) { }
  }

  public class GSUInt2Attribute : GSPartialByteAttribute
  {
    public GSUInt2Attribute(long offset, int bitOffset) : base(offset, bitOffset) { }
  }

  public class GSUInt3Attribute : GSPartialByteAttribute
  {
    public GSUInt3Attribute(long offset, int bitOffset) : base(offset, bitOffset) { }
  }

  public class GSUInt4Attribute : GSPartialByteAttribute
  {
    public GSUInt4Attribute(long offset, int bitOffset) : base(offset, bitOffset) { }
  }

  public class GSUInt5Attribute : GSPartialByteAttribute
  {
    public GSUInt5Attribute(long offset, int bitOffset) : base(offset, bitOffset) { }
  }

  public class GSUInt8Attribute : GSAttribute
  {
    public GSUInt8Attribute(long offset) : base(offset) { }
  }

  public class GSUInt16Attribute : GSAttribute
  {
    public GSUInt16Attribute(long offset) : base(offset) { }
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
}
