using PowerUp.Entities.Players;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Teams;

namespace PowerUp.Databases
{
  public static class DatabaseConfig
  {
    public static PlayerDatabase PlayerDatabase { get; private set; } = new PlayerDatabase("");
    public static TeamDatabase TeamDatabase { get; private set; } = new TeamDatabase("");
    public static RosterDatabase RosterDatabase { get; private set; } = new RosterDatabase("");

    public static void Initialize(string dataDirectory)
    {
      PlayerDatabase = new PlayerDatabase(dataDirectory);
    }
  }

  public class PlayerDatabase : JsonDatabase<Player, PlayerKeyParams>
  {
    public PlayerDatabase(string dataDirectory): base(dataDirectory) { }
  }

  public class TeamDatabase : JsonDatabase<Team, TeamKeyParams>
  {
    public TeamDatabase(string dataDirectory) : base(dataDirectory) { }
  }

  public class RosterDatabase : JsonDatabase<Roster, RosterKeyParams>
  {
    public RosterDatabase(string dataDirectory) : base(dataDirectory) { }
  }
}
