using PowerUp.Entities.Players;

namespace PowerUp.Mappers
{
  public static class PitcherTypeMapper
  {
    public static (bool isStarter, bool isReliever, bool isCloser) ToGSPitcherType(this PitcherType pitcherType)
    {
      return (
        isStarter: pitcherType == PitcherType.Starter,
        isReliever: pitcherType == PitcherType.Reliever,
        isCloser: pitcherType == PitcherType.Closer
      );
    }

    public static PitcherType ToPitcherType(bool isStarter, bool isReliever, bool isCloser)
    {
      if (isStarter)
        return PitcherType.Starter;
      else if (isReliever)
        return PitcherType.Reliever;
      else if (isCloser)
        return PitcherType.Closer;
      else
        return PitcherType.SwingMan;
    }
  }
}
