using System;

namespace PowerUp
{
  public static class DoubleExtensions
  {
    public static int Round(this double value) => (int)Math.Round(value);
    public static int RoundDown(this double value) => (int)Math.Floor(value);
    public static int RoundUp(this double value) => (int)Math.Ceiling(value);

    public static double CapAt(this double value, double cap) => value > cap 
      ? cap 
      : value;

    public static double MinAt(this double value, double min) => value < min
      ? min
      : value;

    public static string ToPercentDisplay(this double value)
    {
      var percentValue = value * 100;
      return $"{percentValue.Round()}%";
    }
  }
}
