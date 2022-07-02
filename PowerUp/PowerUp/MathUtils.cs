using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp
{
  public class MathUtils
  {
    public static Func<double, double> BuildLinearGradientFunction(double max, double avg, double desiredMax, double desiredAverage)
    {
      return (double x) => desiredMax + ((desiredAverage - desiredMax) / (avg - max)) * (x - max);
    }

    // This assumes that the desired line graph continues in a straight line beyond the beyond the first and last points
    public static Func<double, double> PiecewiseFunctionFor(IEnumerable<(double x, double y)> coordinates)
    {
      return (double input) => PiecewiseFunction(coordinates, input);
    }

    private static double PiecewiseFunction(IEnumerable<(double x, double y)> coordinates, double input)
    {
      var (coordinate1, coordinate2) = FindCoordinatesBetween(coordinates, input);
      var slope = (coordinate2.y - coordinate1.y) / (coordinate2.x - coordinate1.x);
      var intercept = coordinate1.y - slope * coordinate1.x;

      return slope * input + intercept;
    }

    private static ((double x, double y) coordinate1, (double x, double y)  coordinate2) FindCoordinatesBetween(IEnumerable<(double x, double y)> coordinates, double input)
    {
      var coordinateList = coordinates.OrderBy(c => c.x).ToList();
      if (coordinateList.Count < 2)
        throw new InvalidOperationException("Coordinate list must contain at least 2 coordinates to generate a piecewise fuinction");

      if(coordinateList.DistinctBy(c => c.x).Count() != coordinateList.Count)
        throw new InvalidOperationException("Coordinate list cannot have two coordinates with the same x value");

      var firstCoordinateGreaterThanInputIndex = coordinateList.FindIndex(c => c.x > input);

      // If the first x is greater than the coordinate, use first two coordinates
      if (firstCoordinateGreaterThanInputIndex == 0)
        return (coordinateList[0], coordinateList[1]);

      // If no x is greater than the input use the last two coordinates
      if (firstCoordinateGreaterThanInputIndex == -1)
        return (coordinateList[coordinateList.Count - 2], coordinateList[coordinateList.Count - 1]);

      return (coordinateList[firstCoordinateGreaterThanInputIndex - 1], coordinateList[firstCoordinateGreaterThanInputIndex]);
    }
  }
}
