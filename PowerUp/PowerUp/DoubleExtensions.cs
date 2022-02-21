using System;

namespace PowerUp
{
  public static class DoubleExtensions
  {
    public static int RoundDown(this double value) => (int)Math.Floor(value);
    public static int RoundUp(this double value) => (int)Math.Ceiling(value);
  }
}
