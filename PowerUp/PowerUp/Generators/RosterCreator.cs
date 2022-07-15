using PowerUp.Entities.Players;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public class RosterParams
  {
    public long PlayerId { get; }
    public double HitterRating { get; }
    public double PitcherRating { get; }
    public int Contact { get; }
    public int Power { get; }
    public int RunSpeed { get; }
    public Position PrimaryPosition { get; }
    public PitcherType PitcherType { get; }
    public IDictionary<Position, Grade> PositionCapabilities { get; }

    public RosterParams(
      long playerId,
      double hitterRating,
      double pitcherRating,
      int contact,
      int power,
      int runSpeed,
      Position primaryPosition,
      IDictionary<Position, Grade> positionCapabilityDictionary
    )
    {
      PlayerId = playerId;
      HitterRating = hitterRating;
      Contact = contact;
      Power = power;
      RunSpeed = runSpeed;
      PrimaryPosition = primaryPosition;
      PositionCapabilities = positionCapabilityDictionary;
    }
  }

  public static class RosterCreator
  {
    public static RosterResult CreateRosters(IEnumerable<RosterParams> players)
    {
      var playerLineupParams = players.Select(p => new LineupParams(
        playerId: p.PlayerId,
        hitterRating: p.HitterRating,
        contact: p.Contact,
        power: p.Power,
        runSpeed: p.RunSpeed,
        primaryPosition: p.PrimaryPosition,
        positionCapabilityDictionary: p.PositionCapabilities
      ));

      var dhLineup = LineupCreator.CreateLineup(playerLineupParams, useDH: true);
      var noDHLineup = LineupCreator.CreateLineup(playerLineupParams, useDH: false);

      var playersOrderedByHitterAbility = players.OrderByDescending(p => p.HitterRating).ToList();

      var playersOrderedByPitcherAbility = players.OrderByDescending(p => p.PitcherRating).ToList();
      var starters = playersOrderedByPitcherAbility.Where(p => p.PitcherType == PitcherType.Starter).Take(5);
      var closer = playersOrderedByPitcherAbility.Where(p => p.PitcherType == PitcherType.Closer).FirstOrDefault();
      var relievers = playersOrderedByPitcherAbility.Where(p => !starters.Any(s => s.PlayerId == p.PlayerId && p.PlayerId != closer?.PlayerId));

      var twentyFiveManRoster = new HashSet<long>();

      // Add all players in lineups
      foreach (var player in dhLineup.Concat(noDHLineup))
      {
        if (player.PlayerId.HasValue)
          twentyFiveManRoster.Add(player.PlayerId!.Value);
      }

      // Add backup catcher
      var backupCatcher = playersOrderedByHitterAbility.Where(p => !twentyFiveManRoster.Any(id => id == p.PlayerId) && p.PrimaryPosition == Position.Catcher).FirstOrDefault();
      if (backupCatcher != null)
        twentyFiveManRoster.Add(backupCatcher.PlayerId);

      // Add other bench players
      for (var i = 0; i < playersOrderedByHitterAbility.Count && twentyFiveManRoster.Count < 12; i++)
        twentyFiveManRoster.Add(playersOrderedByHitterAbility[i].PlayerId);

      // Add starting pitchers
      foreach (var starter in starters)
        twentyFiveManRoster.Add(starter.PlayerId);

      // Add closer
      if (closer != null)
        twentyFiveManRoster.Add(closer.PlayerId);

      // Add relievers
      for (var i = 0; i < playersOrderedByPitcherAbility.Count && twentyFiveManRoster.Count < 25; i++)
        twentyFiveManRoster.Add(playersOrderedByPitcherAbility[i].PlayerId);

      var fortyManRoster = new HashSet<long>(twentyFiveManRoster);
      // Add hitters to 40 man roster
      for (var i = 0; i < playersOrderedByHitterAbility.Count && fortyManRoster.Count < 33; i++)
        fortyManRoster.Add(playersOrderedByHitterAbility[i].PlayerId);

      // Add pitchers to 40 man roster
      for (var i = 0; i < playersOrderedByPitcherAbility.Count && fortyManRoster.Count < 40; i++)
        fortyManRoster.Add(playersOrderedByPitcherAbility[i].PlayerId);

      return new RosterResult(
        twentyFiveManRoster, 
        fortyManRoster,
        starters.Select(s => s.PlayerId).ToHashSet(),
        closer?.PlayerId,
        noDHLineup,
        dhLineup
      );
    }
  }

  public class RosterResult
  {
    public HashSet<long> TwentyFiveManRoster { get; set; }
    public HashSet<long> FortyManRoster { get; set; }
    public HashSet<long> Starters { get; set; }
    public long? Closer { get; set; }
    public IEnumerable<LineupResult> NoDHLineup { get; set; }
    public IEnumerable<LineupResult> DHLineup { get; set; }

    public RosterResult(
      HashSet<long> twentyFiveManRoster, 
      HashSet<long> fortyManRoster,
      HashSet<long> starters,
      long? closer,
      IEnumerable<LineupResult> noDHLineup,
      IEnumerable<LineupResult> dhLineup
    )
    {
      TwentyFiveManRoster = twentyFiveManRoster;
      FortyManRoster = fortyManRoster;
      Starters = starters;
      Closer = closer;
      NoDHLineup = noDHLineup;
      DHLineup = dhLineup;
    }
  }
}
