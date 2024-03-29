﻿using PowerUp.Databases;
using PowerUp.ElectronUI.Api.Shared;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
using PowerUp.Generators;
using System.Text.Json.Serialization;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class LoadTeamEditorResponse
  {
    public int TempTeamId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; }
    public bool CanEdit => SourceType.CanEdit();
    public TeamDetails LastSavedDetails { get; }
    public TeamDetails CurrentDetails { get; }
    public DateTime? LastSaved { get; }

    public LoadTeamEditorResponse(Team team, TempTeam tempTeam)
    {
      TempTeamId = tempTeam.Id!.Value;
      SourceType = team.SourceType;
      LastSavedDetails = new TeamDetails(team);
      CurrentDetails = new TeamDetails(tempTeam.Team!);
      LastSaved = tempTeam.LastSaved;
    }
  }

  public class TeamDetails
  {
    public string Name { get; }
    public IEnumerable<PlayerRoleDefinitionDto> MLBPlayers { get; }
    public IEnumerable<PlayerRoleDefinitionDto> AAAPlayers { get; }
    public IEnumerable<LineupSlotDto> NoDHLineup { get; }
    public IEnumerable<LineupSlotDto> DHLineup { get; }

    public TeamDetails(Team team)
    {
      Name = team!.Name;
      MLBPlayers = team.PlayerDefinitions.Where(d => !d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
      AAAPlayers = team.PlayerDefinitions.Where(d => d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
      NoDHLineup = team.NoDHLineup.Select(l => new LineupSlotDto(l));
      DHLineup = team.DHLineup.Select(l => new LineupSlotDto(l));
    }
  }

  public class PlayerRoleDefinitionDto
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; }
    public bool CanEdit => SourceType.CanEdit();
    public int PlayerId { get; }
    public IEnumerable<GeneratorWarning> GeneratedPlayer_Warnings { get; }
    public bool GeneratedPlayer_IsUnedited { get; }

    public bool IsPinchHitter { get; }
    public bool IsPinchRunner { get; }
    public bool IsDefensiveReplacement { get; }
    public bool IsDefensiveLiability { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PitcherRole PitcherRole { get; }

    public PlayerDetailsResponse Details { get; }

    public PlayerRoleDefinitionDto(PlayerRoleDefinition playerRoleDefinition)
    {
      var player = DatabaseConfig.Database.Load<Player>(playerRoleDefinition.PlayerId)!;

      SourceType = player.SourceType;
      PlayerId = playerRoleDefinition.PlayerId;
      IsPinchHitter = playerRoleDefinition.IsPinchHitter;
      IsPinchRunner = playerRoleDefinition.IsPinchRunner;
      IsDefensiveReplacement = playerRoleDefinition.IsDefensiveReplacement;
      IsDefensiveLiability = playerRoleDefinition.IsDefensiveLiability;
      PitcherRole = playerRoleDefinition.PitcherRole;
      Details = new PlayerDetailsResponse(player);
      GeneratedPlayer_Warnings = player.GeneratorWarnings;
      GeneratedPlayer_IsUnedited = player.GeneratedPlayer_IsUnedited;
    }
  }

  public class LineupSlotDto
  {
    public int? PlayerId { get; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Position Position { get; }

    public LineupSlotDto(LineupSlot lineupSlot)
    {
      PlayerId = lineupSlot.PlayerId;
      Position = lineupSlot.Position;
    }
  }
}
