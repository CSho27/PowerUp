﻿using PowerUp.Databases;
using System;

namespace PowerUp.Entities
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
    public PlayerType Type { get; set; }
    public string SavedName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? Year { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? ImportSource { get; set; }

    PlayerDatabaseKeys IHaveDatabaseKeys<PlayerDatabaseKeys>.DatabaseKeys => Type switch
    {
      PlayerType.Base => PlayerDatabaseKeys.ForBasePlayer(LastName, FirstName),
      PlayerType.Imported => PlayerDatabaseKeys.ForImportedPlayer(ImportSource!, LastName, FirstName),
      PlayerType.Generated => PlayerDatabaseKeys.ForGeneratedPlayer(LastName, FirstName, Year!.Value, BirthDate),
      PlayerType.Custom => PlayerDatabaseKeys.ForCustomPlayer(LastName, FirstName),
      _ => throw new NotImplementedException()
    };
  }

  public class PlayerDatabaseKeys
  {
    public string Type { get; }
    public string? ImportSource { get; }
    public int? Year { get; }
    public string LastName { get; }
    public string FirstName { get; }
    public string? BirthDate { get; }

    private PlayerDatabaseKeys(PlayerType type, string lastName, string firstName, string? importSource, int? year, DateOnly? birthDate)
    {
      Type = type.ToString().ToUpperInvariant();
      Year = year;
      LastName = lastName;
      FirstName = firstName;
      BirthDate = birthDate?.ToString("mmddyyyy");
    }

    public static PlayerDatabaseKeys ForBasePlayer(string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: PlayerType.Base,
        lastName: lastName, 
        firstName: firstName, 
        importSource: null,
        year: null, 
        birthDate: null
      );

    public static PlayerDatabaseKeys ForImportedPlayer(string importSource, string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: PlayerType.Imported,
        lastName: lastName,
        firstName: firstName,
        importSource: importSource,
        year: null,
        birthDate: null
      );

    public static PlayerDatabaseKeys ForGeneratedPlayer(string lastName, string firstName, int year, DateOnly? birthDate)
      => new PlayerDatabaseKeys(
        type: PlayerType.Generated, 
        lastName: lastName, 
        firstName: firstName, 
        importSource: null,
        year: year,
        birthDate: birthDate
      );

    public static PlayerDatabaseKeys ForCustomPlayer(string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: PlayerType.Custom,
        lastName: lastName, 
        firstName: firstName, 
        importSource: null, 
        year: null,
        birthDate: null
      );

  }

  public class Appearance
  {
    public int FaceId { get; private set; }
    public SkinColor SkinColor { get ; private set; }
    public EyeColor EyeColor { get; private set; }
    public HairStyle HairStyle { get; private set; }
  }

  public enum SkinColor { One, Two, Three, Four, Five }
  public enum EyeColor { Brown, Blue }
  public enum HairStyle { }
}
