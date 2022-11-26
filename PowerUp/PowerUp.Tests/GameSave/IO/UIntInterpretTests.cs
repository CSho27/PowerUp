using NUnit.Framework;
using PowerUp.GameSave.IO;
using Shouldly;
using System;

namespace PowerUp.Tests.GameSave.IO
{
  public class UIntInterpretTests
  {
    [Test]
    [TestCase((long)0, 0, 4, ByteOrder.BigEndian, 0, 1)]
    [TestCase((long)1, 0, 8, ByteOrder.BigEndian, 1, 1)]
    [TestCase((long)0, 0, 16, ByteOrder.BigEndian, 0, 2)]
    [TestCase((long)0, 4, 4, ByteOrder.BigEndian, 0, 1)]
    [TestCase((long)0, 4, 8, ByteOrder.BigEndian, 0, 2)]
    [TestCase((long)1, 4, 16, ByteOrder.BigEndian, 1, 3)]
    [TestCase((long)0, 0, 4, ByteOrder.LittleEndian, 0, 1)]
    [TestCase((long)1, 0, 8, ByteOrder.LittleEndian, 0, 1)]
    [TestCase((long)1, 0, 16, ByteOrder.LittleEndian, 0, 2)]
    [TestCase((long)1, 4, 4, ByteOrder.LittleEndian, 0, 1)]
    [TestCase((long)1, 4, 8, ByteOrder.LittleEndian, 0, 2)]
    [TestCase((long)0, 4, 16, ByteOrder.LittleEndian, 0, 4)]
    public void GetsCorrectNumberOfBytesToRead(long offset, int bitOffset, int numberOfBits, ByteOrder byteOrder, int offsetToStartAt, int expectedBytesToRead)
    {
      var result = UIntInterpret.GetBytesToRead(offset, bitOffset, numberOfBits, byteOrder);
      result.offsetToStartAt.ShouldBe(offsetToStartAt);
      result.numberOfBytesToRead.ShouldBe(expectedBytesToRead);
    }

    [Test]
    public void GetsCorrectValueBits_0_4_BigEndian() 
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 4, ByteOrder.BigEndian);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_6_BigEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 6, ByteOrder.BigEndian);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
      valueBits[4].ShouldBe(Convert.ToByte(1));
      valueBits[5].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_8_BigEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 8, ByteOrder.BigEndian);
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
    public void GetsCorrectValueBits_7_4_BigEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 7, 4, ByteOrder.BigEndian);
      valueBits[0].ShouldBe(Convert.ToByte(0));
      valueBits[1].ShouldBe(Convert.ToByte(0));
      valueBits[2].ShouldBe(Convert.ToByte(1));
      valueBits[3].ShouldBe(Convert.ToByte(1));
    }

    [Test]
    public void GetsCorrectValueBits_7_10_BigEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 7, 10, ByteOrder.BigEndian);
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

    [Test]
    public void GetsCorrectValueBits_0_4_LittleEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 4, ByteOrder.LittleEndian);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_6_LittleEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 6, ByteOrder.LittleEndian);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(0));
      valueBits[3].ShouldBe(Convert.ToByte(0));
      valueBits[4].ShouldBe(Convert.ToByte(1));
      valueBits[5].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_0_8_LittleEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 0, 8, ByteOrder.LittleEndian);
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
    public void GetsCorrectValueBits_0_7_4_LittleEndian()
    {
      // [ 11001010, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 7, 4, ByteOrder.LittleEndian);
      valueBits[0].ShouldBe(Convert.ToByte(1));
      valueBits[1].ShouldBe(Convert.ToByte(1));
      valueBits[2].ShouldBe(Convert.ToByte(1));
      valueBits[3].ShouldBe(Convert.ToByte(0));
    }

    [Test]
    public void GetsCorrectValueBits_7_10_LittleEndian()
    {
      // [ 11001010, 01100101, 01100101, 00101011 ]
      var bytesToRead = new byte[] { Convert.ToByte(202), Convert.ToByte(101), Convert.ToByte(101), Convert.ToByte(43) };
      var valueBits = UIntInterpret.GetValueBits(0, bytesToRead, 7, 10, ByteOrder.LittleEndian);
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

    [Test]
    [TestCase(ByteOrder.BigEndian, 0, 0)]
    [TestCase(ByteOrder.BigEndian, 1, 1)]
    [TestCase(ByteOrder.BigEndian, 2, 2)]
    [TestCase(ByteOrder.BigEndian, 3, 3)]
    [TestCase(ByteOrder.LittleEndian, 0, 1)]
    [TestCase(ByteOrder.LittleEndian, 1, 0)]
    [TestCase(ByteOrder.LittleEndian, 2, 3)]
    [TestCase(ByteOrder.LittleEndian, 3, 2)]
    public void GetsCorrectByteIndexToRead(ByteOrder byteOrder, int byteIndex, int expectedIndex)
    {
      UIntInterpret.GetByteIndexToReadFrom(byteOrder, byteIndex).ShouldBe(expectedIndex);
    }
  }
}
