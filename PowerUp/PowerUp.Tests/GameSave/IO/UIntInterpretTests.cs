using NUnit.Framework;
using PowerUp.GameSave.IO;
using Shouldly;

namespace PowerUp.Tests.GameSave.IO
{
  public class UIntInterpretTests
  {
    [Test]
    [TestCase(0, 4, 1)]
    [TestCase(0, 8, 1)]
    [TestCase(0, 16, 2)]
    [TestCase(4, 4, 1)]
    [TestCase(4, 8, 2)]
    [TestCase(4, 16, 3)]
    public void GetsCorrectNumberOfBytesToRead(int bitOffset, int numberOfBits, int expectedBytesToRead)
    {
      UIntInterpret.GetNumberOfBytesNeeded(bitOffset, numberOfBits).ShouldBe(expectedBytesToRead);
    }
  }
}
