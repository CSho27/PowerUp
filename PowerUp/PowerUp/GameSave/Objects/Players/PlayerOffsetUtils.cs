namespace PowerUp.GameSave.Objects.Players
{
  public static class PlayerOffsetUtils
  {
    private const long PLAYER_START_OFFSET = 0x68c74;
    private const long PLAYER_SIZE = 0xb0;

    public static long GetPlayerOffset(int powerProsId) => PLAYER_START_OFFSET + PLAYER_SIZE * (powerProsId - 1);
  }
}
