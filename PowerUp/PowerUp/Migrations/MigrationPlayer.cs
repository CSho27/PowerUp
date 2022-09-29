using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Generators;
using PowerUp.Migrations;
using System;
using System.Collections.Generic;

namespace PowerUp.Databases.MigrationEntities
{
  [MigrationTypeFor(typeof(Player))]
  public class MigrationPlayer
  {
    public int? Id { get; set; }
    public EntitySourceType? SourceType { get; set; }
    public bool? IsCustomPlayer { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; } = string.Empty;
    public string? ImportSource { get; set; }
    public int? SourcePowerProsId { get; set; }

    // Generated Players Only
    public long? GeneratedPlayer_LSPLayerId { get; set; }
    public string? GeneratedPlayer_FullFirstName { get; set; }
    public string? GeneratedPlayer_FullLastName { get; set; }
    public DateTime? GeneratedPlayer_ProDebutDate { get; set; }
    public int? Year { get; set; }
    public List<MigrationGeneratorWarning>? GeneratorWarnings { get; set; } = new List<MigrationGeneratorWarning>();
    public bool? GeneratedPlayer_IsUnedited { get; set; }

    public string? SavedName { get; set; } = string.Empty;
    public int? SpecialSavedNameId { get; set; }
    public int? BirthMonth { get; set; }
    public int? BirthDay { get; set; }
    public int? Age { get; set; }
    public int? YearsInMajors { get; set; }
    public string? UniformNumber { get; set; } = string.Empty;
    public Position? PrimaryPosition { get; set; }
    public PitcherType? PitcherType { get; set; }
    public int? VoiceId { get; set; }
    public BattingSide? BattingSide { get; set; }
    public int? BattingStanceId { get; set; }
    public ThrowingArm? ThrowingArm { get; set; }
    public int? PitchingMechanicsId { get; set; }
    public double? BattingAverage { get; set; }
    public int? RunsBattedIn { get; set; }
    public int? HomeRuns { get; set; }
    public double? EarnedRunAverage { get; set; }
    public MigrationAppearance? Appearance { get; set; } = new MigrationAppearance();
    public MigrationPositionCapabilities? PositionCapabilities { get; set; } = new MigrationPositionCapabilities();
    public HitterAbilities? HitterAbilities { get; set; } = new HitterAbilities();
    public PitcherAbilities? PitcherAbilities { get; set; } = new PitcherAbilities();
    public SpecialAbilities? SpecialAbilities { get; set; } = new SpecialAbilities();
  }

  [MigrationTypeFor(typeof(Appearance))]
  public class MigrationAppearance
  {
    public int? FaceId { get; set; }
    public EyebrowThickness? EyebrowThickness { get; set; }
    public SkinColor? SkinColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairStyle? HairStyle { get; set; }
    public HairColor? HairColor { get; set; }
    public FacialHairStyle? FacialHairStyle { get; set; }
    public HairColor? FacialHairColor { get; set; }
    public BatColor? BatColor { get; set; }
    public GloveColor? GloveColor { get; set; }
    public EyewearType? EyewearType { get; set; }
    public EyewearFrameColor? EyewearFrameColor { get; set; }
    public EyewearLensColor? EyewearLensColor { get; set; }
    public EarringSide? EarringSide { get; set; }
    public AccessoryColor? EarringColor { get; set; }
    public AccessoryColor? RightWristbandColor { get; set; }
    public AccessoryColor? LeftWristbandColor { get; set; }
  }

  [MigrationTypeFor(typeof(PositionCapabilities))]
  public class MigrationPositionCapabilities
  {

  }
}
