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
        PlayerKeys = playerEntries!
          .Select(e => keysByPPId[e.PowerProsPlayerId!.Value]),
        PlayerRoles = playerEntries!
          .Select(p => p.MapToPlayerRoleDefinition(keysByPPId[p.PowerProsPlayerId!.Value])),
        NoDHLineup = lineupDefinition!.NoDHLineup!
          .Select(p => (
            keys: keysByPPId[p.PowerProsPlayerId!.Value],
            position: (Position)p.Position!
          )),
        DHLineup = lineupDefinition!.DHLineup!
          .Select(p => (
            keys: keysByPPId[p.PowerProsPlayerId!.Value],
            position: (Position)p.Position!
          )),
      };
    }

    public static (GSTeam team, GSLineupDefinition lineupDef) MapToGSTeam(
      this Team team,
      MLBPPTeam mlbPPTeam
    )
    {
      return (new GSTeam(), new GSLineupDefinition());
    }

    private static PlayerRoleDefinition MapToPlayerRoleDefinition(
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
        IsDefensiveLiability = gsPlayerEntry.IsDefensiveReplacement!.Value
      };
    }
  }
}
