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

    [GSUInt8(0x54)]
    public int Face { get; set; }

    [GSUInt8(0x55)]
    public int Skin { get; set; }

    [GSUInt3(0x56, 4)]
    public int Bat { get; set; }

    [GSUInt3(0x56, 0)]
    public int Glove { get; set; }
  }
}
