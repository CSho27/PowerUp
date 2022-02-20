﻿using NUnit.Framework;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Teams;
using PowerUp.Mappers;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Tests.Mappers.Teams
{
  public class TeamMapper_ToTeamTests
  {
    private TeamMappingParameters mappingParameters;
    private GSTeam gsTeam;
    private GSLineupDefinition gsLineupDef;
    private Dictionary<ushort, string> keysByPPId;

    [SetUp]
    public void SetUp()
    {
      keysByPPId = new Dictionary<ushort, string>
      {
        { 1, "Sizemore" },
        { 2, "Nixon" },
        { 3, "Hafner" },
        { 4, "Martinez" },
        { 5, "Blake" },
        { 6, "Dellucci" },
        { 7, "Peralta" },
        { 8, "Barfield" },
        { 9, "Marte" }
      };

      mappingParameters = new TeamMappingParameters
      {
        IsBase = false,
        ImportSource = "Roster1",
        KeysByPPId = keysByPPId
      };

      gsTeam = new GSTeam
      {
        PlayerEntries = keysByPPId.Select(kvp => ToPlayerEntry(kvp.Key))
      };

      var noDHLineup = new[]
      {
        new GSLineupPlayer { PowerProsPlayerId = 1, Position = 8 },
        new GSLineupPlayer { PowerProsPlayerId = 2, Position = 9 },
        new GSLineupPlayer { PowerProsPlayerId = 3, Position = 3 },
        new GSLineupPlayer { PowerProsPlayerId = 4, Position = 2 },
        new GSLineupPlayer { PowerProsPlayerId = 6, Position = 7 },
        new GSLineupPlayer { PowerProsPlayerId = 7, Position = 6 },
        new GSLineupPlayer { PowerProsPlayerId = 8, Position = 4 },
        new GSLineupPlayer { PowerProsPlayerId = 9, Position = 5 },
        new GSLineupPlayer { PowerProsPlayerId = 0, Position = 0 }
      };

      var dhLineup = new[]
      {
        new GSLineupPlayer { PowerProsPlayerId = 1, Position = 8 },
        new GSLineupPlayer { PowerProsPlayerId = 2, Position = 9 },
        new GSLineupPlayer { PowerProsPlayerId = 3, Position = 10 },
        new GSLineupPlayer { PowerProsPlayerId = 4, Position = 2 },
        new GSLineupPlayer { PowerProsPlayerId = 5, Position = 3 },
        new GSLineupPlayer { PowerProsPlayerId = 6, Position = 7 },
        new GSLineupPlayer { PowerProsPlayerId = 7, Position = 6 },
        new GSLineupPlayer { PowerProsPlayerId = 8, Position = 4 },
        new GSLineupPlayer { PowerProsPlayerId = 9, Position = 5 }
      };

      gsLineupDef = new GSLineupDefinition
      {
        NoDHLineup = noDHLineup,
        DHLineup = dhLineup
      };
    }

    private GSTeamPlayerEntry ToPlayerEntry(int powerProsPlayerId)
    {
      return new GSTeamPlayerEntry 
      { 
        PowerProsTeamId = (ushort)MLBPPTeam.Indians, 
        PowerProsPlayerId = (ushort)powerProsPlayerId,
        IsAAA = false,
        IsMLB = true,
        IsPinchHitter = false,
        IsPinchRunner = false,
        IsDefensiveLiability = false,
        IsDefensiveReplacement = false,
        PitcherRole = 5
      };
    }

    [Test]
    public void MapToTeam_ShouldMapName()
    {
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      result.Name.ShouldBe("Cleveland Indians");
    }

    [Test]
    [TestCase(true, EntitySourceType.Base)]
    [TestCase(false, EntitySourceType.Imported)]
    public void MapToTeam_ShouldMapSourceType(bool isBase, EntitySourceType sourceType)
    {
      mappingParameters.IsBase = isBase;
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      result.SourceType.ShouldBe(sourceType);
    }

    [Test]
    public void MapToTeam_ShouldMapPlayerKeys()
    {
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      var ppIdByKeys = mappingParameters.KeysByPPId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
      
      foreach(var p in gsTeam.PlayerEntries)
      {
        result.PlayerDefinitions
          .Where(k => p.PowerProsPlayerId == ppIdByKeys[k.PlayerKey])
          .Count()
          .ShouldBe(1);
      }
    }

    [Test]
    public void MapToTeam_ShouldMapPlayerRoles()
    {
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      var ppIdByKeys = mappingParameters.KeysByPPId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

      foreach (var p in gsTeam.PlayerEntries)
      {
        result.PlayerDefinitions
          .Where(k => p.PowerProsPlayerId == ppIdByKeys[k.PlayerKey])
          .Count()
          .ShouldBe(1);
      }
    }

    [Test]
    public void MapToTeam_ShouldFilterOutZeroPlayerIds()
    {
      gsTeam.PlayerEntries = gsTeam.PlayerEntries.Append(new GSTeamPlayerEntry { PowerProsPlayerId = 0 });
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      var ppIdByKeys = mappingParameters.KeysByPPId.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

      result.PlayerDefinitions.Count().ShouldBe(9);
    }

    [Test]
    public void MapToTeam_ShouldMapNoDHLineup()
    {
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      var noDH = result.NoDHLineup;

      noDH.ElementAt(0).PlayerKey.ShouldBe("Sizemore");
      noDH.ElementAt(0).Position.ShouldBe(Position.CenterField);

      noDH.ElementAt(1).PlayerKey.ShouldBe("Nixon");
      noDH.ElementAt(1).Position.ShouldBe(Position.RightField);


      noDH.ElementAt(2).PlayerKey.ShouldBe("Hafner");
      noDH.ElementAt(2).Position.ShouldBe(Position.FirstBase);


      noDH.ElementAt(3).PlayerKey.ShouldBe("Martinez");
      noDH.ElementAt(3).Position.ShouldBe(Position.Catcher);


      noDH.ElementAt(4).PlayerKey.ShouldBe("Dellucci");
      noDH.ElementAt(4).Position.ShouldBe(Position.LeftField);


      noDH.ElementAt(5).PlayerKey.ShouldBe("Peralta");
      noDH.ElementAt(5).Position.ShouldBe(Position.Shortstop);


      noDH.ElementAt(6).PlayerKey.ShouldBe("Barfield");
      noDH.ElementAt(6).Position.ShouldBe(Position.SecondBase);


      noDH.ElementAt(7).PlayerKey.ShouldBe("Marte");
      noDH.ElementAt(7).Position.ShouldBe(Position.ThirdBase);


      noDH.ElementAt(8).PlayerKey.ShouldBeNull();
      noDH.ElementAt(8).Position.ShouldBe(Position.Pitcher);
    }

    [Test]
    public void MapToTeam_ShouldMapDHLineup()
    {
      var result = gsTeam.MapToTeam(gsLineupDef, mappingParameters);
      var dh = result.DHLineup;

      dh.ElementAt(0).PlayerKey.ShouldBe("Sizemore");
      dh.ElementAt(0).Position.ShouldBe(Position.CenterField);

      dh.ElementAt(1).PlayerKey.ShouldBe("Nixon");
      dh.ElementAt(1).Position.ShouldBe(Position.RightField);


      dh.ElementAt(2).PlayerKey.ShouldBe("Hafner");
      dh.ElementAt(2).Position.ShouldBe(Position.DesignatedHitter);


      dh.ElementAt(3).PlayerKey.ShouldBe("Martinez");
      dh.ElementAt(3).Position.ShouldBe(Position.Catcher);

      dh.ElementAt(4).PlayerKey.ShouldBe("Blake");
      dh.ElementAt(4).Position.ShouldBe(Position.FirstBase);

      dh.ElementAt(5).PlayerKey.ShouldBe("Dellucci");
      dh.ElementAt(5).Position.ShouldBe(Position.LeftField);


      dh.ElementAt(6).PlayerKey.ShouldBe("Peralta");
      dh.ElementAt(6).Position.ShouldBe(Position.Shortstop);


      dh.ElementAt(7).PlayerKey.ShouldBe("Barfield");
      dh.ElementAt(7).Position.ShouldBe(Position.SecondBase);


      dh.ElementAt(8).PlayerKey.ShouldBe("Marte");
      dh.ElementAt(8).Position.ShouldBe(Position.ThirdBase);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsAAA(bool value)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      //These two things are in conflict, so both bits must be set
      gsPlayerEntry.IsAAA = value;
      gsPlayerEntry.IsMLB = !value;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.IsAAA.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsPinchHitter(bool value)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      gsPlayerEntry.IsPinchHitter = value;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.IsPinchHitter.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsPinchRunner(bool value)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      gsPlayerEntry.IsPinchRunner = value;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.IsPinchRunner.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsDefensiveReplacement(bool value)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      gsPlayerEntry.IsDefensiveReplacement = value;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.IsDefensiveReplacement.ShouldBe(value);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void MapToPlayerDefinition_ShouldMapIsDefensiveLiability(bool value)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      gsPlayerEntry.IsDefensiveLiability = value;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.IsDefensiveLiability.ShouldBe(value);
    }

    [Test]
    [TestCase((ushort)0, PitcherRole.Starter)]
    [TestCase((ushort)1, PitcherRole.SwingMan)]
    [TestCase((ushort)2, PitcherRole.LongReliever)]
    [TestCase((ushort)3, PitcherRole.MiddleReliever)]
    [TestCase((ushort)4, PitcherRole.SituationalLefty)]
    [TestCase((ushort)5, PitcherRole.MopUpMan)]
    [TestCase((ushort)6, PitcherRole.SetupMan)]
    [TestCase((ushort)7, PitcherRole.Closer)]
    public void MapToPlayerDefinition_ShouldMapPitcherRole(ushort pitcherRole, PitcherRole expectedValue)
    {
      var gsPlayerEntry = ToPlayerEntry(1);
      gsPlayerEntry.PitcherRole = pitcherRole;
      var result = gsPlayerEntry.MapToPlayerRoleDefinition(keysByPPId);

      result.PitcherRole.ShouldBe(expectedValue);
    }
  }
}