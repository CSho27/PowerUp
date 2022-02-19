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
}
