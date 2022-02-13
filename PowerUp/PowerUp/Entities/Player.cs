namespace PowerUp.Entities
{
  public class Player
  {
    public string SavedName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsCustom { get; set; }
  }

  public class Appearance
  {
    public int FaceId { get; private set; }
    public SkinColor SkinColor { get ; private set; }
    public EyeColor EyeColor { get; private set; }
    public HairStyle HairStyle { get; private set; }
  }

  public enum SkinColor { One, Two, Three, Four, Five }
  public enum EyeColor { Brown, Blue }
  public enum HairStyle { }
}
