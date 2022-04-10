using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Libraries;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class PlayerEditorResponse
  {
    public PlayerEditorOptions Options { get; }
    public PlayerPersonalDetailsDto PersonalDetails { get; }
    public AppearanceDetailsDto AppearanceDetails { get; }
    public PositionCapabilityDetailsDto PositionCapabilityDetails { get; }
    public HitterAbilityDetailsDto HitterAbilityDetails { get; }
    public PitcherAbilityDetailsDto PitcherAbilityDetails { get; }
    public SpecialAbilitiesDetailsDto SpecialAbilityDetails { get; }

    public PlayerEditorResponse(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      IFaceLibrary faceLibrary,
      Player player
    )
    {
      Options = new PlayerEditorOptions(voiceLibrary, battingStanceLibrary, pitchingMechanicsLibrary, faceLibrary);
      PersonalDetails = new PlayerPersonalDetailsDto(voiceLibrary, battingStanceLibrary, pitchingMechanicsLibrary, player);
      AppearanceDetails = new AppearanceDetailsDto(faceLibrary, player.Appearance);
      PositionCapabilityDetails = new PositionCapabilityDetailsDto(player.PositonCapabilities);
      HitterAbilityDetails = new HitterAbilityDetailsDto(player.HitterAbilities);
      PitcherAbilityDetails = new PitcherAbilityDetailsDto(player.PitcherAbilities);
      SpecialAbilityDetails = new SpecialAbilitiesDetailsDto(player.SpecialAbilities);
    }
  }

  public class PlayerEditorOptions
  {
    public PlayerPersonalDetailsOptions PersonalDetailsOptions { get; }
    public IEnumerable<KeyedCode> PositionCapabilityOptions => new GradeOptions();
    public PlayerAppearanceOptions AppearanceOptions { get; }
    public PitcherAbilitiesOptions PitcherAbilitiesOptions => new PitcherAbilitiesOptions();
    public SpecialAbilitiesOptions SpecialAbilitiesOptions => new SpecialAbilitiesOptions();

    public PlayerEditorOptions(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      IFaceLibrary faceLibrary
    )
    {
      PersonalDetailsOptions = new PlayerPersonalDetailsOptions(voiceLibrary, battingStanceLibrary, pitchingMechanicsLibrary);
      AppearanceOptions = new PlayerAppearanceOptions(faceLibrary);
    }
  }

  public class PlayerPersonalDetailsOptions
  {
    public IEnumerable<SimpleCode> VoiceOptions { get; }
    public IEnumerable<KeyedCode> Positions => new PrimaryPositionOptions();
    public IEnumerable<KeyedCode> PitcherTypes => EnumExtensions.GetKeyedCodeList<PitcherType>();
    public IEnumerable<KeyedCode> BattingSideOptions => EnumExtensions.GetKeyedCodeList<BattingSide>();
    public IEnumerable<SimpleCode> BattingStanceOptions { get; }
    public IEnumerable<KeyedCode> ThrowingArmOptions => EnumExtensions.GetKeyedCodeList<ThrowingArm>();
    public IEnumerable<SimpleCode> PitchingMechanicsOptions { get; }

    public PlayerPersonalDetailsOptions(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary
    )
    {
      VoiceOptions = voiceLibrary.GetAll().Select(v => new SimpleCode(id: v.Key, name: v.Value));
      BattingStanceOptions = battingStanceLibrary.GetAll().Select(v => new SimpleCode(id: v.Key, name: v.Value));
      PitchingMechanicsOptions = pitchingMechanicsLibrary.GetAll().Select(v => new SimpleCode(id: v.Key, name: v.Value));
    }
  }

  public class PlayerAppearanceOptions
  {
    public IEnumerable<FaceCode> FaceOptions { get; }
    public IEnumerable<KeyedCode> EyebrowThicknessOptions => EnumExtensions.GetKeyedCodeList<EyebrowThickness>();
    public IEnumerable<KeyedCode> SkinColorOptions => EnumExtensions.GetKeyedCodeList<SkinColor>();
    public IEnumerable<KeyedCode> EyeColorOptions => EnumExtensions.GetKeyedCodeList<EyeColor>(c => c.ToNumberedKeyedCode(addOne: true));
    public IEnumerable<KeyedCode> HairStyleOptions => EnumExtensions.GetKeyedCodeList<HairStyle>(c => c.ToNumberedKeyedCode());
    public IEnumerable<KeyedCode> FacialHairStyleOptions => EnumExtensions.GetKeyedCodeList<FacialHairStyle>(c => c.ToNumberedKeyedCode());
    public IEnumerable<KeyedCode> HairColorOptions => EnumExtensions.GetKeyedCodeList<HairColor>(c => c.ToNumberedKeyedCode(addOne: true));
    public IEnumerable<KeyedCode> BatColorOptions => EnumExtensions.GetKeyedCodeList<BatColor>(c => c.ToNumberedKeyedCode(addOne: true));
    public IEnumerable<KeyedCode> GloveColorOptions => EnumExtensions.GetKeyedCodeList<GloveColor>(c => c.ToNumberedKeyedCode(addOne: true));
    public IEnumerable<KeyedCode> EyewearTypeOptions => EnumExtensions.GetKeyedCodeList<EyewearType>(c => c.ToNumberedKeyedCode());
    public IEnumerable<KeyedCode> EyewearFrameColorOptions => EnumExtensions.GetKeyedCodeList<EyewearFrameColor>();
    public IEnumerable<KeyedCode> EyewearLensColorOptions => EnumExtensions.GetKeyedCodeList<EyewearLensColor>();
    public IEnumerable<KeyedCode> EarringSideOptions => EnumExtensions.GetKeyedCodeList<EarringSide>(c => c.ToNumberedKeyedCode());
    public IEnumerable<KeyedCode> AccessoryColorOptions => EnumExtensions.GetKeyedCodeList<AccessoryColor>(c => c.ToNumberedKeyedCode(addOne: true));

    public PlayerAppearanceOptions(IFaceLibrary faceLibrary)
    {
      FaceOptions = faceLibrary.GetAll().Select(v => new FaceCode(id: v.Key, name: v.Value));
    }
  }

  public class FaceCode : SimpleCode
  {
    public bool CanChooseSkin { get; }
    public bool CanChooseEyebrows { get; }
    public bool CanChooseEyes { get; }

    public FaceCode(int id, string name)
      : base(id, name)
    {
      var faceType = FaceTypeHelpers.GetFaceType(id);

      CanChooseSkin = faceType == FaceType.Anime
        || faceType == FaceType.Standard
        || faceType == FaceType.StandardWithoutEyeColor;

      CanChooseEyebrows = faceType == FaceType.Standard
        || faceType == FaceType.StandardWithoutEyeColor;

      CanChooseEyes = faceType == FaceType.Anime
        || faceType == FaceType.Standard;
    }
  }

  public class PitcherAbilitiesOptions
  {
    public IEnumerable<KeyedCode> TwoSeamOptions => new[] { new KeyedCode("TwoSeam", "Two Seam") };
    public IEnumerable<KeyedCode> SliderOptions => EnumExtensions.GetKeyedCodeList<SliderType>();
    public IEnumerable<KeyedCode> CurveOptions => EnumExtensions.GetKeyedCodeList<CurveType>();
    public IEnumerable<KeyedCode> ForkOptions => EnumExtensions.GetKeyedCodeList<ForkType>();
    public IEnumerable<KeyedCode> SinkerOptions => EnumExtensions.GetKeyedCodeList<SinkerType>();
    public IEnumerable<KeyedCode> SinkingFastballOptions => EnumExtensions.GetKeyedCodeList<SinkingFastballType>();
  }

  public class SpecialAbilitiesOptions
  {
    public IEnumerable<KeyedCode> Special1_5Options => EnumExtensions.GetKeyedCodeList<Special1_5>(useAbbrev: true);
    public IEnumerable<KeyedCode> Special2_4Options => EnumExtensions.GetKeyedCodeList<Special2_4>(useAbbrev: true);
    public IEnumerable<KeyedCode> SpecialPositive_NegativeOptions => EnumExtensions.GetKeyedCodeList<SpecialPositive_Negative>();
    public IEnumerable<KeyedCode> BasesLoadedHitterOptions => EnumExtensions.GetKeyedCodeList<BasesLoadedHitter>();
    public IEnumerable<KeyedCode> WalkOffHitterOptions => EnumExtensions.GetKeyedCodeList<WalkOffHitter>();
    public IEnumerable<KeyedCode> SluggerOrSlapHitterOptions => EnumExtensions.GetKeyedCodeList<SluggerOrSlapHitter>();
    public IEnumerable<KeyedCode> AggressiveOrPatientHitterOptions => EnumExtensions.GetKeyedCodeList<AggressiveOrPatientHitter>();
    public IEnumerable<KeyedCode> AggressiveOrCautiousBaseStealerOptions => EnumExtensions.GetKeyedCodeList<AggressiveOrCautiousBaseStealer>();
    public IEnumerable<KeyedCode> BuntingAbilityOptions => EnumExtensions.GetKeyedCodeList<BuntingAbility>();
    public IEnumerable<KeyedCode> InfieldHittingAbilityOptions => EnumExtensions.GetKeyedCodeList<InfieldHittingAbility>();
    public IEnumerable<KeyedCode> CatchingAbilityOptions => EnumExtensions.GetKeyedCodeList<CatchingAbility>();
    public IEnumerable<KeyedCode> BattlerPokerFaceOptions => EnumExtensions.GetKeyedCodeList<BattlerPokerFace>();
    public IEnumerable<KeyedCode> PowerOrBreakingBallPitcher => EnumExtensions.GetKeyedCodeList<PowerOrBreakingBallPitcher>();
  }

  public class PlayerPersonalDetailsDto
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; }
    public int? Year { get; }
    public string? ImportSource { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public bool IsSpecialSavedName { get; }
    public string SavedName { get; }
    public string UniformNumber { get; }
    public KeyedCode Position { get; }
    public KeyedCode PitcherType { get; }
    public SimpleCode Voice { get; }
    public KeyedCode BattingSide { get; }
    public SimpleCode BattingStance { get; }
    public KeyedCode ThrowingArm { get; }
    public SimpleCode PitchingMechanics { get; }

    public PlayerPersonalDetailsDto(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      Player player
    )
    {
      SourceType = player.SourceType;
      Year = player.Year;
      ImportSource = player.ImportSource;
      FirstName = player.FirstName;
      LastName = player.LastName;
      IsSpecialSavedName = player.SpecialSavedNameId.HasValue;
      SavedName = player.SavedName;
      UniformNumber = player.UniformNumber;
      Position = player.PrimaryPosition.ToKeyedCode(useAbbrev: true);
      PitcherType = player.PitcherType.ToKeyedCode();
      Voice = new SimpleCode(id: player.VoiceId, name: voiceLibrary[player.VoiceId]);
      BattingSide = player.BattingSide.ToKeyedCode();
      BattingStance = new SimpleCode(id: player.BattingStanceId, name: battingStanceLibrary[player.BattingStanceId]);
      ThrowingArm = player.ThrowingArm.ToKeyedCode();
      PitchingMechanics = new SimpleCode(id: player.PitchingMechanicsId, name: pitchingMechanicsLibrary[player.PitchingMechanicsId]);
    }
  }

  public class AppearanceDetailsDto
  {
    public SimpleCode Face { get; set; }
    public KeyedCode? Eyebrows { get; set; }
    public KeyedCode? SkinColor { get; set; }
    public KeyedCode? EyeColor { get; set; }
    public KeyedCode? HairStyle { get; set; }
    public KeyedCode? HairColor { get; set; }
    public KeyedCode? FacialHairStyle { get; set; }
    public KeyedCode? FacialHairColor { get; set; }
    public KeyedCode BatColor { get; set; }
    public KeyedCode GloveColor { get; set; }
    public KeyedCode? EyewearType { get; set; }
    public KeyedCode? EyewearFrameColor { get; set; }
    public KeyedCode? EyewearLensColor { get; set; }
    public KeyedCode? EarringSide { get; set; }
    public KeyedCode? EarringColor { get; set; }
    public KeyedCode? RightWristbandColor { get; set; }
    public KeyedCode? LeftWristbandColor { get; set; }

    public AppearanceDetailsDto(IFaceLibrary faceLibrary, Appearance appearance)
    {
      Face = new SimpleCode(id: appearance.FaceId, name: faceLibrary[appearance.FaceId]);
      Eyebrows = appearance.EyebrowThickness?.ToKeyedCode();
      SkinColor = appearance.SkinColor?.ToKeyedCode();
      EyeColor = appearance.EyeColor?.ToNumberedKeyedCode(addOne: true);
      HairStyle = appearance.HairStyle?.ToNumberedKeyedCode();
      HairColor = appearance.HairColor?.ToNumberedKeyedCode(addOne: true);
      FacialHairStyle = appearance.FacialHairStyle?.ToNumberedKeyedCode();
      FacialHairColor = appearance.FacialHairColor?.ToNumberedKeyedCode(addOne: true);
      BatColor = appearance.BatColor.ToKeyedCode();
      GloveColor = appearance.GloveColor.ToKeyedCode();
      EyewearType = appearance.EyewearType?.ToNumberedKeyedCode();
      EyewearFrameColor = appearance.EyewearFrameColor?.ToKeyedCode();
      EyewearLensColor = appearance.EyewearLensColor?.ToKeyedCode();
      EarringSide = appearance.EarringSide?.ToNumberedKeyedCode();
      EarringColor = appearance.EarringColor?.ToNumberedKeyedCode(addOne: true);
      RightWristbandColor = appearance.RightWristbandColor?.ToNumberedKeyedCode(addOne: true);
      LeftWristbandColor = appearance.LeftWristbandColor?.ToNumberedKeyedCode(addOne: true);
    }
  }

  public class PositionCapabilityDetailsDto
  {
    public KeyedCode Pitcher { get; set; }
    public KeyedCode Catcher { get; set; }
    public KeyedCode FirstBase { get; set; }
    public KeyedCode SecondBase { get; set; }
    public KeyedCode ThirdBase { get; set; }
    public KeyedCode Shortstop { get; set; }
    public KeyedCode LeftField { get; set; }
    public KeyedCode CenterField { get; set; }
    public KeyedCode RightField { get; set; }

    public PositionCapabilityDetailsDto(PositionCapabilities positionCapabilities)
    {
      Pitcher = positionCapabilities.Pitcher.ToKeyedCode();
      Catcher = positionCapabilities.Catcher.ToKeyedCode();
      FirstBase = positionCapabilities.FirstBase.ToKeyedCode();
      SecondBase = positionCapabilities.SecondBase.ToKeyedCode();
      ThirdBase = positionCapabilities.ThirdBase.ToKeyedCode();
      Shortstop = positionCapabilities.Shortstop.ToKeyedCode();
      LeftField = positionCapabilities.LeftField.ToKeyedCode();
      CenterField = positionCapabilities.CenterField.ToKeyedCode();
      RightField = positionCapabilities.RightField.ToKeyedCode();
    }
  }

  public class HitterAbilityDetailsDto
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }

    public HotZoneGridDto HotZones { get; set; }

    public HitterAbilityDetailsDto(HitterAbilities hitterAbilities)
    {
      Trajectory = hitterAbilities.Trajectory;
      Contact = hitterAbilities.Contact;
      Power = hitterAbilities.Power;
      RunSpeed = hitterAbilities.RunSpeed;
      ArmStrength = hitterAbilities.ArmStrength;
      Fielding = hitterAbilities.Fielding;
      ErrorResistance = hitterAbilities.ErrorResistance;
      HotZones = new HotZoneGridDto(hitterAbilities.HotZones);
    }
  }

  public class PitcherAbilityDetailsDto
  {
    public int TopSpeed { get; }
    public int Control { get; }
    public int Stamina { get; }

    public KeyedCode? TwoSeamType { get; }
    public int? TwoSeamMovement { get; }

    public KeyedCode? Slider1Type { get; }
    public int? Slider1Movement { get; }

    public KeyedCode? Slider2Type { get; }
    public int? Slider2Movement { get; }

    public KeyedCode? Curve1Type { get; }
    public int? Curve1Movement { get; }

    public KeyedCode? Curve2Type { get; }
    public int? Curve2Movement { get; }

    public KeyedCode? Fork1Type { get; }
    public int? Fork1Movement { get; }

    public KeyedCode? Fork2Type { get; }
    public int? Fork2Movement { get; }

    public KeyedCode? Sinker1Type { get; }
    public int? Sinker1Movement { get; }

    public KeyedCode? Sinker2Type { get; }
    public int? Sinker2Movement { get; set; }

    public KeyedCode? SinkingFastball1Type { get; }
    public int? SinkingFastball1Movement { get; }

    public KeyedCode? SinkingFastball2Type { get; }
    public int? SinkingFastball2Movement { get; }

    public PitcherAbilityDetailsDto(PitcherAbilities pitcherAbilities)
    {
      TopSpeed = (int)pitcherAbilities.TopSpeedMph;
      Control = pitcherAbilities.Control;
      Stamina = pitcherAbilities.Stamina;

      TwoSeamType = pitcherAbilities.HasTwoSeam
        ? new KeyedCode("TwoSeam", "Two Seam")
        : null;
      TwoSeamMovement = pitcherAbilities.TwoSeamMovement;

      Slider1Type = pitcherAbilities.Slider1Type?.ToKeyedCode();
      Slider1Movement = pitcherAbilities.Slider1Movement;

      Slider2Type = pitcherAbilities.Slider2Type?.ToKeyedCode();
      Slider2Movement = pitcherAbilities.Slider2Movement;

      Curve1Type = pitcherAbilities.Curve1Type?.ToKeyedCode();
      Curve1Movement = pitcherAbilities.Curve1Movement;

      Curve2Type = pitcherAbilities.Curve2Type?.ToKeyedCode();
      Curve2Movement = pitcherAbilities.Curve2Movement;

      Fork1Type = pitcherAbilities.Fork1Type?.ToKeyedCode();
      Fork1Movement = pitcherAbilities.Fork1Movement;

      Fork2Type = pitcherAbilities.Fork2Type?.ToKeyedCode();
      Fork2Movement = pitcherAbilities.Fork2Movement;

      Sinker1Type = pitcherAbilities.Sinker1Type?.ToKeyedCode();
      Sinker1Movement = pitcherAbilities.Sinker1Movement;

      Sinker2Type = pitcherAbilities.Sinker2Type?.ToKeyedCode();
      Sinker2Movement = pitcherAbilities.Sinker2Movement;

      SinkingFastball1Type = pitcherAbilities.SinkingFastball1Type?.ToKeyedCode();
      SinkingFastball1Movement = pitcherAbilities.SinkingFastball1Movement;

      SinkingFastball2Type = pitcherAbilities.SinkingFastball2Type?.ToKeyedCode();
      SinkingFastball2Movement = pitcherAbilities.SinkingFastball2Movement;
    }
  }

  public class SpecialAbilitiesDetailsDto
  {
    public GeneralSpecialAbilitiesDetailsDto General { get; set; }
    public HitterSpecialAbilitiesDetailsDto Hitter { get; set; }
    public PitcherSepcialAbilitiesDetailsDto Pitcher { get; set; }

    public SpecialAbilitiesDetailsDto(SpecialAbilities abilities)
    {
      General = new GeneralSpecialAbilitiesDetailsDto(abilities.General);
      Hitter = new HitterSpecialAbilitiesDetailsDto(abilities.Hitter);
      Pitcher = new PitcherSepcialAbilitiesDetailsDto(abilities.Pitcher);
    }
  }

  public class GeneralSpecialAbilitiesDetailsDto
  {
    public bool IsStar { get; }
    public KeyedCode Durability { get; }
    public KeyedCode Morale { get; }

    public GeneralSpecialAbilitiesDetailsDto(GeneralSpecialAbilities general)
    {
      IsStar = general.IsStar;
      Durability = general.Durability.ToKeyedCode();
      Morale = general.Morale.ToKeyedCode();
    }
  }

  public class HitterSpecialAbilitiesDetailsDto
  {
    public SituationalHittingSpecialAbilitiesDetailsDto Situational { get; }
    public HittingApproachSpecialAbilititesDetailsDto Approach { get; }
    public SmallBallSpecialAbilitiesDetailsDto SmallBall { get; }
    public BaseRunningSpecialAbilitiesDetailsDto BaseRunning { get; }
    public FieldingSpecialAbilitiesDetailsDto Fielding { get; }

    public HitterSpecialAbilitiesDetailsDto(HitterSpecialAbilities hitterSpecialAbilities)
    {
      Situational = new SituationalHittingSpecialAbilitiesDetailsDto(hitterSpecialAbilities.SituationalHitting);
      Approach = new HittingApproachSpecialAbilititesDetailsDto(hitterSpecialAbilities.HittingApproach);
      SmallBall = new SmallBallSpecialAbilitiesDetailsDto(hitterSpecialAbilities.SmallBall);
      BaseRunning = new BaseRunningSpecialAbilitiesDetailsDto(hitterSpecialAbilities.BaseRunning);
      Fielding = new FieldingSpecialAbilitiesDetailsDto(hitterSpecialAbilities.Fielding);
    }
  }

  public class SituationalHittingSpecialAbilitiesDetailsDto
  {
    public KeyedCode HittingConsistency { get; }
    public KeyedCode VersusLefty { get; }
    public bool IsTableSetter { get; }
    public bool IsBackToBackHitter { get; }
    public bool IsHotHitter { get; }
    public bool IsRallyHitter { get; }
    public bool IsGoodPinchHitter { get; }
    public KeyedCode? BasesLoadedHitter { get; }
    public KeyedCode? WalkOffHitter { get; }
    public KeyedCode ClutchHitter { get; }

    public SituationalHittingSpecialAbilitiesDetailsDto(SituationalHittingSpecialAbilities situationalHitting)
    {
      HittingConsistency = situationalHitting.Consistency.ToKeyedCode();
      VersusLefty = situationalHitting.VersusLefty.ToKeyedCode();
      IsTableSetter = situationalHitting.IsTableSetter;
      IsBackToBackHitter = situationalHitting.IsBackToBackHitter;
      IsHotHitter = situationalHitting.IsHotHitter;
      IsRallyHitter = situationalHitting.IsRallyHitter;
      IsGoodPinchHitter = situationalHitting.IsGoodPinchHitter;
      BasesLoadedHitter = situationalHitting.BasesLoadedHitter?.ToKeyedCode();
      WalkOffHitter = situationalHitting.WalkOffHitter?.ToKeyedCode();
      ClutchHitter = situationalHitting.ClutchHitter.ToKeyedCode();
    }
  }

  public class HittingApproachSpecialAbilititesDetailsDto
  {
    public bool IsContactHitter { get; }
    public bool IsPowerHitter { get; }
    public KeyedCode? SluggerOrSlapHitter { get; }
    public bool IsPushHitter { get; }
    public bool IsPullHitter { get; }
    public bool IsSprayHitter { get; }
    public bool IsFirstballHitter { get; }
    public KeyedCode? AggressiveOrPatientHitter { get; }
    public bool IsRefinedHitter { get; }
    public bool IsToughOut { get; }
    public bool IsIntimidatingHitter { get; }
    public bool IsSparkplug { get; }

    public HittingApproachSpecialAbilititesDetailsDto(HittingApproachSpecialAbilities approach)
    {
      IsContactHitter = approach.IsContactHitter;
      IsPowerHitter = approach.IsPowerHitter;
      SluggerOrSlapHitter = approach.SluggerOrSlapHitter?.ToKeyedCode();
      IsPushHitter = approach.IsPushHitter;
      IsPullHitter = approach.IsPullHitter;
      IsSprayHitter = approach.IsSprayHitter;
      IsFirstballHitter = approach.IsFirstballHitter;
      AggressiveOrPatientHitter = approach.AggressiveOrPatientHitter?.ToKeyedCode();
      IsRefinedHitter = approach.IsRefinedHitter;
      IsToughOut = approach.IsToughOut;
      IsIntimidatingHitter = approach.IsIntimidator;
      IsSparkplug = approach.IsSparkplug;
    }
  }

  public class SmallBallSpecialAbilitiesDetailsDto
  {
    public KeyedCode SmallBall { get; }
    public KeyedCode? Bunting { get; }
    public KeyedCode? InfieldHitter { get; }

    public SmallBallSpecialAbilitiesDetailsDto(SmallBallSpecialAbilities smallBall)
    {
      SmallBall = smallBall.SmallBall.ToKeyedCode();
      Bunting = smallBall.Bunting?.ToKeyedCode();
      InfieldHitter = smallBall.InfieldHitting?.ToKeyedCode();
    }
  }

  public class BaseRunningSpecialAbilitiesDetailsDto
  {
    public KeyedCode BaseRunning { get; }
    public KeyedCode Stealing { get; }
    public bool IsAggressiveRunner { get; }
    public KeyedCode? AggressiveOrPatientBaseStealer { get; }
    public bool IsToughRunner { get; }
    public bool WillBreakupDoublePlay { get; }
    public bool WillSlideHeadFirst { get; }

    public BaseRunningSpecialAbilitiesDetailsDto(BaseRunningSpecialAbilities baseRunning)
    {
      BaseRunning = baseRunning.BaseRunning.ToKeyedCode();
      Stealing = baseRunning.Stealing.ToKeyedCode();
      IsAggressiveRunner = baseRunning.IsAggressiveRunner;
      AggressiveOrPatientBaseStealer = baseRunning.AggressiveOrCautiousBaseStealer?.ToKeyedCode();
      IsToughRunner = baseRunning.IsToughRunner;
      WillBreakupDoublePlay = baseRunning.WillBreakupDoublePlay;
      WillSlideHeadFirst = baseRunning.WillSlideHeadFirst;
    }
  }

  public class FieldingSpecialAbilitiesDetailsDto
  {
    public bool IsGoldGlover { get; }
    public bool CanSpiderCatch { get; }
    public bool CanBarehandCatch { get; }
    public bool IsAggressiveFielder { get; }
    public bool IsPivotMan { get; }
    public bool IsErrorProne { get; }
    public bool IsGoodBlocker { get; }
    public KeyedCode? Catching { get; }
    public KeyedCode Throwing { get; }
    public bool HasCannonArm { get; }
    public bool IsTrashTalker { get; }

    public FieldingSpecialAbilitiesDetailsDto(FieldingSpecialAbilities fielding)
    {

      IsGoldGlover = fielding.IsGoldGlover;
      CanSpiderCatch = fielding.CanSpiderCatch;
      CanBarehandCatch = fielding.CanBarehandCatch;
      IsAggressiveFielder = fielding.IsAggressiveFielder;
      IsPivotMan = fielding.IsPivotMan;
      IsErrorProne = fielding.IsErrorProne;
      IsGoodBlocker = fielding.IsGoodBlocker;
      Catching = fielding.Catching?.ToKeyedCode();
      Throwing = fielding.Throwing.ToKeyedCode();
      HasCannonArm = fielding.HasCannonArm;
      IsTrashTalker = fielding.IsTrashTalker;
    }
  }

  public class PitcherSepcialAbilitiesDetailsDto
  {
    public SituationalPitchingSpecialAbilitiesDetailsDto Situational { get; }
    public PitchingDemeanorSpecialAbilitiesDetialsDto Demeanor { get; }
    public PitchingMechanicsSpecialAbilitiesDetailsDto Mechanics { get; }
    public PitchQualitiesSpecialAbilitiesDetailsDto PitchQualities { get; }

    public PitcherSepcialAbilitiesDetailsDto(PitcherSpecialAbilities pitcherSpecialAbilities)
    {
      Situational = new SituationalPitchingSpecialAbilitiesDetailsDto(pitcherSpecialAbilities.SituationalPitching);
      Demeanor = new PitchingDemeanorSpecialAbilitiesDetialsDto(pitcherSpecialAbilities.Demeanor);
      Mechanics = new PitchingMechanicsSpecialAbilitiesDetailsDto(pitcherSpecialAbilities.PitchingMechanics);
      PitchQualities = new PitchQualitiesSpecialAbilitiesDetailsDto(pitcherSpecialAbilities.PitchQuailities);
    }
  }

  public class SituationalPitchingSpecialAbilitiesDetailsDto
  {
    public KeyedCode PitchingConsistency { get; }
    public KeyedCode PitchingVersusLefty { get; }
    public KeyedCode Poise { get; }
    public bool PoorVersusRunner { get; }
    public KeyedCode WithRunnersInScoringPosition { get; }
    public bool IsSlowStarter { get; }
    public bool IsStarterFinisher { get; }
    public bool IsChokeArtist { get; }
    public bool IsSandbag { get; }
    public bool DoctorK { get; }
    public bool IsWalkProne { get; }
    public KeyedCode Luck { get; }
    public KeyedCode Recovery { get; }

    public SituationalPitchingSpecialAbilitiesDetailsDto(SituationalPitchingSpecialAbilities situationalPitching)
    {
      PitchingConsistency = situationalPitching.Consistency.ToKeyedCode();
      PitchingVersusLefty = situationalPitching.VersusLefty.ToKeyedCode();
      Poise = situationalPitching.Poise.ToKeyedCode();
      PoorVersusRunner = situationalPitching.PoorVersusRunner;
      WithRunnersInScoringPosition = situationalPitching.WithRunnersInSocringPosition.ToKeyedCode();
      IsSlowStarter = situationalPitching.IsSlowStarter;
      IsStarterFinisher = situationalPitching.IsStarterFinisher;
      IsChokeArtist = situationalPitching.IsChokeArtist;
      IsSandbag = situationalPitching.IsSandbag;
      DoctorK = situationalPitching.DoctorK;
      IsWalkProne = situationalPitching.IsWalkProne;
      Luck = situationalPitching.Luck.ToKeyedCode();
      Recovery = situationalPitching.Recovery.ToKeyedCode();
    }
  }

  public class PitchingDemeanorSpecialAbilitiesDetialsDto
  {
    public bool IsIntimidatingPitcher { get; }
    public KeyedCode? BattlerOrPokerFace { get; }
    public bool IsHotHead { get; }

    public PitchingDemeanorSpecialAbilitiesDetialsDto(PitchingDemeanorSpecialAbilities demeanor)
    {
      IsIntimidatingPitcher = demeanor.IsIntimidator;
      BattlerOrPokerFace = demeanor.BattlerPokerFace?.ToKeyedCode();
      IsHotHead = demeanor.IsHotHead;
    }
  }

  public class PitchingMechanicsSpecialAbilitiesDetailsDto
  {
    public bool GoodDelivery { get; }
    public KeyedCode Release { get; }
    public bool GoodPace { get; }
    public bool GoodReflexes { get; }
    public bool GoodPickoff { get; }

    public PitchingMechanicsSpecialAbilitiesDetailsDto(PitchingMechanicsSpecialAbilities mechanics)
    {
      GoodDelivery = mechanics.GoodDelivery;
      Release = mechanics.Release.ToKeyedCode();
      GoodPace = mechanics.GoodPace;
      GoodReflexes = mechanics.GoodReflexes;
      GoodPickoff = mechanics.GoodPickoff;
    }
  }

  public class PitchQualitiesSpecialAbilitiesDetailsDto
  {
    public KeyedCode? PowerOrBreakingBallPitcher { get; }
    public KeyedCode FastballLife { get; }
    public KeyedCode Spin { get; }
    public KeyedCode SafeOrFatPitch { get; }
    public KeyedCode GroundBallOrFlyBallPitcher { get; }
    public bool GoodLowPitch { get; }
    public bool Gyroball { get; }
    public bool ShuttoSpin { get; }

    public PitchQualitiesSpecialAbilitiesDetailsDto(PitchQualitiesSpecialAbilities pitchQualities)
    {
      PowerOrBreakingBallPitcher = pitchQualities.PowerOrBreakingBallPitcher?.ToKeyedCode();
      FastballLife = pitchQualities.FastballLife.ToKeyedCode();
      Spin = pitchQualities.Spin.ToKeyedCode();
      SafeOrFatPitch = pitchQualities.SafeOrFatPitch.ToKeyedCode();
      GroundBallOrFlyBallPitcher = pitchQualities.GroundBallOrFlyBallPitcher.ToKeyedCode();
      GoodLowPitch = pitchQualities.GoodLowPitch;
      Gyroball = pitchQualities.Gyroball;
      ShuttoSpin = pitchQualities.ShuttoSpin;
    }
  }
}
