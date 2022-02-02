using PowerUp.DebugUtils;

namespace PowerUp.GameSave
{
  public class GSPlayer
  {
    [GSUInt(0x00, bits: 16)]
    public int PowerProsId { get; set; }

    [GSString(0x02, stringLength: 10)]
    public string? SavedName { get; set; }

    [GSString(0x16, stringLength: 14)]
    public string? LastName { get; set; }

    [GSString(0x32, stringLength: 14)]
    public string? FirstName { get; set; }

    [GSBoolean(0x50, bitOffset: 4)]
    public bool? IsEdited { get; set; }

    [GSUInt(0x51, bits: 10, bitOffset: 7)]
    public int PlayerNumber { get; set; }

    [GSUInt(0x51, bits: 2, bitOffset: 5)]
    public int PlayerNumberNumberOfDigits { get; set; }

    public string? PlayerNumberDisplay
    {
      get
      {
        var trimmedNumber = PlayerNumber.ToString();
        return $"{new string('0', PlayerNumberNumberOfDigits - trimmedNumber.Length)}{trimmedNumber}";
      }
    }

    [GSUInt(0x54, bits: 8, bitOffset: 5)]
    public int Face { get; set; }

    [GSUInt(0x55, bits: 4, bitOffset: 4)]
    public int SkinAndEyes { get; set; }
    public int Skin => SkinAndEyes % 5;
    public bool AreEyesBrown => SkinAndEyes >= 5;

    [GSUInt(0x56, bits: 3, bitOffset: 1)]
    public int Bat { get; set; }

    [GSUInt(0x56, bits: 3, bitOffset: 4)]
    public int Glove { get; set; }
    
    [GSUInt(0x58, bits: 5, bitOffset: 0)]
    public int Hair { get; set; }

    [GSUInt(0x58, bits: 4, bitOffset: 5)]
    public int HairColor { get; set; }

    [GSBytes(0x58, numberOfBytes: 2)]
    public byte[]? HairBytes { get; set; }
  }
}
