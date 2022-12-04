using System.Collections.Generic;

namespace PowerUp.GameSave.Objects.Teams
{
  public interface IGSTeam
  {
    IEnumerable<GSTeamPlayerEntry>? PlayerEntries { get; set; }
  }

  public interface IGSTeamPlayerEntry
  {
    bool? IsAAA { get; set; }
    bool? IsDefensiveLiability { get; set; }
    bool? IsDefensiveReplacement { get; set; }
    bool? IsMLB { get; set; }
    bool? IsPinchHitter { get; set; }
    bool? IsPinchRunner { get; set; }
    byte[]? OtherPlayerByte { get; set; }
    ushort? PitcherRole { get; set; }
    ushort? PowerProsPlayerId { get; set; }
    ushort? PowerProsTeamId { get; set; }
  }
}