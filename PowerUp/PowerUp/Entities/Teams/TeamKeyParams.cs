namespace PowerUp.Entities.Teams
{
  public class TeamKeyParams
  {
    public string Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public string Name { get; set; }

    private TeamKeyParams(EntitySourceType type, string name, string? importSource, int? year)
    {
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      Name = name;
    }

    public static TeamKeyParams ForBaseTeam(string name)
      => new TeamKeyParams(
        type: EntitySourceType.Base,
        name: name,
        importSource: null,
        year: null
      );

    public static TeamKeyParams ForImportedTeam(string importSource, string name)
      => new TeamKeyParams(
        type: EntitySourceType.Imported,
        name: name,
        importSource: importSource,
        year: null
      );

    public static TeamKeyParams ForGeneratedTeam(string name, int year)
      => new TeamKeyParams(
        type: EntitySourceType.Generated,
        name: name,
        importSource: null,
        year: year
      );

    public static TeamKeyParams ForCustomTeam(string name)
      => new TeamKeyParams(
        type: EntitySourceType.Custom,
        name: name,
        importSource: null,
        year: null
      );
  }
}
