using System;

namespace PowerUp
{
  public class MathUtils
  {
    public static Func<double, double> BuildLinearGradientFunction(double max, double avg, double desiredMax, double desiredAverage)
    {
      return (double x) => desiredMax + ((desiredAverage - desiredMax) / (avg - max)) * (x - max);
    }
  }
}
