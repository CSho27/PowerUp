namespace PowerUp.Entities.Players
{
  public enum Special1_5
  {
    [Order(1), Abbrev("1")]
    One = -2,
    [Order(2), Abbrev("2")]
    Two = -3,
    [Order(3), Abbrev("3")]
    Three = 0,
    [Order(4), Abbrev("4")]
    Four,
    [Order(5), Abbrev("5")]
    Five
  }

  public enum Special2_4
  {
    [Order(1), Abbrev("2")]
    Two = -1,
    [Order(2), Abbrev("3")]
    Three,
    [Order(3), Abbrev("4")]
    Four,
  }

  public enum SpecialPositive_Negative
  {
    [Order(1)]
    Negative = -1,
    [Order(2)]
    Neutral,
    [Order(3)]
    Positive
  }

  public enum BasesLoadedHitter
  {
    [Order(1), DisplayName("Hits Well")]
    HitsWell = 1,
    [Order(2), DisplayName("Homers Often")]
    HomersOften,
    // Based on the values for Walk-Off Hitter
    // I think that's what 3 means, and the description the game gives is a bug
    [Order(3), DisplayName("Both")]
    HitsWellAndHomersOften
  }

  public enum WalkOffHitter
  {
    [Order(1), DisplayName("Hits Well")]
    HitsWell = 1,
    [Order(2), DisplayName("Homers Often")]
    HomersOften,
    [Order(3), DisplayName("Both")]
    HitsWellAndHomersOften
  }

  public enum SluggerOrSlapHitter
  {
    [Order(1), DisplayName("Slap Hitter")]
    SlapHitter = -1,
    [Order(2)]
    Slugger = 1
  }

  public enum AggressiveOrPatientHitter
  {
    [Order(1)]
    Patient = -1,
    [Order(2)]
    Aggressive = 1
  }

  public enum AggressiveOrCautiousBaseStealer
  {
    [Order(1)]
    Cautious = -1,
    [Order(2)]
    Aggressive = 1
  }

  public enum BuntingAbility
  {
    [Order(1)]
    Good = 1,
    [Order(2), DisplayName("Bunt Master")]
    BuntMaster
  }

  public enum InfieldHittingAbility
  {
    [Order(1), DisplayName("Gd Inf Hitter")]
    GoodInfieldHitter = 1,
    [Order(2), DisplayName("Grt Inf Hitter")]
    GreatInfieldHitter
  }

  public enum CatchingAbility
  {
    [Order(1), DisplayName("Good Catcher")]
    GoodCatcher = 1,
    [Order(2), DisplayName("Great Catcher")]
    GreatCatcher
  }

  public enum BattlerPokerFace
  {
    [Order(1)]
    Battler,
    [Order(2), DisplayName("Poker Face")]
    PokerFace
  }

  public enum PowerOrBreakingBallPitcher
  {
    [Order(1), DisplayName("Brk Ball Pitcher")]
    BreakingBall = -1,
    [Order(2), DisplayName("Power Pitcher")]
    Power = 1
  }

  public static class SpecialAbilityValueExtensions
  {
    public static int GetAbbrevInt(this Special1_5 value) => int.Parse(value.GetAbbrev());
    public static int GetAbbrevInt(this Special2_4 value) => int.Parse(value.GetAbbrev());
  }
}
