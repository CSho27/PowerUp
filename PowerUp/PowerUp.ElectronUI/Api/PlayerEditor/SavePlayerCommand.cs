using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;

namespace PowerUp.ElectronUI.Api.PlayerEditor
{
  public class SavePlayerCommand : ICommand<SavePlayerRequest, ResultResponse>
  {
    private readonly IPlayerApi _playerApi;

    public SavePlayerCommand(IPlayerApi playerApi)
    {
      _playerApi = playerApi;
    }

    public ResultResponse Execute(SavePlayerRequest request)
    {
      if (!request.PlayerId.HasValue)
        throw new ArgumentNullException(nameof(request.PlayerId));

      var player = DatabaseConfig.Database.Load<Player>(request.PlayerId!.Value);
      _playerApi.UpdatePlayer(player!, request.GetParameters());
      DatabaseConfig.Database.Save(player!);

      return ResultResponse.Succeeded();
    }
  }

  public class SavePlayerRequest
  {
    public int? PlayerId { get; set; }
    public PersonalDetailsRequest? PersonalDetails { get; set; }
    public AppearanceRequest? Appearance { get; set; }
    public PositionCapabilitiesRequest? PositionCapabilities { get; set; }
    public HitterAbilitiesRequest? HitterAbilities { get; set; }
    public PitcherAbilitiesRequest? PitcherAbilities { get; set; }
    public SpecialAbilitiesRequest? SpecialAbilities { get; set; }

    public PlayerParameters GetParameters()
    {
      return new PlayerParameters
      {
        PersonalDetails = PersonalDetails!.GetParameters(),
        Appearance = Appearance!.GetParameters(),
        PositionCapabilities = PositionCapabilities!.GetParameters(),
        HitterAbilities = HitterAbilities!.GetParameters(),
        PitcherAbilities = PitcherAbilities!.GetParameters(),
        SpecialAbilities = SpecialAbilities!.GetParameters()
      };
    }
  }

  public class PersonalDetailsRequest
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool UseSpecialSavedName { get; set; }
    public string? SavedName { get; set; }
    public string? UniformNumber { get; set; }
    public string? PositionKey { get; set; }
    public string? PitcherTypeKey { get; set; }
    public int? VoiceId { get; set; }
    public string? BattingSideKey { get; set; }
    public int? BattingStanceId { get; set; }
    public string? ThrowingArmKey { get; set; }
    public int? PitchingMechanicsId { get; set; }

    public PlayerPersonalDetailsParameters GetParameters()
    {
      return new PlayerPersonalDetailsParameters
      {
        FirstName = FirstName,
        LastName = LastName,
        KeepSpecialSavedName = UseSpecialSavedName,
        SavedName = SavedName,
        UniformNumber = UniformNumber,
        Position = Enum.Parse<Position>(PositionKey!),
        PitcherType = Enum.Parse<PitcherType>(PitcherTypeKey!),
        VoiceId = VoiceId,
        BattingSide = Enum.Parse<BattingSide>(BattingSideKey!),
        BattingStanceId = BattingStanceId,
        ThrowingArm = Enum.Parse<ThrowingArm>(ThrowingArmKey!),
        PitchingMechanicsId = PitchingMechanicsId,
      };
    }
  }

  public class AppearanceRequest
  {
    public int? FaceId { get; set; }
    public string? EyebrowThicknessKey { get; set; }
    public string? SkinColorKey { get; set; }
    public string? EyeColorKey { get; set; }
    public string? HairStyleKey { get; set; }
    public string? HairColorKey { get; set; }
    public string? FacialHairStyleKey { get; set; }
    public string? FacialHairColorKey { get; set; }
    public string? BatColorKey { get; set; }
    public string? GloveColorKey { get; set; }
    public string? EyewearTypeKey { get; set; }
    public string? EyewearFrameColorKey { get; set; }
    public string? EyewearLensColorKey { get; set; }
    public string? EarringSideKey { get; set; }
    public string? EarringColorKey { get; set; }
    public string? RightWristbandColorKey { get; set; }
    public string? LeftWristbandColorKey { get; set; }

    public PlayerAppearanceParameters GetParameters()
    {
      var faceType = FaceTypeHelpers.GetFaceType(FaceId!.Value);

      return new PlayerAppearanceParameters
      {
        FaceId = FaceId!.Value,
        EyebrowThickness = EyebrowThicknessKey != null && FaceTypeHelpers.CanChooseEyebrows(faceType)
            ? Enum.Parse<EyebrowThickness>(EyebrowThicknessKey)
            : null,
        SkinColor = SkinColorKey != null && FaceTypeHelpers.CanChooseSkinColor(faceType)
          ? Enum.Parse<SkinColor>(SkinColorKey)
          : null,
        EyeColor = EyeColorKey != null && FaceTypeHelpers.CanChooseEyes(faceType)
            ? Enum.Parse<EyeColor>(EyeColorKey)
            : null,
        HairStyle = HairStyleKey != null
          ? Enum.Parse<HairStyle>(HairStyleKey)
          : null,
        HairColor = HairStyleKey != null && HairColorKey != null
          ? Enum.Parse<HairColor>(HairColorKey)
          : null,
        FacialHairStyle = FacialHairStyleKey != null
          ? Enum.Parse<FacialHairStyle>(FacialHairStyleKey)
          : null,
        FacialHairColor = FacialHairStyleKey != null && FacialHairColorKey != null
          ? Enum.Parse<HairColor>(FacialHairColorKey)
          : null,
        BatColor = Enum.Parse<BatColor>(BatColorKey!),
        GloveColor = Enum.Parse<GloveColor>(GloveColorKey!),
        EyewearType = EyewearTypeKey != null
          ? Enum.Parse<EyewearType>(EyewearTypeKey)
          : null,
        EyewearFrameColor = EyewearTypeKey != null && EyewearFrameColorKey != null
          ? Enum.Parse<EyewearFrameColor>(EyewearFrameColorKey)
          : null,
        EyewearLensColor = EyewearTypeKey != null && EyewearLensColorKey != null
          ? Enum.Parse<EyewearLensColor>(EyewearLensColorKey)
          : null,
        EarringSide = EarringSideKey != null
          ? Enum.Parse<EarringSide>(EarringSideKey)
          : null,
        EarringColor = EarringSideKey != null && EarringColorKey != null
          ? Enum.Parse<AccessoryColor>(EarringColorKey)
          : null,
        RightWristbandColor = RightWristbandColorKey != null
          ? Enum.Parse<AccessoryColor>(RightWristbandColorKey)
          : null,
        LeftWristbandColor = LeftWristbandColorKey != null
          ? Enum.Parse<AccessoryColor>(LeftWristbandColorKey)
          : null,
      };
    }
  }

  public class PositionCapabilitiesRequest
  {
    public string? Pitcher { get; set; }
    public string? Catcher { get; set; }
    public string? FirstBase { get; set; }
    public string? SecondBase { get; set; }
    public string? ThirdBase { get; set; }
    public string? Shortstop { get; set; }
    public string? LeftField { get; set; }
    public string? CenterField { get; set; }
    public string? RightField { get; set; }

    public PlayerPositionCapabilitiesParameters GetParameters()
    {
      return new PlayerPositionCapabilitiesParameters()
      {
        Pitcher = Enum.Parse<Grade>(Pitcher!),
        Catcher = Enum.Parse<Grade>(Catcher!),
        FirstBase = Enum.Parse<Grade>(FirstBase!),
        SecondBase = Enum.Parse<Grade>(SecondBase!),
        ThirdBase = Enum.Parse<Grade>(ThirdBase!),
        Shortstop = Enum.Parse<Grade>(Shortstop!),
        LeftField = Enum.Parse<Grade>(LeftField!),
        CenterField = Enum.Parse<Grade>(CenterField!),
        RightField = Enum.Parse<Grade>(RightField!)
      };
    }
  }
  
  public class HitterAbilitiesRequest
  {
    public int Trajectory { get; set; }
    public int Contact { get; set; }
    public int Power { get; set; }
    public int RunSpeed { get; set; }
    public int ArmStrength { get; set; }
    public int Fielding { get; set; }
    public int ErrorResistance { get; set; }
    public HotZoneGridDto? HotZoneGrid { get; set; }

    public PlayerHitterAbilityParameters GetParameters()
    {
      return new PlayerHitterAbilityParameters
      {
        Trajectory = Trajectory,
        Contact = Contact,
        Power = Power,
        RunSpeed = RunSpeed,
        ArmStrength = ArmStrength,
        Fielding = Fielding,
        ErrorResistance = ErrorResistance,
        HotZoneGridParameters = HotZoneGrid?.GetParameters()
      };
    }
  }

  public class PitcherAbilitiesRequest
  {
    public int TopSpeed { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }

    public string? TwoSeamTypeKey { get; set; }
    public int? TwoSeamMovement { get; set; }

    public string? Slider1TypeKey { get; set; }
    public int? Slider1Movement { get; set; }

    public string? Slider2TypeKey { get; set; }
    public int? Slider2Movement { get; set; }

    public string? Curve1TypeKey { get; set; }
    public int? Curve1Movement { get; set; }

    public string? Curve2TypeKey { get; set; }
    public int? Curve2Movement { get; set; }

    public string? Fork1TypeKey { get; set; }
    public int? Fork1Movement { get; set; }

    public string? Fork2TypeKey { get; set; }
    public int? Fork2Movement { get; set; }

    public string? Sinker1TypeKey { get; set; }
    public int? Sinker1Movement { get; set; }

    public string? Sinker2TypeKey { get; set; }
    public int? Sinker2Movement { get; set; }

    public string? SinkingFastball1TypeKey { get; set; }
    public int? SinkingFastball1Movement { get; set; }

    public string? SinkingFastball2TypeKey { get; set; }
    public int? SinkingFastball2Movement { get; set; }

    public PlayerPitcherAbilitiesParameters GetParameters()
    {
      return new PlayerPitcherAbilitiesParameters
      {
        TopSpeed = TopSpeed,
        Control = Control,
        Stamina = Stamina,

        HasTwoSeam = TwoSeamTypeKey != null,
        TwoSeamMovement = TwoSeamTypeKey != null
          ? TwoSeamMovement
          : null,

        Slider1Type = Slider1TypeKey != null
          ? Enum.Parse<SliderType>(Slider1TypeKey)
          : null,
        Slider1Movement = Slider1Movement != null
          ? Slider1Movement
          : null,

        Slider2Type = Slider2TypeKey != null
          ? Enum.Parse<SliderType>(Slider2TypeKey)
          : null,
        Slider2Movement = Slider2TypeKey != null
          ? Slider2Movement
          : null,

        Curve1Type = Curve1TypeKey != null
          ? Enum.Parse<CurveType>(Curve1TypeKey)
          : null,
        Curve1Movement = Curve1TypeKey != null
          ? Curve1Movement
          : null,

        Curve2Type = Curve2TypeKey != null
          ? Enum.Parse<CurveType>(Curve2TypeKey)
          : null,
        Curve2Movement = Curve2TypeKey != null
          ? Curve2Movement
          : null,

        Fork1Type = Fork1TypeKey != null
          ? Enum.Parse<ForkType>(Fork1TypeKey)
          : null,
        Fork1Movement = Fork1TypeKey != null
          ? Fork1Movement
          : null,

        Fork2Type = Fork2TypeKey != null
          ? Enum.Parse<ForkType>(Fork2TypeKey)
          : null,
        Fork2Movement = Fork2TypeKey != null
          ? Fork2Movement
          : null,

        Sinker1Type = Sinker1TypeKey != null
          ? Enum.Parse<SinkerType>(Sinker1TypeKey)
          : null,
        Sinker1Movement = Sinker1TypeKey != null
          ? Sinker1Movement
          : null,

        Sinker2Type = Sinker2TypeKey != null
          ? Enum.Parse<SinkerType>(Sinker2TypeKey)
          : null,
        Sinker2Movement = Sinker2TypeKey != null
          ? Sinker2Movement
          : null,

        SinkingFastball1Type = SinkingFastball1TypeKey != null
          ? Enum.Parse<SinkingFastballType>(SinkingFastball1TypeKey)
          : null,
        SinkingFastball1Movement = SinkingFastball1TypeKey != null
          ? SinkingFastball1Movement
          : null,

        SinkingFastball2Type = SinkingFastball2TypeKey != null
          ? Enum.Parse<SinkingFastballType>(SinkingFastball2TypeKey)
          : null,
        SinkingFastball2Movement = SinkingFastball2TypeKey != null
          ? SinkingFastball2Movement
          : null
      };
    }
  }
}
