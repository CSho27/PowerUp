using PowerUp.Entities.Players;
using PowerUp.Libraries;
using System.Collections.Generic;

namespace PowerUp.Generators
{
  public interface IPitchingMechanicsGuesser
  {
    int GuessPitchingMechanics(int year, PitcherType pitcherType);
  }

  public class PitchingMechanicsGuesser : IPitchingMechanicsGuesser
  {
    private readonly IPitchingMechanicsLibrary _pitchingMechanicsLibrary;

    public PitchingMechanicsGuesser(IPitchingMechanicsLibrary pitchingMechanicsLibrary)
    {
      _pitchingMechanicsLibrary = pitchingMechanicsLibrary;
    }

    public int GuessPitchingMechanics(int year, PitcherType pitcherType)
    {
      if (year < 1930)
        return ProbabilityUtils.PickRandomItem(GetPre1930Weights());
      else if (year < 2000)
        return ProbabilityUtils.PickRandomItem(Get1930To2000Weights(pitcherType));
      else
        return ProbabilityUtils.PickRandomItem(GetPost2000Weights(pitcherType));
    }

    public IEnumerable<(int pitchingMechanicsId, double weight)> GetPre1930Weights()
    {
      yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS8_KEY], .60);
      yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.SIDEARM1_KEY], .20);
      yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.SIDEARM2_KEY], .20);
    }

    public IEnumerable<(int pitchingMechanicsId, double weight)> Get1930To2000Weights(PitcherType pitcherType)
    {
      if(pitcherType == PitcherType.Starter)
      {
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS8_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS9_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND2_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND9_KEY], .20); 
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND14_KEY], .20);
      }
      else
      {
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS12_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS18_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND16_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND17_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.SIDEARM6_KEY], .20);
      }
    }

    public IEnumerable<(int pitchingMechanicsId, double weight)> GetPost2000Weights(PitcherType pitcherType)
    {
      if (pitcherType == PitcherType.Starter)
      {
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS16_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS9_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND17_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND9_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND14_KEY], .20);
      }
      else
      {
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS12_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary._3_QUARTERS5_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND16_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.OVERHAND17_KEY], .20);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.SIDEARM6_KEY], .17);
        yield return (_pitchingMechanicsLibrary[PitchingMechanicsLibrary.SUBMARINE1_KEY], .03);
      }
    }
  }
}
