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

    public static string ToPercentDisplay(this double? value) => value.HasValue
      ? ToPercentDisplay(value.Value)
      : string.Empty;
    public static string ToPercentDisplay(this double value) => $"{value.ToPercent()}%";

    public static int ToPercent(this double? value) => value.HasValue
      ? ToPercent(value.Value)
      : 0;
    public static int ToPercent(this double value)
    {
      var percentValue = value * 100;
      return percentValue.Round();
    }
  }
}
