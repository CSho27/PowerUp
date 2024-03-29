﻿using PowerUp.Databases;
using PowerUp.Generators;
using System;
using System.Collections.Generic;

namespace PowerUp.Entities.Players
{
  public class Player : Entity<Player>
  {
    public string Identifier => $"P{Id}";
    public EntitySourceType SourceType { get; set; } = EntitySourceType.Custom;
    public override string? GetBaseMatchIdentifier()
    {
      return SourceType == EntitySourceType.Base
        ? $"{SourceType}_{SourcePowerProsId}"
        : null;
    }
    public bool IsCustomPlayer { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FormalDisplayName => $"{LastName}, {FirstName}";
    public string InformalDisplayName => $"{FirstName} {LastName}";
    public string? ImportSource { get; set; }
    public int? SourcePowerProsId { get; set; }

    // Generated Players Only
    public long? GeneratedPlayer_LSPLayerId { get; set; }
    public string? GeneratedPlayer_FullFirstName { get; set; }
    public string? GeneratedPlayer_FullLastName { get; set; }
    public DateTime? GeneratedPlayer_ProDebutDate { get; set; }
    public int? Year { get; set; }
    public List<GeneratorWarning> GeneratorWarnings { get; set; } = new List<GeneratorWarning>();
    public bool GeneratedPlayer_IsUnedited { get; set; }

    public string SavedName { get; set; } = string.Empty;
    public int? SpecialSavedNameId { get; set; }
    public int BirthMonth { get; set; } = 1;
    public int BirthDay { get; set; } = 1;
    public int Age { get; set; } = 29;
    public int YearsInMajors { get; set; } = 6;
    public string UniformNumber { get; set; } = string.Empty;
    public Position PrimaryPosition { get; set; }
    public PitcherType PitcherType { get; set; }
    public int VoiceId { get; set; } = 2052;
    public BattingSide BattingSide { get; set; }
    public int BattingStanceId { get; set; }
    public ThrowingArm ThrowingArm { get; set; }
    public int PitchingMechanicsId { get; set; }
    public double? BattingAverage { get; set; }
    public int? RunsBattedIn { get; set; }
    public int? HomeRuns { get; set; }
    public double? EarnedRunAverage { get; set; }
    public Appearance Appearance { get; set; } = new Appearance();
    public PositionCapabilities PositionCapabilities { get; set; } = new PositionCapabilities();
    public HitterAbilities HitterAbilities { get; set; } = new HitterAbilities();
    public PitcherAbilities PitcherAbilities { get; set; } = new PitcherAbilities();
    public SpecialAbilities SpecialAbilities { get; set; } = new SpecialAbilities();

    public double Overall => PrimaryPosition == Position.Pitcher
      ? PitcherRating
      : HitterRating;
    public double HitterRating => HitterAbilities.GetHitterRating();
    public double PitcherRating => PitcherAbilities.GetPitcherRating();

    public string BatsAndThrows => $"{BattingSide.GetAbbrev()}/{ThrowingArm.GetAbbrev()}";
  }
}
