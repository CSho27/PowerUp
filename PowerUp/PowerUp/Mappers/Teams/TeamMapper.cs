using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.GameSave.Api;
using PowerUp.GameSave.Objects.Lineups;
using PowerUp.GameSave.Objects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Mappers
{
  public class TeamMappingParameters
  {
    public bool IsBase { get; set; }
    public string? ImportSource { get; set; }

    public IDictionary<int, int>? IdsByPPId { get; set; }

    public static TeamMappingParameters FromImportParameters(RosterImportParameters importParameters, IDictionary<int, int> idsByPPId)
      => new TeamMappingParameters { IsBase = importParameters.IsBase, ImportSource = importParameters.ImportSource, IdsByPPId = idsByPPId };
  }

  public static class TeamMapper
  {
    public static Team MapToTeam(
      this GSTeam gsTeam, 
      GSLineupDefinition lineupDefinition, 
      TeamMappingParameters parameters
    )
    {
      if (parameters.IdsByPPId == null)
        throw new ArgumentNullException(nameof(parameters.IdsByPPId));

      var idsByPPId = parameters.IdsByPPId;
      var playerEntries = gsTeam.PlayerEntries;
      var ppTeam = (MLBPPTeam)playerEntries!.First().PowerProsTeamId!.Value;

      return new Team
      {
        SourceType = parameters.IsBase
          ? EntitySourceType.Base
          : EntitySourceType.Imported,
        Name = ppTeam.GetFullDisplayName()!,
        ImportSource = parameters.IsBase
          ? null
          : parameters.ImportSource,
        PlayerDefinitions = playerEntries!
          .Where(p => p.PowerProsPlayerId != 0)
          .Select(p => p.MapToPlayerRoleDefinition(idsByPPId)),
        NoDHLineup = lineupDefinition!.NoDHLineup!
          .Select(p => p.MapToLineupSlot(idsByPPId)),
        DHLineup = lineupDefinition!.DHLineup!
          .Select(p => p.MapToLineupSlot(idsByPPId))
      };
    }

    public static PlayerRoleDefinition MapToPlayerRoleDefinition(
      this GSTeamPlayerEntry gsPlayerEntry,
      IDictionary<int, int> idsByPPId
    )
    {
      var playerKey = idsByPPId[gsPlayerEntry.PowerProsPlayerId!.Value];
      return new PlayerRoleDefinition(playerKey)
      {
        IsAAA = gsPlayerEntry.IsAAA!.Value && !gsPlayerEntry.IsMLB!.Value,
        IsPinchHitter = gsPlayerEntry.IsPinchHitter!.Value,
        IsPinchRunner = gsPlayerEntry.IsPinchRunner!.Value,
        IsDefensiveReplacement = gsPlayerEntry.IsDefensiveReplacement!.Value,
        IsDefensiveLiability = gsPlayerEntry.IsDefensiveLiability!.Value,
        PitcherRole = (PitcherRole)gsPlayerEntry.PitcherRole!.Value,
      };
    }

    public static LineupSlot MapToLineupSlot(
      this GSLineupPlayer lineupPlayer,
      IDictionary<int, int> idsByPPId
    )
    {
      return new LineupSlot 
      { 
        PlayerId = lineupPlayer.PowerProsPlayerId != 0
          ? idsByPPId[lineupPlayer.PowerProsPlayerId!.Value]
          : null,
        Position = lineupPlayer.Position != 0
          ? (Position)lineupPlayer.Position!
          : Position.Pitcher
      };
    }

    public static (GSTeam team, GSLineupDefinition lineupDef) MapToGSTeamAndLineup(
      this Team team,
      MLBPPTeam mlbPPTeam,
      IDictionary<int, ushort> idsByKey
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
      IDictionary<int, ushort> ppIdsById
    )
    {
      var playerCount = team.PlayerDefinitions.Count();
      var emptyPlayerSlots = Enumerable.Repeat(GSTeamPlayerEntry.Empty, 40 - playerCount);

      return new GSTeam
      {
        PlayerEntries = team.PlayerDefinitions
          .OrderBy(p => p.IsAAA)
          .OrderBy(p => p.PitcherRole != PitcherRole.SwingMan)
          .OrderBy(p => p.PitcherRole)
          .Select(p => p.MapToGSTeamPlayerEntry(mlbPPTeam, ppIdsById[p.PlayerId]))
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
      IDictionary<int, ushort> ppIdsById
    )
    {
      return new GSLineupDefinition
      {
        NoDHLineup = team.NoDHLineup.Select(e => e.MapToGSLineupPlayer(ppIdsById)),
        DHLineup = team.DHLineup.Select(e => e.MapToGSLineupPlayer(ppIdsById))
      };
    }

    public static GSLineupPlayer MapToGSLineupPlayer(
      this LineupSlot lineupSlot,
      IDictionary<int, ushort> ppIdsById
    )
    {
      return new GSLineupPlayer
      {
        PowerProsPlayerId = lineupSlot.PlayerId != null
          ? ppIdsById[lineupSlot.PlayerId!.Value]
          : (ushort)0,
        Position = lineupSlot.Position != Position.Pitcher
          ? (ushort)lineupSlot.Position
          : (ushort)0
      };
    }
  }
}
