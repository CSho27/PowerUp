using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Generators;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Tests.Generators
{
  public class LineupCreatorTests
  {
    private static DictionaryWithSetExpression<Position, Grade> CAPABILITIES => new DictionaryWithSetExpression<Position, Grade>()
    {
      { Position.Pitcher, Grade.G },
      { Position.Catcher, Grade.G },
      { Position.FirstBase, Grade.G },
      { Position.SecondBase, Grade.G },
      { Position.ThirdBase, Grade.G },
      { Position.Shortstop, Grade.G },
      { Position.LeftField, Grade.G },
      { Position.CenterField, Grade.G },
      { Position.RightField, Grade.G }
    };

    [Test]
    public void FindBestPlayerAtEachPosition_FindsBestPlayers()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 60, 10, 130, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 10, 130, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 10, 130, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 82, 10, 130, 9, Position.SecondBase, CAPABILITIES.Set(Position.SecondBase, Grade.A)),
        new LineupParams(5, 85, 10, 130, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 10, 130, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 10, 130, 14, Position.LeftField, CAPABILITIES.Set(Position.LeftField, Grade.A)),
        new LineupParams(8, 76, 10, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 10, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 10, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var bestPlayerByPosition = LineupCreator.FindBestPlayerAtEachPosition(players);
      bestPlayerByPosition.TryGetValue(Position.Pitcher, out var _).ShouldBe(false);
      bestPlayerByPosition[Position.Catcher].ShouldBe(2);
      bestPlayerByPosition[Position.FirstBase].ShouldBe(3);
      bestPlayerByPosition[Position.SecondBase].ShouldBe(4);
      bestPlayerByPosition[Position.ThirdBase].ShouldBe(5);
      bestPlayerByPosition[Position.Shortstop].ShouldBe(6);
      bestPlayerByPosition[Position.LeftField].ShouldBe(7);
      bestPlayerByPosition[Position.CenterField].ShouldBe(8);
      bestPlayerByPosition[Position.RightField].ShouldBe(9);
      bestPlayerByPosition[Position.DesignatedHitter].ShouldBe(10);
    }

    [Test]
    public void FindBestPlayerAtEachPosition_WhenMissingAPosition()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 60, 10, 130, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 10, 130, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 10, 130, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 82, 10, 130, 9, Position.SecondBase, CAPABILITIES.Set(Position.SecondBase, Grade.A)),
        new LineupParams(5, 85, 10, 130, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 10, 130, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 10, 130, 14, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, 10, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 10, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 10, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var bestPlayerByPosition = LineupCreator.FindBestPlayerAtEachPosition(players);
      bestPlayerByPosition.TryGetValue(Position.Pitcher, out var _).ShouldBe(false);
      bestPlayerByPosition[Position.Catcher].ShouldBe(2);
      bestPlayerByPosition[Position.FirstBase].ShouldBe(3);
      bestPlayerByPosition[Position.SecondBase].ShouldBe(4);
      bestPlayerByPosition[Position.ThirdBase].ShouldBe(5);
      bestPlayerByPosition[Position.Shortstop].ShouldBe(6);
      bestPlayerByPosition[Position.LeftField].ShouldBe(7);
      bestPlayerByPosition[Position.CenterField].ShouldBe(8);
      bestPlayerByPosition[Position.RightField].ShouldBe(9);
      bestPlayerByPosition[Position.DesignatedHitter].ShouldBe(10);
    }

    [Test]
    public void FindBestPlayerAtEachPosition_WhenMissing2Positions()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 60, 10, 130, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 10, 130, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 10, 130, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, 10, 130, 9, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, 10, 130, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 10, 130, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 10, 130, 14, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, 10, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 10, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 10, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var bestPlayerByPosition = LineupCreator.FindBestPlayerAtEachPosition(players);
      bestPlayerByPosition.TryGetValue(Position.Pitcher, out var _).ShouldBe(false);
      bestPlayerByPosition[Position.Catcher].ShouldBe(2);
      bestPlayerByPosition[Position.FirstBase].ShouldBe(3);
      bestPlayerByPosition[Position.SecondBase].ShouldBe(4);
      bestPlayerByPosition[Position.ThirdBase].ShouldBe(5);
      bestPlayerByPosition[Position.Shortstop].ShouldBe(6);
      bestPlayerByPosition[Position.LeftField].ShouldBe(7);
      bestPlayerByPosition[Position.CenterField].ShouldBe(8);
      bestPlayerByPosition[Position.RightField].ShouldBe(9);
      bestPlayerByPosition[Position.DesignatedHitter].ShouldBe(10);
    }

    [Test]
    public void FindBestPlayerAtEachPosition_PutsShoheiOhtaniAtDH()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 95, 10, 130, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 10, 130, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 10, 130, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, 10, 130, 9, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, 10, 130, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 10, 130, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 10, 130, 14, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, 10, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 10, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 10, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var bestPlayerByPosition = LineupCreator.FindBestPlayerAtEachPosition(players);
      bestPlayerByPosition.TryGetValue(Position.Pitcher, out var _).ShouldBe(false);
      bestPlayerByPosition[Position.Catcher].ShouldBe(2);
      bestPlayerByPosition[Position.FirstBase].ShouldBe(3);
      bestPlayerByPosition[Position.SecondBase].ShouldBe(4);
      bestPlayerByPosition[Position.ThirdBase].ShouldBe(5);
      bestPlayerByPosition[Position.Shortstop].ShouldBe(6);
      bestPlayerByPosition[Position.LeftField].ShouldBe(7);
      bestPlayerByPosition[Position.CenterField].ShouldBe(8);
      bestPlayerByPosition[Position.RightField].ShouldBe(9);
      bestPlayerByPosition[Position.DesignatedHitter].ShouldBe(1);
    }

    [Test]
    public void CreateLineup_CreatesDHLineupFromPlayers()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 50, 1, 1, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 7, 100, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 11, 225, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, 9, 190, 9, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, 8, 150, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 12, 180, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 6, 150, 14, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, 5, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 8, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 7, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var dhLineup = LineupCreator.CreateLineup(players, useDH: true);
      dhLineup.ElementAt(0).PlayerId.ShouldBe(6);
      dhLineup.ElementAt(0).Position.ShouldBe(Position.Shortstop);

      dhLineup.ElementAt(1).PlayerId.ShouldBe(7);
      dhLineup.ElementAt(1).Position.ShouldBe(Position.LeftField);

      dhLineup.ElementAt(2).PlayerId.ShouldBe(3);
      dhLineup.ElementAt(2).Position.ShouldBe(Position.FirstBase);

      dhLineup.ElementAt(3).PlayerId.ShouldBe(4);
      dhLineup.ElementAt(3).Position.ShouldBe(Position.SecondBase);

      dhLineup.ElementAt(4).PlayerId.ShouldBe(5);
      dhLineup.ElementAt(4).Position.ShouldBe(Position.ThirdBase);

      dhLineup.ElementAt(5).PlayerId.ShouldBe(9);
      dhLineup.ElementAt(5).Position.ShouldBe(Position.RightField);

      dhLineup.ElementAt(6).PlayerId.ShouldBe(10);
      dhLineup.ElementAt(6).Position.ShouldBe(Position.DesignatedHitter);

      dhLineup.ElementAt(7).PlayerId.ShouldBe(8);
      dhLineup.ElementAt(7).Position.ShouldBe(Position.CenterField);

      dhLineup.ElementAt(8).PlayerId.ShouldBe(2);
      dhLineup.ElementAt(8).Position.ShouldBe(Position.Catcher);
    }

    [Test]
    public void CreateLineup_CreatesNoDHLineupFromPlayers()
    {
      var players = new List<LineupParams>()
      {
        new LineupParams(1, 50, 1, 1, 2, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, 7, 100, 6, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, 11, 225, 7, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, 9, 190, 9, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, 8, 150, 8, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, 12, 180, 11, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, 6, 150, 14, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, 5, 130, 13, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, 8, 130, 10, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, 7, 130, 6, Position.DesignatedHitter, CAPABILITIES),
      };

      var dhLineup = LineupCreator.CreateLineup(players, useDH: false);
      dhLineup.ElementAt(0).PlayerId.ShouldBe(6);
      dhLineup.ElementAt(0).Position.ShouldBe(Position.Shortstop);

      dhLineup.ElementAt(1).PlayerId.ShouldBe(7);
      dhLineup.ElementAt(1).Position.ShouldBe(Position.LeftField);

      dhLineup.ElementAt(2).PlayerId.ShouldBe(3);
      dhLineup.ElementAt(2).Position.ShouldBe(Position.FirstBase);

      dhLineup.ElementAt(3).PlayerId.ShouldBe(4);
      dhLineup.ElementAt(3).Position.ShouldBe(Position.SecondBase);

      dhLineup.ElementAt(4).PlayerId.ShouldBe(5);
      dhLineup.ElementAt(4).Position.ShouldBe(Position.ThirdBase);

      dhLineup.ElementAt(5).PlayerId.ShouldBe(9);
      dhLineup.ElementAt(5).Position.ShouldBe(Position.RightField);

      dhLineup.ElementAt(6).PlayerId.ShouldBe(8);
      dhLineup.ElementAt(6).Position.ShouldBe(Position.CenterField);

      dhLineup.ElementAt(7).PlayerId.ShouldBe(2);
      dhLineup.ElementAt(7).Position.ShouldBe(Position.Catcher);

      dhLineup.ElementAt(8).PlayerId.ShouldBe(null);
      dhLineup.ElementAt(8).Position.ShouldBe(Position.Pitcher);
    }
  }

  public class DictionaryWithSetExpression<TKey, TValue> : Dictionary<TKey, TValue>
  {
    public DictionaryWithSetExpression<TKey, TValue> Set(TKey key, TValue value)
    {
      this[key] = value;
      return this;
    }
  }
}
