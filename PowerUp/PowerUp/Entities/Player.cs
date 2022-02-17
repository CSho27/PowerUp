using PowerUp.Databases;
using System;

namespace PowerUp.Entities
{
  public class Player : IHaveDatabaseKeys<PlayerDatabaseKeys>
  {
    public string SavedName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int? Year { get; set; }
    public bool IsCustom { get; set; }

    PlayerDatabaseKeys IHaveDatabaseKeys<PlayerDatabaseKeys>.DatabaseKeys 
      => !IsCustom
      ? PlayerDatabaseKeys.ForStandardPlayer(LastName, FirstName, Year!.Value, DateOnly.Parse("1/1/1995"))
      : PlayerDatabaseKeys.ForCustomPlayer(LastName, FirstName);
  }

  public class PlayerDatabaseKeys
  {
    public string LastName { get; }
    public string FirstName { get; }
    public int? Year { get; }
    public DateOnly? BirthDate { get; }

    private PlayerDatabaseKeys(string lastName, string firstName, int? year, DateOnly? birthDate)
    {
      LastName = lastName;
      FirstName = firstName;
      Year = year;
      BirthDate = birthDate;
    }

    public static PlayerDatabaseKeys ForStandardPlayer(string lastName, string firstName, int year, DateOnly birthDate)
      => new PlayerDatabaseKeys(lastName, firstName, year, birthDate);

    public static PlayerDatabaseKeys ForCustomPlayer(string lastName, string firstName)
      => new PlayerDatabaseKeys(lastName, firstName, null, null);

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
