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
    public PositionCapabilityDetailsDto PositionCapabilityDetails { get; }
    public HitterAbilityDetailsDto HitterAbilityDetails { get; }
    public PitcherAbilityDetailsDto PitcherAbilityDetails { get; }

    public PlayerEditorResponse(
      IVoiceLibrary voiceLibrary,
      IBattingStanceLibrary battingStanceLibrary,
      IPitchingMechanicsLibrary pitchingMechanicsLibrary,
      Player player
    )
    {
      Options = new PlayerEditorOptions(voiceLibrary, battingStanceLibrary, pitchingMechanicsLibrary);
      PersonalDetails = new PlayerPersonalDetailsDto(voiceLibrary, battingStanceLibrary, pitchingMechanicsLibrary, player);
      PositionCapabilityDetails = new PositionCapabilityDetailsDto(player.PositonCapabilities);
      HitterAbilityDetails = new HitterAbilityDetailsDto(player.HitterAbilities);
      PitcherAbilityDetails = new PitcherAbilityDetailsDto(player.PitcherAbilities);
    }
  }

  public class PlayerEditorOptions
  {
    public IEnumerable<SimpleCode> VoiceOptions { get; }
    public IEnumerable<KeyedCode> Positions => new PrimaryPositionOptions();
    public IEnumerable<KeyedCode> PitcherTypes => EnumExtensions.GetKeyedCodeList<PitcherType>();
    public IEnumerable<KeyedCode> BattingSideOptions => EnumExtensions.GetKeyedCodeList<BattingSide>();
    public IEnumerable<SimpleCode> BattingStanceOptions { get; }
    public IEnumerable<KeyedCode> ThrowingArmOptions => EnumExtensions.GetKeyedCodeList<ThrowingArm>();
    public IEnumerable<SimpleCode> PitchingMechanicsOptions { get; }
    public IEnumerable<KeyedCode> PositionCapabilityOptions => new GradeOptions();
    public IEnumerable<KeyedCode> TwoSeamOptions => new[] { new KeyedCode("TwoSeam", "Two Seam") };
    public IEnumerable<KeyedCode> SliderOptions => EnumExtensions.GetKeyedCodeList<SliderType>();
    public IEnumerable<KeyedCode> CurveOptions => EnumExtensions.GetKeyedCodeList<CurveType>();
    public IEnumerable<KeyedCode> ForkOptions => EnumExtensions.GetKeyedCodeList<ForkType>();
    public IEnumerable<KeyedCode> SinkerOptions => EnumExtensions.GetKeyedCodeList<SinkerType>();
    public IEnumerable<KeyedCode> SinkingFastballOptions => EnumExtensions.GetKeyedCodeList<SinkingFastballType>();

    public PlayerEditorOptions(
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
    public int TopSpeed { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }

    public KeyedCode? TwoSeamType { get; set; }
    public int? TwoSeamMovement { get; set; }

    public KeyedCode? Slider1Type { get; set; }
    public int? Slider1Movement { get; set; }

    public KeyedCode? Slider2Type { get; set; }
    public int? Slider2Movement { get; set; }

    public KeyedCode? Curve1Type { get; set; }
    public int? Curve1Movement { get; set; }

    public KeyedCode? Curve2Type { get; set; }
    public int? Curve2Movement { get; set; }

    public KeyedCode? Fork1Type { get; set; }
    public int? Fork1Movement { get; set; }

    public KeyedCode? Fork2Type { get; set; }
    public int? Fork2Movement { get; set; }

    public KeyedCode? Sinker1Type { get; set; }
    public int? Sinker1Movement { get; set; }

    public KeyedCode? Sinker2Type { get; set; }
    public int? Sinker2Movement { get; set; }

    public KeyedCode? SinkingFastball1Type { get; set; }
    public int? SinkingFastball1Movement { get; set; }

    public KeyedCode? SinkingFastball2Type { get; set; }
    public int? SinkingFastball2Movement { get; set; }

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
}
