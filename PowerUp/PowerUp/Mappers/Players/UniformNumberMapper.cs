namespace PowerUp.Mappers.Players
{
  public static class UniformNumberMapper
  {
    public static (ushort numberOfDigits, ushort uniformNumberValue) ToGSUniformNumber(this string uniformNumber)
    {
      return (numberOfDigits: (ushort)uniformNumber.Length, uniformNumberValue: ushort.Parse(uniformNumber));
    }

    public static string ToUniformNumber(ushort? numberOfDigits, ushort? uniformNumberValue)
    {
      if (!numberOfDigits.HasValue || numberOfDigits == 0) return "";

      var trimmedNumber = uniformNumberValue.ToString();
      return $"{new string('0', numberOfDigits.Value - trimmedNumber!.Length)}{trimmedNumber}";
    }
  }
}
