using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp
{
  public static class AverageTargetingUtils
  {
    public static bool IsValueValid(
      double targetMin,
      double targetMax,
      double minProbableValue,
      double maxProbablevalue,
      int valuesKnown,
      int totalValues,
      double currentSum,
      double value
    )
    {
      var newSum = currentSum + value;
      var minAllowedRemainingSum = (targetMin * totalValues) - newSum;
      var maxAlllowedRemainingSum = (targetMax * totalValues) - newSum;
      var valuesUnknown = totalValues - valuesKnown - 1;
      var minRemainingSum = valuesUnknown * minProbableValue;
      var maxRemainingSum = valuesUnknown * maxProbablevalue;
      return minRemainingSum <= maxAlllowedRemainingSum
        && maxRemainingSum >= minAllowedRemainingSum;
    }
  }
}
