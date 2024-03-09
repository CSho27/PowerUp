using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.CSV
{
  public interface IPlayerCsvService
  {
    public Task ExportRoster(Stream stream, Roster roster);
    public Task<Roster> ImportRoster(Stream stream);
  }

  public class RosterCsvService : IPlayerCsvService
  {
    private readonly IPlayerCsvReader _reader;
    private readonly IPlayerCsvWriter _writer;
    private readonly IPlayerApi _playerApi;
    private readonly ITeamApi _teamApi;
    private readonly IRosterApi _rosterApi;

    public RosterCsvService(
      IPlayerCsvReader reader,
      IPlayerCsvWriter writer,
      IPlayerApi playerApi,
      ITeamApi teamApi,
      IRosterApi rosterApi
    )
    {
      _reader = reader;
      _writer = writer;
      _playerApi = playerApi;
      _teamApi = teamApi;
      _rosterApi = rosterApi;
    }

    public async Task ExportRoster(Stream stream, Roster roster)
    {
      var teamPlayers = roster.GetTeams().SelectMany(t => GetCsvPlayers(t.Key, t.Value.GetLSTeamId()));
      var freeAgents = roster.GetFreeAgentPlayers().Select(p => GetCsvPlayer(p));
      var csvPlayers = teamPlayers.Concat(freeAgents);
      await _writer.WriteAllPlayers(stream, csvPlayers);
    }

    public async Task<Roster> ImportRoster(Stream stream)
    {
      var entries = await _reader.ReadAllPlayers(stream);
      foreach(var entry in entries)
      {
        var player = CreatePlayerForEntry(entry);
      }

      return null;
    }

    private static IEnumerable<CsvRosterEntry> GetCsvPlayers(Team team, long mlbTeamId)
    {
      var playersAndRoles = team.GetPlayers().Select(p => (p, team.PlayerDefinitions.Single(d => d.PlayerId == p.Id))).ToList();
      var playersByPitcherRole = playersAndRoles.GroupBy(p => p.Item2.PitcherRole).ToDictionary(g => g.Key);
      return playersAndRoles.Select(playerAndRole =>
      {
        var (player, role) = playerAndRole;
        playersByPitcherRole[role.PitcherRole].FirstOrDefault(p => p.p.Id == player.Id, out var indexWithinPitcherRole);
        var noDHLineupSlot = team.NoDHLineup.FirstOrDefault(p => p.PlayerId == player.Id, out var noDHLineupIndex);
        var dhLineupSlot = team.DHLineup.FirstOrDefault(p => p.PlayerId == player.Id, out var dhLineupIndex);
        return GetCsvPlayer(
          player,
          mlbTeamId,
          role,
          indexWithinPitcherRole,
          noDHLineupIndex,
          noDHLineupSlot?.Position,
          dhLineupIndex,
          dhLineupSlot?.Position
        );
      });
    }

    private static CsvRosterEntry GetCsvPlayer(
      Player player, 
      long? mlbTeamId = null,
      PlayerRoleDefinition? playerRole = null,
      int? indexWithinPitcherRole = null,
      int? noDHLineupIndex = null,
      Position? noDHLineupPosition = null,
      int? dhLineupIndex = null,
      Position? dhLineupPosition = null
    )
    {
      var appearance = player.Appearance;
      var capabilities = player.PositionCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var pitcherAbilities = player.PitcherAbilities;
      var specialAbilities = player.SpecialAbilities;

      return new CsvRosterEntry
      {
        FirstName = player.FirstName,
        LastName = player.LastName,
        SavedName = player.SavedName,
        BirthMonth = player.BirthMonth,
        BirthDay = player.BirthDay,
        Age = player.Age,
        YearsInMajors = player.YearsInMajors,
        UniformNumber = player.UniformNumber,
        PrimaryPosition = (int)player.PrimaryPosition,
        PitcherType = (int)player.PitcherType,
        VoiceId = player.VoiceId,
        BattingSide = player.BattingSide.GetAbbrev(),
        BattingStanceId = player.BattingStanceId,
        ThrowingArm = player.ThrowingArm.GetAbbrev(),
        PitchingMechanicsId = player.PitchingMechanicsId,
        Avg = player.BattingAverage.HasValue && !double.IsNaN(player.BattingAverage.Value)
          ? player.BattingAverage
          : null,
        RBI = player.RunsBattedIn,
        HR = player.HomeRuns,
        ERA = player.EarnedRunAverage.HasValue && !double.IsNaN(player.EarnedRunAverage.Value)
          ? player.EarnedRunAverage
          : null,
        FaceId = appearance.FaceId,
        EyebrowThickness = (int?)appearance.EyebrowThickness,
        SkinColor = appearance.SkinColor.HasValue
          ? ((int)appearance.SkinColor.Value) + 1
          : null,
        EyeColor = (int?)appearance.EyeColor,
        HairStyle = (int?)appearance.HairStyle,
        HairColor = (int?)appearance.HairColor,
        FacialHairStyle = (int?)appearance.FacialHairStyle,
        FacialHairColor = (int?)appearance.FacialHairColor,
        BatColor = (int?)appearance.BatColor,
        GloveColor = (int?)appearance.GloveColor,
        EyewearType = (int?)appearance.EyewearType,
        EyewearFrameColor = (int?)appearance.EyewearFrameColor,
        EyewearLensColor = (int?)appearance.EyewearLensColor,
        EarringSide = (int?)appearance.EarringSide,
        EarringColor = (int?)appearance.EarringColor,
        RightWristbandColor = (int?)appearance.RightWristbandColor,
        LeftWristbandColor = (int?)appearance.LeftWristbandColor,
        Capabilities_P = capabilities.Pitcher.ToString(),
        Capabilities_C = capabilities.Catcher.ToString(),
        Capabilities_1B = capabilities.FirstBase.ToString(),
        Capabilities_2B = capabilities.SecondBase.ToString(),
        Capabilities_3B = capabilities.ThirdBase.ToString(),
        Capabilities_SS = capabilities.Shortstop.ToString(),
        Capabilities_LF = capabilities.LeftField.ToString(),
        Capabilities_CF = capabilities.CenterField.ToString(),
        Capabilities_RF = capabilities.RightField.ToString(),
        Trajectory = hitterAbilities.Trajectory,
        Contact = hitterAbilities.Contact,
        Power = hitterAbilities.Power,
        RunSpeed = hitterAbilities.RunSpeed,
        ArmStrength = hitterAbilities.ArmStrength,
        Fielding = hitterAbilities.Fielding,
        ErrorResistance = hitterAbilities.ErrorResistance,
        HZ_UpAndIn = hitterAbilities.HotZones.UpAndIn.GetAbbrev(),
        HZ_Up = hitterAbilities.HotZones.Up.GetAbbrev(),
        HZ_UpAndAway = hitterAbilities.HotZones.UpAndAway.GetAbbrev(),
        HZ_MiddleIn = hitterAbilities.HotZones.MiddleIn.GetAbbrev(),
        HZ_Middle = hitterAbilities.HotZones.Middle.GetAbbrev(),
        HZ_MiddleAway = hitterAbilities.HotZones.MiddleAway.GetAbbrev(),
        HZ_DownAndIn = hitterAbilities.HotZones.DownAndIn.GetAbbrev(),
        HZ_Down = hitterAbilities.HotZones.Down.GetAbbrev(),
        HZ_DownAndAway = hitterAbilities.HotZones.DownAndAway.GetAbbrev(),
        TopSpeedMph = pitcherAbilities.TopSpeedMph.RoundDown(),
        Control = pitcherAbilities.Control,
        Stamina = pitcherAbilities.Stamina,
        TwoSeam = pitcherAbilities.HasTwoSeam.ToInt(),
        TwoSeamMovement = pitcherAbilities.TwoSeamMovement,
        Slider1 = (int?)pitcherAbilities.Slider1Type,
        Slider1Movement = pitcherAbilities.Slider1Movement,
        Slider2 = (int?)pitcherAbilities.Slider2Type,
        Slider2Movement = pitcherAbilities.Slider2Movement,
        Curve1 = (int?)pitcherAbilities.Curve1Type,
        Curve1Movement = pitcherAbilities.Curve1Movement,
        Curve2 = (int?)pitcherAbilities.Curve2Type,
        Curve2Movement = pitcherAbilities.Curve2Movement,
        Fork1 = (int?)pitcherAbilities.Fork1Type,
        Fork1Movement = pitcherAbilities.Fork1Movement,
        Fork2 = (int?)pitcherAbilities.Fork2Type,
        Fork2Movement = pitcherAbilities.Fork2Movement,
        Sinker1 = (int?)pitcherAbilities.Sinker1Type,
        Sinker1Movement = pitcherAbilities.Sinker1Movement,
        Sinker2 = (int?)pitcherAbilities.Sinker2Type,
        Sinker2Movement = pitcherAbilities.Sinker2Movement,
        SinkFb1 = (int?)pitcherAbilities.SinkingFastball1Type,
        SinkFb1Movement = pitcherAbilities.SinkingFastball1Movement,
        SinkFb2 = (int?)pitcherAbilities.SinkingFastball2Type,
        SinkFb2Movement = pitcherAbilities.SinkingFastball2Movement,
        SP_Star = specialAbilities.General.IsStar.ToInt(),
        SP_Durability = specialAbilities.General.Durability.GetAbbrevInt(),
        SP_Morale = (int)specialAbilities.General.Morale,
        SP_Rain = (int)specialAbilities.General.InRainAbility,
        SP_DayGame = (int)specialAbilities.General.DayGameAbility,
        SP_HConsistency = specialAbilities.Hitter.SituationalHitting.Consistency.GetAbbrevInt(),
        SP_HVsLefty = specialAbilities.Hitter.SituationalHitting.VersusLefty.GetAbbrevInt(),
        SP_TableSetter = specialAbilities.Hitter.SituationalHitting.IsTableSetter.ToInt(),
        SP_B2BHitter = specialAbilities.Hitter.SituationalHitting.IsBackToBackHitter.ToInt(),
        SP_HotHitter = specialAbilities.Hitter.SituationalHitting.IsHotHitter.ToInt(),
        SP_RallyHitter = specialAbilities.Hitter.SituationalHitting.IsRallyHitter.ToInt(),
        SP_PinchHitter = specialAbilities.Hitter.SituationalHitting.IsGoodPinchHitter.ToInt(),
        SP_BasesLoadedHitter = (int)(specialAbilities.Hitter.SituationalHitting.BasesLoadedHitter ?? 0),
        SP_WalkOffHitter = (int)(specialAbilities.Hitter.SituationalHitting.WalkOffHitter ?? 0),
        SP_ClutchHitter = specialAbilities.Hitter.SituationalHitting.ClutchHitter.GetAbbrevInt(),
        SP_ContactHitter = specialAbilities.Hitter.HittingApproach.IsContactHitter.ToInt(),
        SP_PowerHitter = specialAbilities.Hitter.HittingApproach.IsPowerHitter.ToInt(),
        SP_SluggerOrSlapHitter = (int)(specialAbilities.Hitter.HittingApproach.SluggerOrSlapHitter ?? 0),
        SP_PushHitter = specialAbilities.Hitter.HittingApproach.IsPushHitter.ToInt(),
        SP_PullHitter = specialAbilities.Hitter.HittingApproach.IsPullHitter.ToInt(),
        SP_SprayHitter = specialAbilities.Hitter.HittingApproach.IsSprayHitter.ToInt(),
        SP_FirstballHitter = specialAbilities.Hitter.HittingApproach.IsFirstballHitter.ToInt(),
        SP_AggressiveOrPatientHitter = (int)(specialAbilities.Hitter.HittingApproach.AggressiveOrPatientHitter ?? 0),
        SP_RefinedHitter = specialAbilities.Hitter.HittingApproach.IsRefinedHitter.ToInt(),
        SP_FreeSwinger = specialAbilities.Hitter.HittingApproach.IsFreeSwinger.ToInt(),
        SP_ToughOut = specialAbilities.Hitter.HittingApproach.IsToughOut.ToInt(),
        SP_HIntimidator = specialAbilities.Hitter.HittingApproach.IsIntimidator.ToInt(),
        SP_Sparkplug = specialAbilities.Hitter.HittingApproach.IsSparkplug.ToInt(),
        SP_SmallBall = (int)specialAbilities.Hitter.SmallBall.SmallBall,
        SP_Bunting = (int)(specialAbilities.Hitter.SmallBall.Bunting ?? 0),
        SP_InfieldHitting = (int)(specialAbilities.Hitter.SmallBall.InfieldHitting ?? 0),
        SP_BaseRunning = specialAbilities.Hitter.BaseRunning.BaseRunning.GetAbbrevInt(),
        SP_Stealing = specialAbilities.Hitter.BaseRunning.Stealing.GetAbbrevInt(),
        SP_AggressiveRunner = specialAbilities.Hitter.BaseRunning.IsAggressiveRunner.ToInt(),
        SP_AggressiveBaseStealer = (int)(specialAbilities.Hitter.BaseRunning.AggressiveOrCautiousBaseStealer ?? 0),
        SP_ToughRunner = specialAbilities.Hitter.BaseRunning.IsToughRunner.ToInt(),
        SP_BreakupDp = specialAbilities.Hitter.BaseRunning.WillBreakupDoublePlay.ToInt(),
        SP_HeadFirstSlide = specialAbilities.Hitter.BaseRunning.WillSlideHeadFirst.ToInt(),
        SP_GoldGlover = specialAbilities.Hitter.Fielding.IsGoldGlover.ToInt(),
        SP_SpiderCatch = specialAbilities.Hitter.Fielding.CanSpiderCatch.ToInt(),
        SP_BarehandCatch = specialAbilities.Hitter.Fielding.CanBarehandCatch.ToInt(),
        SP_AggressiveFielder = specialAbilities.Hitter.Fielding.IsAggressiveFielder.ToInt(),
        SP_PivotMan = specialAbilities.Hitter.Fielding.IsPivotMan.ToInt(),
        SP_ErrorProne = specialAbilities.Hitter.Fielding.IsErrorProne.ToInt(),
        SP_GoodBlocker = specialAbilities.Hitter.Fielding.IsGoodBlocker.ToInt(),
        SP_Catching = (int)(specialAbilities.Hitter.Fielding.Catching ?? 0),
        SP_Throwing = specialAbilities.Hitter.Fielding.Throwing.GetAbbrevInt(),
        SP_Cannon = specialAbilities.Hitter.Fielding.HasCannonArm.ToInt(),
        SP_TrashTalker = specialAbilities.Hitter.Fielding.IsTrashTalker.ToInt(),
        SP_PConsistency = specialAbilities.Pitcher.SituationalPitching.Consistency.GetAbbrevInt(),
        SP_PVsLefty = specialAbilities.Pitcher.SituationalPitching.VersusLefty.GetAbbrevInt(),
        SP_Poise = specialAbilities.Pitcher.SituationalPitching.Poise.GetAbbrevInt(),
        SP_VsRunner = specialAbilities.Pitcher.SituationalPitching.PoorVersusRunner ? 2 : 3,
        SP_WRisp = specialAbilities.Pitcher.SituationalPitching.WithRunnersInSocringPosition.GetAbbrevInt(),
        SP_SlowStarter = specialAbilities.Pitcher.SituationalPitching.IsSlowStarter.ToInt(),
        SP_StarterFinisher = specialAbilities.Pitcher.SituationalPitching.IsStarterFinisher.ToInt(),
        SP_ChokeArtist = specialAbilities.Pitcher.SituationalPitching.IsChokeArtist.ToInt(),
        SP_Sandbag = specialAbilities.Pitcher.SituationalPitching.IsSandbag.ToInt(),
        SP_DrK = specialAbilities.Pitcher.SituationalPitching.DoctorK.ToInt(),
        SP_WalkProne = specialAbilities.Pitcher.SituationalPitching.IsWalkProne.ToInt(),
        SP_Luck = (int)specialAbilities.Pitcher.SituationalPitching.Luck,
        SP_Recovery = specialAbilities.Pitcher.SituationalPitching.Recovery.GetAbbrevInt(),
        SP_PIntimidator = specialAbilities.Pitcher.Demeanor.IsIntimidator.ToInt(),
        SP_Battler = (int)(specialAbilities.Pitcher.Demeanor.BattlerPokerFace ?? 0),
        SP_HotHead = specialAbilities.Pitcher.Demeanor.IsHotHead.ToInt(),
        SP_GoodDelivery = specialAbilities.Pitcher.PitchingMechanics.GoodDelivery.ToInt(),
        SP_Release = specialAbilities.Pitcher.PitchingMechanics.Release.GetAbbrevInt(),
        SP_GoodPace = specialAbilities.Pitcher.PitchingMechanics.GoodPace.ToInt(),
        SP_GoodReflexes = specialAbilities.Pitcher.PitchingMechanics.GoodReflexes.ToInt(),
        SP_GoodPickoff = specialAbilities.Pitcher.PitchingMechanics.GoodPickoff.ToInt(),
        SP_PowerOrBreakingBall = (int)(specialAbilities.Pitcher.PitchQuailities.PowerOrBreakingBallPitcher ?? 0),
        SP_FastballLife = specialAbilities.Pitcher.PitchQuailities.FastballLife.GetAbbrevInt(),
        SP_Spin = specialAbilities.Pitcher.PitchQuailities.Spin.GetAbbrevInt(),
        SP_SafeOrFatPitch = (int)specialAbilities.Pitcher.PitchQuailities.SafeOrFatPitch,
        SP_GroundBallOrFlyBall = (int)specialAbilities.Pitcher.PitchQuailities.GroundBallOrFlyBallPitcher,
        SP_GoodLowPitch = specialAbilities.Pitcher.PitchQuailities.GoodLowPitch.ToInt(),
        SP_Gyroball = specialAbilities.Pitcher.PitchQuailities.Gyroball.ToInt(),
        SP_ShuttoSpin = specialAbilities.Pitcher.PitchQuailities.ShuttoSpin.ToInt(),
        TM_MLBId = mlbTeamId,
        TM_AAA = playerRole?.IsAAA.ToInt(),
        TM_PinchHitter = playerRole?.IsDefensiveLiability.ToInt(),
        TM_PinchRunner = playerRole?.IsPinchRunner.ToInt(),
        TM_DefensiveReplacement = playerRole?.IsDefensiveReplacement.ToInt(),
        TM_DefensiveLiability = playerRole?.IsDefensiveLiability.ToInt(),
        TM_PitcherRole = (int?)playerRole?.PitcherRole,
        TM_PitcherRoleSlot = indexWithinPitcherRole + 1,
        TM_NoDHLineupSlot = noDHLineupIndex + 1,
        TM_NoDHLineupPosition = (int?)noDHLineupPosition,
        TM_DHLineupSlot = dhLineupIndex + 1,
        TM_DHLineupPosition = (int?)dhLineupPosition
      };
    }

    private Player CreatePlayerForEntry(CsvRosterEntry entry)
    {
      var isPitcher = entry.PrimaryPosition == (int)Position.Pitcher;
      var defaultPlayer = _playerApi.CreateDefaultPlayer(EntitySourceType.Custom, isPitcher);
      var parameters = new PlayerParameters
      {
        PersonalDetails = new PlayerPersonalDetailsParameters
        {
          FirstName = entry.FirstName.IsNotNullOrWhiteSpace()
            ? entry.FirstName
            : defaultPlayer.FirstName,
          LastName = entry.LastName.IsNotNullOrWhiteSpace()
            ? entry.LastName
            : defaultPlayer.LastName,
          SavedName = entry.SavedName.IsNotNullOrWhiteSpace()
            ? entry.SavedName 
            : entry.FirstName.IsNotNullOrWhiteSpace() && entry.LastName.IsNotNullOrWhiteSpace()
              ? NameUtils.GetSavedName(entry.FirstName, entry.LastName)
              : defaultPlayer.SavedName,
        }
      };
      return _playerApi.CreatePlayer(EntitySourceType.Custom, parameters);
    }
  }
}
