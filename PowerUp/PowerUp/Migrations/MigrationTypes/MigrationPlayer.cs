using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Migrations;
using System;
using System.Collections.Generic;

namespace PowerUp.Migrations.MigrationTypes
{
  [MigrationTypeFor(typeof(Player))]
  public class MigrationPlayer
  {
    public int? Id { get; set; }
    public EntitySourceType? SourceType { get; set; }
    public bool? IsCustomPlayer { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? ImportSource { get; set; }
    public int? SourcePowerProsId { get; set; }

    // Generated Players Only
    public long? GeneratedPlayer_LSPLayerId { get; set; }
    public string? GeneratedPlayer_FullFirstName { get; set; }
    public string? GeneratedPlayer_FullLastName { get; set; }
    public DateTime? GeneratedPlayer_ProDebutDate { get; set; }
    public int? Year { get; set; }
    public List<MigrationGeneratorWarning>? GeneratorWarnings { get; set; }
    public bool? GeneratedPlayer_IsUnedited { get; set; }

    public string? SavedName { get; set; } = string.Empty;
    public int? SpecialSavedNameId { get; set; }
    public int? BirthMonth { get; set; }
    public int? BirthDay { get; set; }
    public int? Age { get; set; }
    public int? YearsInMajors { get; set; }
    public string? UniformNumber { get; set; } = string.Empty;
    public Position? PrimaryPosition { get; set; }
    public PitcherType? PitcherType { get; set; }
    public int? VoiceId { get; set; }
    public BattingSide? BattingSide { get; set; }
    public int? BattingStanceId { get; set; }
    public ThrowingArm? ThrowingArm { get; set; }
    public int? PitchingMechanicsId { get; set; }
    public double? BattingAverage { get; set; }
    public int? RunsBattedIn { get; set; }
    public int? HomeRuns { get; set; }
    public double? EarnedRunAverage { get; set; }
    public MigrationAppearance? Appearance { get; set; }
    public MigrationPositionCapabilities? PositionCapabilities { get; set; }
    public MigrationHitterAbilities? HitterAbilities { get; set; }
    public MigrationPitcherAbilities? PitcherAbilities { get; set; }
    public MigrationSpecialAbilities? SpecialAbilities { get; set; }
  }

  [MigrationTypeFor(typeof(Appearance))]
  public class MigrationAppearance
  {
    public int? FaceId { get; set; }
    public EyebrowThickness? EyebrowThickness { get; set; }
    public SkinColor? SkinColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairStyle? HairStyle { get; set; }
    public HairColor? HairColor { get; set; }
    public FacialHairStyle? FacialHairStyle { get; set; }
    public HairColor? FacialHairColor { get; set; }
    public BatColor? BatColor { get; set; }
    public GloveColor? GloveColor { get; set; }
    public EyewearType? EyewearType { get; set; }
    public EyewearFrameColor? EyewearFrameColor { get; set; }
    public EyewearLensColor? EyewearLensColor { get; set; }
    public EarringSide? EarringSide { get; set; }
    public AccessoryColor? EarringColor { get; set; }
    public AccessoryColor? RightWristbandColor { get; set; }
    public AccessoryColor? LeftWristbandColor { get; set; }
  }

  [MigrationTypeFor(typeof(PositionCapabilities))]
  public class MigrationPositionCapabilities
  {
    public Grade? Pitcher { get; set; }
    public Grade? Catcher { get; set; }
    public Grade? FirstBase { get; set; }
    public Grade? SecondBase { get; set; }
    public Grade? ThirdBase { get; set; }
    public Grade? Shortstop { get; set; }
    public Grade? LeftField { get; set; }
    public Grade? CenterField { get; set; }
    public Grade? RightField { get; set; }
  }

  [MigrationTypeFor(typeof(HitterAbilities))]
  public class MigrationHitterAbilities
  {
    public int? Trajectory { get; set; }
    public int? Contact { get; set; }
    public int? Power { get; set; }
    public int? RunSpeed { get; set; }
    public int? ArmStrength { get; set; }
    public int? Fielding { get; set; }
    public int? ErrorResistance { get; set; }
    public MigrationHotZoneGrid? HotZones { get; set; }
  }

  [MigrationTypeFor(typeof(HotZoneGrid))]
  public class MigrationHotZoneGrid
  {
    public HotZonePreference? UpAndIn { get; set; }
    public HotZonePreference? Up { get; set; }
    public HotZonePreference? UpAndAway { get; set; }
    public HotZonePreference? MiddleIn { get; set; }
    public HotZonePreference? Middle { get; set; }
    public HotZonePreference? MiddleAway { get; set; }
    public HotZonePreference? DownAndIn { get; set; }
    public HotZonePreference? Down { get; set; }
    public HotZonePreference? DownAndAway { get; set; }
  }

  [MigrationTypeFor(typeof(PitcherAbilities))]
  public class MigrationPitcherAbilities
  {
    public double? TopSpeedMph { get; set; }
    public int? Control { get; set; }
    public int? Stamina { get; set; }

    public bool? HasTwoSeam { get; set; }
    public int? TwoSeamMovement { get; set; }

    public SliderType? Slider1Type { get; set; }
    public int? Slider1Movement { get; set; }

    public SliderType? Slider2Type { get; set; }
    public int? Slider2Movement { get; set; }

    public CurveType? Curve1Type { get; set; }
    public int? Curve1Movement { get; set; }

    public CurveType? Curve2Type { get; set; }
    public int? Curve2Movement { get; set; }

    public ForkType? Fork1Type { get; set; }
    public int? Fork1Movement { get; set; }

    public ForkType? Fork2Type { get; set; }
    public int? Fork2Movement { get; set; }

    public SinkerType? Sinker1Type { get; set; }
    public int? Sinker1Movement { get; set; }

    public SinkerType? Sinker2Type { get; set; }
    public int? Sinker2Movement { get; set; }

    public SinkingFastballType? SinkingFastball1Type { get; set; }
    public int? SinkingFastball1Movement { get; set; }

    public SinkingFastballType? SinkingFastball2Type { get; set; }
    public int? SinkingFastball2Movement { get; set; }
  }

  [MigrationTypeFor(typeof(SpecialAbilities))]
  public class MigrationSpecialAbilities
  {
    public MigrationGeneralSpecialAbilities? General { get; set; }
    public MigrationHitterSpecialAbilities? Hitter { get; set; }
    public MigrationPitcherSpecialAbilities? Pitcher { get; set; }
  }

  [MigrationTypeFor(typeof(GeneralSpecialAbilities))]
  public class MigrationGeneralSpecialAbilities
  {
    public bool? IsStar { get; set; }
    public Special2_4? Durability { get; set; }
    public SpecialPositive_Negative? Morale { get; set; }
    public SpecialPositive_Negative? InRainAbility { get; set; }
    public SpecialPositive_Negative? DayGameAbility { get; set; }
  }

  [MigrationTypeFor(typeof(HitterSpecialAbilities))]
  public class MigrationHitterSpecialAbilities
  {
    public MigrationSituationalHittingSpecialAbilities? SituationalHitting { get; set; }
    public MigrationHittingApproachSpecialAbilities? HittingApproach { get; set; }
    public MigrationSmallBallSpecialAbilities? SmallBall { get; set; }
    public MigrationBaseRunningSpecialAbilities? BaseRunning { get; set; }
    public MigrationFieldingSpecialAbilities? Fielding { get; set; }
  }

  [MigrationTypeFor(typeof(PitcherSpecialAbilities))]
  public class MigrationPitcherSpecialAbilities
  {
    public MigrationSituationalPitchingSpecialAbilities? SituationalPitching { get; set; }
    public MigrationPitchingDemeanorSpecialAbilities? Demeanor { get; set; }
    public MigrationPitchingMechanicsSpecialAbilities? PitchingMechanics { get; set; }
    public MigrationPitchQualitiesSpecialAbilities? PitchQuailities { get; set; }
  }

  [MigrationTypeFor(typeof(SituationalHittingSpecialAbilities))]
  public class MigrationSituationalHittingSpecialAbilities
  {
    public Special2_4? Consistency { get; set; }
    public Special1_5? VersusLefty { get; set; }
    public bool? IsTableSetter { get; set; }
    public bool? IsBackToBackHitter { get; set; }
    public bool? IsHotHitter { get; set; }
    public bool? IsRallyHitter { get; set; }
    public bool? IsGoodPinchHitter { get; set; }
    public BasesLoadedHitter? BasesLoadedHitter { get; set; }
    public WalkOffHitter? WalkOffHitter { get; set; }
    public Special1_5? ClutchHitter { get; set; }
  }

  [MigrationTypeFor(typeof(HittingApproachSpecialAbilities))]
  public class MigrationHittingApproachSpecialAbilities
  {
    public bool? IsContactHitter { get; set; }
    public bool? IsPowerHitter { get; set; }
    public SluggerOrSlapHitter? SluggerOrSlapHitter { get; set; }
    public bool? IsPushHitter { get; set; }
    public bool? IsPullHitter { get; set; }
    public bool? IsSprayHitter { get; set; }
    public bool? IsFirstballHitter { get; set; }
    public AggressiveOrPatientHitter? AggressiveOrPatientHitter { get; set; }
    public bool? IsRefinedHitter { get; set; }
    public bool? IsFreeSwinger { get; set; }
    public bool? IsToughOut { get; set; }
    public bool? IsIntimidator { get; set; }
    public bool? IsSparkplug { get; set; }
  }

  [MigrationTypeFor(typeof(SmallBallSpecialAbilities))]
  public class MigrationSmallBallSpecialAbilities
  {
    public SpecialPositive_Negative? SmallBall { get; set; }
    public BuntingAbility? Bunting { get; set; }
    public InfieldHittingAbility? InfieldHitting { get; set; }
  }

  [MigrationTypeFor(typeof(BaseRunningSpecialAbilities))]
  public class MigrationBaseRunningSpecialAbilities
  {
    public Special2_4? BaseRunning { get; set; }
    public Special2_4? Stealing { get; set; }
    public bool? IsAggressiveRunner { get; set; }
    public AggressiveOrCautiousBaseStealer? AggressiveOrCautiousBaseStealer { get; set; }
    public bool? IsToughRunner { get; set; }
    public bool? WillBreakupDoublePlay { get; set; }
    public bool? WillSlideHeadFirst { get; set; }
  }

  [MigrationTypeFor(typeof(FieldingSpecialAbilities))]
  public class MigrationFieldingSpecialAbilities
  {
    public bool? IsGoldGlover { get; set; }
    public bool? CanSpiderCatch { get; set; }
    public bool? CanBarehandCatch { get; set; }
    public bool? IsAggressiveFielder { get; set; }
    public bool? IsPivotMan { get; set; }
    public bool? IsErrorProne { get; set; }
    public bool? IsGoodBlocker { get; set; }
    public CatchingAbility? Catching { get; set; }
    public Special2_4? Throwing { get; set; }
    public bool? HasCannonArm { get; set; }
    public bool? IsTrashTalker { get; set; }
  }

  [MigrationTypeFor(typeof(SituationalPitchingSpecialAbilities))]
  public class MigrationSituationalPitchingSpecialAbilities
  {
    public Special2_4? Consistency { get; set; }
    public Special2_4? VersusLefty { get; set; }
    public Special2_4? Poise { get; set; }
    public bool? PoorVersusRunner { get; set; }
    public Special2_4? WithRunnersInSocringPosition { get; set; }
    public bool? IsSlowStarter { get; set; }
    public bool? IsStarterFinisher { get; set; }
    public bool? IsChokeArtist { get; set; }
    public bool? IsSandbag { get; set; }
    public bool? DoctorK { get; set; }
    public bool? IsWalkProne { get; set; }
    public SpecialPositive_Negative? Luck { get; set; }
    public Special2_4? Recovery { get; set; }
  }

  [MigrationTypeFor(typeof(PitchingDemeanorSpecialAbilities))]
  public class MigrationPitchingDemeanorSpecialAbilities
  {
    public bool? IsIntimidator { get; set; }
    public BattlerPokerFace? BattlerPokerFace { get; set; }
    public bool? IsHotHead { get; set; }
  }

  [MigrationTypeFor(typeof(PitchingMechanicsSpecialAbilities))]
  public class MigrationPitchingMechanicsSpecialAbilities
  {
    public bool? GoodDelivery { get; set; }
    public Special2_4? Release { get; set; }
    public bool? GoodPace { get; set; }
    public bool? GoodReflexes { get; set; }
    public bool? GoodPickoff { get; set; }
  }

  [MigrationTypeFor(typeof(PitchQualitiesSpecialAbilities))]
  public class MigrationPitchQualitiesSpecialAbilities
  {
    public PowerOrBreakingBallPitcher? PowerOrBreakingBallPitcher { get; set; }
    public Special2_4? FastballLife { get; set; }
    public Special2_4? Spin { get; set; }
    public SpecialPositive_Negative? SafeOrFatPitch { get; set; }
    public SpecialPositive_Negative? GroundBallOrFlyBallPitcher { get; set; }
    public bool? GoodLowPitch { get; set; }
    public bool? Gyroball { get; set; }
    public bool? ShuttoSpin { get; set; }
  }
}
