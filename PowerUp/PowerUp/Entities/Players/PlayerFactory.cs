namespace PowerUp.Entities.Players
{
  public static class PlayerFactory
  {
    public static Player BuildDefaultHitter(EntitySourceType sourceType)
    {
      return new Player()
      {
        SourceType = sourceType,
        IsCustomPlayer = false,
        LastName = "First",
        FirstName = "Last",
        SavedName = "(New!)",
        UniformNumber = "000",
        PrimaryPosition = Position.DesignatedHitter,
        PitcherType = PitcherType.SwingMan,
        VoiceId = 2052,
        BattingSide = BattingSide.Right,
        BattingStanceId = 9,
        ThrowingArm = ThrowingArm.Right,
        PitchingMechanicsId = 0,
        Appearance = DEFAULT_APPEARANCE,
        PositionCapabilities = DEFAULT_POS_CAPABILITIES_FOR_HITTER,
        HitterAbilities = DEFAULT_HITTER_ABILITIES_FOR_HITTER,
        PitcherAbilities = DEFAULT_PITCHER_ABILITIES_FOR_HITTER,
        SpecialAbilities = new SpecialAbilities()
      };
    }

    public static Player BuildDefaultPitcher(EntitySourceType sourceType)
    {
      return new Player()
      {
        SourceType = sourceType,
        IsCustomPlayer = false,
        LastName = "First",
        FirstName = "Last",
        SavedName = "(New!)",
        UniformNumber = "000",
        PrimaryPosition = Position.Pitcher,
        PitcherType = PitcherType.Reliever,
        VoiceId = 2052,
        BattingSide = BattingSide.Right,
        BattingStanceId = 9,
        ThrowingArm = ThrowingArm.Right,
        PitchingMechanicsId = 0,
        Appearance = DEFAULT_APPEARANCE,
        PositionCapabilities = DEFAULT_POS_CAPABILITIES_FOR_PITCHER,
        HitterAbilities = DEFAULT_HITTER_ABILITIES_FOR_PITCHER,
        PitcherAbilities = DEFAULT_PITCHER_ABILITIES_FOR_PITCHER,
        SpecialAbilities = new SpecialAbilities()
      };
    }

    private static Appearance DEFAULT_APPEARANCE => new Appearance
    {
      FaceId = 177,
      EyebrowThickness = EyebrowThickness.Thick,
      SkinColor = SkinColor.One,
      EyeColor = EyeColor.Brown,
      HairStyle = null,
      HairColor = null,
      FacialHairStyle = null,
      FacialHairColor = null,
      BatColor = BatColor.Natural,
      GloveColor = GloveColor.Tan,
      EyewearType = null,
      EyewearFrameColor = null,
      EyewearLensColor = null,
      EarringSide = null,
      EarringColor = null,
      RightWristbandColor = null,
      LeftWristbandColor = null
    };

    private static PositionCapabilities DEFAULT_POS_CAPABILITIES_FOR_HITTER => new PositionCapabilities
    {
      Pitcher = Grade.G,
      Catcher = Grade.G,
      FirstBase = Grade.G,
      SecondBase = Grade.G,
      ThirdBase = Grade.G,
      Shortstop = Grade.G,
      LeftField = Grade.G,
      CenterField = Grade.G,
      RightField = Grade.G
    };

    private static PositionCapabilities DEFAULT_POS_CAPABILITIES_FOR_PITCHER => new PositionCapabilities
    {
      Pitcher = Grade.A,
      Catcher = Grade.G,
      FirstBase = Grade.G,
      SecondBase = Grade.G,
      ThirdBase = Grade.G,
      Shortstop = Grade.G,
      LeftField = Grade.G,
      CenterField = Grade.G,
      RightField = Grade.G
    };

    private static HitterAbilities DEFAULT_HITTER_ABILITIES_FOR_HITTER => new HitterAbilities
    {
      Trajectory = 2,
      Contact = 4,
      Power = 60,
      RunSpeed = 6,
      ArmStrength = 6,
      Fielding = 6,
      ErrorResistance = 6,
      HotZones = new HotZoneGrid()
    };

    private static HitterAbilities DEFAULT_HITTER_ABILITIES_FOR_PITCHER => new HitterAbilities
    {
      Trajectory = 1,
      Contact = 1,
      Power = 16,
      RunSpeed = 2,
      ArmStrength = 7,
      Fielding = 6,
      ErrorResistance = 5,
      HotZones = new HotZoneGrid()
    };

    private static PitcherAbilities DEFAULT_PITCHER_ABILITIES_FOR_HITTER => new PitcherAbilities
    {
      TopSpeedMph = 74,
      Control = 0,
      Stamina = 0
    };

    private static PitcherAbilities DEFAULT_PITCHER_ABILITIES_FOR_PITCHER => new PitcherAbilities
    {
      TopSpeedMph = 88,
      Control = 98,
      Stamina = 37,
      Slider1Type = SliderType.Slider,
      Slider1Movement = 1,
      Fork1Type = ForkType.ChangeUp,
      Fork1Movement = 2
    };
  }
}
