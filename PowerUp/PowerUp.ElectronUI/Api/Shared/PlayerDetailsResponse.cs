﻿using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Generators;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Shared
{
  public class PlayerDetailsResponse
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; }
    public bool CanEdit => SourceType.CanEdit();
    public int PlayerId { get; }
    public string FullName { get; }
    public string SavedName { get; }
    public IEnumerable<GeneratorWarning> GeneratedPlayer_Warnings { get; }
    public bool GeneratedPlayer_IsUnedited { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Position Position { get; }
    public string PositionAbbreviation => Position.GetAbbrev();
    public int Overall { get; }
    public string BatsAndThrows { get; }
    public string PitcherType { get; }
    public string ThrowingArm { get; }
    public string TopSpeed { get; }
    public string Control { get; }
    public string Stamina { get; }

    public PlayerDetailsResponse(Player player)
    {
      SourceType = player.SourceType;
      PlayerId = player.Id!.Value;
      FullName = player.InformalDisplayName;
      SavedName = player.SavedName;
      GeneratedPlayer_Warnings = player.GeneratorWarnings;
      GeneratedPlayer_IsUnedited = player.GeneratedPlayer_IsUnedited;
      Position = player.PrimaryPosition;
      Overall = player.Overall.RoundDown();
      BatsAndThrows = player.BatsAndThrows;
      PitcherType = player.PitcherType.GetAbbrev();
      ThrowingArm = player.ThrowingArm.GetAbbrev();
      TopSpeed = $"{player.PitcherAbilities.TopSpeedMph.RoundDown()}mph";
      Control = player.PitcherAbilities.Control.ToString();
      Stamina = player.PitcherAbilities.Stamina.ToString();
    }
  }
}
