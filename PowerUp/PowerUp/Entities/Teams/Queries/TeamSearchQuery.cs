using PowerUp.Databases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams.Queries
{
  public class TeamSearchQuery
  {
    private readonly string _searchText;

    public TeamSearchQuery(string searchText)
    {
      _searchText = searchText;
    }

    public IEnumerable<Team> Execute()
    {
      return DatabaseConfig.Database.Query<Team>()
        .Where(r => r.Name!.Contains(_searchText))
        .OrderBy(r => r.Name)
        .ToEnumerable();
    }
  }
}
