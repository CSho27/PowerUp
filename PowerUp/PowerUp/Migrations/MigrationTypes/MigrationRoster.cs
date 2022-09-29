using PowerUp.Entities;
using PowerUp.Entities.Rosters;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Migrations.MigrationTypes
{
  [MigrationTypeFor(typeof(Roster))]
  public class MigrationRoster
  {
    public EntitySourceType? SourceType { get; set; }
    public string? Name { get; set; }
    public int? Year { get; set; }
    public string? ImportSource { get; set; }
    public IDictionary<MLBPPTeam, int>? TeamIdsByPPTeam { get; set; }
    public IEnumerable<int>? FreeAgentPlayerIds { get; set; } = Enumerable.Empty<int>();
  }
}
