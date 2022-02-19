namespace PowerUp.Entities.Players
{
  public enum SkinColor { One, Two, Three, Four, Five }
  public enum EyeColor { Brown, Blue }
  public enum HairStyle { }

  public class Appearance
  {
    public int FaceId { get; private set; }
    public SkinColor SkinColor { get; private set; }
    public EyeColor EyeColor { get; private set; }
    public HairStyle HairStyle { get; private set; }
  }
}
