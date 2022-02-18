using PowerUp.Entities.Players;
using PowerUp.GameSave;
using System;

namespace PowerUp.Mappers
{
  public class PlayerMappingParameters
  {
    public PlayerType PlayerType { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
  }

  public static class PlayerMapper
  {
    public static Player MapToPlayer(this GSPlayer gsPlayer, PlayerMappingParameters parameters)
    {
      return new Player
      {
        PlayerType = gsPlayer.IsEdited!.Value
          ? PlayerType.Custom
          : parameters.PlayerType,
        LastName = gsPlayer.LastName!,
        FirstName = gsPlayer.FirstName!,
        SavedName = gsPlayer.SavedName!,
        ImportSource = parameters.PlayerType == PlayerType.Imported
          ? parameters.ImportSource
          : null,
        Year = parameters.PlayerType == PlayerType.Generated
          ? parameters.Year
          : null,
        BirthDate = parameters.PlayerType == PlayerType.Generated
          ? parameters.BirthDate
          : null,
        UniformNumber = UniformNumberMapper.ToUniformNumber(gsPlayer.PlayerNumberNumberOfDigits, gsPlayer.PlayerNumber),
        PrimaryPosition = (Position)gsPlayer.PrimaryPosition!,
        PitcherType = PitcherTypeMapper.ToPitcherType(gsPlayer.IsStarter!.Value, gsPlayer.IsReliever!.Value, gsPlayer.IsCloser!.Value),
        VoiceId = gsPlayer.VoiceId!.Value,
        BattingSide = (BattingSide)gsPlayer.BattingSide!,
        BattingStanceId = gsPlayer.BattingForm!.Value,
        ThrowingSide = gsPlayer.ThrowsLefty!.Value
          ? ThrowingSide.Left
          : ThrowingSide.Right,
        PitchingMechanicsId = gsPlayer.PitchingForm!.Value,
        PositonCapabilities = gsPlayer.GetPositionCapabilities(),
        HitterAbilities = gsPlayer.GetHitterAbilities(),
      };
    }

    public static GSPlayer MapToGSPlayer(this Player player)
    {
      var gsPlayerNumber = player.UniformNumber.ToGSUniformNumber();
      var gsPitcherType = player.PitcherType.ToGSPitcherType();
      var positionCapabilities = player.PositonCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var hotZones = player.HitterAbilities.HotZones;

      return new GSPlayer
      {
        LastName = player.LastName,
        FirstName = player.FirstName,
        SavedName = player.SavedName,
        IsEdited = player.PlayerType == PlayerType.Custom,
        PlayerNumber = gsPlayerNumber.uniformNumberValue,
        PlayerNumberNumberOfDigits = gsPlayerNumber.numberOfDigits,
        PrimaryPosition = (ushort)player.PrimaryPosition,
        IsStarter = gsPitcherType.isStarter,
        IsReliever = gsPitcherType.isReliever,
        IsCloser = gsPitcherType.isCloser,
        VoiceId = (ushort)player.VoiceId,
        BattingSide = (ushort)player.BattingSide,
        BattingForm = (ushort)player.BattingStanceId,
        ThrowsLefty = player.ThrowingSide == ThrowingSide.Left,
        PitchingForm = (ushort)player.PitchingMechanicsId,

        // Position Capabilities
        PitcherCapability = (ushort)positionCapabilities.Pitcher,
        CatcherCapability = (ushort)positionCapabilities.Catcher,
        FirstBaseCapability = (ushort)positionCapabilities.FirstBase,
        SecondBaseCapability = (ushort)positionCapabilities.SecondBase,
        ThirdBaseCapability = (ushort)positionCapabilities.ThirdBase,
        ShortstopCapability = (ushort)positionCapabilities.Shortstop,
        LeftFieldCapability = (ushort)positionCapabilities.LeftField,
        CenterFieldCapability = (ushort)positionCapabilities.CenterField,
        RightFieldCapability = (ushort)positionCapabilities.RightField,

        // Hitter Abilities
        Trajectory = (ushort)hitterAbilities.Trajectory,
        Contact = (ushort)hitterAbilities.Contact,
        Power = (ushort)hitterAbilities.Power,
        RunSpeed = (ushort)hitterAbilities.RunSpeed,
        ArmStrength = (ushort)hitterAbilities.ArmStrength,
        Fielding = (ushort)hitterAbilities.Fielding,
        ErrorResistance = (ushort)hitterAbilities.ErrorResistance,

        // Hot Zones
        HotZoneUpAndIn = (ushort)hotZones.UpAndIn,
        HotZoneUp = (ushort)hotZones.Up,
        HotZoneUpAndAway = (ushort)hotZones.UpAndAway,
        HotZoneMiddleIn = (ushort)hotZones.MiddleIn,
        HotZoneMiddle = (ushort)hotZones.Middle,
        HotZoneMiddleAway = (ushort)hotZones.MiddleAway,
        HotZoneDownAndIn = (ushort)hotZones.DownAndIn,
        HotZoneDown = (ushort)hotZones.Down,
        HotZoneDownAndAway = (ushort)hotZones.DownAndAway,
      };
    }

  }
}
