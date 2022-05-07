using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp
{
  public class SetUtils
  {
    public static SetDiffResult<TA, TB> GetDiff<TA, TB>(IEnumerable<TA> setA, IEnumerable<TB> setB, Func<TA, TB, bool> comparator)
    {
      return new SetDiffResult<TA, TB>(
        aNotInB: setA.Where(a => !setB.Any(b => comparator(a, b))), 
        bNotInA: setB.Where(b => !setA.Any(a => comparator(a, b))),
        matches: setA
          .Where(a => setB.Any(b => comparator(a, b)))
          .Select(a => (a: a, b: setB.Single(b => comparator(a, b)) ))
      );
    } 
  }

  public class SetDiffResult<TA, TB>
  {
    public IEnumerable<TA> ANotInB { get; set; }
    public IEnumerable<TB> BNotInA { get; set; }
    public IEnumerable<(TA a, TB b)> Matches { get; set; }

    public SetDiffResult(
      IEnumerable<TA> aNotInB,
      IEnumerable<TB> bNotInA,
      IEnumerable<(TA, TB)> matches
    )
    {
      ANotInB = aNotInB.ToList();
      BNotInA = bNotInA.ToList();
      Matches = matches.ToList();
    }
  }
}
