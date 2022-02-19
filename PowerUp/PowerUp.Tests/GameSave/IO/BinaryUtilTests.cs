using NUnit.Framework;
using PowerUp.DebugUtils;
using Shouldly;
using System;

namespace PowerUp.Tests.GameSave.IO
{
  public class BinaryUtilTests
  {
    private readonly static byte _0 = Convert.ToByte(0);
    private readonly static byte _1 = Convert.ToByte(1);

    [Test]
    public void SetBit_0_SET_3rd_TO_1()
    {
      var @byte = new byte();
      @byte = @byte.SetBit(3, 1);
      @byte.ShouldBe(Convert.ToByte(16));
      @byte.GetBit(3).ShouldBe(_1);
    }

    [Test]
    public void SetBit_0_SET_6th_TO_1()
    {
      var @byte = new byte();
      @byte = @byte.SetBit(6, 1);
      @byte.ShouldBe(Convert.ToByte(2));
      @byte.GetBit(6).ShouldBe(_1);
    }

    [Test]
    public void SetBit_0_SET_6th_TO_0()
    {
      var @byte = new byte();
      @byte = @byte.SetBit(6, 0);
      @byte.ShouldBe(Convert.ToByte(0));
      @byte.GetBit(6).ShouldBe(_0);
    }

    [Test]
    public void SetBit_1_SET_0th_TO_0()
    {
      var @byte = Convert.ToByte(255);
      @byte = @byte.SetBit(0, 0);
      @byte.ShouldBe(Convert.ToByte(127));
      @byte.GetBit(0).ShouldBe(_0);
    }

    [Test]
    public void ToBitArray_GetsBitsFor1()
    {
      var result = ((ushort)1).ToBitArray(3);
      result[0].ShouldBe(_0);
      result[1].ShouldBe(_0);
      result[2].ShouldBe(_1);
    }

    [Test]
    public void ToBitArray_GetsBitsFor2()
    {
      var result = ((ushort)2).ToBitArray(3);
      result[0].ShouldBe(_0);
      result[1].ShouldBe(_1);
      result[2].ShouldBe(_0);
    }

    [Test]
    public void ToBitArray_GetsBitsFor6()
    {
      var result = ((ushort)6).ToBitArray(3);
      result[0].ShouldBe(_1);
      result[1].ShouldBe(_1);
      result[2].ShouldBe(_0);
    }

    [Test]
    public void ToBitArray_GetsBitsFor56()
    {
      var result = ((ushort)56).ToBitArray(6);
      result[0].ShouldBe(_1);
      result[1].ShouldBe(_1);
      result[2].ShouldBe(_1);
      result[3].ShouldBe(_0);
      result[4].ShouldBe(_0);
      result[5].ShouldBe(_0);
    }

    [Test]
    public void ToBitArray_GetsBitsFor823()
    {
      var result = ((ushort)823).ToBitArray(10);
      result[0].ShouldBe(_1);
      result[1].ShouldBe(_1);
      result[2].ShouldBe(_0);
      result[3].ShouldBe(_0);
      result[4].ShouldBe(_1);
      result[5].ShouldBe(_1);
      result[6].ShouldBe(_0);
      result[7].ShouldBe(_1);
      result[8].ShouldBe(_1);
      result[9].ShouldBe(_1);
    }
  }
}
