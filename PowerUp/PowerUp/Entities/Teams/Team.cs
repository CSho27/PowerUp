using PowerUp.Databases;
using PowerUp.Entities.Players;
using PowerUp.Migrations;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team : Entity<Team>
  {
    public string Identifier => $"T{Id}";
    public EntitySourceType SourceType { get; set; }
    public override string? GetBaseMatchIdentifier()
    {
      return SourceType == EntitySourceType.Base
        ? $"{SourceType}_{Id}_{Name}"
        : null;
    }

    public string Name { get; set; } = string.Empty;
    public string? ImportSource { get; set; }
    public long? GeneratedTeam_LSTeamId { get; set; }
    public int? Year { get; set; }
    
    [MigrationLateMap(typeof(TeamLateMappers.PlayerDefinitionsLateMapper))]
    public IEnumerable<PlayerRoleDefinition> PlayerDefinitions { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    [MigrationLateMap(typeof(TeamLateMappers.NoDHLineupLateMapper))]
    public IEnumerable<LineupSlot> NoDHLineup { get; set; } = Enumerable.Empty<LineupSlot>();

    [MigrationLateMap(typeof(TeamLateMappers.DHLineupLateMapper))]
    public IEnumerable<LineupSlot> DHLineup { get; set; } = Enumerable.Empty<LineupSlot>();

    private IEnumerable<Player>? players = null;
    public IEnumerable<Player> GetPlayers()
    {
      if(players == null)
        players = PlayerDefinitions.Select(pd => DatabaseConfig.Database.Load<Player>(pd.PlayerId)!);

      return players;
    }

    public double GetHittingRating() => TeamRatingCalculator.CalculateHittingRating(GetPlayers().Select(h => h.HitterRating));
    public double GetPitchingRating() => TeamRatingCalculator.CalculatePitchingRating(GetPlayers().Select(p => p.PitcherRating));
    public double GetOverallRating() => TeamRatingCalculator.CalculateOverallRating(new TeamRatingParameters
    {
      HitterRatings = GetPlayers().Select(h => h.HitterRating),
      PitcherRatings = GetPlayers().Select(p => p.PitcherRating) 
    });
  }
}
