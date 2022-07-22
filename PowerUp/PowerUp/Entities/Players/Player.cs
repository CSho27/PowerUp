using PowerUp.Databases;
using System;

namespace PowerUp.Entities.Players
{
  public class Player : Entity<Player>
  {
    public string Identifier => $"P{Id}";
    public EntitySourceType SourceType { get; set; }
    public bool IsCustomPlayer { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FormalDisplayName => $"{LastName}, {FirstName}";
    public string InformalDisplayName => $"{FirstName} {LastName}";
    public long? GeneratedPlayer_LSPLayerId { get; set; }
    public string? GeneratedPlayer_FullFirstName { get; set; }
    public string? GeneratedPlayer_FullLastName { get; set; }
    public DateTime? GeneratedPlayer_ProDebutDate { get; set; }
    public int? Year { get; set; }
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
    public Appearance Appearance { get; set; } = new Appearance();
    public PositionCapabilities PositionCapabilities { get; set; } = new PositionCapabilities();
    public HitterAbilities HitterAbilities { get; set; } = new HitterAbilities();
    public PitcherAbilities PitcherAbilities { get; set; } = new PitcherAbilities();
    public SpecialAbilities SpecialAbilities { get; set; } = new SpecialAbilities();

    public double Overall => PrimaryPosition == Position.Pitcher
      ? PitcherAbilities.GetPitcherRating()
      : HitterAbilities.GetHitterRating();

    public string BatsAndThrows => $"{BattingSide.GetAbbrev()}/{ThrowingArm.GetAbbrev()}";
  }
}
