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
        Type = parameters.Type,
        LastName = gsPlayer.LastName!,
        FirstName = gsPlayer.FirstName!,
        SavedName = gsPlayer.SavedName!,
        ImportSource = parameters.Type == PlayerType.Imported
          ? parameters.ImportSource
          : null,
        Year = parameters.Type == PlayerType.Generated
          ? parameters.Year
          : null,
        BirthDate = parameters.Type == PlayerType.Generated
          ? parameters.BirthDate
          : null,
      };
    }

    public static GSPlayer MapToGSPlayer(this Player player)
    {
      return new GSPlayer
      {
        LastName = player.LastName,
        FirstName = player.FirstName,
        SavedName = player.SavedName,
        IsEdited = player.Type == PlayerType.Custom
      };
    }

  }

  public class PlayerMappingParameters
  {
    public PlayerType Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
  }
}
