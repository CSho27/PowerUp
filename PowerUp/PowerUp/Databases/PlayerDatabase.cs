using PowerUp.Entities;

namespace PowerUp.Databases
{
  public interface IPlayerDatabase
  {
    void Save(Player player);
    Player Load(PlayerDatabaseKeys playerDatabaseKeys);
  }

  internal class PlayerDatabase : IPlayerDatabase
  {
    private readonly JsonDatabase<Player, PlayerDatabaseKeys> _jsonDatabase;

    public PlayerDatabase(string playerDirectory)
    {
      _jsonDatabase = new JsonDatabase<Player, PlayerDatabaseKeys>(playerDirectory);
    }

    public Player Load(PlayerDatabaseKeys playerDatabaseKeys) => _jsonDatabase.Load(playerDatabaseKeys);
    public void Save(Player player) => _jsonDatabase.Save(player);
  }
}
