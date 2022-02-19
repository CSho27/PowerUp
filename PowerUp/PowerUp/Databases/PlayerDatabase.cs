using PowerUp.Entities.Players;

namespace PowerUp.Databases
{
  public interface IPlayerDatabase
  {
    void Save(Player player);
    Player Load(PlayerKeyParams playerDatabaseKeys);
  }

  internal class PlayerDatabase : IPlayerDatabase
  {
    private readonly JsonDatabase<Player, PlayerKeyParams> _jsonDatabase;

    public PlayerDatabase(string playerDirectory)
    {
      _jsonDatabase = new JsonDatabase<Player, PlayerKeyParams>(playerDirectory);
    }

    public Player Load(PlayerKeyParams playerDatabaseKeys) => _jsonDatabase.Load(playerDatabaseKeys);
    public void Save(Player player) => _jsonDatabase.Save(player);
  }
}
