using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Mappers
{
  public class TeamMappingParameters
  {
    public bool IsImported { get; set; }
    public string? ImportSource { get; set; }
    public int? Year { get; set; }

    public IDictionary<ushort, PlayerDatabaseKeys>? KeysByPPId { get; set; }
  }

  public static class TeamMapper
  {
    public static Team MapToTeam(
      this GSTeam gsTeam, 
      GSLineupDefinition lineupDefinition, 
      TeamMappingParameters parameters
    )
    {
      if (parameters.KeysByPPId == null)
        throw new ArgumentNullException(nameof(parameters.KeysByPPId));

      var keysByPPId = parameters.KeysByPPId;
      var playerEntries = gsTeam.PlayerEntries;
      var ppTeam = (MLBPPTeam)playerEntries!.First().PowerProsTeamId!.Value;

      return new Team
      {
        SourceType = parameters.IsImported
          ? EntitySourceType.Imported
          : EntitySourceType.Base,
        Name = ppTeam.GetFullDisplayName()!,
        ImportSource = parameters.IsImported
          ? parameters.ImportSource
          : null,
        Players = playerEntries!
          .Where(p => p.PowerProsPlayerId != 0)
          .Select(p => p.MapToPlayerRoleDefinition(keysByPPId[p.PowerProsPlayerId!.Value])),
        NoDHLineup = lineupDefinition!.NoDHLineup!
          .Select(p => (
            keys: p.PowerProsPlayerId!.Value != 0
              ? keysByPPId[p.PowerProsPlayerId!.Value]
              : null,
            position: p.Position != 0
              ? (Position)p.Position!
              : Position.Pitcher
          )),
        DHLineup = lineupDefinition!.DHLineup!
          .Select(p => (
            keys: keysByPPId[p.PowerProsPlayerId!.Value],
            position: (Position)p.Position!
          )),
      };
    }

    public static PlayerRoleDefinition MapToPlayerRoleDefinition(
      this GSTeamPlayerEntry gsPlayerEntry,
      PlayerDatabaseKeys playerKeys
    )
    {
      return new PlayerRoleDefinition(playerKeys)
      {
        IsAAA = gsPlayerEntry.IsAAA!.Value && !gsPlayerEntry.IsMLB!.Value,
        IsPinchHitter = gsPlayerEntry.IsPinchHitter!.Value,
        IsPinchRunner = gsPlayerEntry.IsPinchRunner!.Value,
        IsDefensiveReplacement = gsPlayerEntry.IsDefensiveReplacement!.Value,
        IsDefensiveLiability = gsPlayerEntry.IsDefensiveLiability!.Value,
        PitcherRole = (PitcherRole)gsPlayerEntry.PitcherRole!.Value,
      };
    }

    public static (GSTeam team, GSLineupDefinition lineupDef) MapToGSTeamAndLineup(
      this Team team,
      MLBPPTeam mlbPPTeam,
      IDictionary<PlayerDatabaseKeys, ushort> idsByKey
    )
    {
      return (
        team: team.MapToGSTeam(mlbPPTeam, idsByKey),
        lineupDef: new GSLineupDefinition()
      );
    }

    public static GSTeam MapToGSTeam(
      this Team team,
      MLBPPTeam mlbPPTeam,
      IDictionary<PlayerDatabaseKeys, ushort> idsByKey
    )
    {
      var playerCount = team.Players.Count();
      var emptyPlayerSlots = Enumerable.Repeat(GSTeamPlayerEntry.Empty, 40 - playerCount);

      return new GSTeam
      {
        PlayerEntries = team.Players
          .Select(p => p.MapToGSTeamPlayerEntry(mlbPPTeam, idsByKey[p.PlayerKeys]))
          .Concat(emptyPlayerSlots)
      };
    }

    public static GSTeamPlayerEntry MapToGSTeamPlayerEntry(
      this PlayerRoleDefinition roleDefinition,
      MLBPPTeam mlbPPTeam,
      ushort powerProsId
    )
    {
      return new GSTeamPlayerEntry
      {
        PowerProsTeamId = (ushort)mlbPPTeam,
        PowerProsPlayerId = powerProsId,
        IsAAA = roleDefinition.IsAAA,
        IsMLB = !roleDefinition.IsAAA,
        IsPinchHitter = roleDefinition.IsPinchHitter,
        IsPinchRunner = roleDefinition.IsPinchRunner,
        IsDefensiveReplacement = roleDefinition.IsDefensiveReplacement,
        IsDefensiveLiability = roleDefinition.IsDefensiveLiability,
        PitcherRole = (ushort)roleDefinition.PitcherRole
      };
    }

    public static GSLineupDefinition MapToGSLineup(
      this Team team,
      IDictionary<PlayerDatabaseKeys, ushort> idsByKey
    )
    {
      return new GSLineupDefinition
      {
        NoDHLineup = team.NoDHLineup.Select(e => e.MapToGSLineupPlayer(idsByKey)),
        DHLineup = team.DHLineup.Select(e => e.MapToGSLineupPlayer(idsByKey))
      };
    }

    public static GSLineupPlayer MapToGSLineupPlayer(
      this (PlayerDatabaseKeys? playerKeys, Position position) lineupEntry,
      IDictionary<PlayerDatabaseKeys, ushort> idsByKey
    )
    {
      return new GSLineupPlayer
      {
        PowerProsPlayerId = lineupEntry.playerKeys != null
          ? idsByKey[lineupEntry.playerKeys]
          : (ushort)0,
        Position = lineupEntry.position != Position.Pitcher
          ? (ushort)lineupEntry.position
          : (ushort)0
      };
    }
  }
}
