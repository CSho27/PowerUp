namespace PowerUp
{
  public static class IntegerExtensions
  {
    public static int CapAt(this int value, int cap) => value > cap
      ? cap
      : value;
  }
}
