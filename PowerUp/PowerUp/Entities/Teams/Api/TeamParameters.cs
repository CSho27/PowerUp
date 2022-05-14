using PowerUp.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Entities.Teams.Api
{
  public class TeamParameters
  {
    public string? Name { get; set; }
    public IEnumerable<PlayerRoleParameters> MLBPlayers { get; set; } = Enumerable.Empty<PlayerRoleParameters>();
    public IEnumerable<PlayerRoleParameters> AAAPlayers { get; set; } = Enumerable.Empty<PlayerRoleParameters>();
  }

  public class PlayerRoleParameters
  {
    public int PlayerId { get; set; }
    public bool IsPinchHitter { get; set; }
    public bool IsPinchRunner { get; set; }
    public bool IsDefensiveReplacement { get; set; }
    public bool IsDefensiveLiability { get; set; }
    public PitcherRole PitcherRole { get; set; }
    public int OrderInPitcherRole { get; set; }
  }

  public class TeamParamtersValidator : Validator<TeamParameters>
  {
    public override void Validate(TeamParameters parameters)
    {
      ThrowIfNullOrEmpty(parameters.Name);

      var mlbPlayerCount = parameters.MLBPlayers.Count();
      var overallPlayerCount = mlbPlayerCount + parameters.AAAPlayers.Count();
      if (mlbPlayerCount > 25)
        throw new InvalidOperationException("Cannot have more than 25 players on a MLB Roster");
      if (overallPlayerCount > 40)
        throw new InvalidOperationException("Cannot have more than 40 players on 40-man Roster");

      var distinctCount = parameters.MLBPlayers.Concat(parameters.AAAPlayers).DistinctBy(p => p.PlayerId).Count();
      if (distinctCount != overallPlayerCount)
        throw new InvalidOperationException("Player cannot exist more than once on a team");
    }
  }
}
