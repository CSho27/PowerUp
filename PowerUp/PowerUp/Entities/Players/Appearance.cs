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

  public enum HairStyle
  {
    [DisplayName("Flat (Medium)")]
    FlatMedium = 1,
    [DisplayName("Flat (Long)")]
    FlatLong,
    [DisplayName("Flat (Short)")]
    FlatShort,
    [DisplayName("Flow (Medium)")]
    FlowMedium,
    [DisplayName("Flow (Long)")]
    FlowLong,
    Bob,
    [DisplayName("Dreadlocks")]
    Drealocks,
    Afro,
    [DisplayName("Curly (Medium)")]
    CurlyMedium,
    [DisplayName("Curly (Short)")]
    CurlyShort,
    [DisplayName("Curly Flare (X Long)")]
    CurlyFlareExtraLong,
    [DisplayName("Curly (Long)")]
    CurlyLong,
    [DisplayName("Curly Flare w/ Side Burns (X Long)")]
    CurlyFlareWithSideburnsExtraLong,
    [DisplayName("Curly (X Long)")]
    CurlyExtraLong,
    Basic,
    [DisplayName("Basic w/ Short Sideburns")]
    BasicShortSideburns,
    [DisplayName("Basic w/ Medium Sideburns")]
    BasicMediumSideburns,
    [DisplayName("Basic w/ Long Sideburns")]
    BasicLongSideburns,
    [DisplayName("Fade")]
    Fade,
    [DisplayName("Fade w/ Short Sideburns")]
    FadeShortSideburns,
    [DisplayName("Fade w/ Medium Sideburns")]
    FadeMediumSideburns
  }

  public enum FacialHairStyle
  {
    [DisplayName("Lampshade Mustache")]
    LampshadeMustache = 1,
    [DisplayName("Thin Mustache")]
    ThinMustache,
    [DisplayName("Fu Manchu")]
    FuManchu,
    [DisplayName("Pencil Mustache w/ Gap")]
    PencilMustacheWithGap,
    [DisplayName("Pencil Mustache")]
    PencilMustache,
    [DisplayName("Soul Patch")]
    SoulPatch,
    [DisplayName("Petite Goatee")]
    PetiteGoatee,
    [DisplayName("Thin Petite Goatee")]
    ThinPetiteGoatee,
    [DisplayName("Anchor Goatee")]
    AnchorGoatee,
    [DisplayName("Goatee")]
    Goatee,
    [DisplayName("Rounded Goatee")]
    RoundedGoatee,
    [DisplayName("Goat Patch")]
    GoatPatch,
    [DisplayName("Soul Patch Goatee")]
    SoulPatchGoatee,
    [DisplayName("Thick Circle")]
    ThickCircle,
    [DisplayName("Circle")]
    Circle,
    [DisplayName("Thin Circle")]
    ThinCircle,
    [DisplayName("Full")]
    Full,
    [DisplayName("Van Dyke")]
    VanDyke,
    [DisplayName("Disconnected Circle")]
    DisconnectedCircle,
    [DisplayName("Chinstrap w/ Mustache")]
    ChinstrapWithMustache,
    [DisplayName("Chinstrap")]
    Chinstrap,
    [DisplayName("Thick Chinstrap")]
    ThickChinstrap,
    [DisplayName("Klingon Goatee")]
    KlingonGoatee,
    [DisplayName("Klingon")]
    Klingon,
    [DisplayName("Thick Klingon")]
    ThickKlingon,
    [DisplayName("Thick Full")]
    ThickFull,
    [DisplayName("Thick Full X Long")]
    ThickFullExtraLong,
    [DisplayName("Mutton Chops")]
    MuttonChops
  }

  public enum HairColor
  {
    [DisplayName("Light Blonde")]
    LightBlonde,
    [DisplayName("Blonde")]
    Blonde,
    [DisplayName("Dark Blonde")]
    DarkBlonde,
    [DisplayName("Dark Brown")]
    DarkBrown,
    [DisplayName("Very Light Brown")]
    VeryLightBrown,
    [DisplayName("Very Light Brown w/ Red")]
    VeryLightBrownWithRed,
    [DisplayName("Light Brown")]
    LightBrown,
    [DisplayName("Brown")]
    Brown,
    [DisplayName("Black")]
    Black,
    [DisplayName("Gray")]
    Gray,
    [DisplayName("Yellow")]
    Yellow,
    [DisplayName("Red")]
    Red,
    [DisplayName("Blue")]
    Blue,
    [DisplayName("Green")]
    Green
  }
  
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
    Orange, 
    Black,
    Tan, 
    Blue, 
    Brown, 
    Red 
  }

  public enum EyewearType
  {
    [DisplayName("Eye Black")]
    EyeBlack = 1,
    [DisplayName("Rec Specs")]
    RecSpecs,
    [DisplayName("Rectangle Rec Specs")]
    RectangleRecSpecs,
    [DisplayName("Pointed Rec Specs")]
    PointedRecSpecs,
    [DisplayName("Oversized Rec Specs")]
    OversizedRecSpecs,
    Circle,
    Oval,
    [DisplayName("Thin Oval")]
    ThinOval,
    [DisplayName("Semi-Circle")]
    SemiCircle,
    [DisplayName("Flak Jacket")]
    FlakJacket,
    [DisplayName("Half Jacket")]
    HalfJacket,
    [DisplayName("Radar")]
    Radar,
    [DisplayName("Thin Radar")]
    ThinRadar
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
    public int FaceId { get; set; } = 177;
    public EyebrowThickness? EyebrowThickness { get; set; }
    public SkinColor? SkinColor { get; set; }
    public EyeColor? EyeColor { get; set; }
    public HairStyle? HairStyle { get; set; }
    public HairColor? HairColor { get; set; }
    public FacialHairStyle? FacialHairStyle { get; set; }
    public HairColor? FacialHairColor { get; set; }
    public BatColor BatColor { get; set; }
    public GloveColor GloveColor { get; set; }
    public EyewearType? EyewearType { get; set; }
    public EyewearFrameColor? EyewearFrameColor { get; set; }
    public EyewearLensColor? EyewearLensColor { get; set; }
    public EarringSide? EarringSide { get; set;}
    public AccessoryColor? EarringColor { get; set; }
    public AccessoryColor? RightWristbandColor { get; set; }
    public AccessoryColor? LeftWristbandColor { get; set; }
  }
}
