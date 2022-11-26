using System;

namespace PowerUp.GameSave.IO
{
  public static class UIntInterpret
  {
    // We need the bytes for every bit touched by this uint, so if the divided value is a fraction, we always want to round up
    public static int GetNumberOfBytesNeeded(int bitOffset, int numberOfBits) => (int)Math.Ceiling((bitOffset + numberOfBits) / (double)BinaryUtils.BYTE_LENGTH);

    public static byte[] GetValueBits(byte[] bytesToReadFrom, int bitOffset, int numberOfBits)
    {
      var valueBits = new byte[numberOfBits];

      int byteIndex = 0;
      int bitsRead = 0;
      int bitOfCurrentByte = bitOffset;
      byte currentByte = bytesToReadFrom[0];
      while (bitsRead < numberOfBits)
      {
        if (bitOfCurrentByte >= BinaryUtils.BYTE_LENGTH)
        {
          currentByte = bytesToReadFrom[++byteIndex];
          bitOfCurrentByte = 0;
        }

        valueBits[bitsRead] = currentByte.GetBit(bitOfCurrentByte);

        bitsRead++;
        bitOfCurrentByte++;
      }

      return valueBits;
    }
  }
  public enum ByteOrder { BigEndian, LittleEndian };
}
