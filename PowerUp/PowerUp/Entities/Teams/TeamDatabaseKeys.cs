namespace PowerUp.Entities.Teams
{
  public class TeamDatabaseKeys
  {
    public string Type { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }
    public string Name { get; set; }

    private TeamDatabaseKeys(EntitySourceType type, string name, string? importSource, int? year)
    {
      Type = type.ToString().ToUpperInvariant();
      ImportSource = importSource;
      Year = year;
      Name = name;
    }

    public static TeamDatabaseKeys ForBaseTeam(string name)
      => new TeamDatabaseKeys(
        type: EntitySourceType.Base,
        name: name,
        importSource: null,
        year: null
      );

    public static TeamDatabaseKeys ForImportedTeam(string importSource, string name)
      => new TeamDatabaseKeys(
        type: EntitySourceType.Imported,
        name: name,
        importSource: importSource,
        year: null
      );

    public static TeamDatabaseKeys ForGeneratedTeam(string name, int year)
      => new TeamDatabaseKeys(
        type: EntitySourceType.Generated,
        name: name,
        importSource: null,
        year: year
      );

    public static TeamDatabaseKeys ForCustomTeam(string name)
      => new TeamDatabaseKeys(
        type: EntitySourceType.Custom,
        name: name,
        importSource: null,
        year: null
      );
  }
}
