using PowerUp.Entities.Teams;

namespace PowerUp.Databases
{
  public interface ITeamDatabase
  {
    void Save(Team team);
    Team Load(TeamKeyParams teamDatabaseKeys);
  }

  public class TeamDatabase : ITeamDatabase
  {
    private readonly JsonDatabase<Team, TeamKeyParams> _jsonDatabase;

    public TeamDatabase(string teamDirectory)
    {
      _jsonDatabase = new JsonDatabase<Team, TeamKeyParams>(teamDirectory);
    }

    public Team Load(TeamKeyParams teamDatabaseKeys) => _jsonDatabase.Load(teamDatabaseKeys);
    public void Save(Team team) => _jsonDatabase.Save(team);
  }
}
