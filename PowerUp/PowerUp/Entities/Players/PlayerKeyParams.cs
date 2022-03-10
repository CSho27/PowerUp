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
    public int? BasePowerProsTeam { get; set; }
    public string? BirthDate { get; set; }

    private PlayerKeyParams(int id, EntitySourceType type, string lastName, string firstName, string? importSource, int? sourcePowerProsId, int? year, DateOnly? birthDate)
    {
      Id = id;
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      LastName = lastName;
      FirstName = firstName;
      BirthDate = birthDate?.ToString("mmddyyyy");
      BasePowerProsTeam = sourcePowerProsId;
    }

    public static PlayerKeyParams ForBasePlayer(string lastName, string firstName, int sourcePowerProsId)
      => new PlayerKeyParams(
        id: 0,
        type: EntitySourceType.Base,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null,
        sourcePowerProsId: sourcePowerProsId
      );

    public static PlayerKeyParams ForImportedPlayer(string importSource, string lastName, string firstName, int sourcePowerProsId)
      => new PlayerKeyParams(
        id: 0,
        type: EntitySourceType.Imported,
        lastName: lastName,
        firstName: firstName,
        importSource: importSource,
        year: null,
        birthDate: null,
        sourcePowerProsId: sourcePowerProsId
      );

    public static PlayerKeyParams ForGeneratedPlayer(string lastName, string firstName, int year, DateOnly? birthDate)
      => new PlayerKeyParams(
        id: 0,
        type: EntitySourceType.Generated,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: year,
        birthDate: birthDate,
        sourcePowerProsId: null
      );

    public static PlayerKeyParams ForCustomPlayer(string lastName, string firstName)
      => new PlayerKeyParams(
        id: 0,
        type: EntitySourceType.Custom,
        lastName: lastName,
        firstName: firstName,
        importSource: null,
        year: null,
        birthDate: null,
        sourcePowerProsId: null
      );

  }
}
