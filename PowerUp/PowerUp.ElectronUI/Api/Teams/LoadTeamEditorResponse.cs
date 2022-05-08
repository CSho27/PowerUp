﻿using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;
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

    public TeamDetails(Team team)
    {
      Name = team!.Name;
      MLBPlayers = team.PlayerDefinitions.Where(d => !d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
      AAAPlayers = team.PlayerDefinitions.Where(d => d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
    }
  }

  public class PlayerRoleDefinitionDto
  {
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EntitySourceType SourceType { get; }
    public bool CanEdit => SourceType.CanEdit();
    public int PlayerId { get; }
    public string FullName { get; }
    public string SavedName { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Position Position { get; }
    public string PositionAbbreviation { get; set; }
    public int Overall { get; }
    public string BatsAndThrows { get; }
    public bool IsPinchHitter { get; }
    public bool IsPinchRunner { get; }
    public bool IsDefensiveReplacement { get; }
    public bool IsDefensiveLiability { get; }

    public PlayerRoleDefinitionDto(PlayerRoleDefinition playerRoleDefinition)
    {
      var player = DatabaseConfig.Database.Load<Player>(playerRoleDefinition.PlayerId)!;

      SourceType = player.SourceType;
      PlayerId = playerRoleDefinition.PlayerId;
      FullName = player.InformalDisplayName;
      SavedName = player.SavedName;
      Position = player.PrimaryPosition;
      PositionAbbreviation = player.PrimaryPosition.GetAbbrev();
      Overall = player.Overall.RoundDown();
      BatsAndThrows = player.BatsAndThrows;
      IsPinchHitter = playerRoleDefinition.IsPinchHitter;
      IsPinchRunner = playerRoleDefinition.IsPinchRunner;
      IsDefensiveReplacement = playerRoleDefinition.IsDefensiveReplacement;
      IsDefensiveLiability = playerRoleDefinition.IsDefensiveLiability;
    }
  }
}
