using PowerUp.Databases;

namespace PowerUp.Entities.Teams
{
  public class TempTeam : Entity<TempTeam>
  {
    public Team? Team { get; set; }

    public TempTeam() { }

    public TempTeam(Team team)
    {
      Team = team;
    }

    public void UpdateTeam(Team team)
    {
      Team = team;
    }
  }
}
