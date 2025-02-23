using System;

namespace PowerUp
{
  public static class Boolean
  {
    public static int ToInt(this bool value) => Convert.ToInt32(value);
  }
}
