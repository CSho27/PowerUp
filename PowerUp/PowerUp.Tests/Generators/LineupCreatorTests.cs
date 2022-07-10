using NUnit.Framework;
using PowerUp.Entities.Players;
using PowerUp.Generators;
using Shouldly;
using System.Collections.Generic;

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
        new LineupParams(1, 60, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 82, Position.SecondBase, CAPABILITIES.Set(Position.SecondBase, Grade.A)),
        new LineupParams(5, 85, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, Position.LeftField, CAPABILITIES.Set(Position.LeftField, Grade.A)),
        new LineupParams(8, 76, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, Position.DesignatedHitter, CAPABILITIES),
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
        new LineupParams(1, 60, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 82, Position.SecondBase, CAPABILITIES.Set(Position.SecondBase, Grade.A)),
        new LineupParams(5, 85, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, Position.DesignatedHitter, CAPABILITIES),
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
        new LineupParams(1, 60, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, Position.DesignatedHitter, CAPABILITIES),
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
        new LineupParams(1, 95, Position.Pitcher, CAPABILITIES.Set(Position.Pitcher, Grade.A)),
        new LineupParams(2, 80, Position.Catcher, CAPABILITIES.Set(Position.Catcher, Grade.A).Set(Position.FirstBase, Grade.B)),
        new LineupParams(3, 96, Position.FirstBase, CAPABILITIES.Set(Position.FirstBase, Grade.A)),
        new LineupParams(4, 84, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A).Set(Position.LeftField, Grade.C).Set(Position.SecondBase, Grade.E)),
        new LineupParams(5, 85, Position.ThirdBase, CAPABILITIES.Set(Position.ThirdBase, Grade.A)),
        new LineupParams(6, 91, Position.Shortstop, CAPABILITIES.Set(Position.Shortstop, Grade.A)),
        new LineupParams(7, 82, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A).Set(Position.LeftField, Grade.B)),
        new LineupParams(8, 76, Position.CenterField, CAPABILITIES.Set(Position.CenterField, Grade.A)),
        new LineupParams(9, 86, Position.RightField, CAPABILITIES.Set(Position.RightField, Grade.A)),
        new LineupParams(10, 80, Position.DesignatedHitter, CAPABILITIES),
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
