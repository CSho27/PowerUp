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

    [GSBytes(0x5e, numberOfBytes: 2)]
    public byte[]? TestBytes { get; set; }
  }
}
