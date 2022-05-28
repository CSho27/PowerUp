using PowerUp.Databases;
using System.Collections.Generic;

namespace PowerUp.Entities.Teams.Queries
{
  public class TeamSearchQuery
  {
    private readonly string _searchText;

    public TeamSearchQuery(string searchText)
    {
      _searchText = searchText;
    }

    public IEnumerable<TeamSearchResult> Execute()
    {
      return DatabaseConfig.Database.Query<TeamSearchResult, Team>()
        .Where(r => r.Name!.StartsWith(_searchText))
        .OrderBy(r => r.Name)
        .ToEnumerable();
    }
  }

  public class TeamSearchResult
  {
    public int Id { get; set; }
    public EntitySourceType SourceType { get; set; }
    public string? Name { get; set; }
  }
}
