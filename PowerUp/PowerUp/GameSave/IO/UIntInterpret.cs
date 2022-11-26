using System;

namespace PowerUp.GameSave.IO
{
  public static class UIntInterpret
  {
    // We need the bytes for every bit touched by this uint, so if the divided value is a fraction, we always want to round up
    public static int GetNumberOfBytesNeeded(int bitOffset, int numberOfBits) => (int)Math.Ceiling((bitOffset + numberOfBits) / (double)BinaryUtils.BYTE_LENGTH);
  }
}
