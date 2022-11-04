using PowerUp.Databases;
using PowerUp.Migrations;
using System;

namespace PowerUp.Entities.Teams
{
  [MigrationIgnore]
  public class TempTeam : Entity<TempTeam>
  {
    public Team? Team { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? LastSaved { get; set; }

    public TempTeam() { }

    public TempTeam(Team team)
    {
      Team = team;
      CreatedOn = DateTime.Now;
    }
  }
}
