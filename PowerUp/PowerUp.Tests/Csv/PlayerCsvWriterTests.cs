using NUnit.Framework;
using PowerUp.CSV;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerUp.Tests.Csv
{
  public class PlayerCsvWriterTests
  {
    private readonly static string TEST_WRITE_PATH = Path.Combine(TestConfig.AssetDirectoryPath, "TestExport.csv");
    private IPlayerCsvWriter _writer;
    private IPlayerCsvReader _reader;

    [SetUp]
    public void Setup()
    {
      _writer = new RosterCsvWriter();
      _reader = new RosterCsvReader();
      File.Delete(TEST_WRITE_PATH);
    }

    [Test]
    public async Task ReadAllPlayers_ReadsPlayers()
    {
      // Arrange
      using (var writeStream = File.OpenWrite(TEST_WRITE_PATH))
      {
        // Act
        await _writer.WriteAllPlayers(writeStream, GetCsvPlayers());
      }

      // Assert
      using var readStream = File.OpenRead(TEST_WRITE_PATH);
      var players = await _reader.ReadAllPlayers(readStream);

      var joseRamirez = players.ElementAt(0);
      joseRamirez.FirstName.ShouldBe("Jose");
      joseRamirez.LastName.ShouldBe("Ramirez");
      joseRamirez.SavedName.ShouldBe("J.Ramirez");
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
      joseRamirez.TM_MLBId.ShouldBe(114);
    }

    private IEnumerable<CsvRosterEntry> GetCsvPlayers()
    {
      yield return new CsvRosterEntry
      {
        FirstName = "Jose",
        LastName = "Ramirez",
        SavedName = "J.Ramirez",
        BirthMonth = 9,
        BirthDay = 17,
        Age = 31,
        YearsInMajors = 9,
        UniformNumber = "11",
        PrimaryPosition = 5,
        PitcherType = 0,
        VoiceId = 2702,
        BattingSide = "S",
        BattingStanceId = 11,
        ThrowingArm = "R",
        PitchingMechanicsId = 0,
        Avg = 0.282,
        RBI = 80,
        HR = 24,
        ERA = null,
        FaceId = 95,
        EyebrowThickness = 1,
        SkinColor = 4,
        EyeColor = 0,
        HairStyle = 16,
        HairColor = 3,
        FacialHairStyle = 7,
        FacialHairColor = 3,
        BatColor = 2,
        GloveColor = 5,
        EyewearType = 0,
        EyewearFrameColor = 0,
        EyewearLensColor = 0,
        EarringSide = 0,
        EarringColor = 0,
        RightWristbandColor = 0,
        LeftWristbandColor = 0,
        Capabilities_P = "G",
        Capabilities_C = "G",
        Capabilities_1B = "F",
        Capabilities_2B = "E",
        Capabilities_3B = "A",
        Capabilities_SS = "E",
        Capabilities_LF = "G",
        Capabilities_CF = "G",
        Capabilities_RF = "G",
        Trajectory = 3,
        Contact = 9,
        Power = 174,
        RunSpeed = 10,
        ArmStrength = 8,
        Fielding = 10,
        ErrorResistance = 11,
        HZ_UpAndIn = "H",
        HZ_Up = "C",
        HZ_UpAndAway = "N",
        HZ_MiddleIn = "H",
        HZ_Middle = "C",
        HZ_MiddleAway = "N",
        HZ_DownAndIn = "N",
        HZ_Down = "C",
        HZ_DownAndAway = "H",
        TopSpeedMph = 74,
        Control = 0,
        Stamina = 0,
        TwoSeam = 0,
        TwoSeamMovement = 0,
        Slider1 = 0,
        Slider1Movement = 0,
        Slider2 = 0,
        Slider2Movement = 0,
        Curve1 = 0,
        Curve1Movement = 0,
        Curve2 = 0,
        Curve2Movement = 0,
        Fork1 = 0,
        Fork1Movement = 0,
        Fork2 = 0,
        Fork2Movement = 0,
        Sinker1 = 0,
        Sinker1Movement = 0,
        Sinker2 = 0,
        Sinker2Movement = 0,
        SinkFb1 = 0,
        SinkFb1Movement = 0,
        SinkFb2 = 0,
        SinkFb2Movement = 0,
        SP_Star = 0,
        SP_Durability = 3,
        SP_Morale = 0,
        SP_Rain = null,
        SP_DayGame = null,
        SP_HConsistency = null,
        SP_HVsLefty = null,
        SP_TableSetter = null,
        SP_B2BHitter = null,
        SP_HotHitter = null,
        SP_RallyHitter = null,
        SP_PinchHitter = null,
        SP_BasesLoadedHitter = null,
        SP_WalkOffHitter = null,
        SP_ClutchHitter = null,
        SP_ContactHitter = null,
        SP_PowerHitter = null,
        SP_SluggerOrSlapHitter = null,
        SP_PushHitter = null,
        SP_PullHitter = null,
        SP_SprayHitter = null,
        SP_FirstballHitter = null,
        SP_AggressiveOrPatientHitter = null,
        SP_RefinedHitter = null,
        SP_FreeSwinger = null,
        SP_ToughOut = null,
        SP_HIntimidator = null,
        SP_Sparkplug = null,
        SP_SmallBall = null,
        SP_Bunting = null,
        SP_InfieldHitting = null,
        SP_BaseRunning = null,
        SP_Stealing = null,
        SP_AggressiveRunner = null,
        SP_AggressiveBaseStealer = null,
        SP_ToughRunner = null,
        SP_BreakupDp = null,
        SP_HeadFirstSlide = null,
        SP_GoldGlover = null,
        SP_SpiderCatch = null,
        SP_BarehandCatch = null,
        SP_AggressiveFielder = null,
        SP_PivotMan = null,
        SP_ErrorProne = null,
        SP_GoodBlocker = null,
        SP_Catching = null,
        SP_Throwing = null,
        SP_Cannon = null,
        SP_TrashTalker = null,
        SP_PConsistency = null,
        SP_PVsLefty = null,
        SP_Poise = null,
        SP_VsRunner = null,
        SP_WRisp = null,
        SP_SlowStarter = null,
        SP_StarterFinisher = null,
        SP_ChokeArtist = null,
        SP_Sandbag = null,
        SP_DrK = null,
        SP_WalkProne = null,
        SP_Luck = null,
        SP_Recovery = null,
        SP_PIntimidator = null,
        SP_Battler = null,
        SP_HotHead = null,
        SP_GoodDelivery = null,
        SP_Release = null,
        SP_GoodPace = null,
        SP_GoodReflexes = null,
        SP_GoodPickoff = null,
        SP_PowerOrBreakingBall = null,
        SP_FastballLife = null,
        SP_Spin = null,
        SP_SafeOrFatPitch = null,
        SP_GroundBallOrFlyBall = null,
        SP_GoodLowPitch = null,
        SP_Gyroball = null,
        SP_ShuttoSpin = null,
        TM_MLBId = 114,
      };
    }
  }

}
