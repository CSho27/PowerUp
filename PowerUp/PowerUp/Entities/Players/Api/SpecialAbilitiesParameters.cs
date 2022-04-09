namespace PowerUp.Entities.Players.Api
{
  public class SpecialAbilitiesParameters
  {
    public GeneralSpecialAbilitiesParameters General { get; set; } = new GeneralSpecialAbilitiesParameters();
    public HitterSpecialAbilitiesParameters Hitter { get; set; } = new HitterSpecialAbilitiesParameters();
    public PitcherSpecialAbilitiesParameters Pitcher { get; set; } = new PitcherSpecialAbilitiesParameters();
  }

  public class GeneralSpecialAbilitiesParameters
  {
    public bool IsStar { get; set; }
    public Special2_4 Durability { get; set; }
    public SpecialPositive_Negative Morale { get; set; }
  }

  public class HitterSpecialAbilitiesParameters
  {
    public SituationalHittingSpecialAbilitiesParameters SituationalHitting { get; set; } = new SituationalHittingSpecialAbilitiesParameters();
    public HittingApproachSpecialAbilitiesParameters HittingApproach { get; set; } = new HittingApproachSpecialAbilitiesParameters();
    public SmallBallSpecialAbilitiesParameters SmallBall { get; set; } = new SmallBallSpecialAbilitiesParameters();
    public BaseRunningSpecialAbilitiesParameters BaseRunning { get; set; } = new BaseRunningSpecialAbilitiesParameters();
    public FieldingSpecialAbilitiesParameters Fielding { get; set; } = new FieldingSpecialAbilitiesParameters();
  }

  public class SituationalHittingSpecialAbilitiesParameters
  {
    public Special2_4 Consistency { get; set; }
    public Special1_5 VersusLefty { get; set; }
    public bool IsTableSetter { get; set; }
    public bool IsBackToBackHitter { get; set; }
    public bool IsHotHitter { get; set; }
    public bool IsRallyHitter { get; set; }
    public bool IsGoodPinchHitter { get; set; }
    public BasesLoadedHitter? BasesLoadedHitter { get; set; }
    public WalkOffHitter? WalkOffHitter { get; set; }
    public Special1_5 ClutchHitter { get; set; }
  }

  public class HittingApproachSpecialAbilitiesParameters
  {
    public bool IsContactHitter { get; set; }
    public bool IsPowerHitter { get; set; }
    public SluggerOrSlapHitter? SluggerOrSlapHitter { get; set; }
    public bool IsPushHitter { get; set; }
    public bool IsPullHitter { get; set; }
    public bool IsSprayHitter { get; set; }
    public bool IsFirstballHitter { get; set; }
    public AggressiveOrPatientHitter? AggressiveOrPatientHitter { get; set; }
    public bool IsRefinedHitter { get; set; }
    public bool IsToughOut { get; set; }
    public bool IsIntimidator { get; set; }
    public bool IsSparkplug { get; set; }
  }

  public class SmallBallSpecialAbilitiesParameters
  {
    public SpecialPositive_Negative SmallBall { get; set; }
    public BuntingAbility? Bunting { get; set; }
    public InfieldHittingAbility? InfieldHitting { get; set; }
  }

  public class BaseRunningSpecialAbilitiesParameters
  {
    public Special2_4 BaseRunning { get; set; }
    public Special2_4 Stealing { get; set; }
    public bool IsAggressiveRunner { get; set; }
    public AggressiveOrCautiousBaseStealer? AggressiveOrCautiousBaseStealer { get; set; }
    public bool IsToughRunner { get; set; }
    public bool WillBreakupDoublePlay { get; set; }
    public bool WillSlideHeadFirst { get; set; }
  }

  public class FieldingSpecialAbilitiesParameters
  {
    public bool IsGoldGlover { get; set; }
    public bool CanSpiderCatch { get; set; }
    public bool CanBarehandCatch { get; set; }
    public bool IsAggressiveFielder { get; set; }
    public bool IsPivotMan { get; set; }
    public bool IsErrorProne { get; set; }
    public bool IsGoodBlocker { get; set; }
    public CatchingAbility? Catching { get; set; }
    public Special2_4 Throwing { get; set; }
    public bool HasCannonArm { get; set; }
    public bool IsTrashTalker { get; set; }
  }

  public class PitcherSpecialAbilitiesParameters
  {
    public SituationalPitchingSpecialAbilitiesParameters SituationalPitching { get; set; } = new SituationalPitchingSpecialAbilitiesParameters();
    public PitchingDemeanorSpecialAbilitiesParameters Demeanor { get; set; } = new PitchingDemeanorSpecialAbilitiesParameters();
    public PitchingMechanicsSpecialAbilitiesParameters PitchingMechanics { get; set; } = new PitchingMechanicsSpecialAbilitiesParameters();
    public PitchQualitiesSpecialAbilitiesParameters PitchQuailities { get; set; } = new PitchQualitiesSpecialAbilitiesParameters();
  }

  public class SituationalPitchingSpecialAbilitiesParameters
  {
    public Special2_4 Consistency { get; set; }
    public Special2_4 VersusLefty { get; set; }
    public Special2_4 Poise { get; set; }
    public bool PoorVersusRunner { get; set; }
    public Special2_4 WithRunnersInSocringPosition { get; set; }
    public bool IsSlowStarter { get; set; }
    public bool IsStarterFinisher { get; set; }
    public bool IsChokeArtist { get; set; }
    public bool IsSandbag { get; set; }
    public bool DoctorK { get; set; }
    public bool IsWalkProne { get; set; }
    public SpecialPositive_Negative Luck { get; set; }
    public Special2_4 Recovery { get; set; }
  }

  public class PitchingDemeanorSpecialAbilitiesParameters
  {
    public bool IsIntimidator { get; set; }
    public BattlerPokerFace? BattlerPokerFace { get; set; }
    public bool IsHotHead { get; set; }
  }

  public class PitchingMechanicsSpecialAbilitiesParameters
  {
    public bool GoodDelivery { get; set; }
    public Special2_4 Release { get; set; }
    public bool GoodPace { get; set; }
    public bool GoodReflexes { get; set; }
    public bool GoodPickoff { get; set; }
  }

  public class PitchQualitiesSpecialAbilitiesParameters
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
