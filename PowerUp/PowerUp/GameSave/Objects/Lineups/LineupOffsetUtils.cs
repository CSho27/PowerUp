namespace PowerUp.GameSave.Objects.Lineups
{
  public class LineupOffsetUtils
  {
    private const long LINEUP_START_OFFSET = 0xa93b4;
    private const long LINEUP_LENGTH = 0x48;

    public static long GetLineupOffset(int powerProsTeamId) => LINEUP_START_OFFSET + LINEUP_LENGTH * (powerProsTeamId - 1);
  }
}
