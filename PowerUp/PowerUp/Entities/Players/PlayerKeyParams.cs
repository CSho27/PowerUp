using PowerUp.Databases;
using System;

namespace PowerUp.Entities.Players
{
  public class PlayerKeyParams : KeyParams
  {
    public string Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? BirthDate { get; set; }

    private PlayerKeyParams(EntitySourceType type, string lastName, string firstName, string? importSource, int? year, DateOnly? birthDate)
    {
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      LastName = lastName;
      FirstName = firstName;
      BirthDate = birthDate?.ToString("mmddyyyy");
    }

    public static PlayerKeyParams ForBasePlayer(string lastName, string firstName)
      => new PlayerKeyParams(
        type: EntitySourceType.Base,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null
      );

    public static PlayerKeyParams ForImportedPlayer(string importSource, string lastName, string firstName)
      => new PlayerKeyParams(
        type: EntitySourceType.Imported,
        lastName: lastName,
        firstName: firstName,
        importSource: importSource,
        year: null,
        birthDate: null
      );

    public static PlayerKeyParams ForGeneratedPlayer(string lastName, string firstName, int year, DateOnly? birthDate)
      => new PlayerKeyParams(
        type: EntitySourceType.Generated,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: year,
        birthDate: birthDate
      );

    public static PlayerKeyParams ForCustomPlayer(string lastName, string firstName)
      => new PlayerKeyParams(
        type: EntitySourceType.Custom,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null
      );

  }
}
