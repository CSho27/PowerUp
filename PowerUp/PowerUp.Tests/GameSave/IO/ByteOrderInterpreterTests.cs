﻿using NUnit.Framework;
using PowerUp.GameSave.IO;
using Shouldly;

namespace PowerUp.Tests.GameSave.IO
{
  public class ByteOrderInterpreterTests
  {
    [Test]
    [TestCase(0, ByteOrder.BigEndian, 0)]
    [TestCase(1, ByteOrder.BigEndian, 1)]
    [TestCase(2, ByteOrder.BigEndian, 2)]
    [TestCase(3, ByteOrder.BigEndian, 3)]
    [TestCase(0, ByteOrder.LittleEndian, -1)]
    [TestCase(1, ByteOrder.LittleEndian, 2)]
    [TestCase(2, ByteOrder.LittleEndian, 1)]
    [TestCase(3, ByteOrder.LittleEndian, 4)]
    public void TranslateOffset_ShouldReturnCorrectOffset(int offset, ByteOrder byteOrder, int expectedOffset)
    {
      ByteOrderInterpreter.TranslateOffset(offset, byteOrder, false).ShouldBe(expectedOffset);
    }

    [Test]
    [TestCase(0, ByteOrder.BigEndian, 1)]
    [TestCase(1, ByteOrder.BigEndian, 2)]
    [TestCase(2, ByteOrder.BigEndian, 3)]
    [TestCase(3, ByteOrder.BigEndian, 4)]
    [TestCase(0, ByteOrder.LittleEndian, -1)]
    [TestCase(1, ByteOrder.LittleEndian, 4)]
    [TestCase(2, ByteOrder.LittleEndian, 1)]
    [TestCase(3, ByteOrder.LittleEndian, 6)]
    public void GetNextByteOffset_ShouldReturnCorrectOffset(int offset, ByteOrder byteOrder, int expectedNextOffset)
    {
      ByteOrderInterpreter.GetNextByteOffset(offset, byteOrder, dataStartsOnEven: false, traverseSequentially: false).ShouldBe(expectedNextOffset);
    }
  }
}
