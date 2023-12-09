using NUnit.Framework;
using PowerUp.GameSave.IO;
using Shouldly;
using System;

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

    [Test]
    public void GetsCorrectValueBits_0_4() 
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(bytesToRead, 0, 4);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_6()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(bytesToRead, 0, 6);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
      valueBits[4].ShouldBe(Convert.ToByte(1));
      valueBits[5].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_8()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(bytesToRead, 0, 8);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
      valueBits[4].ShouldBe(Convert.ToByte(1));
      valueBits[5].ShouldBe(Convert.ToByte(0));
      valueBits[6].ShouldBe(Convert.ToByte(1));
      valueBits[7].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_7_4()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(bytesToRead, 7, 4);
      valueBits[0].ShouldBe(Convert.ToByte(0));
      valueBits[1].ShouldBe(Convert.ToByte(0));
      valueBits[2].ShouldBe(Convert.ToByte(1));
      valueBits[3].ShouldBe(Convert.ToByte(1));
    }

    [Test]
    public void GetsCorrectValueBits_7_10()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(bytesToRead, 7, 10);
      valueBits[0].ShouldBe(Convert.ToByte(0));
      valueBits[1].ShouldBe(Convert.ToByte(0));
      valueBits[2].ShouldBe(Convert.ToByte(1));
      valueBits[3].ShouldBe(Convert.ToByte(1));
      valueBits[4].ShouldBe(Convert.ToByte(0));
      valueBits[5].ShouldBe(Convert.ToByte(0));
      valueBits[6].ShouldBe(Convert.ToByte(1));
      valueBits[7].ShouldBe(Convert.ToByte(0));
      valueBits[8].ShouldBe(Convert.ToByte(1));
      valueBits[9].ShouldBe(Convert.ToByte(0));
    }
  }
}
