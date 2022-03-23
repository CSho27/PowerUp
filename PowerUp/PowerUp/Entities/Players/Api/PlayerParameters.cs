using PowerUp.Validation;

namespace PowerUp.Entities.Players.Api
{
  public class PlayerParameters
  {
    public PlayerPersonalDetailsParameters? PersonalDetails { get; set; }
    public PlayerPositionCapabilitiesParameters? PositionCapabilities { get; set; }
    public PlayerHitterAbilityParameters? HitterAbilities { get; set; }
  }

  public class PlayerPersonalDetailsParameters
  {
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

  public class PlayerParametersValidator : Validator<PlayerParameters>
  {
    public override void Validate(PlayerParameters parameters)
    {
      new PlayerPersonalDetailsParametersValidator().Validate(parameters.PersonalDetails!);
      new PlayerHitterAbilityParametersValidator().Validate(parameters.HitterAbilities!);
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

  public class PlayerHitterAbilityParametersValidator : Validator<PlayerHitterAbilityParameters>
  {
    public override void Validate(PlayerHitterAbilityParameters parameters)
    {
      ThrowIfNotBetween(parameters.Trajectory, 1, 4);
      ThrowIfNotBetween(parameters.Contact, 1, 15);
      ThrowIfNotBetween(parameters.Power, 1, 255);
      ThrowIfNotBetween(parameters.RunSpeed, 1, 15);
      ThrowIfNotBetween(parameters.ArmStrength, 1, 15);
      ThrowIfNotBetween(parameters.Fielding, 1, 15);
      ThrowIfNotBetween(parameters.ErrorResistance, 1, 15);
      ThrowIfNull(parameters.HotZoneGridParameters);
    }
  }
}
