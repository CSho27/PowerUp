using System;

namespace PowerUp.Entities.Players
{
  public enum Position
  {
    [Abbrev("P"), DisplayName("Pitcher")]
    Pitcher = 1,
    [Abbrev("C"), DisplayName("Catcher")]
    Catcher,
    [Abbrev("1B"), DisplayName("First Base")]
    FirstBase,
    [Abbrev("2B"), DisplayName("Second Base")]
    SecondBase,
    [Abbrev("3B"), DisplayName("Third Base")]
    ThirdBase,
    [Abbrev("SS"), DisplayName("Shortstop")]
    Shortstop,
    [Abbrev("LF"), DisplayName("Left Field")]
    LeftField,
    [Abbrev("CF"), DisplayName("Center Field")]
    CenterField,
    [Abbrev("RF"), DisplayName("Right Field")]
    RightField,
    [Abbrev("DH"), DisplayName("Designated Hitter")]
    DesignatedHitter,
  }

  public enum PositionType
  {
    Pitcher,
    Catcher,
    Infielder,
    Outfielder
  }

  public static class PositionExtensions
  {
    public static PositionType GetPositionType(this Position position)
    {
      switch (position)
      {
        case Position.Catcher:
          return PositionType.Catcher;
        case Position.Pitcher:
          return PositionType.Pitcher;
        case Position.FirstBase:
        case Position.SecondBase:
        case Position.ThirdBase:
        case Position.Shortstop:
          return PositionType.Infielder;
        case Position.LeftField:
        case Position.CenterField:
        case Position.RightField:
        case Position.DesignatedHitter:
          return PositionType.Outfielder;
        default:
          throw new InvalidOperationException("Invalid position");
      }
    }
  }
}
