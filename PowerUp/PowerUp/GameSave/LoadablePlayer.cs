namespace PowerUp.GameSave
{
  public class LoadablePlayer
  {
    [GSUInt16(0x00)]
    public int PowerProsId { get; set; }

    [GSString(0x02, stringLength: 10)]
    public string? SavedName { get; set; }

    [GSString(0x16, stringLength: 14)]
    public string? LastName { get; set; }

    [GSString(0x32, stringLength: 14)]
    public string? FirstName { get; set; }
  }
}
