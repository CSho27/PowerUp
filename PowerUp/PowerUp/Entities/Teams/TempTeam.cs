using PowerUp.Databases;
using System;

namespace PowerUp.Entities.Teams
{
  public class TempTeam : Entity<TempTeam>
  {
    public Team? Team { get; set; }
    public DateTime CreatedOn { get; set; }

    public TempTeam() { }

    public TempTeam(Team team)
    {
      Team = team;
      CreatedOn = DateTime.Now;
    }

    public void UpdateTeam(Team team)
    {
      Team = team;
    }
  }
}
