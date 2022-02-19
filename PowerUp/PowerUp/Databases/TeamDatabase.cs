using PowerUp.Entities.Teams;

namespace PowerUp.Databases
{
  public interface ITeamDatabase
  {
    void Save(Team team);
    Team Load(TeamDatabaseKeys teamDatabaseKeys);
  }

  public class TeamDatabase : ITeamDatabase
  {
    private readonly JsonDatabase<Team, TeamDatabaseKeys> _jsonDatabase;

    public TeamDatabase(string teamDirectory)
    {
      _jsonDatabase = new JsonDatabase<Team, TeamDatabaseKeys>(teamDirectory);
    }

    public Team Load(TeamDatabaseKeys teamDatabaseKeys) => _jsonDatabase.Load(teamDatabaseKeys);
    public void Save(Team team) => _jsonDatabase.Save(team);
  }
}
