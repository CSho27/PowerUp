namespace PowerUp.GameSave.IO
{
  public static class ByteOrderInterpreter
  {
    public static long TranslateOffset(long offset, ByteOrder byteOrder)
    {
      if (byteOrder == ByteOrder.BigEndian)
        return offset;

      var offsetIsEven = offset % 2 == 0;
      return offsetIsEven
        ? offset + 1
        : offset - 1;
    }

    public static long GetNextByteOffset(long offset, ByteOrder byteOrder)
    {
      if(byteOrder == ByteOrder.BigEndian)
        return offset + 1;

      var offsetIsEven = offset % 2 == 0;
      return offsetIsEven
        ? offset + 3
        : offset - 1;
    }
  }
}
