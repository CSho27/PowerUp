using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamOffsetUtils
  {
    private const long TEAM_START_OFFSET = 0xAA2F4;
    private const long TEAM_LENGTH = 0x140;
    private const long PLAYER_ENTRY_SIZE = 0x8;

    public static long GetTeamOffset(int powerProsTeamId) => TEAM_START_OFFSET + TEAM_LENGTH * (powerProsTeamId - 1);
  }
}
