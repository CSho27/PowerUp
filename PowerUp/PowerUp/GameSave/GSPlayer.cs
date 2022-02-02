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

    //[GSUInt8(0x54)]
    public int Face
    {
      get
      {
        if (FaceBytes == null)
          return 0;

        var bits = new byte[10];
        for (int i = 0; i < 3; i++)
          bits[i] = FaceBytes[0].GetBit(i + 5);

        for (int i = 3; i < 8; i++)
          bits[i] = FaceBytes[1].GetBit(i - 3);

        return bits.ToUInt16();
      }
    }

    [GSBytes(0x54, numberOfBytes: 2)]
    public byte[]? FaceBytes { get; set; }

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

    [GSBytes(0x58, numberOfBytes: 2)]
    public byte[]? HairColorBytes { get; set; }
    public int HairColor
    {
      get
      {
        var bits = new byte[4];
        for (int i = 0; i < 3; i++)
          bits[i] = HairColorBytes![0].GetBit(i + 5);
        bits[3] = HairColorBytes![1].GetBit(0);
        return bits.ToUInt16();
      }
    }

    [GSBytes(0x58, numberOfBytes: 2)]
    public byte[]? HairBytes { get; set; }
  }
}
