using NUnit.Framework;
using PowerUp.CSV;
using Shouldly;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.Tests.Csv
{
  public class PlayerCsvReaderTests
  {
    private readonly static string TEST_READ_GAME_SAVE_FILE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "TestImport.csv");
    private IPlayerCsvReader _reader;

    [SetUp]
    public void Setup()
    {
      _reader = new PlayerCsvReader();          
    }

    [Test]
    public async Task ReadAllPlayers_ReadsPlayers()
    {
      // Arrange
      using var fileStream = File.OpenRead(TEST_READ_GAME_SAVE_FILE_PATH);

      // Act
      var players = await _reader.ReadAllPlayers(fileStream);

      // Assert
      var joseRamirez = players.ElementAt(0); 
      joseRamirez.TeamId.ShouldBe(114);
      joseRamirez.FirstName.ShouldBe("Jose");
      joseRamirez.LastName.ShouldBe("Ramirez");
      joseRamirez.BirthMonth.ShouldBe(9);
      joseRamirez.BirthDay.ShouldBe(17);
      joseRamirez.Age.ShouldBe(31);
      joseRamirez.YearsInMajors.ShouldBe(9);
      joseRamirez.UniformNumber.ShouldBe("11");
      joseRamirez.PrimaryPosition.ShouldBe(5);
      joseRamirez.PitcherType.ShouldBe(0);
      joseRamirez.VoiceId.ShouldBe(2702);
      joseRamirez.BattingSide.ShouldBe("S");
      joseRamirez.BattingStanceId.ShouldBe(11);
      joseRamirez.ThrowingArm.ShouldBe("R");
      joseRamirez.PitchingMechanicsId.ShouldBe(0);
      joseRamirez.Avg.ShouldBe(0.282);
      joseRamirez.RBI.ShouldBe(80);
      joseRamirez.HR.ShouldBe(24);
      joseRamirez.ERA.ShouldBe(null);
      joseRamirez.FaceId.ShouldBe(95);
      joseRamirez.EyebrowThickness.ShouldBe(1);
      joseRamirez.SkinColor.ShouldBe(4);
      joseRamirez.EyeColor.ShouldBe(0);
      joseRamirez.HairStyle.ShouldBe(16);
      joseRamirez.HairColor.ShouldBe(3);
      joseRamirez.FacialHairStyle.ShouldBe(7);
      joseRamirez.FacialHairColor.ShouldBe(3);
      joseRamirez.BatColor.ShouldBe(2);
      joseRamirez.GloveColor.ShouldBe(5);
      joseRamirez.EyewearType.ShouldBe(0);
      joseRamirez.EyewearFrameColor.ShouldBe(0);
      joseRamirez.EyewearLensColor.ShouldBe(0);
      joseRamirez.EarringSide.ShouldBe(0);
      joseRamirez.EarringColor.ShouldBe(0);
      joseRamirez.RightWristbandColor.ShouldBe(0);
      joseRamirez.LeftWristbandColor.ShouldBe(0);
      joseRamirez.Capabilities_P.ShouldBe("G");
      joseRamirez.Capabilities_C.ShouldBe("G");
      joseRamirez.Capabilities_1B.ShouldBe("F");
      joseRamirez.Capabilities_2B.ShouldBe("E");
      joseRamirez.Capabilities_3B.ShouldBe("A");
      joseRamirez.Capabilities_SS.ShouldBe("E");
      joseRamirez.Capabilities_LF.ShouldBe("G");
      joseRamirez.Capabilities_CF.ShouldBe("G");
      joseRamirez.Capabilities_RF.ShouldBe("G");
      joseRamirez.Trajectory.ShouldBe(3);
      joseRamirez.Contact.ShouldBe(9);
      joseRamirez.Power.ShouldBe(174);
      joseRamirez.RunSpeed.ShouldBe(10);
      joseRamirez.ArmStrength.ShouldBe(8);
      joseRamirez.Fielding.ShouldBe(10);
      joseRamirez.ErrorResistance.ShouldBe(11);
      joseRamirez.HZ_UpAndIn.ShouldBe("H");
      joseRamirez.HZ_Up.ShouldBe("C");
      joseRamirez.HZ_UpAndAway.ShouldBe("N");
      joseRamirez.HZ_MiddleIn.ShouldBe("H");
      joseRamirez.HZ_Middle.ShouldBe("C");
      joseRamirez.HZ_MiddleAway.ShouldBe("N");
      joseRamirez.HZ_DownAndIn.ShouldBe("N");
      joseRamirez.HZ_Down.ShouldBe("C");
      joseRamirez.HZ_DownAndAway.ShouldBe("H");
      joseRamirez.TopSpeedMph.ShouldBe(74);
      joseRamirez.Control.ShouldBe(0);
      joseRamirez.Stamina.ShouldBe(0);
      joseRamirez.TwoSeam.ShouldBe(0);
      joseRamirez.TwoSeamMovement.ShouldBe(0);
      joseRamirez.Slider1.ShouldBe(0);
      joseRamirez.Slider1Movement.ShouldBe(0);
      joseRamirez.Slider2.ShouldBe(0);
      joseRamirez.Slider2Movement.ShouldBe(0);
      joseRamirez.Curve1.ShouldBe(0);
      joseRamirez.Curve1Movement.ShouldBe(0);
      joseRamirez.Curve2.ShouldBe(0);
      joseRamirez.Curve2Movement.ShouldBe(0);
      joseRamirez.Fork1.ShouldBe(0);
      joseRamirez.Fork1Movement.ShouldBe(0);
      joseRamirez.Fork2.ShouldBe(0);
      joseRamirez.Fork2Movement.ShouldBe(0);
      joseRamirez.Sinker1.ShouldBe(0);
      joseRamirez.Sinker1Movement.ShouldBe(0);
      joseRamirez.Sinker2.ShouldBe(0);
      joseRamirez.Sinker2Movement.ShouldBe(0);
      joseRamirez.SinkFb1.ShouldBe(0);
      joseRamirez.SinkFb1Movement.ShouldBe(0);
      joseRamirez.SinkFb2.ShouldBe(0);
      joseRamirez.SinkFb2Movement.ShouldBe(0);
      joseRamirez.SP_Star.ShouldBe(0);
      joseRamirez.SP_Durability.ShouldBe(3);
      joseRamirez.SP_Morale.ShouldBe(0);
      joseRamirez.SP_Rain.ShouldBe(null);
      joseRamirez.SP_DayGame.ShouldBe(null);
      joseRamirez.SP_HConsistency.ShouldBe(null);
      joseRamirez.SP_HVsLefty.ShouldBe(null);
      joseRamirez.SP_TableSetter.ShouldBe(null);
      joseRamirez.SP_B2BHitter.ShouldBe(null);
      joseRamirez.SP_HotHitter.ShouldBe(null);
      joseRamirez.SP_RallyHitter.ShouldBe(null);
      joseRamirez.SP_PinchHitter.ShouldBe(null);
      joseRamirez.SP_BasesLoadedHitter.ShouldBe(null);
      joseRamirez.SP_WalkOffHitter.ShouldBe(null);
      joseRamirez.SP_ClutchHitter.ShouldBe(null);
      joseRamirez.SP_ContactHitter.ShouldBe(null);
      joseRamirez.SP_PowerHitter.ShouldBe(null);
      joseRamirez.SP_SluggerOrSlapHitter.ShouldBe(null);
      joseRamirez.SP_PushHitter.ShouldBe(null);
      joseRamirez.SP_PullHitter.ShouldBe(null);
      joseRamirez.SP_SprayHitter.ShouldBe(null);
      joseRamirez.SP_FirstballHitter.ShouldBe(null);
      joseRamirez.SP_AggressiveOrPatientHitter.ShouldBe(null);
      joseRamirez.SP_RefinedHitter.ShouldBe(null);
      joseRamirez.SP_FreeSwinger.ShouldBe(null);
      joseRamirez.SP_ToughOut.ShouldBe(null);
      joseRamirez.SP_HIntimidator.ShouldBe(null);
      joseRamirez.SP_Sparkplug.ShouldBe(null);
      joseRamirez.SP_SmallBall.ShouldBe(null);
      joseRamirez.SP_Bunting.ShouldBe(null);
      joseRamirez.SP_InfieldHitting.ShouldBe(null);
      joseRamirez.SP_BaseRunning.ShouldBe(null);
      joseRamirez.SP_Stealing.ShouldBe(null);
      joseRamirez.SP_AggressiveRunner.ShouldBe(null);
      joseRamirez.SP_AggressiveBaseStealer.ShouldBe(null);
      joseRamirez.SP_ToughRunner.ShouldBe(null);
      joseRamirez.SP_BreakupDp.ShouldBe(null);
      joseRamirez.SP_HeadFirstSlide.ShouldBe(null);
      joseRamirez.SP_GoldGlover.ShouldBe(null);
      joseRamirez.SP_SpiderCatch.ShouldBe(null);
      joseRamirez.SP_BarehandCatch.ShouldBe(null);
      joseRamirez.SP_AggressiveFielder.ShouldBe(null);
      joseRamirez.SP_PivotMan.ShouldBe(null);
      joseRamirez.SP_ErrorProne.ShouldBe(null);
      joseRamirez.SP_GoodBlocker.ShouldBe(null);
      joseRamirez.SP_Catching.ShouldBe(null);
      joseRamirez.SP_Throwing.ShouldBe(null);
      joseRamirez.SP_Cannon.ShouldBe(null);
      joseRamirez.SP_TrashTalker.ShouldBe(null);
      joseRamirez.SP_PConsistency.ShouldBe(null);
      joseRamirez.SP_PVsLefty.ShouldBe(null);
      joseRamirez.SP_Poise.ShouldBe(null);
      joseRamirez.SP_VsRunner.ShouldBe(null);
      joseRamirez.SP_WRisp.ShouldBe(null);
      joseRamirez.SP_SlowStarter.ShouldBe(null);
      joseRamirez.SP_StarterFinisher.ShouldBe(null);
      joseRamirez.SP_ChokeArtist.ShouldBe(null);
      joseRamirez.SP_Sandbag.ShouldBe(null);
      joseRamirez.SP_DrK.ShouldBe(null);
      joseRamirez.SP_WalkProne.ShouldBe(null);
      joseRamirez.SP_Luck.ShouldBe(null);
      joseRamirez.SP_Recovery.ShouldBe(null);
      joseRamirez.SP_PIntimidator.ShouldBe(null);
      joseRamirez.SP_Battler.ShouldBe(null);
      joseRamirez.SP_HotHead.ShouldBe(null);
      joseRamirez.SP_GoodDelivery.ShouldBe(null);
      joseRamirez.SP_Release.ShouldBe(null);
      joseRamirez.SP_GoodPace.ShouldBe(null);
      joseRamirez.SP_GoodReflexes.ShouldBe(null);
      joseRamirez.SP_GoodPickoff.ShouldBe(null);
      joseRamirez.SP_PowerOrBreakingBall.ShouldBe(null);
      joseRamirez.SP_FastballLife.ShouldBe(null);
      joseRamirez.SP_Spin.ShouldBe(null);
      joseRamirez.SP_SafeOrFatPitch.ShouldBe(null);
      joseRamirez.SP_GroundBallOrFlyBall.ShouldBe(null);
      joseRamirez.SP_GoodLowPitch.ShouldBe(null);
      joseRamirez.SP_Gyroball.ShouldBe(null);
      joseRamirez.SP_ShuttoSpin.ShouldBe(null);
    }
  }
}
