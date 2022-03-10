using PowerUp.Databases;
using System;
using System.Collections.Generic;

namespace PowerUp.Entities.Players
{
  public class Player : Entity<PlayerKeyParams>
  {
    public EntitySourceType SourceType { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? ImportSource { get; set; }
    public int? SourcePowerProsId { get; set; }

    public string SavedName { get; set; } = string.Empty;
    public int? SpecialSavedNameId { get; set; }
    public string UniformNumber { get; set; } = string.Empty;
    public Position PrimaryPosition { get; set; }
    public PitcherType PitcherType { get; set; }
    public int VoiceId { get; set; }
    public BattingSide BattingSide { get; set; }
    public int BattingStanceId { get; set; }
    public ThrowingArm ThrowingArm { get; set; }
    public int PitchingMechanicsId { get; set; }
    public PositionCapabilities PositonCapabilities { get; set; } = new PositionCapabilities();
    public HitterAbilities HitterAbilities { get; set; } = new HitterAbilities();
    public PitcherAbilities PitcherAbilities { get; set; } = new PitcherAbilities();

    protected override PlayerKeyParams GetKeyParams() => new PlayerKeyParams
    {
      Type = SourceType.ToString().ToUpperInvariant(),
      LastName = LastName,
      FirstName = FirstName,
      SourceId = SourcePowerProsId,
      ImportSource = ImportSource
    };

    public double GetOverallRating() => PrimaryPosition == Position.Pitcher
      ? PitcherAbilities.GetPitcherRating()
      : HitterAbilities.GetHitterRating();
  }
}
