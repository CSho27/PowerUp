﻿using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Mappers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Tests.Mappers.Teams
{
  public class TeamMapper_ToGSTeamTests
  {
    private Team team;
    private Dictionary<int, ushort> ppIdsByPlayerId;

    [SetUp]
    public void SetUp()
    {
      ppIdsByPlayerId = new Dictionary<int, ushort>()
      {
        { 1, 1 },
        { 2, 2 },
        { 3, 3 },
        { 4, 4 },
        { 5, 5 },
        { 6, 6 },
        { 7, 7 },
        { 8, 8 },
        { 9, 9 },
      };

      team = new Team()
      {
        PlayerDefinitions = ppIdsByPlayerId.Select(kvp => new PlayerRoleDefinition(kvp.Key))
      };
    }

    [Test]
    public void MapToGSTeam_MapsPlayerEntries()
    {
      var result = TeamMapper.MapToGSTeam(team, MLBPPTeam.Indians, ppIdsByPlayerId);
      var keysById = ppIdsByPlayerId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

      result.PlayerEntries.Count().ShouldBe(40);

      foreach (var roleDef in team.PlayerDefinitions)
      {
        result.PlayerEntries
          .Where(p => {
            keysById.TryGetValue(p.PowerProsPlayerId!.Value, out var keys);
            return keys == roleDef.PlayerId;
          })
          .Count()
          .ShouldBe(1);
      }
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToGSPlayerEntry_ShouldMapIsAAA(bool value)
    {
      var entry = new PlayerRoleDefinition(5) 
      { 
        IsAAA = value 
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.IsAAA.ShouldBe(value);
      result.IsMLB.ShouldBe(!value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsPinchHitter(bool value)
    {
      var entry = new PlayerRoleDefinition(5)
      {
        IsPinchHitter = value
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.IsPinchHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsPinchRunner(bool value)
    {
      var entry = new PlayerRoleDefinition(5)
      {
        IsPinchRunner = value
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.IsPinchRunner.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsDefensiveReplacement(bool value)
    {
      var entry = new PlayerRoleDefinition(5)
      {
        IsDefensiveReplacement = value
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.IsDefensiveReplacement.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsDefensiveLiability(bool value)
    {
      var entry = new PlayerRoleDefinition(5)
      {
        IsDefensiveLiability = value
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.IsDefensiveLiability.ShouldBe(value);
    }

    [Test]
    [TestCase(PitcherRole.Starter, 0)]
    [TestCase(PitcherRole.SwingMan, 1)]
    [TestCase(PitcherRole.LongReliever, 2)]
    [TestCase(PitcherRole.MiddleReliever, 3)]
    [TestCase(PitcherRole.SituationalLefty, 4)]
    [TestCase(PitcherRole.MopUpMan, 5)]
    [TestCase(PitcherRole.SetupMan, 6)]
    [TestCase(PitcherRole.Closer, 7)]
    public void MapToPlayerDefinition_ShouldMapPitcherRole(PitcherRole pitcherRole, int expectedValue)
    {
      var entry = new PlayerRoleDefinition(5)
      {
        PitcherRole = pitcherRole
      };
      var result = TeamMapper.MapToGSTeamPlayerEntry(entry, MLBPPTeam.Phillies, 6);

      result.PitcherRole.ShouldBe((ushort)expectedValue);
    }
  }
}
