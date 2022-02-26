namespace PowerUp.Entities.Players
{
  public class PitcherAbilities
  {
    /// <summary>Round down to display</summary>
    public double TopSpeedMph { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }

    public bool HasTwoSeam { get; set; }
    public int? TwoSeamMovement { get; set; }

    public SliderType? Slider1Type { get; set; }
    public int? Slider1Movement { get; set; }

    public SliderType? Slider2Type { get; set; }
    public int? Slider2Movement { get; set; }

    public CurveType? Curve1Type { get; set; }
    public int? Curve1Movement { get; set; }

    public CurveType? Curve2Type { get; set; }
    public int? Curve2Movement { get; set; }

    public ForkType? Fork1Type { get; set; }
    public int? Fork1Movement { get; set; }

    public ForkType? Fork2Type { get; set; }
    public int? Fork2Movement { get; set; }

    public SinkerType? Sinker1Type { get;set; }
    public int? Sinker1Movement { get; set; }

    public SinkerType? Sinker2Type { get;set; }
    public int? Sinker2Movement { get; set; }

    public SinkingFastballType? SinkingFastball1Type { get; set; }
    public int? SinkingFastball1Movement { get; set; }

    public SinkingFastballType? SinkingFastball2Type { get; set; }
    public int? SinkingFastball2Movement { get; set; }

    public double GetPitcherRating()
      => RatingCalculator.CalculatePitcherRating(new PitcherRatingParamters
      {
        TopSpeedMph = TopSpeedMph,
        Control = Control,
        Stamina = Stamina,
        TwoSeamMovement = TwoSeamMovement ?? 0,
        SliderMovement = Slider1Movement ?? 0,
        CurveMovement = Curve1Movement ?? 0,
        ForkMovement = Fork1Movement ?? 0,
        SinkerMovement = Sinker1Movement ?? 0,
        SinkingFastballMovement = SinkingFastball1Movement ?? 0
      });
  }

  public enum SliderType
  {
    [Abbrev("Sld")]
    Slider = 3,
    [Abbrev("H-Sld"), DisplayName("Hard Slider")]
    HardSlider,
    [Abbrev("Cut")]
    Cutter
  }

  public enum CurveType
  {
    [Abbrev("Cb"), DisplayName("Curveball")]
    Curve = 6,
    [Abbrev("SCb"), DisplayName("Slow Curve")]
    SlowCurve,
    [Abbrev("DCb"), DisplayName("Drop Curve")]
    DropCurve = 9,
    [Abbrev("Slv")]
    Slurve,
    [Abbrev("KnCb"), DisplayName("Knuckle Curve")]
    KnuckleCurve
  }

  public enum ForkType
  {
    [Abbrev("Fork")]
    Forkball = 12,
    [Abbrev("Palm")]
    Palmball,
    [Abbrev("Chg"), DisplayName("Change-up")]
    ChangeUp,
    [Abbrev("CChg"), DisplayName("Circle Change-up")]
    CircleChangeUp,
    [Abbrev("V-Sld"), DisplayName("Vertical Slider")]
    VerticalSlider,
    [Abbrev("Kn")]
    Knuckleball,
    [Abbrev("Splt")]
    Splitter,
    [Abbrev("Fosh")]
    Foshball
  }

  public enum SinkerType
  {
    [Abbrev("Snk")]
    Sinker = 20,
    [Abbrev("H-Snk"), DisplayName("Hard Sinker")]
    HardSinker,
    [Abbrev("SC")]
    Screwball
  }

  public enum SinkingFastballType
  {
    [Abbrev("Shu")]
    Shuuto = 23,
    [Abbrev("H-Shu"), DisplayName("Hard Shuuto")]
    HardShuuto,
    [Abbrev("SiFb"), DisplayName("Sinking Fastball")]
    SinkingFastball
  }
}
