namespace PowerUp.Entities.Players
{
  public class SpecialAbilities
  {
    public GeneralSpecialAbilities General { get; set; } = new GeneralSpecialAbilities();
    public HitterSpecialAbilities Hitter { get; set; } = new HitterSpecialAbilities();
    public PitcherSpecialAbilities Pitcher { get; set; } = new PitcherSpecialAbilities();
  }

  public class GeneralSpecialAbilities
  {
    public bool Star { get; set; }
    public Special2_4 Durability { get; set; }
    public SpecialPositive_Negative Morale { get; set; }
  }

  public class HitterSpecialAbilities
  {
    public SituationalHittingSpecialAbilities SituationalHitting { get; set; } = new SituationalHittingSpecialAbilities();
    public HittingApproachSpecialAbilities HittingApproach { get; set; } = new HittingApproachSpecialAbilities();
    public SmallBallSpecialAbilities SmallBall { get; set; } = new SmallBallSpecialAbilities();
    public BaseRunningSpecialAbilities BaseRunning { get; set; } = new BaseRunningSpecialAbilities();
    public FieldingSpecialAbilities Fielding { get; set; } = new FieldingSpecialAbilities();
  }

  public class SituationalHittingSpecialAbilities
  {
    public Special2_4 Consistency { get; set; }
    public Special1_5 VersusLefty { get; set; }
    public bool TableSetter { get; set; }
    public bool BackToBackHitter { get; set; }
    public bool HotHitter { get; set; }
    public bool RallyHitter { get; set; }
    public bool GoodPinchHitter { get; set; }
    public BasesLoadedHitter? BasesLoadedHitter { get; set; }
    public WalkOffHitter? WalkOffHitter { get; set; }
    public Special1_5 ClutchHitter { get; set; }
  }

  public class HittingApproachSpecialAbilities
  {
    public bool ContactHitter { get; set; }
    public bool PowerHitter { get; set; }
    public SluggerOrSlapHitter SluggerOrSlapHitter { get; set; }
    public bool PushHitter { get; set; }
    public bool PullHitter { get; set; }
    public bool SprayHitter { get; set; }
    public bool FirstballHitter { get; set; }
    public AggressiveOrPatient AggressiveOrPatientHitter { get; set; }
    public bool Refined { get; set; }
    public bool ToughOut { get; set; }
    public bool Intimidator { get; set; }
    public bool Sparkplug { get; set; }
  }

  public class SmallBallSpecialAbilities
  {
    public SpecialPositive_Negative SmallBall { get; set; }
    public BuntingAbility Bunting { get; set; }
    public InfieldHittingAbility InfieldHitting { get; set; }
  }

  public class BaseRunningSpecialAbilities
  {
    public Special2_4 BaseRunning { get; set; }
    public Special2_4 Stealing { get; set; }
    public bool AggressiveRunner { get; set; }
    public AggressiveOrPatient AggressiveOrPatientBaseStealer { get; set; }
    public bool ToughRunner { get; set; }
    public bool BreakupDoublePlay { get; set; }
    public bool HeadFirstSlide { get; set; }
  }

  public class FieldingSpecialAbilities
  {
    public bool GoldGlover { get; set; }
    public bool SpiderCatch { get; set; }
    public bool BarehandCatch { get; set; }
    public bool AggressiveFielder { get; set; }
    public bool PivotMan { get; set; }
    public bool ErrorProne { get; set; }
    public bool GoodBlocker { get; set; }
    public CatchingAbility CatchingAbility { get; set; }
    public Special2_4 Throwing { get; set; }
    public bool CannonArm { get; set; }
    public bool TrashTalk { get; set; }
  }

  public class PitcherSpecialAbilities
  {
    public SituationalPitchingSpecialAbilities SituationalPitching { get; set; } = new SituationalPitchingSpecialAbilities();
    public PitchingDemeanorSpecialAbilities Demeanor { get; set; } = new PitchingDemeanorSpecialAbilities();
    public PitchingMechanicsSpecialAbilities PitchingMechanics { get; set; } = new PitchingMechanicsSpecialAbilities();
    public PitchQuailitiesSpecialAbilities PitchQuailities { get; set; } = new PitchQuailitiesSpecialAbilities();
  }

  public class SituationalPitchingSpecialAbilities
  {
    public Special2_4 Consistency { get; set; }
    public Special2_4 VersusLefty { get; set; }
    public Special2_4 Poise { get; set; }
    public bool PoorVersusRunner { get; set; }
    public Special2_4 WithRunnersInSocringPosition { get; set; }
    public bool SlowStarter { get; set; }
    public bool StarterFinisher { get; set; }
    public bool ChokeArtist { get; set; }
    public bool Sandbag { get; set; }
    public bool DoctorK { get; set; }
    public bool Walk { get; set; }
    public SpecialPositive_Negative Lucky { get; set; }
    public Special2_4 Recovery { get; set; }
  }

  public class PitchingDemeanorSpecialAbilities
  {
    public bool Intimidator { get; set; }
    public BattlerPokerFace? BattlerPokerFace { get; set; }
    public bool HotHead { get; set; }
  }

  public class PitchingMechanicsSpecialAbilities
  {
    public bool GoodDelivery { get; set; }
    public Special2_4 Release { get; set; }
    public bool GoodPace { get; set; }
    public bool GoodReflexes { get; set; }
    public bool GoodPickoff { get; set; }
  }

  public class PitchQuailitiesSpecialAbilities
  {
    public PowerOrBreakingBallPitcher? PowerOrBreakingBallPitcher { get; set; }
    public Special2_4 FastballLife { get; set; }
    public Special2_4 Spin { get; set; }
    public SpecialPositive_Negative SafeOrFatPitch { get; set; }
    public SpecialPositive_Negative GroundBallOrFlyBallPitcher { get; set; }
    public bool GoodLowPitch { get; set; }
    public bool Gyroball { get; set; }
    public bool ShuttoSpin { get; set; }
  }
}
