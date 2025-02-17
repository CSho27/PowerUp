using PowerUp.Databases;
using PowerUp.Entities;
using PowerUp.Entities.Players;
using PowerUp.Entities.Players.Api;
using PowerUp.Entities.Rosters;
using PowerUp.Entities.Rosters.Api;
using PowerUp.Entities.Teams;
using PowerUp.Entities.Teams.Api;
using PowerUp.Libraries;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PowerUp.CSV
{
  public interface IPlayerCsvService
  {
    public Task ExportRoster(Stream stream, Roster roster);
    public Task<Roster?> ImportRoster(Stream stream, string rosterName);
  }

  public class RosterCsvService : IPlayerCsvService
  {
    private readonly IPlayerCsvReader _reader;
    private readonly IPlayerCsvWriter _writer;
    private readonly ISpecialSavedNameLibrary _specialSavedNameLibrary;
    private readonly IPlayerApi _playerApi;
    private readonly ITeamApi _teamApi;
    private readonly IRosterApi _rosterApi;

    public RosterCsvService(
      IPlayerCsvReader reader,
      IPlayerCsvWriter writer,
      ISpecialSavedNameLibrary specialSavedNameLibrary,
      IPlayerApi playerApi,
      ITeamApi teamApi,
      IRosterApi rosterApi
    )
    {
      _reader = reader;
      _writer = writer;
      _specialSavedNameLibrary = specialSavedNameLibrary;
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

    public async Task<Roster?> ImportRoster(Stream stream, string rosterName)
    {
      var entries = await _reader.ReadAllPlayers(stream);
      var teamPlayers = new Dictionary<long, IList<CreatedPlayerEntry>>();
      var nonTeamPlayers = new List<CreatedPlayerEntry>();
      foreach(var entry in entries)
      {
        var player = CreatePlayerForEntry(entry);
        var savedPlayerEntry = new CreatedPlayerEntry(player, entry);
        if (entry.TM_MLBId.HasValue)
        {
          teamPlayers.TryGetValue(entry.TM_MLBId.Value, out var existingList);
          if (existingList is not null)
            existingList.Add(savedPlayerEntry);
          else
            teamPlayers.Add(entry.TM_MLBId.Value, new List<CreatedPlayerEntry> { savedPlayerEntry });
        }
        else
        {
          nonTeamPlayers.Add(savedPlayerEntry);
        }
      }

      DatabaseConfig.Database.SaveAll(teamPlayers.SelectMany(t => t.Value).Select(p => p.Player));
      DatabaseConfig.Database.SaveAll(nonTeamPlayers.Select(p => p.Player));

      var teams = new List<CreatedTeamEntry>();
      foreach(var teamPlayerList in teamPlayers)
      {
        var team = CreateTeamFromEntries(teamPlayerList.Value);
        teams.Add(new CreatedTeamEntry(team, teamPlayerList.Key));
      }
      if (teams.Count > 0)
        DatabaseConfig.Database.SaveAll(teams.Select(t => t.Team));
      else
        return null;


      return _rosterApi.CreateRosterFromTeams(
        rosterName, 
        teams.ToDictionary(t => MLBPPTeamExtensions.FromTeamId(t.MLBTeamId), t => t.Team.Id!.Value),
        nonTeamPlayers.Take(15).Select(p => p.Player.Id!.Value)
      );
    }

    private record CreatedPlayerEntry(Player Player, CsvRosterEntry Entry);
    private record CreatedTeamEntry(Team Team, long MLBTeamId);

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
          team.Name,
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
      string? teamName = null,
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
        SP_BasesLoadedHitter = (int?)specialAbilities.Hitter.SituationalHitting.BasesLoadedHitter,
        SP_WalkOffHitter = (int?)specialAbilities.Hitter.SituationalHitting.WalkOffHitter,
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
        SP_CannonArm = specialAbilities.Hitter.Fielding.HasCannonArm.ToInt(),
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
        SP_BattlerPokerFace = (int)(specialAbilities.Pitcher.Demeanor.BattlerPokerFace ?? 0),
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
        SP_ShuutoSpin = specialAbilities.Pitcher.PitchQuailities.ShuttoSpin.ToInt(),
        TM_MLBId = mlbTeamId,
        TM_Name = teamName, 
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
      var @default = _playerApi.CreateDefaultPlayer(EntitySourceType.Imported, isPitcher);
      var isSpecialSavedName = entry.SavedName.IsNotNullOrWhiteSpace() && _specialSavedNameLibrary.ContainsName(entry.SavedName);

      var parameters = new PlayerParameters
      {
        PersonalDetails = new PlayerPersonalDetailsParameters
        {
          FirstName = entry.FirstName.IsNotNullOrWhiteSpace()
            ? entry.FirstName
            : @default.FirstName,
          LastName = entry.LastName.IsNotNullOrWhiteSpace()
            ? entry.LastName
            : @default.LastName,
          SpecialSavedNameId = isSpecialSavedName
            ? _specialSavedNameLibrary[entry.SavedName!]
            : null,
          SavedName = entry.SavedName.IsNotNullOrWhiteSpace()
            ? isSpecialSavedName
              ? entry.SavedName
              : entry.SavedName.ShortenNameToLength(10)
            : entry.FirstName.IsNotNullOrWhiteSpace() && entry.LastName.IsNotNullOrWhiteSpace()
              ? NameUtils.GetSavedName(entry.FirstName, entry.LastName)
              : @default.SavedName,
          BirthMonth = entry.BirthMonth ?? @default.BirthMonth,
          BirthDay = entry.BirthDay ?? @default.BirthDay,
          Age = entry.Age ?? @default.Age,
          YearsInMajors = entry.YearsInMajors ?? @default.YearsInMajors,
          UniformNumber = entry.UniformNumber ?? @default.UniformNumber,
          Position = ((Position?)entry.PrimaryPosition) ?? @default.PrimaryPosition,
          PitcherType = ((PitcherType?)entry.PitcherType) ?? @default.PitcherType,
          VoiceId = entry.VoiceId ?? @default.VoiceId,
          BattingSide = EnumExtensions.FromAbbrev<BattingSide>(entry.BattingSide) ?? @default.BattingSide,
          ThrowingArm = EnumExtensions.FromAbbrev<ThrowingArm>(entry.ThrowingArm) ?? @default.ThrowingArm,
          BattingStanceId = entry.BattingStanceId ?? @default.BattingStanceId,
          PitchingMechanicsId = entry.PitchingMechanicsId ?? @default.PitchingMechanicsId,
          BattingAverage = entry.Avg ?? @default.BattingAverage,
          RunsBattedIn = entry.RBI ?? @default.RunsBattedIn,
          HomeRuns = entry.HR ?? @default.HomeRuns,
          EarnedRunAverage = entry.ERA ?? @default.EarnedRunAverage,
        },
        Appearance = new PlayerAppearanceParameters
        {
          FaceId = entry.FaceId ?? @default.Appearance.FaceId,
          EyebrowThickness = ((EyebrowThickness?)entry.EyebrowThickness) ?? @default.Appearance.EyebrowThickness,
          SkinColor = ((SkinColor?)entry.SkinColor - 1) ?? @default.Appearance.SkinColor,
          EyeColor = ((EyeColor?)entry.EyeColor) ?? @default.Appearance.EyeColor,
          HairStyle = ((HairStyle?)entry.HairStyle) ?? @default.Appearance.HairStyle,
          HairColor = ((HairColor?)entry.HairColor) ?? @default.Appearance.HairColor,
          FacialHairStyle = ((FacialHairStyle?)entry.FacialHairStyle) ?? @default.Appearance.FacialHairStyle,
          FacialHairColor = ((HairColor?)entry.FacialHairColor) ?? @default.Appearance.FacialHairColor,
          BatColor = ((BatColor?)entry.BatColor) ?? @default.Appearance.BatColor,
          GloveColor = ((GloveColor?)entry.GloveColor) ?? @default.Appearance.GloveColor,
          EyewearType = ((EyewearType?)entry.EyewearType) ?? @default.Appearance.EyewearType,
          EyewearFrameColor = ((EyewearFrameColor?)entry.EyewearFrameColor) ?? @default.Appearance.EyewearFrameColor,
          EyewearLensColor = ((EyewearLensColor?)entry.EyewearLensColor) ?? @default.Appearance.EyewearLensColor,
          EarringSide = ((EarringSide?)entry.EarringSide) ?? @default.Appearance.EarringSide,
          EarringColor = ((AccessoryColor?)entry.EarringColor) ?? @default.Appearance.EarringColor,
          RightWristbandColor = ((AccessoryColor?)entry.RightWristbandColor) ?? @default.Appearance.RightWristbandColor,
          LeftWristbandColor = ((AccessoryColor?)entry.LeftWristbandColor) ?? @default.Appearance.LeftWristbandColor,
        },
        PositionCapabilities = new PlayerPositionCapabilitiesParameters
        {
          Pitcher = EnumExtensions.ToEnum<Grade>(entry.Capabilities_P) ?? @default.PositionCapabilities.Pitcher,
          Catcher = EnumExtensions.ToEnum<Grade>(entry.Capabilities_C) ?? @default.PositionCapabilities.Catcher,
          FirstBase = EnumExtensions.ToEnum<Grade>(entry.Capabilities_1B) ?? @default.PositionCapabilities.FirstBase,
          SecondBase = EnumExtensions.ToEnum<Grade>(entry.Capabilities_2B) ?? @default.PositionCapabilities.SecondBase,
          ThirdBase = EnumExtensions.ToEnum<Grade>(entry.Capabilities_3B) ?? @default.PositionCapabilities.ThirdBase,
          Shortstop = EnumExtensions.ToEnum<Grade>(entry.Capabilities_SS) ?? @default.PositionCapabilities.Shortstop,
          LeftField = EnumExtensions.ToEnum<Grade>(entry.Capabilities_LF) ?? @default.PositionCapabilities.LeftField,
          CenterField = EnumExtensions.ToEnum<Grade>(entry.Capabilities_CF) ?? @default.PositionCapabilities.CenterField,
          RightField = EnumExtensions.ToEnum<Grade>(entry.Capabilities_RF) ?? @default.PositionCapabilities.RightField,
        },
        HitterAbilities = new PlayerHitterAbilityParameters
        {
          Trajectory = entry.Trajectory ?? @default.HitterAbilities.Trajectory,
          Contact = entry.Contact ?? @default.HitterAbilities.Contact,
          Power = entry.Power ?? @default.HitterAbilities.Power,
          RunSpeed = entry.RunSpeed ?? @default.HitterAbilities.RunSpeed,
          ArmStrength = entry.ArmStrength ?? @default.HitterAbilities.ArmStrength,
          Fielding = entry.Fielding ?? @default.HitterAbilities.Fielding,
          ErrorResistance = entry.ErrorResistance ?? @default.HitterAbilities.ErrorResistance,
          HotZoneGridParameters = new HotZoneGridParameters
          {
            UpAndIn = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_UpAndIn) ?? @default.HitterAbilities.HotZones.UpAndIn,
            Up = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_Up) ?? @default.HitterAbilities.HotZones.Up,
            UpAndAway = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_UpAndAway) ?? @default.HitterAbilities.HotZones.UpAndAway,
            MiddleIn = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_MiddleIn) ?? @default.HitterAbilities.HotZones.MiddleIn,
            Middle = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_Middle) ?? @default.HitterAbilities.HotZones.Middle,
            MiddleAway = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_MiddleAway) ?? @default.HitterAbilities.HotZones.MiddleAway,
            DownAndIn = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_DownAndIn) ?? @default.HitterAbilities.HotZones.DownAndIn,
            Down = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_Down) ?? @default.HitterAbilities.HotZones.Down,
            DownAndAway = EnumExtensions.FromAbbrev<HotZonePreference>(entry.HZ_DownAndAway) ?? @default.HitterAbilities.HotZones.DownAndAway,
          },
        },
        PitcherAbilities = new PlayerPitcherAbilitiesParameters
        {
          TopSpeed = entry.TopSpeedMph ?? @default.PitcherAbilities.TopSpeedMph.RoundDown(),
          Control = entry.Control ?? @default.PitcherAbilities.Control,
          Stamina = entry.Stamina ?? @default.PitcherAbilities.Stamina,
          HasTwoSeam = entry.TwoSeam.ToNullableBool() ?? @default.PitcherAbilities.HasTwoSeam,
          TwoSeamMovement = entry.TwoSeamMovement ?? @default.PitcherAbilities.TwoSeamMovement,
          Slider1Type = ((SliderType?)entry.Slider1) ?? @default.PitcherAbilities.Slider1Type,
          Slider1Movement = entry.Slider1Movement ?? @default.PitcherAbilities.Slider1Movement,
          Slider2Type = ((SliderType?)entry.Slider2) ?? @default.PitcherAbilities.Slider2Type,
          Slider2Movement = entry.Slider2Movement ?? @default.PitcherAbilities.Slider2Movement,
          Curve1Type = ((CurveType?)entry.Curve1) ?? @default.PitcherAbilities.Curve1Type,
          Curve1Movement = entry.Curve1Movement ?? @default.PitcherAbilities.Curve1Movement,
          Curve2Type = ((CurveType?)entry.Curve2) ?? @default.PitcherAbilities.Curve2Type,
          Curve2Movement = entry.Curve2Movement ?? @default.PitcherAbilities.Curve2Movement,
          Fork1Type = ((ForkType?)entry.Fork1) ?? @default.PitcherAbilities.Fork1Type,
          Fork1Movement = entry.Fork1Movement ?? @default.PitcherAbilities.Fork1Movement,
          Fork2Type = ((ForkType?)entry.Fork2) ?? @default.PitcherAbilities.Fork2Type,
          Fork2Movement = entry.Fork2Movement ?? @default.PitcherAbilities.Fork2Movement,
          Sinker1Type = ((SinkerType?)entry.Sinker1) ?? @default.PitcherAbilities.Sinker1Type,
          Sinker1Movement = entry.Sinker1Movement ?? @default.PitcherAbilities.Sinker1Movement,
          Sinker2Type = ((SinkerType?)entry.Sinker2) ?? @default.PitcherAbilities.Sinker2Type,
          Sinker2Movement = entry.Sinker2Movement ?? @default.PitcherAbilities.Sinker2Movement,
          SinkingFastball1Type = ((SinkingFastballType?)entry.SinkFb1) ?? @default.PitcherAbilities.SinkingFastball1Type,
          SinkingFastball1Movement = entry.SinkFb1Movement ?? @default.PitcherAbilities.SinkingFastball1Movement,
          SinkingFastball2Type = ((SinkingFastballType?)entry.SinkFb2) ?? @default.PitcherAbilities.SinkingFastball2Type,
          SinkingFastball2Movement = entry.SinkFb2Movement ?? @default.PitcherAbilities.SinkingFastball2Movement,
        },
        SpecialAbilities = new SpecialAbilitiesParameters
        {
          General = new GeneralSpecialAbilitiesParameters
          {
            IsStar = entry.SP_Star.ToNullableBool() ?? @default.SpecialAbilities.General.IsStar,
            Durability = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Durability?.ToString()) ?? @default.SpecialAbilities.General.Durability,
            Morale = ((SpecialPositive_Negative?)entry.SP_Morale) ?? @default.SpecialAbilities.General.Morale,
            InRainAbility = ((SpecialPositive_Negative?)entry.SP_Rain) ?? @default.SpecialAbilities.General.InRainAbility,
            DayGameAbility = ((SpecialPositive_Negative?)entry.SP_DayGame) ?? @default.SpecialAbilities.General.DayGameAbility,
          },
          Hitter = new HitterSpecialAbilitiesParameters
          {
            SituationalHitting = new SituationalHittingSpecialAbilitiesParameters
            {
              Consistency = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_HConsistency?.ToString()) ?? @default.SpecialAbilities.Hitter.SituationalHitting.Consistency,
              VersusLefty = EnumExtensions.FromAbbrev<Special1_5>(entry.SP_HVsLefty?.ToString()) ?? @default.SpecialAbilities.Hitter.SituationalHitting.VersusLefty,
              IsTableSetter = entry.SP_TableSetter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.SituationalHitting.IsTableSetter,
              IsBackToBackHitter = entry.SP_B2BHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.SituationalHitting.IsBackToBackHitter,
              IsHotHitter = entry.SP_HotHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.SituationalHitting.IsHotHitter,
              IsRallyHitter = entry.SP_RallyHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.SituationalHitting.IsRallyHitter,
              IsGoodPinchHitter = entry.SP_PinchHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.SituationalHitting.IsGoodPinchHitter,
              BasesLoadedHitter = entry.SP_BasesLoadedHitter.HasValue && entry.SP_BasesLoadedHitter != 0
                ? (BasesLoadedHitter)entry.SP_BasesLoadedHitter
                : @default.SpecialAbilities.Hitter.SituationalHitting.BasesLoadedHitter,
              WalkOffHitter = entry.SP_WalkOffHitter.HasValue && entry.SP_WalkOffHitter != 0
                ? (WalkOffHitter)entry.SP_WalkOffHitter
                : @default.SpecialAbilities.Hitter.SituationalHitting.WalkOffHitter,
              ClutchHitter = EnumExtensions.FromAbbrev<Special1_5>(entry.SP_ClutchHitter?.ToString()) ?? @default.SpecialAbilities.Hitter.SituationalHitting.ClutchHitter,
            },
            HittingApproach = new HittingApproachSpecialAbilitiesParameters
            {
              IsContactHitter = entry.SP_ContactHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsContactHitter,
              IsPowerHitter = entry.SP_PowerHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsPowerHitter,
              SluggerOrSlapHitter = entry.SP_SluggerOrSlapHitter.HasValue && entry.SP_SluggerOrSlapHitter != 0
                ? (SluggerOrSlapHitter)entry.SP_SluggerOrSlapHitter
                : @default.SpecialAbilities.Hitter.HittingApproach.SluggerOrSlapHitter,
              IsPushHitter = entry.SP_PushHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsPushHitter,
              IsPullHitter = entry.SP_PullHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsPullHitter,
              IsSprayHitter = entry.SP_SprayHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsSprayHitter,
              IsFirstballHitter = entry.SP_FirstballHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsFirstballHitter,
              AggressiveOrPatientHitter = entry.SP_AggressiveOrPatientHitter.HasValue && entry.SP_AggressiveOrPatientHitter != 0
                ? (AggressiveOrPatientHitter)entry.SP_AggressiveOrPatientHitter
                : @default.SpecialAbilities.Hitter.HittingApproach.AggressiveOrPatientHitter,
              IsRefinedHitter = entry.SP_RefinedHitter.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsRefinedHitter,
              IsFreeSwinger = entry.SP_FreeSwinger.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsFreeSwinger,
              IsToughOut = entry.SP_ToughOut.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsToughOut,
              IsIntimidator = entry.SP_HIntimidator.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsIntimidator,
              IsSparkplug = entry.SP_Sparkplug.ToNullableBool() ?? @default.SpecialAbilities.Hitter.HittingApproach.IsSparkplug,
            },
            SmallBall = new SmallBallSpecialAbilitiesParameters
            {
              SmallBall = ((SpecialPositive_Negative?)entry.SP_SmallBall) ?? @default.SpecialAbilities.Hitter.SmallBall.SmallBall,
              Bunting = entry.SP_Bunting.HasValue && entry.SP_Bunting != 0
                ? (BuntingAbility)entry.SP_Bunting
                : @default.SpecialAbilities.Hitter.SmallBall.Bunting,
              InfieldHitting = entry.SP_InfieldHitting.HasValue && entry.SP_InfieldHitting != 0
                ? (InfieldHittingAbility)entry.SP_InfieldHitting
                : @default.SpecialAbilities.Hitter.SmallBall.InfieldHitting,
            },
            BaseRunning = new BaseRunningSpecialAbilitiesParameters
            {
              BaseRunning = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_BaseRunning?.ToString()) ?? @default.SpecialAbilities.Hitter.BaseRunning.BaseRunning,
              Stealing = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Stealing?.ToString()) ?? @default.SpecialAbilities.Hitter.BaseRunning.Stealing,
              IsAggressiveRunner = entry.SP_AggressiveRunner.ToNullableBool() ?? @default.SpecialAbilities.Hitter.BaseRunning.IsAggressiveRunner,
              AggressiveOrCautiousBaseStealer = entry.SP_AggressiveBaseStealer.HasValue && entry.SP_AggressiveBaseStealer != 0
                ? (AggressiveOrCautiousBaseStealer)entry.SP_AggressiveBaseStealer
                : @default.SpecialAbilities.Hitter.BaseRunning.AggressiveOrCautiousBaseStealer,
              IsToughRunner = entry.SP_ToughRunner.ToNullableBool() ?? @default.SpecialAbilities.Hitter.BaseRunning.IsToughRunner,
              WillBreakupDoublePlay = entry.SP_BreakupDp.ToNullableBool() ?? @default.SpecialAbilities.Hitter.BaseRunning.WillBreakupDoublePlay,
              WillSlideHeadFirst = entry.SP_HeadFirstSlide.ToNullableBool() ?? @default.SpecialAbilities.Hitter.BaseRunning.WillSlideHeadFirst,
            },
            Fielding = new FieldingSpecialAbilitiesParameters
            {
              IsGoldGlover = entry.SP_GoldGlover.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsGoldGlover,
              CanSpiderCatch = entry.SP_SpiderCatch.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.CanSpiderCatch,
              CanBarehandCatch = entry.SP_BarehandCatch.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.CanBarehandCatch,
              IsAggressiveFielder = entry.SP_AggressiveFielder.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsAggressiveFielder,
              IsPivotMan = entry.SP_PivotMan.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsPivotMan,
              IsErrorProne = entry.SP_ErrorProne.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsErrorProne,
              IsGoodBlocker = entry.SP_GoodBlocker.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsGoodBlocker,
              Catching = entry.SP_Catching.HasValue && entry.SP_Catching != 0
                ? (CatchingAbility)entry.SP_Catching
                : @default.SpecialAbilities.Hitter.Fielding.Catching,
              Throwing = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Throwing?.ToString()) ?? @default.SpecialAbilities.Hitter.Fielding.Throwing,
              HasCannonArm = entry.SP_CannonArm.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.HasCannonArm,
              IsTrashTalker = entry.SP_TrashTalker.ToNullableBool() ?? @default.SpecialAbilities.Hitter.Fielding.IsTrashTalker,
            },
          },
          Pitcher = new PitcherSpecialAbilitiesParameters
          {
            SituationalPitching = new SituationalPitchingSpecialAbilitiesParameters
            {
              Consistency = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_PConsistency?.ToString()) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.Consistency,
              VersusLefty = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_PVsLefty?.ToString()) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.VersusLefty,
              Poise = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Poise?.ToString()) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.Poise,
              PoorVersusRunner = entry.SP_VsRunner.HasValue && entry.SP_VsRunner.Value == 2,
              WithRunnersInSocringPosition = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_WRisp?.ToString()) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.WithRunnersInSocringPosition,
              IsSlowStarter = entry.SP_SlowStarter.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.IsSlowStarter,
              IsStarterFinisher = entry.SP_StarterFinisher.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.IsStarterFinisher,
              IsChokeArtist = entry.SP_ChokeArtist.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.IsChokeArtist,
              IsSandbag = entry.SP_Sandbag.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.IsSandbag,
              DoctorK = entry.SP_DrK.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.DoctorK,
              IsWalkProne = entry.SP_WalkProne.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.SituationalPitching.IsWalkProne,
              Luck = ((SpecialPositive_Negative?)entry.SP_Luck) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.Luck,
              Recovery = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Recovery?.ToString()) ?? @default.SpecialAbilities.Pitcher.SituationalPitching.Recovery,
            },
            Demeanor = new PitchingDemeanorSpecialAbilitiesParameters
            {
              IsIntimidator = entry.SP_PIntimidator.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.Demeanor.IsIntimidator,
              BattlerPokerFace = entry.SP_BattlerPokerFace.HasValue && entry.SP_BattlerPokerFace != 0
                ? (BattlerPokerFace)entry.SP_BattlerPokerFace
                : @default.SpecialAbilities.Pitcher.Demeanor.BattlerPokerFace,
              IsHotHead = entry.SP_HotHead.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.Demeanor.IsHotHead,
            },
            PitchingMechanics = new PitchingMechanicsSpecialAbilitiesParameters
            {
              GoodDelivery = entry.SP_GoodDelivery.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchingMechanics.GoodDelivery,
              Release = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Release?.ToString()) ?? @default.SpecialAbilities.Pitcher.PitchingMechanics.Release,
              GoodPace = entry.SP_GoodPace.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchingMechanics.GoodPace,
              GoodReflexes = entry.SP_GoodReflexes.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchingMechanics.GoodReflexes,
              GoodPickoff = entry.SP_GoodPickoff.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchingMechanics.GoodPickoff,
            },
            PitchQuailities = new PitchQualitiesSpecialAbilitiesParameters
            {
              PowerOrBreakingBallPitcher = entry.SP_PowerOrBreakingBall.HasValue && entry.SP_PowerOrBreakingBall != 0
                ? (PowerOrBreakingBallPitcher)entry.SP_PowerOrBreakingBall
                : @default.SpecialAbilities.Pitcher.PitchQuailities.PowerOrBreakingBallPitcher,
              FastballLife = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_FastballLife?.ToString()) ?? @default.SpecialAbilities.Pitcher.PitchQuailities.FastballLife,
              Spin = EnumExtensions.FromAbbrev<Special2_4>(entry.SP_Spin?.ToString()) ?? @default.SpecialAbilities.Pitcher.PitchQuailities.Spin,
              SafeOrFatPitch = ((SpecialPositive_Negative?)entry.SP_SafeOrFatPitch) ?? @default.SpecialAbilities.Pitcher.PitchQuailities.SafeOrFatPitch,
              GroundBallOrFlyBallPitcher = ((SpecialPositive_Negative?)entry.SP_GroundBallOrFlyBall) ?? @default.SpecialAbilities.Pitcher.PitchQuailities.GroundBallOrFlyBallPitcher,
              GoodLowPitch = entry.SP_GoodLowPitch.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchQuailities.GoodLowPitch,
              Gyroball = entry.SP_Gyroball.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchQuailities.Gyroball,
              ShuttoSpin = entry.SP_ShuutoSpin.ToNullableBool() ?? @default.SpecialAbilities.Pitcher.PitchQuailities.ShuttoSpin,
            }
          }
        }
      };
      return _playerApi.CreatePlayer(EntitySourceType.Imported, parameters);
    }
    
    private Team CreateTeamFromEntries(IEnumerable<CreatedPlayerEntry> playerEntries)
    {
      var firstPlayer = playerEntries.First();
      var teamName = firstPlayer.Entry.TM_Name ?? MLBPPTeamExtensions.FromTeamId(firstPlayer.Entry.TM_MLBId!.Value).GetFullDisplayName();
      var team = _teamApi.CreateFromPlayers(playerEntries.Select(p => p.Player.Id!.Value), teamName, EntitySourceType.Imported);
      var mlbPlayers = playerEntries.Where(p => p.Entry.TM_AAA != 1).Take(25);
      var aaaPlayers = playerEntries.Where(p => !mlbPlayers.Contains(p)).Take(15);

      var hasNoDHLineup = playerEntries.Any(p => p.Entry.TM_NoDHLineupSlot.HasValue || p.Entry.TM_NoDHLineupPosition.HasValue);
      var hasDHLineup = playerEntries.Any(p => p.Entry.TM_NoDHLineupSlot.HasValue || p.Entry.TM_DHLineupPosition.HasValue);

      var updateParams = new TeamParameters
      {
        Name = teamName,
        MLBPlayers = mlbPlayers.Select(p => CreatePlayerRoleEntry(team, p, hasNoDHLineup, hasDHLineup)).ToList(),
        AAAPlayers = aaaPlayers.Select(p => CreatePlayerRoleEntry(team, p, hasNoDHLineup, hasDHLineup)).ToList()
      };
      _teamApi.EditTeam(team, updateParams);
      return team;
    }

    private PlayerRoleParameters CreatePlayerRoleEntry(
      Team team,
      CreatedPlayerEntry entry,
      bool teamEntryHasNoDHLineup,
      bool teamEntryHasDHLineup
    )
    {
      var csvEntry = entry.Entry;
      var defaultRole = team.PlayerDefinitions.Single(p => p.PlayerId == entry.Player.Id);
      var playersByPitcherRole = team.PlayerDefinitions.Where(p => p.PitcherRole == defaultRole.PitcherRole);
      playersByPitcherRole.FirstOrDefault(p => p.PlayerId == entry.Player.Id, out var defaultRoleIndex);
      var defaultNoDHLineupSlot = team.NoDHLineup.FirstOrDefault(p => p.PlayerId == entry.Player.Id, out var defaultNoDHLineupIndex);
      var defaultDHLineupSlot = team.DHLineup.FirstOrDefault(p => p.PlayerId == entry.Player.Id, out var defaultDHLineupIndex);

      var entryHasPitcherRole = csvEntry.TM_PitcherRole.HasValue && csvEntry.TM_PitcherRoleSlot.HasValue;

      return new PlayerRoleParameters
      {
        PlayerId = entry.Player.Id!.Value,
        PitcherRole = entryHasPitcherRole
          ? (PitcherRole)csvEntry.TM_PitcherRole!.Value
          : defaultRole.PitcherRole,
        OrderInPitcherRole = entryHasPitcherRole
          ? csvEntry.TM_PitcherRoleSlot!.Value - 1
          : defaultRoleIndex ?? 0,
        OrderInNoDHLineup = teamEntryHasNoDHLineup
          ? csvEntry.TM_NoDHLineupSlot
          : defaultNoDHLineupIndex,
        PositionInNoDHLineup = teamEntryHasNoDHLineup
          ? (Position?)csvEntry.TM_NoDHLineupPosition
          : defaultNoDHLineupSlot?.Position,
        OrderInDHLineup = teamEntryHasDHLineup
          ? csvEntry.TM_DHLineupSlot
          : defaultDHLineupIndex,
        PositionInDHLineup = teamEntryHasDHLineup
          ? (Position?)csvEntry.TM_DHLineupPosition
          : defaultDHLineupSlot?.Position,
        IsPinchHitter = csvEntry.TM_PinchHitter.ToNullableBool() ?? defaultRole.IsPinchHitter,
        IsPinchRunner = csvEntry.TM_PinchRunner.ToNullableBool() ?? defaultRole.IsPinchRunner,
        IsDefensiveReplacement = csvEntry.TM_DefensiveReplacement.ToNullableBool() ?? defaultRole.IsDefensiveReplacement,
        IsDefensiveLiability = csvEntry.TM_DefensiveLiability.ToNullableBool() ?? defaultRole.IsDefensiveLiability,
      };
    }
  }
}
