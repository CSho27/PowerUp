using System;

namespace PowerUp.GameSave.IO
{
  public static class UIntInterpret
  {
    // We need the bytes for every bit touched by this uint, so if the divided value is a fraction, we always want to round up
    public static (long offsetToStartAt, int numberOfBytesToRead) GetBytesToRead(long offset, int bitOffset, int numberOfBits, ByteOrder byteOrder)
    {
      var numberOfBytesActuallyNeeded = (int)Math.Ceiling((bitOffset + numberOfBits) / (double)BinaryUtils.BYTE_LENGTH);
      if(byteOrder == ByteOrder.BigEndian)
        return (offset, numberOfBytesActuallyNeeded);

      var isOffsetOdd = offset % 2 == 1;
      var offsetToStartAt = isOffsetOdd
        ? offset - 1
        : offset;
      var numberOfBytesActuallyNeededIsOdd = numberOfBytesActuallyNeeded % 2 == 1;
      var numberOfBytesToRead = numberOfBytesActuallyNeeded > 1 && numberOfBytesActuallyNeededIsOdd != isOffsetOdd
          ? numberOfBytesActuallyNeeded + 1
          : numberOfBytesActuallyNeeded;
      return (offsetToStartAt, numberOfBytesToRead);
    }
    

    public static byte[] GetValueBits(long offset, byte[] bytesToReadFrom, int bitOffset, int numberOfBits, ByteOrder byteOrder)
    {
      var valueBits = new byte[numberOfBits];
      bool isOffsetOdd = offset % 2 == 1;
      int byteIndex = isOffsetOdd && byteOrder == ByteOrder.LittleEndian
        ? 1
        : 0;
      int bitsRead = 0;
      int bitOfCurrentByte = bitOffset;
      byte currentByte = bytesToReadFrom[GetByteIndexToReadFrom(byteOrder, byteIndex)];
      while (bitsRead < numberOfBits)
      {
        if (bitOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          byteIndex++;
          currentByte = bytesToReadFrom[GetByteIndexToReadFrom(byteOrder, byteIndex)];
          bitOfCurrentByte = 0;
        }

        valueBits[bitsRead] = currentByte.GetBit(bitOfCurrentByte);

        bitsRead++;
        bitOfCurrentByte++;
      }

      return valueBits;
    }

    public static int GetByteIndexToReadFrom(ByteOrder byteOrder, int byteIndex)
    {
      if (byteOrder == ByteOrder.BigEndian)
        return byteIndex;

      var isEvenIndex = byteIndex % 2 == 0;
      return isEvenIndex
        ? byteIndex + 1
        : byteIndex - 1;
    }
  }

  public enum ByteOrder { BigEndian, LittleEndian };
}
