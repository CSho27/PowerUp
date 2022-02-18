using PowerUp.Entities.Players;
using PowerUp.GameSave;
using System;

namespace PowerUp.Mappers
{
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
        PitcherType = PitcherTypeMapper.ToPitcherType(gsPlayer.IsStarter!.Value, gsPlayer.IsReliever!.Value, gsPlayer.IsCloser!.Value)
      };
    }

    public static GSPlayer MapToGSPlayer(this Player player)
    {
      var gsPlayerNumber = player.UniformNumber.ToGSUniformNumber();
      var gsPitcherType = player.PitcherType.ToGSPitcherType();

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
      };
    }

  }

  public class PlayerMappingParameters
  {
    public PlayerType PlayerType { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
  }
}
