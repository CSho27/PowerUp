using System;

namespace PowerUp.Entities.Players
{
  public class PlayerDatabaseKeys
  {
    public string Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? BirthDate { get; set; }

    private PlayerDatabaseKeys(EntitySourceType type, string lastName, string firstName, string? importSource, int? year, DateOnly? birthDate)
    {
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      LastName = lastName;
      FirstName = firstName;
      BirthDate = birthDate?.ToString("mmddyyyy");
    }

    public static PlayerDatabaseKeys ForBasePlayer(string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: EntitySourceType.Base,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null
      );

    public static PlayerDatabaseKeys ForImportedPlayer(string importSource, string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: EntitySourceType.Imported,
        lastName: lastName,
        firstName: firstName,
        importSource: importSource,
        year: null,
        birthDate: null
      );

    public static PlayerDatabaseKeys ForGeneratedPlayer(string lastName, string firstName, int year, DateOnly? birthDate)
      => new PlayerDatabaseKeys(
        type: EntitySourceType.Generated,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: year,
        birthDate: birthDate
      );

    public static PlayerDatabaseKeys ForCustomPlayer(string lastName, string firstName)
      => new PlayerDatabaseKeys(
        type: EntitySourceType.Custom,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null
      );

  }
}
