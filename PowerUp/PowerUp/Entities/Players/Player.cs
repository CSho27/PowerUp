using PowerUp.Databases;
using System;

namespace PowerUp.Entities.Players
{
  public class Player : Entity<PlayerKeyParams>
  {
    protected override PlayerKeyParams GetKeyParams() => SourceType switch
    {
      EntitySourceType.Base => PlayerKeyParams.ForBasePlayer(LastName, FirstName, BaseTeam!.Value),
      EntitySourceType.Imported => PlayerKeyParams.ForImportedPlayer(ImportSource!, LastName, FirstName),
      EntitySourceType.Generated => PlayerKeyParams.ForGeneratedPlayer(LastName, FirstName, Year!.Value, BirthDate),
      EntitySourceType.Custom => PlayerKeyParams.ForCustomPlayer(LastName, FirstName),
      _ => throw new NotImplementedException()
    };

    public EntitySourceType SourceType { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? ImportSource { get; set; }
    public MLBPPTeam? BaseTeam { get; set; }

    public string SavedName { get; set; } = string.Empty;
    public string UniformNumber { get; set; } = string.Empty;
    public Position PrimaryPosition { get; set; }
    public PitcherType PitcherType { get; set; }
    public int VoiceId { get; set; }
    public BattingSide BattingSide { get; set; }
    public int BattingStanceId { get; set; }
    public ThrowingSide ThrowingSide { get; set; }
    public int PitchingMechanicsId { get; set; }
    public PositionCapabilities PositonCapabilities { get; set; } = new PositionCapabilities();
    public HitterAbilities HitterAbilities { get; set; } = new HitterAbilities();
    public PitcherAbilities PitcherAbilities { get; set; } = new PitcherAbilities();
  }
}
