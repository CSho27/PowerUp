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

    public IEnumerable<Player> Execute()
    {
      return DatabaseConfig.Database.Query<Player>()
        .Where(r => r.FirstName!.StartsWith(_searchText)
          || r.LastName!.StartsWith(_searchText))
        .OrderBy(r => r.FormalDisplayName)
        .ToEnumerable();
    }
  }
}
