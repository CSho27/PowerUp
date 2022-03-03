using PowerUp.Validation;
using System;

namespace PowerUp.Entities.Players.Api
{
  public interface IPlayerApi
  {
    void UpdatePlayer(Player player, PlayerParameters parameters);
  }

  public class PlayerApi
  {
    public void UpdatePlayer(Player player, PlayerParameters parameters)
    {
      new PlayerParametersValidator().Validate(parameters);

      player.FirstName = parameters.FirstName!;
      player.LastName = parameters.LastName!;

      if(!parameters.KeepSpecialSavedName)
        player.SavedName = parameters.SavedName!;

      player.UniformNumber = parameters.UniformNumber!;
      player.PrimaryPosition = Enum.Parse<Position>(parameters.PositionKey!);
      player.PitcherType = Enum.Parse<PitcherType>(parameters.PitcherTypeKey!);
      player.VoiceId = parameters.VoiceId!.Value;
      player.BattingSide = Enum.Parse<BattingSide>(parameters.BattingSideKey!);
      player.BattingStanceId = parameters.BattingStanceId!.Value;
      player.ThrowingArm = Enum.Parse<ThrowingArm>(parameters.ThrowingArmKey!);
      player.PitchingMechanicsId = parameters.PitchingMechanicsId!.Value;
    }
  }

  public class PlayerParameters
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool KeepSpecialSavedName { get; set; }
    public string? SavedName { get; set; }
    public string? UniformNumber { get; set; }
    public string? PositionKey { get; set; }
    public string? PitcherTypeKey { get; set; }
    public int? VoiceId { get; set; }
    public string? BattingSideKey { get; set; }
    public int? BattingStanceId { get; set; }
    public string? ThrowingArmKey { get; set; }
    public int? PitchingMechanicsId { get; set; }
  }

  public class PlayerParametersValidator : Validator<PlayerParameters>
  {
    public override void Validate(PlayerParameters parameters)
    {
      ThrowIfNullOrEmpty(parameters.FirstName);
      ThrowIfLongerThanMaxLength(parameters.FirstName, 14);
      
      ThrowIfNullOrEmpty(parameters.LastName);
      ThrowIfLongerThanMaxLength(parameters.LastName, 14);
      
      if(!parameters.KeepSpecialSavedName)
      {
        ThrowIfNullOrEmpty(parameters.SavedName);
        ThrowIfLongerThanMaxLength(parameters.SavedName, 10);
      }

      ThrowIfNull(parameters.UniformNumber);
      ThrowIfLongerThanMaxLength(parameters.UniformNumber, 3);

      ThrowIfNull(parameters.PositionKey);
      ThrowIfNull(parameters.PitcherTypeKey);
      ThrowIfNull(parameters.VoiceId);
      ThrowIfNull(parameters.BattingSideKey);
      ThrowIfNull(parameters.BattingStanceId);
      ThrowIfNull(parameters.ThrowingArmKey);
      ThrowIfNull(parameters.PitchingMechanicsId);
    }
  }
}
