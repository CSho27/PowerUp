using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.GameSave.Objects.Teams
{
  public class Ps2GSTeam : IGSTeam
  {
    public IEnumerable<GSTeamPlayerEntry>? PlayerEntries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
  }

  public class Ps2GSTeamPlayerEntry : IGSTeamPlayerEntry
  {
    public bool? IsAAA { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsDefensiveLiability { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsDefensiveReplacement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsMLB { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsPinchHitter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool? IsPinchRunner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public byte[]? OtherPlayerByte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ushort? PitcherRole { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ushort? PowerProsPlayerId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public ushort? PowerProsTeamId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
  }
}
