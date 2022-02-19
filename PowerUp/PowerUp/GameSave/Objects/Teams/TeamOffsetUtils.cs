namespace PowerUp.GameSave.Objects.Teams
{
  public class TeamOffsetUtils
  {
    private const long TEAM_START_OFFSET = 0xaa2f4;
    private const long TEAM_LENGTH = 0x140;

    public static long GetTeamOffset(int powerProsTeamId) => TEAM_START_OFFSET + TEAM_LENGTH * (powerProsTeamId - 1);
  }
}
