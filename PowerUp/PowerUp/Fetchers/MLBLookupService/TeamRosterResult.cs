using PowerUp.Entities.Players;
using System.Collections.Generic;
using System.Linq;

namespace PowerUp.Fetchers.MLBLookupService
{
  public enum PlayerRosterStatus
  {
    Active,
    Assigned,
    Bereavement,
    Claimed,
    Deceased,
    DFA,
    FreeAgent,
    NRI,
    Reassigned,
    Released,
    Restricted,
    Retired,
    Signed,
    Suspended,
    TemporaryInactive,
    Traded,
    Waived,
    IL
  }

  public class TeamRosterResult
  {
    public long TotalResults { get; }
    public IEnumerable<TeamRosterPlayerResult> Results { get; }

    public TeamRosterResult(int totalResults, IEnumerable<LSTeamRosterPlayerResult> results)
    {
      TotalResults = totalResults;
      Results = results.Select(r => new TeamRosterPlayerResult(r));
    }
  }

  public class TeamRosterPlayerResult
  {
    public long LSPlayerId { get; }
    public string? UniformNumber { get; }
    public string FormalDisplayName { get; }
    public PlayerRosterStatus Status { get; }
    public Position Position { get; }
    public BattingSide BattingSide { get; }
    public ThrowingArm ThrowingArm { get; }

    public TeamRosterPlayerResult(LSTeamRosterPlayerResult result)
    {
      LSPlayerId = long.Parse(result.player_id!);
      UniformNumber = result.jersey_number.StringIfNotEmpty();
      FormalDisplayName = result.name_last_first!;
      Status = LookupServiceValueMapper.MapRosterStatus(result.status_short!); 
      Position = LookupServiceValueMapper.MapPosition(result.primary_position_cd!);
      BattingSide = LookupServiceValueMapper.MapBatingSide(result.bats!);
      ThrowingArm = LookupServiceValueMapper.MapThrowingArm(result.throws!);
    }
  }
}
