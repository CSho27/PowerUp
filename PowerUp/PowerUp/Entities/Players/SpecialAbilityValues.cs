using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Entities.Players
{
  public enum Special1_5
  {
    [Abbrev("1")]
    One = -2,
    [Abbrev("2")]
    Two = -3,
    [Abbrev("3")]
    Three = 0,
    [Abbrev("4")]
    Four,
    [Abbrev("5")]
    Five
  }

  public enum Special2_4
  {
    [Abbrev("2")]
    Two = -1,
    [Abbrev("3")]
    Three,
    [Abbrev("4")]
    Four,
  }

  public enum SpecialPositive_Negative
  {
    Negative = -1,
    Neutral,
    Positive
  }

  public enum BasesLoadedHitter
  {
    [DisplayName("Hits Well")]
    HitsWell = 1,
    [DisplayName("Homers Often")]
    HomersOften,
    // Based on the values for Walk-Off Hitter
    // I think that's what 3 means, and the description the game gives is a bug
    [DisplayName("Hits Well and Homers Often")]
    HitsWellAndHomersOften
  }

  public enum WalkOffHitter
  {
    [DisplayName("Hits Well")]
    HitsWell = 1,
    [DisplayName("Homers Often")]
    HomersOften,
    [DisplayName("Hits Well and Homers Often")]
    HitsWellAndHomersOften
  }

  public enum SluggerOrSlapHitter
  {
    [DisplayName("Slap Hitter")]
    SlapHitter = -1,
    Slugger = 1
  }

  public enum AggressiveOrPatient
  {
    Patient = -1,
    Aggressive = 0
  }

  public enum BuntingAbility
  {
    Good = 1,
    [DisplayName("Bunt Master")]
    BuntMaster
  }

  public enum InfieldHittingAbility
  {
    [DisplayName("Good Infield Hitter")]
    GoodInfieldHitter = 1,
    [DisplayName("Great Infield Hitter")]
    GreatInfieldHitter
  }

  public enum CatchingAbility
  {
    [DisplayName("Good Catcher")]
    GoodCatcher = 1,
    [DisplayName("Great Catcher")]
    GreatCatcher
  }

  public enum BattlerPokerFace
  {
    Battler,
    [DisplayName("Poker Face")]
    PokerFace
  }

  public enum PowerOrBreakingBallPitcher
  {
    [DisplayName("Breaking Ball Pitcher")]
    BreakingBall = -1,
    [DisplayName("Power Pitcher")]
    Power = 1
  }
}
