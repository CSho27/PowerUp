using NUnit.Framework;
using Shouldly;

namespace PowerUp.Tests.Utils
{
  public class SoundexTests
  {
    [Test]
    public void Soundex_ShouldEncodeWords()
    {
      Soundex.Of("Robert").ShouldBe("R163");
      Soundex.Of("Rupert").ShouldBe("R163");
      Soundex.Of("Rubin").ShouldBe("R150");
      Soundex.Of("Ashcraft").ShouldBe("A261");
      Soundex.Of("Ashcroft").ShouldBe("A261");
      Soundex.Of("Tumczak").ShouldBe("T522");
      Soundex.Of("Pfister").ShouldBe("P236");
      Soundex.Of("Honeyman").ShouldBe("H555");
      Soundex.Of("Of").ShouldBe("O100");
      Soundex.Of("").ShouldBe("0000");
      Soundex.Of("HHHHoneyMan").ShouldBe("H555");
      Soundex.Of("H*H(M@P#R").ShouldBe("H516");
    }

    [Test]
    public void Soundex_ShouldHandleMultiWord()
    {
      Soundex.Of("de LaRosa").ShouldBe("D000 L620");
      Soundex.Of("Isiah Kiner-Falefa").ShouldBe("I200 K560 F410");
    }
  }
}
