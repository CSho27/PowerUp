﻿using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.CSV
{
  public interface IPlayerCsvReader
  {
    Task<IEnumerable<CsvPlayer>> ReadAllPlayers(Stream stream);
  }

  public class PlayerCsvReader : IPlayerCsvReader
  {
    public async Task<IEnumerable<CsvPlayer>> ReadAllPlayers(Stream stream)
    {
      using var reader = new StreamReader(stream);
      using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
      return await csv.GetRecordsAsync<CsvPlayer>().ToListAsync();
    }
  }

  public record CsvPlayer
  {
    public int? TeamId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? BirthMonth { get; set; }
    public int? BirthDay { get; set; }
    public int? Age { get; set; }
    public int? YearsInMajors { get; set; }
    public string? UniformNumber { get; set; }
    public int? PrimaryPosition { get; set; }
    public int? PitcherType { get; set; }
    public int? VoiceId { get; set; }
    public string? BattingSide { get; set; }
    public int? BattingStanceId { get; set; }
    public string? ThrowingArm { get; set; }
    public int? PitchingMechanicsId { get; set; }
    public double? Avg { get; set; }
    public int? RBI { get; set; }
    public int? HR { get; set; }
    public double? ERA { get; set; }
    public int? FaceId { get; set; }
    public int? EyebrowThickness { get; set; }
    public int? SkinColor { get; set; }
    public int? EyeColor { get; set; }
    public int? HairStyle { get; set; }
    public int? HairColor { get; set; }
    public int? FacialHairStyle { get; set; }
    public int? FacialHairColor { get; set; }
    public int? BatColor { get; set; }
    public int? GloveColor { get; set; }
    public int? EyewearType { get; set; }
    public int? EyewearFrameColor { get; set; }
    public int? EyewearLensColor { get; set; }
    public int? EarringSide { get; set; }
    public int? EarringColor { get; set; }
    public int? RightWristbandColor { get; set; }
    public int? LeftWristbandColor { get; set; }
    public string? Capabilities_P { get; set; }
    public string? Capabilities_C { get; set; }
    public string? Capabilities_1B { get; set; }
    public string? Capabilities_2B { get; set; }
    public string? Capabilities_3B { get; set; }
    public string? Capabilities_SS { get; set; }
    public string? Capabilities_LF { get; set; }
    public string? Capabilities_CF { get; set; }
    public string? Capabilities_RF { get; set; }
    public int? Trajectory { get; set; }
    public int? Contact { get; set; }
    public int? Power { get; set; }
    public int? RunSpeed { get; set; }
    public int? ArmStrength { get; set; }
    public int? Fielding { get; set; }
    public int? ErrorResistance { get; set; }
    public string? HZ_UpAndIn { get; set; }
    public string? HZ_Up { get; set; }
    public string? HZ_UpAndAway { get; set; }
    public string? HZ_MiddleIn { get; set; }
    public string? HZ_Middle { get; set; }
    public string? HZ_MiddleAway { get; set; }
    public string? HZ_DownAndIn { get; set; }
    public string? HZ_Down { get; set; }
    public string? HZ_DownAndAway { get; set; }
    public int? TopSpeedMph { get; set; }
    public int? Control { get; set; }
    public int? Stamina { get; set; }
    public int? TwoSeam { get; set; }
    public int? TwoSeamMovement { get; set; }
    public int? Slider1 { get; set; }
    public int? Slider1Movement { get; set; }
    public int? Slider2 { get; set; }
    public int? Slider2Movement { get; set; }
    public int? Curve1 { get; set; }
    public int? Curve1Movement { get; set; }
    public int? Curve2 { get; set; }
    public int? Curve2Movement { get; set; }
    public int? Fork1 { get; set; }
    public int? Fork1Movement { get; set; }
    public int? Fork2 { get; set; }
    public int? Fork2Movement { get; set; }
    public int? Sinker1 { get; set; }
    public int? Sinker1Movement { get; set; }
    public int? Sinker2 { get; set; }
    public int? Sinker2Movement { get; set; }
    public int? SinkFb1 { get; set; }
    public int? SinkFb1Movement { get; set; }
    public int? SinkFb2 { get; set; }
    public int? SinkFb2Movement { get; set; }
    public int? SP_Star { get; set; }
    public int? SP_Durability { get; set; }
    public int? SP_Morale { get; set; }
    public int? SP_Rain { get; set; }
    public int? SP_DayGame { get; set; }
    public int? SP_HConsistency { get; set; }
    public int? SP_HVsLefty { get; set; }
    public int? SP_TableSetter { get; set; }
    public int? SP_B2BHitter { get; set; }
    public int? SP_HotHitter { get; set; }
    public int? SP_RallyHitter { get; set; }
    public int? SP_PinchHitter { get; set; }
    public int? SP_BasesLoadedHitter { get; set; }
    public int? SP_WalkOffHitter { get; set; }
    public int? SP_ClutchHitter { get; set; }
    public int? SP_ContactHitter { get; set; }
    public int? SP_PowerHitter { get; set; }
    public int? SP_SluggerOrSlapHitter { get; set; }
    public int? SP_PushHitter { get; set; }
    public int? SP_PullHitter { get; set; }
    public int? SP_SprayHitter { get; set; }
    public int? SP_FirstballHitter { get; set; }
    public int? SP_AggressiveOrPatientHitter { get; set; }
    public int? SP_RefinedHitter { get; set; }
    public int? SP_FreeSwinger { get; set; }
    public int? SP_ToughOut { get; set; }
    public int? SP_HIntimidator { get; set; }
    public int? SP_Sparkplug { get; set; }
    public int? SP_SmallBall { get; set; }
    public int? SP_Bunting { get; set; }
    public int? SP_InfieldHitting { get; set; }
    public int? SP_BaseRunning { get; set; }
    public int? SP_Stealing { get; set; }
    public int? SP_AggressiveRunner { get; set; }
    public int? SP_AggressiveBaseStealer { get; set; }
    public int? SP_ToughRunner { get; set; }
    public int? SP_BreakupDp { get; set; }
    public int? SP_HeadFirstSlide { get; set; }
    public int? SP_GoldGlover { get; set; }
    public int? SP_SpiderCatch { get; set; }
    public int? SP_BarehandCatch { get; set; }
    public int? SP_AggressiveFielder { get; set; }
    public int? SP_PivotMan { get; set; }
    public int? SP_ErrorProne { get; set; }
    public int? SP_GoodBlocker { get; set; }
    public int? SP_Catching { get; set; }
    public int? SP_Throwing { get; set; }
    public int? SP_Cannon { get; set; }
    public int? SP_TrashTalker { get; set; }
    public int? SP_PConsistency { get; set; }
    public int? SP_PVsLefty { get; set; }
    public int? SP_Poise { get; set; }
    public int? SP_VsRunner { get; set; }
    public int? SP_WRisp { get; set; }
    public int? SP_SlowStarter { get; set; }
    public int? SP_StarterFinisher { get; set; }
    public int? SP_ChokeArtist { get; set; }
    public int? SP_Sandbag { get; set; }
    public int? SP_DrK { get; set; }
    public int? SP_WalkProne { get; set; }
    public int? SP_Luck { get; set; }
    public int? SP_Recovery { get; set; }
    public int? SP_PIntimidator { get; set; }
    public int? SP_Battler { get; set; }
    public int? SP_HotHead { get; set; }
    public int? SP_GoodDelivery { get; set; }
    public int? SP_Release { get; set; }
    public int? SP_GoodPace { get; set; }
    public int? SP_GoodReflexes { get; set; }
    public int? SP_GoodPickoff { get; set; }
    public int? SP_PowerOrBreakingBall { get; set; }
    public int? SP_FastballLife { get; set; }
    public int? SP_Spin { get; set; }
    public int? SP_SafeOrFatPitch { get; set; }
    public int? SP_GroundBallOrFlyBall { get; set; }
    public int? SP_GoodLowPitch { get; set; }
    public int? SP_Gyroball { get; set; }
    public int? SP_ShuttoSpin { get; set; }
  }
}
