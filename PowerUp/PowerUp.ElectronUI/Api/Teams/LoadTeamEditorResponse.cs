using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Entities.Teams;

namespace PowerUp.ElectronUI.Api.Teams
{
  public class LoadTeamEditorResponse
  {
    public string Name { get; }
    public TeamRosterDetails TeamRosterDetails { get; }

    public LoadTeamEditorResponse(Team team)
    {
      Name = team.Name;
      TeamRosterDetails = new TeamRosterDetails(team);
    }
  }

  public class TeamRosterDetails
  {
    public IEnumerable<PlayerRoleDefinitionDto> MLBPlayers { get; }
    public IEnumerable<PlayerRoleDefinitionDto> AAAPlayers { get; }

    public TeamRosterDetails(Team team)
    {
      MLBPlayers = team.PlayerDefinitions.Where(d => !d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
      AAAPlayers = team.PlayerDefinitions.Where(d => d.IsAAA).Select(p => new PlayerRoleDefinitionDto(p));
    }
  }

  public class PlayerRoleDefinitionDto
  {
    public int PlayerId { get; }
    public string FullName { get; }
    public string SavedName { get; }
    public bool IsPinchHitter { get; }
    public bool IsPinchRunner { get; }
    public bool IsDefensiveReplacement { get; }
    public bool IsDefensiveLiability { get; }

    public PlayerRoleDefinitionDto(PlayerRoleDefinition playerRoleDefinition)
    {
      var player = DatabaseConfig.Database.Load<Player>(playerRoleDefinition.PlayerId)!;

      PlayerId = playerRoleDefinition.PlayerId;
      FullName = player.InformalDisplayName;
      SavedName = player.SavedName;
      IsPinchHitter = playerRoleDefinition.IsPinchHitter;
      IsPinchRunner = playerRoleDefinition.IsPinchRunner;
      IsDefensiveReplacement = playerRoleDefinition.IsDefensiveReplacement;
      IsDefensiveLiability = playerRoleDefinition.IsDefensiveReplacement;
    }
  }
}
