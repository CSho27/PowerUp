using System;

namespace PowerUp
{
  public static class IntegerExtensions
  {
    public static int CapAt(this int value, int cap) => value > cap
      ? cap
      : value;

    public static int MinAt(this int value, int min) => value < min
      ? min
      : value;

    public static bool? ToNullableBool(this int? value) 
      => value.HasValue 
        ? Convert.ToBoolean(value) 
        : null;
  }
}
