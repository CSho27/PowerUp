using System.Linq;
using System.Text;

namespace PowerUp
{
  public static class StringExtensions
  {
    public static char FirstCharacter(this string @string) => @string.ToCharArray().First();

    public static int BeginningCharsInCommon(this string string1, string string2)
    {
      var index = 0;
      while(index < string1.Length && index < string2.Length && string1[index] == string2[index])
        index++;

      return index;
    }

    public const string LOWER_A_CHARS = "àáâãäåāăą";
    public const string UPPER_A_CHARS = "ÀÁÂÃÄÅĀĂĄ";
    public const string LOWER_E_CHARS = "èéêëēĕėęě";
    public const string UPPER_E_CHARS = "ÈÉÊËĒĔĖĘĚ";
    public const string LOWER_I_CHARS = "ìíîïìĩīĭ";
    public const string UPPER_I_CHARS = "ÌÍÎÏÌĨĪĬ";
    public const string LOWER_O_CHARS = "òóôõöøōŏő";
    public const string UPPER_O_CHARS = "ÒÓÔÕÖØŌŎŐ";
    public const string LOWER_U_CHARS = "ùúûüũūŭůửữựủứừứừ";
    public const string UPPER_U_CHARS = "ÙÚÛÜŨŪŬŮỦỤỨỤỨ";
    public const string LOWER_C_CHARS = "ç";
    public const string UPPER_C_CHARS = "Ç";
    public const string LOWER_N_CHARS = "ñ";
    public const string UPPER_N_CHARS = "Ñ";
    public const string LOWER_Y_CHARS = "ÿ";
    public const string LOWER_S_CHARS = "ß";

    public static string RemoveAccents(this string value)
    {
      var sb = new StringBuilder();
      foreach (char c in value)
      {
        if (LOWER_A_CHARS.Any(a => a == c))
          sb.Append('a');
        else if (UPPER_A_CHARS.Any(A => A == c))
          sb.Append('A');
        else if (LOWER_E_CHARS.Any(e => e == c))
          sb.Append('e');
        else if (UPPER_E_CHARS.Any(E => E == c))
          sb.Append('E');
        else if (LOWER_I_CHARS.Any(i => i == c))
          sb.Append('i');
        else if (UPPER_I_CHARS.Any(I => I == c))
          sb.Append('I');
        else if (LOWER_O_CHARS.Any(o => o == c))
          sb.Append('o');
        else if (UPPER_O_CHARS.Any(O => O == c))
          sb.Append('O');
        else if (LOWER_U_CHARS.Any(u => u == c))
          sb.Append('u');
        else if (UPPER_U_CHARS.Any(U => U == c))
          sb.Append('U');
        else if (LOWER_C_CHARS.Any(ch => ch == c))
          sb.Append('c');
        else if (UPPER_C_CHARS.Any(ch => ch == c))
          sb.Append('C');
        else if (LOWER_N_CHARS.Any(n => n == c))
          sb.Append('n');
        else if (UPPER_N_CHARS.Any(N => N == c))
          sb.Append('N');
        else if (LOWER_Y_CHARS.Any(y => y == c))
          sb.Append('y');
        else if (LOWER_S_CHARS.Any(s => s == c))
          sb.Append('s');
        else sb.Append(c);
      }

      return sb.ToString();
    }
  }
}
