namespace PowerUp.Entities.Players
{
  public enum SkinColor 
  { 
    [DisplayName("1")]
    One, 
    [DisplayName("2")]
    Two, 
    [DisplayName("3")]
    Three, 
    [DisplayName("4")]
    Four, 
    [DisplayName("5")]
    Five 
  }
  public enum EyeColor { Brown, Blue }
  public enum EyebrowThickness { Thick, Thin }
  
  public enum BatColor 
  { 
    Natural, 
    Black, 
    [DisplayName("Natural/Black")]
    Natural_Black, 
    [DisplayName("Black/Natural")]
    Black_Natural, 
    Red, 
    Brown, 
    [DisplayName("Red/Black")]
    Red_Black 
  }
  
  public enum GloveColor 
  { 
    Brown, 
    Black,
    Tan, 
    Blue, 
    [DisplayName("Dark Brown")]
    DarkBrown, 
    Red 
  }

  public enum EyewearFrameColor
  {
    Black,
    Gray,
    Red,
    Blue,
    Gold
  }

  public enum EyewearLensColor
  {
    Clear,
    Orange,
    Black
  }

  public enum EarringSide
  {
    Right = 1,
    Left,
    Both
  }

  public enum AccessoryColor
  {
    Black,
    White,
    Red,
    [DisplayName("Dark Blue")]
    DarkBlue,
    Pink,
    Gray,
    Orange,
    Yellow,
    [DisplayName("Light Blue")]
    LightBlue,
    Green
  }

  public class Appearance
  {
    public int FaceId { get; set; }
    public EyebrowThickness? EyebrowThickness { get; set; }
    public SkinColor? SkinColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public int HairStyleId { get; set; }
    public int HairColorId { get; set; }
    public int FacialHairStyleId { get; set; }
    public int FacialHairColorId { get; set; }
    public BatColor BatColor { get; set; }
    public GloveColor GloveColor { get; set; }
    public int? EyewearId { get; set; }
    public EyewearFrameColor? EyewearFrameColor { get; set; }
    public EyewearLensColor? EyewearLensColor { get; set; }
    public EarringSide? EarringSide { get; set;}
    public AccessoryColor? EarringColor { get; set; }
    public AccessoryColor? RightWristbandColor { get; set; }
    public AccessoryColor? LeftWristbandColor { get; set; }
  }
}
