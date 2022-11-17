using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp
{
  public static class ProbabilityUtils
  {
    public static T PickRandomItem<T>(IEnumerable<T> items)
    {
      var rand = Random.Shared.NextDouble();
      var itemCount = items.Count();
      return PickRandomItem(items.Select(item => (item, 1.0/itemCount)));
    }

    public static T PickRandomItem<T>(IEnumerable<(T item, double weight)> itemsAndWeights)
    {
      var rand = Random.Shared.NextDouble();
      var weightNormalizingFactor = 1 / itemsAndWeights.Sum(i => i.weight);
      double accumulatedWeight = 0;
      return itemsAndWeights.SkipWhile(i =>
      {
        accumulatedWeight = accumulatedWeight + (weightNormalizingFactor * i.weight);
        return accumulatedWeight < rand;
      }).ToList().First().item;
    }
  }
}
