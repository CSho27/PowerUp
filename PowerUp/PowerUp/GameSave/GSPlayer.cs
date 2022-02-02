using PowerUp.DebugUtils;

namespace PowerUp.GameSave
{
  public class GSPlayer
  {
    [GSUInt16(0x00)]
    public int PowerProsId { get; set; }

    [GSString(0x02, stringLength: 10)]
    public string? SavedName { get; set; }

    [GSString(0x16, stringLength: 14)]
    public string? LastName { get; set; }

    [GSString(0x32, stringLength: 14)]
    public string? FirstName { get; set; }

    [GSBoolean(0x50, bitOffset: 4)]
    public bool? IsEdited { get; set; }

    [GSBytes(0x51, numberOfBytes: 3)]
    public byte[]? PlayerNumberBytes { get; set; }
    public string? PlayerNumber
    {
      get 
      {
        if (PlayerNumberBytes == null)
          return null;

        var bits = new byte[10];
        bits[0] = PlayerNumberBytes[0].GetBit(7);
        for (int i = 1; i < 9; i++)
          bits[i] = PlayerNumberBytes[1].GetBit(i - 1);
        bits[9] = PlayerNumberBytes[2].GetBit(0);

        var numberOfDigits = PlayerNumberBytes[0].GetBitsValue(5, 2);
        var trimmedNumber = bits.ToUInt16().ToString();
        return $"{new string('0', numberOfDigits - trimmedNumber.Length)}{trimmedNumber}";
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

    [GSUInt4(0x55, bitOffset: 4)]
    public int SkinAndEyes { get; set; }
    public int Skin => SkinAndEyes % 5;
    public bool AreEyesBrown => SkinAndEyes >= 5;

    [GSUInt3(0x56, bitOffset: 1)]
    public int Bat { get; set; }

    [GSUInt3(0x56, bitOffset: 4)]
    public int Glove { get; set; }
    
    [GSUInt5(0x58, bitOffset: 0)]
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
