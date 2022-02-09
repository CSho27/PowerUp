using PowerUp.DebugUtils;

namespace PowerUp.GameSave
{
  public class GSPlayer
  {
    [GSUInt(0x00, bits: 16)]
    public ushort? PowerProsId { get; set; }

    [GSString(0x02, stringLength: 10)]
    public string? SavedName { get; set; }

    [GSString(0x16, stringLength: 14)]
    public string? LastName { get; set; }

    [GSString(0x32, stringLength: 14)]
    public string? FirstName { get; set; }

    [GSBoolean(0x50, bitOffset: 4)]
    public bool? IsEdited { get; set; }

    [GSUInt(0x51, bits: 10, bitOffset: 7)]
    public ushort? PlayerNumber { get; set; }

    [GSUInt(0x51, bits: 2, bitOffset: 5)]
    public ushort? PlayerNumberNumberOfDigits { get; set; }

    public string? PlayerNumberDisplay
    {
      get
      {
        if (!PlayerNumberNumberOfDigits.HasValue || PlayerNumberNumberOfDigits == 0) return "";

        var trimmedNumber = PlayerNumber!.ToString();
        return $"{new string('0', PlayerNumberNumberOfDigits.Value - trimmedNumber!.Length)}{trimmedNumber}";
      }
    }

    [GSUInt(0x54, bits: 8, bitOffset: 5)]
    public ushort? Face { get; set; }

    [GSUInt(0x55, bits: 4, bitOffset: 4)]
    public ushort? SkinAndEyes { get; set; }
    public ushort? Skin => SkinAndEyes.HasValue
      ? (ushort)(SkinAndEyes % 5)
      : null;
    public bool? AreEyesBrown => SkinAndEyes.HasValue
      ? SkinAndEyes >= 5
      : null;

    [GSUInt(0x56, bits: 3, bitOffset: 1)]
    public ushort? Bat { get; set; }

    [GSUInt(0x56, bits: 3, bitOffset: 4)]
    public ushort? Glove { get; set; }
    
    [GSUInt(0x58, bits: 5, bitOffset: 0)]
    public ushort? Hair { get; set; }

    [GSUInt(0x58, bits: 4, bitOffset: 5)]
    public ushort? HairColor { get; set; }

    [GSUInt(0x59, bits: 5, bitOffset: 1)]
    public ushort? FacialHair { get; set; }

    [GSUInt(0x59, bits: 4, bitOffset: 6)]
    public ushort? FacialHairColor { get; set; }

    // There is no 2. It jumps from eye black at 1, to first pair of glasses at 3
    [GSUInt(0x5b, bits: 4, bitOffset: 2)]
    public ushort? GlassesType { get; set; }

    [GSUInt(0x5c, bits: 4, bitOffset: 0)]
    public ushort? GlassesColor { get; set; }

    [GSUInt(0x5A, bits: 2, bitOffset: 4)]
    public ushort? EarringType { get; set; }

    [GSUInt(0x5A, bits: 4, bitOffset: 6)]
    public ushort? EarringColor { get; set; }

    [GSUInt(0x56, bits: 4, bitOffset: 7)]
    public ushort? RightWristband { get; set; }

    [GSUInt(0x57, bits: 4, bitOffset: 3)]
    public ushort? LeftWristband { get; set; }

    // TODO: Add Batting Form
    // TODO: Add Pitching Form

    [GSUInt(0x5e, bits: 4, bitOffset: 4)]
    public ushort? PrimaryPosition { get; set; }

    [GSUInt(0x5f, bits: 3, bitOffset: 0)]
    public ushort? PitcherCapability { get; set; }

    [GSUInt(0x5f, bits: 3, bitOffset: 3)]
    public ushort? CatcherCapability { get; set; }

    [GSUInt(0x60, bits: 3, bitOffset: 0)]
    public ushort? FirstBaseCapability { get; set; }

    [GSUInt(0x60, bits: 3, bitOffset: 3)]
    public ushort? SecondBaseCapability { get; set; }

    [GSUInt(0x60, bits: 3, bitOffset: 6)]
    public ushort? ThirdBaseCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 1)]
    public ushort? ShortstopCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 4)]
    public ushort? LeftFieldCapability { get; set; }

    [GSUInt(0x61, bits: 3, bitOffset: 7)]
    public ushort? CenterFieldCapability { get; set; }

    [GSUInt(0x62, bits: 3, bitOffset: 2)]
    public ushort? RightFieldCapability { get; set; }

    [GSBoolean(0x62, bitOffset: 7)]
    public bool? IsStarter { get; set; }

    [GSBoolean(0x63, bitOffset: 2)]
    public bool? IsReliever { get; set; }

    [GSBoolean(0x63, bitOffset: 5)]
    public bool? IsCloser { get; set; }

    /// <summary>
    /// For switch hitters hot zones follow righty conventions
    /// </summary>
    [GSUInt(0x67, bits: 2, bitOffset: 3)]
    public ushort? HotZoneUpAndIn { get; set; }

    [GSUInt(0x67, bits: 2, bitOffset: 5)]
    public ushort? HotZoneUp { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 0)]
    public ushort? HotZoneUpAndAway { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 2)]
    public ushort? HotZoneMiddleIn { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 4)]
    public ushort? HotZoneMiddle { get; set; }

    [GSUInt(0x68, bits: 2, bitOffset: 6)]
    public ushort? HotZoneMiddleAway { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 0)]
    public ushort? HotZoneDownAndIn { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 2)]
    public ushort? HotZoneDown { get; set; }

    [GSUInt(0x69, bits: 2, bitOffset: 4)]
    public ushort? HotZoneDownAndAway { get; set; }


    [GSUInt(0x69, bits: 2, bitOffset: 6)]
    public ushort? BattingSide { get; set; }

    [GSBoolean(0x6a, bitOffset: 1)]
    public bool? ThrowsLefty { get; set; }


    [GSSInt(0x6a, bits: 2, bitOffset: 2)]
    public short? Durability { get; set; }

    /// <summary>
    /// Game value is value loaded plus 1
    /// </summary>
    [GSUInt(0x6a, bits: 2, bitOffset: 5)]
    public ushort? Trajectory { get; set; }

    [GSUInt(0x6a, bits: 4, bitOffset: 7)]
    public ushort? Contact { get; set; }

    [GSUInt(0x6c, bits: 8, bitOffset: 0)]
    public ushort? Power { get; set; }

    [GSUInt(0x6d, bits: 4, bitOffset: 0)]
    public ushort? RunSpeed { get; set; }

    [GSUInt(0x6d, bits: 4, bitOffset: 4)]
    public ushort? ArmStrength { get; set; }

    [GSUInt(0x6e, bits: 4, bitOffset: 0)]
    public ushort? Fielding { get; set; }

    [GSUInt(0x6e, bits: 4, bitOffset: 4)]
    public ushort? ErrorResistance { get; set; }

    [GSSInt(0x6f, bits: 2, bitOffset: 0)]
    public short? HittingConsistency { get; set; }

    // These two properties are signed ints but -3 represents -1
    [GSSInt(0x6f, bits: 3, bitOffset: 2)]
    public short? HittingVersusLefty1 { get; set; }

    [GSSInt(0x6f, bits: 3, bitOffset: 5)]
    public short? HittingVersusLefty2 { get; set; }

    [GSSInt(0x70, bits: 3, bitOffset: 0)]
    public short? ClutchHit { get; set; }

    [GSBoolean(0x70, bitOffset: 3)]
    public bool? IsTableSetter { get; set; }

    [GSSInt(0x70, bits: 2, bitOffset: 4)]
    public short? Morale { get; set; }

    [GSBoolean(0x70, bitOffset: 6)]
    public bool? IsSparkplug { get; set; }

    [GSBoolean(0x70, bitOffset: 7)]
    public bool? IsRallyHitter { get; set; }

    // MPH/KMH = .618 (for game purposes not in real life)
    [GSUInt(0x79, bits: 8, bitOffset: 0)]
    public ushort? TopThrowingSpeedKMH { get; set; }

    [GSUInt(0x7a, bits: 8, bitOffset: 0)]
    public ushort? Control { get; set; }

    [GSUInt(0x7b, bits: 8, bitOffset: 0)]
    public ushort? Stamina { get; set; }

    // Pitching Speical Abilities

    [GSUInt(0x92, bits: 16, bitOffset: 0)]
    public ushort? VoiceId { get; set; }

    [GSUInt(0x95, bits: 5, bitOffset: 0)]
    public ushort? Slider1Type { get; set; }

    [GSUInt(0x95, bits: 3, bitOffset: 5)]
    public ushort? Slider1Movement { get; set; }

    [GSUInt(0x96, bits: 5, bitOffset: 0)]
    public ushort? Curve1Type { get; set; }

    [GSUInt(0x96, bits: 3, bitOffset: 5)]
    public ushort? Curve1Movement { get; set; }

    [GSUInt(0x97, bits: 5, bitOffset: 0)]
    public ushort? Fork1Type { get; set; }

    [GSUInt(0x97, bits: 3, bitOffset: 5)]
    public ushort? Fork1Movement { get; set; }

    [GSUInt(0x98, bits: 5, bitOffset: 0)]
    public ushort? Sinker1Type { get; set; }

    [GSUInt(0x98, bits: 3, bitOffset: 5)]
    public ushort? Sinker1Movement { get; set; }

    [GSUInt(0x99, bits: 5, bitOffset: 0)]
    public ushort? SinkingFastball1Type { get; set; }

    [GSUInt(0x99, bits: 3, bitOffset: 5)]
    public ushort? SinkingFastball1Movement { get; set; }

    [GSUInt(0x9a, bits: 5, bitOffset: 0)]
    public ushort? TwoSeamType { get; set; }

    [GSUInt(0x9a, bits: 3, bitOffset: 5)]
    public ushort? TwoSeamMovement { get; set; }

    [GSUInt(0x9b, bits: 5, bitOffset: 0)]
    public ushort? Slider2Type { get; set; }

    [GSUInt(0x9b, bits: 3, bitOffset: 5)]
    public ushort? Slider2Movement { get; set; }

    [GSUInt(0x9c, bits: 5, bitOffset: 0)]
    public ushort? Curve2Type { get; set; }

    [GSUInt(0x9c, bits: 3, bitOffset: 5)]
    public ushort? Curve2Movement { get; set; }

    [GSUInt(0x9d, bits: 5, bitOffset: 0)]
    public ushort? Fork2Type { get; set; }

    [GSUInt(0x9d, bits: 3, bitOffset: 5)]
    public ushort? Fork2Movement { get; set; }

    [GSUInt(0x9e, bits: 5, bitOffset: 0)]
    public ushort? Sinker2Type { get; set; }

    [GSUInt(0x9e, bits: 3, bitOffset: 5)]
    public ushort? Sinker2Movement { get; set; }

    [GSUInt(0x9f, bits: 5, bitOffset: 0)]
    public ushort? SinkingFastball2Type { get; set; }

    [GSUInt(0x9f, bits: 3, bitOffset: 5)]
    public ushort? SinkingFastball2Movement { get; set; }

    [GSBytes(0x6f, numberOfBytes: 2)]
    public byte[]? TestBytes { get; set; }
  }
}
