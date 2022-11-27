namespace PowerUp.GameSave.IO
{
  public static class ByteOrderInterpreter
  {
    public static long TranslateOffset(long offset, ByteOrder byteOrder, bool dataStartsOnEven)
    {
      if (byteOrder == ByteOrder.BigEndian)
        return offset;

      var offsetIsOdd = offset % 2 == 1;
      if(dataStartsOnEven)
      {
        return !offsetIsOdd
          ? offset + 1
          : offset - 1;
      }
      else
      {
        return offsetIsOdd
          ? offset + 1
          : offset - 1;
      }
    }

    public static long GetNextByteOffset(long offset, ByteOrder byteOrder, bool dataStartsOnEven, bool traverseSequentially)
    {
      if(byteOrder == ByteOrder.BigEndian || traverseSequentially)
        return offset + 1;

      var offsetIsOdd = offset % 2 == 1;
      if (dataStartsOnEven)
      {
        return !offsetIsOdd
          ? offset + 3
          : offset - 1;
      }
      else
      {
        return offsetIsOdd
          ? offset + 3
          : offset - 1;
      }
    }
  }
}
