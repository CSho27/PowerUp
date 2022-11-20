using System;
using System.Linq;

namespace PowerUp
{
  public static class ReflectionExtensions
  {
    public static object InstantiateWithEmptyConstructor(this Type type) => type.GetConstructors().First().Invoke(null);
  }
}
