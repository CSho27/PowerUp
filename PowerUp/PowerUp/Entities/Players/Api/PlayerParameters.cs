using PowerUp.Validation;

namespace PowerUp.Entities.Players.Api
{
  public class PlayerParameters
  {
    public PlayerPersonalDetailsParameters? PersonalDetails { get; set; }
    public PlayerAppearanceParameters? Appearance { get; set; }
    public PlayerPositionCapabilitiesParameters? PositionCapabilities { get; set; }
    public PlayerHitterAbilityParameters? HitterAbilities { get; set; }
    public PlayerPitcherAbilitiesParameters? PitcherAbilities { get; set; }
    public SpecialAbilitiesParameters? SpecialAbilities { get; set; }
  }

  public class PlayerPersonalDetailsParameters
  {
    public bool IsCustomPlayer { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool KeepSpecialSavedName { get; set; }
    public string? SavedName { get; set; }
    public string? UniformNumber { get; set; }
    public Position Position { get; set; }
    public PitcherType PitcherType { get; set; }
    public int? VoiceId { get; set; }
    public BattingSide BattingSide { get; set; }
    public int? BattingStanceId { get; set; }
    public ThrowingArm ThrowingArm { get; set; }
    public int? PitchingMechanicsId { get; set; }
  }

  public class PlayerAppearanceParameters
  {
    public int FaceId { get; set; }
    public EyebrowThickness? EyebrowThickness { get; set; }
    public SkinColor? SkinColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairStyle? HairStyle { get; set; }
    public HairColor? HairColor { get; set; }
    public FacialHairStyle? FacialHairStyle { get; set; }
    public HairColor? FacialHairColor { get; set; }
    public BatColor BatColor { get; set; }
    public GloveColor GloveColor { get; set; }
    public EyewearType? EyewearType { get; set; }
    public EyewearFrameColor? EyewearFrameColor { get; set; }
    public EyewearLensColor? EyewearLensColor { get; set; }
    public EarringSide? EarringSide { get; set; }
    public AccessoryColor? EarringColor { get; set; }
    public AccessoryColor? RightWristbandColor { get; set; }
    public AccessoryColor? LeftWristbandColor { get; set; }
  }

  public class PlayerPositionCapabilitiesParameters
  {
    public Grade Pitcher { get; set; }
    public Grade Catcher { get; set; }
    public Grade FirstBase { get; set; }
    public Grade SecondBase { get; set; }
    public Grade ThirdBase { get; set; }
    public Grade Shortstop { get; set; }
    public Grade LeftField { get; set; }
    public Grade CenterField { get; set; }
    public Grade RightField { get; set; }
  }

  public class PlayerHitterAbilityParameters
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }
    public HotZoneGridParameters? HotZoneGridParameters { get; set; }
  }

  public class HotZoneGridParameters
  {
    public HotZonePreference UpAndIn { get; set; }
    public HotZonePreference Up { get; set; }
    public HotZonePreference UpAndAway { get; set; }
    public HotZonePreference MiddleIn { get; set; }
    public HotZonePreference Middle { get; set; }
    public HotZonePreference MiddleAway { get; set; }
    public HotZonePreference DownAndIn { get; set; }
    public HotZonePreference Down { get; set; }
    public HotZonePreference DownAndAway { get; set; }
  }

  public class PlayerPitcherAbilitiesParameters
  {
    public int TopSpeed { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }

    public bool HasTwoSeam { get; set; }
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

  public class PlayerParametersValidator : Validator<PlayerParameters>
  {
    public override void Validate(PlayerParameters parameters)
    {
      new PlayerPersonalDetailsParametersValidator().Validate(parameters.PersonalDetails!);
      new PlayerAppearanceParametersValidator().Validate(parameters.Appearance!);
      new PlayerHitterAbilityParametersValidator().Validate(parameters.HitterAbilities!);
      new PlayerPitcherAbilityParametersValidator().Validate(parameters.PitcherAbilities!);
    }
  }

  public class PlayerPersonalDetailsParametersValidator : Validator<PlayerPersonalDetailsParameters>
  {
    public override void Validate(PlayerPersonalDetailsParameters parameters)
    {
      ThrowIfNullOrEmpty(parameters.FirstName);
      ThrowIfLongerThanMaxLength(parameters.FirstName, 14);

      ThrowIfNullOrEmpty(parameters.LastName);
      ThrowIfLongerThanMaxLength(parameters.LastName, 14);

      if (!parameters.KeepSpecialSavedName)
      {
        ThrowIfNullOrEmpty(parameters.SavedName);
        ThrowIfLongerThanMaxLength(parameters.SavedName, 10);
      }

      ThrowIfNull(parameters.UniformNumber);
      ThrowIfLongerThanMaxLength(parameters.UniformNumber, 3);

      ThrowIfNull(parameters.VoiceId);
      ThrowIfNull(parameters.BattingStanceId);
      ThrowIfNull(parameters.PitchingMechanicsId);
    }
  }

  public class PlayerAppearanceParametersValidator : Validator<PlayerAppearanceParameters>
  {
    public override void Validate(PlayerAppearanceParameters parameters)
    {
      var faceType = FaceTypeHelpers.GetFaceType(parameters.FaceId);

      if (FaceTypeHelpers.CanChooseEyebrows(faceType))
        ThrowIfNull(parameters.EyebrowThickness);
      
      if (FaceTypeHelpers.CanChooseSkinColor(faceType))
        ThrowIfNull(parameters.SkinColor);

      if (FaceTypeHelpers.CanChooseEyes(faceType))
        ThrowIfNull(parameters.EyeColor);

      if(parameters.HairStyle.HasValue)
        ThrowIfNull(parameters.HairColor);

      if (parameters.FacialHairStyle.HasValue)
        ThrowIfNull(parameters.FacialHairColor);

      if (parameters.EyewearType.HasValue && parameters.EyewearType != EyewearType.EyeBlack)
      {
        ThrowIfNull(parameters.EyewearFrameColor);
        ThrowIfNull(parameters.EyewearLensColor);
      }

      if (parameters.EarringSide.HasValue)
        ThrowIfNull(parameters.EarringColor);
    }
  }

  public class PlayerHitterAbilityParametersValidator : Validator<PlayerHitterAbilityParameters>
  {
    public override void Validate(PlayerHitterAbilityParameters parameters)
    {
      ThrowIfNotBetween(parameters.Trajectory, 1, 4);
      ThrowIfNotBetween(parameters.Contact, 1, 15);
      ThrowIfNotBetween(parameters.Power, 0, 255);
      ThrowIfNotBetween(parameters.RunSpeed, 1, 15);
      ThrowIfNotBetween(parameters.ArmStrength, 1, 15);
      ThrowIfNotBetween(parameters.Fielding, 1, 15);
      ThrowIfNotBetween(parameters.ErrorResistance, 1, 15);
      ThrowIfNull(parameters.HotZoneGridParameters);
    }
  }

  public class PlayerPitcherAbilityParametersValidator : Validator<PlayerPitcherAbilitiesParameters>
  {
    public override void Validate(PlayerPitcherAbilitiesParameters parameters)
    {
      ThrowIfNotBetween(parameters.TopSpeed, 49, 105);
      ThrowIfNotBetween(parameters.Control, 0, 255);
      ThrowIfNotBetween(parameters.Stamina, 0, 255);

      if (parameters.HasTwoSeam)
        ThrowIfNull(parameters.TwoSeamMovement);
      ThrowIfNotBetween(parameters.TwoSeamMovement, 1, 3);

      if (parameters.Slider1Type != null)
        ThrowIfNull(parameters.Slider1Movement);
      ThrowIfNotBetween(parameters.Slider1Movement, 1, 7);

      if (parameters.Slider2Type != null)
        ThrowIfNull(parameters.Slider2Movement);
      ThrowIfNotBetween(parameters.Slider2Movement, 1, 7);

      if (parameters.Curve1Type != null)
        ThrowIfNull(parameters.Curve1Movement);
      ThrowIfNotBetween(parameters.Curve1Movement, 1, 7);

      if (parameters.Curve2Type != null)
        ThrowIfNull(parameters.Curve2Movement);
      ThrowIfNotBetween(parameters.Curve2Movement, 1, 7);

      if (parameters.Fork1Type != null)
        ThrowIfNull(parameters.Fork1Movement);
      ThrowIfNotBetween(parameters.Fork1Movement, 1, 7);

      if (parameters.Fork2Type != null)
        ThrowIfNull(parameters.Fork2Movement);
      ThrowIfNotBetween(parameters.Fork2Movement, 1, 7);

      if (parameters.Sinker1Type != null)
        ThrowIfNull(parameters.Sinker1Movement);
      ThrowIfNotBetween(parameters.Sinker1Movement, 1, 7);

      if (parameters.Sinker2Type != null)
        ThrowIfNull(parameters.Sinker2Movement);
      ThrowIfNotBetween(parameters.Sinker2Movement, 1, 7);

      if (parameters.SinkingFastball1Type != null)
        ThrowIfNull(parameters.SinkingFastball1Type);
      ThrowIfNotBetween(parameters.SinkingFastball1Movement, 1, 7);

      if (parameters.SinkingFastball2Type != null)
        ThrowIfNull(parameters.SinkingFastball2Movement);
      ThrowIfNotBetween(parameters.SinkingFastball2Movement, 1, 7);
    } 
  }
}
