using PowerUp.GameSave.Objects.Players;

namespace PowerUp.ElectronUI.Api
{
  public class PlayerEditorDTO
  {
    public int? PowerProsId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SavedName { get; set; }
    public string? Position { get; set; }
    public string? PlayerNumber { get; set; }
    public static PlayerEditorDTO FromGSPlayer(GSPlayer player)
    {
      return new PlayerEditorDTO
      {
        PowerProsId = (int)player.PowerProsId!,
        FirstName = player.FirstName!,
        LastName = player.LastName!,
        SavedName = player.SavedName!,
        Position = "1B",
      };
    }

    public GSPlayer ToGSPlayer()
    {
      return new GSPlayer
      {
        FirstName = FirstName,
        LastName = LastName,
        SavedName = SavedName
      };
    }
  }
}
