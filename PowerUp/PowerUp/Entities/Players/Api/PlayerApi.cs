namespace PowerUp.Entities.Players.Api
{
  public interface IPlayerApi
  {
    void UpdatePlayer(Player player, PlayerParameters parameters);
  }

  public class PlayerApi : IPlayerApi
  {
    public void UpdatePlayer(Player player, PlayerParameters parameters)
    {
      new PlayerParametersValidator().Validate(parameters);

      UpdatePersonalDetails(player, parameters.PersonalDetails!);
      UpdatePositionCapabilities(player.PositonCapabilities, parameters.PositionCapabilities!);
      UpdateHitterAbilities(player.HitterAbilities, parameters.HitterAbilities!);
      UpdatePitcherAbilities(player.PitcherAbilities, parameters.PitcherAbilities!);
    }

    private void UpdatePersonalDetails(Player player, PlayerPersonalDetailsParameters parameters)
    {
      player.FirstName = parameters.FirstName!;
      player.LastName = parameters.LastName!;

      if (!parameters.KeepSpecialSavedName)
        player.SavedName = parameters.SavedName!;

      player.UniformNumber = parameters.UniformNumber!;
      player.PrimaryPosition = parameters.Position;
      player.PitcherType = parameters.PitcherType;
      player.VoiceId = parameters.VoiceId!.Value;
      player.BattingSide = parameters.BattingSide;
      player.BattingStanceId = parameters.BattingStanceId!.Value;
      player.ThrowingArm = parameters.ThrowingArm;
      player.PitchingMechanicsId = parameters.PitchingMechanicsId!.Value;
    }

    private void UpdatePositionCapabilities(PositionCapabilities positionCapabitlies, PlayerPositionCapabilitiesParameters parameters)
    {
      positionCapabitlies.Pitcher = parameters.Pitcher;
      positionCapabitlies.Catcher = parameters.Catcher;
      positionCapabitlies.FirstBase = parameters.FirstBase;
      positionCapabitlies.SecondBase = parameters.SecondBase;
      positionCapabitlies.ThirdBase = parameters.ThirdBase;
      positionCapabitlies.Shortstop = parameters.Shortstop;
      positionCapabitlies.LeftField = parameters.LeftField;
      positionCapabitlies.CenterField = parameters.CenterField;
      positionCapabitlies.RightField = parameters.RightField;
    }

    private void UpdateHitterAbilities(HitterAbilities hitterAbilities, PlayerHitterAbilityParameters parameters)
    {
      var hzParams = parameters.HotZoneGridParameters!;

      hitterAbilities.Trajectory = parameters.Trajectory;
      hitterAbilities.Contact = parameters.Contact;
      hitterAbilities.Power = parameters.Power;
      hitterAbilities.RunSpeed = parameters.RunSpeed;
      hitterAbilities.ArmStrength = parameters.ArmStrength;
      hitterAbilities.Fielding = parameters.Fielding;
      hitterAbilities.ErrorResistance = parameters.ErrorResistance;

      hitterAbilities.HotZones.UpAndIn = hzParams.UpAndIn;
      hitterAbilities.HotZones.Up = hzParams.Up;
      hitterAbilities.HotZones.UpAndAway = hzParams.UpAndAway;
      hitterAbilities.HotZones.MiddleIn = hzParams.MiddleIn;
      hitterAbilities.HotZones.Middle = hzParams.Middle;
      hitterAbilities.HotZones.MiddleAway = hzParams.MiddleAway;
      hitterAbilities.HotZones.DownAndIn = hzParams.DownAndIn;
      hitterAbilities.HotZones.Down = hzParams.Down;
      hitterAbilities.HotZones.DownAndAway = hzParams.DownAndAway;
    }

    private void UpdatePitcherAbilities(PitcherAbilities pitcherAbilities, PlayerPitcherAbilitiesParameters parameters)
    {
      pitcherAbilities.TopSpeedMph = parameters.TopSpeed;
      pitcherAbilities.Control = parameters.Control;
      pitcherAbilities.Stamina = parameters.Stamina;

      pitcherAbilities.HasTwoSeam = parameters.HasTwoSeam;
      pitcherAbilities.TwoSeamMovement = parameters.HasTwoSeam
        ? parameters.TwoSeamMovement
        : null;

      pitcherAbilities.Slider1Type = parameters.Slider1Type;
      pitcherAbilities.Slider1Movement = parameters.Slider1Type != null
        ? parameters.Slider1Movement
        : null;

      pitcherAbilities.Slider2Type = parameters.Slider2Type;
      pitcherAbilities.Slider2Movement = parameters.Slider2Type != null
        ? parameters.Slider2Movement
        : null;

      pitcherAbilities.Curve1Type = parameters.Curve1Type;
      pitcherAbilities.Curve1Movement = parameters.Curve1Type != null 
        ? parameters.Curve1Movement 
        : null;

      pitcherAbilities.Curve2Type = parameters.Curve2Type;
      pitcherAbilities.Curve2Movement = parameters.Curve2Type != null
        ? parameters.Curve2Movement
        : null;

      pitcherAbilities.Fork1Type = parameters.Fork1Type;
      pitcherAbilities.Fork1Movement = parameters.Fork1Type != null 
        ? parameters.Fork1Movement 
        : null;

      pitcherAbilities.Fork2Type = parameters.Fork2Type;
      pitcherAbilities.Fork2Movement = parameters.Fork2Type != null
        ? parameters.Fork2Movement
        : null;

      pitcherAbilities.Sinker1Type = parameters.Sinker1Type;
      pitcherAbilities.Sinker1Movement = parameters.Sinker1Type != null
        ? parameters.Sinker1Movement
        : null;

      pitcherAbilities.Sinker2Type = parameters.Sinker2Type;
      pitcherAbilities.Sinker2Movement = parameters.Sinker2Type != null
        ? parameters.Sinker2Movement
        : null;

      pitcherAbilities.SinkingFastball1Type = parameters.SinkingFastball1Type;
      pitcherAbilities.SinkingFastball1Movement = parameters.SinkingFastball1Type != null
        ? parameters.SinkingFastball1Movement
        : null;

      pitcherAbilities.SinkingFastball2Type = parameters.SinkingFastball2Type;
      pitcherAbilities.SinkingFastball2Movement = parameters.SinkingFastball2Type != null
        ? parameters.SinkingFastball2Movement
        : null;
    }
  }
}
