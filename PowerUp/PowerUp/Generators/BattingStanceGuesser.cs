using PowerUp.Libraries;
using System.Collections.Generic;

namespace PowerUp.Generators
{
  public interface IBattingStanceGuesser
  {
    public int GuessBattingStance(int year, int contact, int power);
  }

  public class BattingStanceGuesser : IBattingStanceGuesser
  {
    private readonly IBattingStanceLibrary _battingStanceLibrary;

    public BattingStanceGuesser(IBattingStanceLibrary battingStanceLibrary)
    {
      _battingStanceLibrary = battingStanceLibrary;
    }

    public int GuessBattingStance(int year, int contact, int power)
    {
      if (year < 1900)
        return ProbabilityUtils.PickRandomItem(GetPre1900Weights());
      else if (year < 1920)
        return ProbabilityUtils.PickRandomItem(Get1900To1920Weights());
      else if (year < 1940)
        return ProbabilityUtils.PickRandomItem(Get1920To1940Weights());
      else if (year < 1960)
        return ProbabilityUtils.PickRandomItem(Get1940To1960Weights());
      else if (year < 1980)
        return ProbabilityUtils.PickRandomItem(Get1960To1980Weights(contact, power));
      else if (year < 2000)
        return ProbabilityUtils.PickRandomItem(Get1980To2000Weights(contact, power));
      else if (year < 2020)
        return ProbabilityUtils.PickRandomItem(Get2000To2020Weights(contact, power));
      else
        return ProbabilityUtils.PickRandomItem(GetPost2020Weights(contact, power));
    }

    public IEnumerable<(int battingStanceId, double weight)> GetPre1900Weights()
    {
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD1_KEY], .27);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .25);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED1_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .03);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .03);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get1900To1920Weights()
    {
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD1_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .25);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .20);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED1_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .10);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get1920To1940Weights()
    {
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD1_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .25);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED1_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .10);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get1940To1960Weights()
    {
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD1_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED1_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .20);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get1960To1980Weights(int contact, int power)
    {
      if(power > 150)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN5_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD13_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING1_KEY], .05);
      }
      else if(contact > 9)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED2_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .05);
      }

      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .20);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .20);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get1980To2000Weights(int contact, int power)
    {
      if (power > 150)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN5_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD13_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING1_KEY], .05);
      }
      else if (contact > 9)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED2_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING3_KEY], .05);
      }

      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .15);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD14_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD10_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD7_KEY], .05);
    }

    public IEnumerable<(int battingStanceId, double weight)> Get2000To2020Weights(int contact, int power)
    {
      if (power > 150)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN5_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD13_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN13_KEY], .05);
      }
      else if (contact > 9)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED2_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CROUCHING2_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN19_KEY], .05);
      }

      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .03);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .10);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD14_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD10_KEY], .03);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD7_KEY], .05);

      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN6_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN8_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN11_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN14_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN18_KEY], .02);
    }

    public IEnumerable<(int battingStanceId, double weight)> GetPost2020Weights(int contact, int power)
    {
      if (power > 150)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN5_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD13_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN13_KEY], .05);
      }
      else if (contact > 9)
      {
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED2_KEY], .15);
        yield return (_battingStanceLibrary[BattingStanceLibrary.CLOSED6_KEY], .12);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN18_KEY], .08);
        yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN19_KEY], .05);
      }

      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD2_KEY], .03);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD3_KEY], .8);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD4_KEY], .8);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD6_KEY], .8);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD14_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD10_KEY], .03);
      yield return (_battingStanceLibrary[BattingStanceLibrary.STANDARD7_KEY], .05);

      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN3_KEY], .04);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN6_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN8_KEY], .02);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN11_KEY], .04);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN13_KEY], .05);
      yield return (_battingStanceLibrary[BattingStanceLibrary.OPEN14_KEY], .02);
    }
  }
}
