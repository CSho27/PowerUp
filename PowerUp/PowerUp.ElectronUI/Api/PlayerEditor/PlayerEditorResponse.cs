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
    public IEnumerable<KeyedCode> PositionCapabilityOptions => EnumExtensions.GetKeyedCodeList<Grade>();

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
}
