using PowerUp.Databases;
using PowerUp.Entities.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams
{
  public class Team : Entity<Team>
  {
    public EntitySourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImportSource { get; set; }
    
    public IEnumerable<PlayerRoleDefinition> PlayerDefinitions { get; set; } = Enumerable.Empty<PlayerRoleDefinition>();

    public IEnumerable<LineupSlot> NoDHLineup { get; set; } = Enumerable.Empty<LineupSlot>();
    public IEnumerable<LineupSlot> DHLineup { get; set; } = Enumerable.Empty<LineupSlot>();

    private IEnumerable<Player>? players = null;
    public IEnumerable<Player> GetPlayers()
    {
      if(players == null)
        players = PlayerDefinitions.Select(pd => DatabaseConfig.Database.Load<Player>(pd.PlayerId)!);

      return players;
    }

    private IEnumerable<Player> GetHitters() => GetPlayers().Where(p => p.PrimaryPosition != Position.Pitcher);
    private IEnumerable<Player> GetPitchers() => GetPlayers().Where(p => p.PrimaryPosition == Position.Pitcher);

    public double GetHittingRating() => TeamRatingCalculator.CalculateHittingRating(GetHitters().Select(h => h.Overall));
    public double GetPitchingRating() => TeamRatingCalculator.CalculatePitchingRating(GetPitchers().Select(p => p.Overall));
    public double GetOverallRating() => TeamRatingCalculator.CalculateOverallRating(new TeamRatingParameters
    {
      HitterRatings = GetHitters().Select(h => h.Overall),
      PitcherRatings = GetPitchers().Select(p => p.Overall) 
    });
  }
}
