using PowerUp.Validation;
using System;

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

      player.FirstName = parameters.FirstName!;
      player.LastName = parameters.LastName!;

      if(!parameters.KeepSpecialSavedName)
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
  }

  public class PlayerParameters
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

      ThrowIfNull(parameters.VoiceId);
      ThrowIfNull(parameters.BattingStanceId);
      ThrowIfNull(parameters.PitchingMechanicsId);
    }
  }
}
