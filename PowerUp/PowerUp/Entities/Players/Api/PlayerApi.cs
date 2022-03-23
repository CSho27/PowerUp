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
  }
}
