using PowerUp.Databases;
using System.Collections.Generic;

namespace PowerUp.Entities.Players.Queries
{
  public class PlayerSearchQuery
  {
    private readonly string _searchText;

    public PlayerSearchQuery(string searchText)
    {
      _searchText = searchText;
    }

    public IEnumerable<PlayerSearchResult> Execute()
    {
      return DatabaseConfig.Database.Query<PlayerSearchResult, Player>()
        .Where(r => r.FirstName!.StartsWith(_searchText)
          || r.LastName!.StartsWith(_searchText))
        .OrderBy(r => r.FormalDisplayName)
        .ToEnumerable();
    }
  }
  
  public class PlayerSearchResult
  {
    public int Id { get; set; }
    public EntitySourceType SourceType { get; set; }
    public string? UniformNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SavedName { get; set; }
    public string? FormalDisplayName { get; set; }
    public string? InformalDisplayName { get; set; }
    public Position PrimaryPosition { get; set; }
    public string? BatsAndThrows { get; set; }
    public double Overall { get; set; }
  }
}
