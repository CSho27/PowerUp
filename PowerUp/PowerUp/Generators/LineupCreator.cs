using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Generators
{
  public class LineupParams
  {
    public long PlayerId { get; }
    public double HitterRating { get; }
    public Position PrimaryPosition { get; }
    public IDictionary<Position, Grade> PositionCapabilities { get; }

    public LineupParams(
      long playerId,
      double hitterRating,
      Position primaryPosition,
      IDictionary<Position, Grade> positionCapabilityDictionary
    )
    {
      PlayerId = playerId;
      HitterRating = hitterRating;
      PrimaryPosition = primaryPosition;
      PositionCapabilities = positionCapabilityDictionary;
    }
  }

  public class LineupResult
  {
    public long PlayerId { get; }
    public Position Position { get; }
  }

  public static class LineupCreator
  {
    public static IEnumerable<LineupResult> CreateLineup(IEnumerable<LineupParams> players, bool useDH)
    {
      var bestPlayerAtEachPosition = FindBestPlayerAtEachPosition(players);
      return Enumerable.Empty<LineupResult>();
    }

    public static Dictionary<Position, long> FindBestPlayerAtEachPosition(IEnumerable<LineupParams> players)
    {
      var orderedPlayers = players.OrderByDescending(p => p.HitterRating).ToList();
      var positionDictionary = new Dictionary<Position, long>();
      var usedPlayerIdHashset = new HashSet<long>();
      var positionList = Enum.GetValues<Position>().ToList();
      var positionsSet = 0;

      var gradeVal = 7;
      while(gradeVal > 0 && positionsSet < positionList.Count)
      {
        var grade = (Grade)gradeVal;

        var i = 0;
        while (i < orderedPlayers.Count && positionsSet < positionList.Count)
        {
          var player = orderedPlayers[i];
          if (!usedPlayerIdHashset.Contains(player.PlayerId))
          {
            var positionsQualifiedFor = player.PositionCapabilities
              .Where(kvp => kvp.Key != Position.Pitcher && kvp.Key != Position.DesignatedHitter && kvp.Value == grade)
              .Select(kvp => (Position?)kvp.Key)
              .OrderByDescending(p => p == player.PrimaryPosition);

            var positionToAdd = positionsQualifiedFor.FirstOrDefault(p => !positionDictionary.TryGetValue(p!.Value, out var _));

            if (positionToAdd.HasValue)
            {
              positionDictionary.Add(positionToAdd.Value, player.PlayerId);
              usedPlayerIdHashset.Add(player.PlayerId);
              positionsSet++;
            }
          }

          i++;
        }

        gradeVal--;
      }

      var dh = orderedPlayers.FirstOrDefault(p => !usedPlayerIdHashset.Contains(p.PlayerId));
      if (dh != null)
        positionDictionary.Add(Position.DesignatedHitter, dh.PlayerId);

      return positionDictionary;
    }
  }
}
