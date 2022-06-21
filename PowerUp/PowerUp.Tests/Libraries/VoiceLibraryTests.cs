using NUnit.Framework;
using PowerUp.Libraries;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.Libraries
{
  public class VoiceLibraryTests
  {
    IVoiceLibrary voiceLibrary;

    [SetUp]
    public void SetUp()
    {
      voiceLibrary = TestConfig.VoiceLibrary.Value;
    }

    [Test]
    public void FindClosest_FindsReasonablyCloseName()
    {
      var ripken = voiceLibrary.FindClosestTo("Lou Gherig");
      ripken.Key.ShouldBe("Ripken");
    }
  }
}
