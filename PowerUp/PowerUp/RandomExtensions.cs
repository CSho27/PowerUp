using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp
{
  public static class RandomExtensions
  {
    public static int Range(this Random random, int min, int max)
    {
      var rand = random.NextDouble();
      var value = min + (rand * (max - min + 1));
      return (int)value;
    }

    public static T GetRandomElement<T>(this Random random, IEnumerable<T> values)
    {
      if (!values.Any())
        throw new InvalidOperationException("List of values cannot be empty");
      var valuesList = values.ToList();
      var max = valuesList.Count-1;
      var rand = random.Range(0, max);
      return valuesList[rand];
    }
  }
}
