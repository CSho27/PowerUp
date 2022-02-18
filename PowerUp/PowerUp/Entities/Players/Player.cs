using PowerUp.Databases;
using System;

namespace PowerUp.Entities.Players
{
  public enum PlayerType
  {
    Base,
    Imported,
    Generated,
    Custom
  }

  public class Player : IHaveDatabaseKeys<PlayerDatabaseKeys>
  {
    public PlayerType PlayerType { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? ImportSource { get; set; }

    public string SavedName { get; set; } = string.Empty;
    public string UniformNumber { get; set; } = string.Empty;
    public Position PrimaryPosition { get; set; }
    public PitcherType PitcherType { get; set; }
    public int VoiceId { get; set; }
    public BattingSide BattingSide { get; set; }
    public int BattingStanceId { get; set; }
    public ThrowingSide ThrowingSide { get; set; }

    PlayerDatabaseKeys IHaveDatabaseKeys<PlayerDatabaseKeys>.DatabaseKeys => PlayerType switch
    {
      PlayerType.Base => PlayerDatabaseKeys.ForBasePlayer(LastName, FirstName),
      PlayerType.Imported => PlayerDatabaseKeys.ForImportedPlayer(ImportSource!, LastName, FirstName),
      PlayerType.Generated => PlayerDatabaseKeys.ForGeneratedPlayer(LastName, FirstName, Year!.Value, BirthDate),
      PlayerType.Custom => PlayerDatabaseKeys.ForCustomPlayer(LastName, FirstName),
      _ => throw new NotImplementedException()
    };
  }

  public class Appearance
  {
    public int FaceId { get; private set; }
    public SkinColor SkinColor { get; private set; }
    public EyeColor EyeColor { get; private set; }
    public HairStyle HairStyle { get; private set; }
  }

  public enum SkinColor { One, Two, Three, Four, Five }
  public enum EyeColor { Brown, Blue }
  public enum HairStyle { }
}
