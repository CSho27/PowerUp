namespace PowerUp.Entities.Players.Api
{
  public interface IPlayerApi
  {
    Player CreateDefaultPlayer(EntitySourceType sourceType, bool isPitcher);
    Player CreatePlayer(EntitySourceType sourceType, PlayerParameters parameters);
    Player CreateCustomCopyOfPlayer(Player player);
    void UpdatePlayer(Player player, PlayerParameters parameters);
  }

  public class PlayerApi : IPlayerApi
  {
    public Player CreateDefaultPlayer(EntitySourceType sourceType, bool isPitcher) => isPitcher
      ? PlayerFactory.BuildDefaultPitcher(sourceType)
      : PlayerFactory.BuildDefaultHitter(sourceType);

    public Player CreatePlayer(EntitySourceType sourceType, PlayerParameters parameters)
    {
      new PlayerParametersValidator().Validate(parameters);

      var player = CreateDefaultPlayer(sourceType, parameters.PersonalDetails!.Position == Position.Pitcher);

      UpdatePersonalDetails(player, parameters.PersonalDetails!);
      UpdateAppearance(player.Appearance, parameters.Appearance!);
      UpdatePositionCapabilities(player.PositonCapabilities, parameters.PositionCapabilities!);
      UpdateHitterAbilities(player.HitterAbilities, parameters.HitterAbilities!);
      UpdatePitcherAbilities(player.PitcherAbilities, parameters.PitcherAbilities!);
      UpdateSpecialAbilities(player.SpecialAbilities, parameters.SpecialAbilities!);

      return player;
    }

    public Player CreateCustomCopyOfPlayer(Player player)
    {
      var playerParameters = PlayerCopyParametersBuilder.Build(player);
      return CreatePlayer(EntitySourceType.Custom, playerParameters);
    }

    public void UpdatePlayer(Player player, PlayerParameters parameters)
    {
      new PlayerParametersValidator().Validate(parameters);

      UpdatePersonalDetails(player, parameters.PersonalDetails!);
      UpdateAppearance(player.Appearance, parameters.Appearance!);
      UpdatePositionCapabilities(player.PositonCapabilities, parameters.PositionCapabilities!);
      UpdateHitterAbilities(player.HitterAbilities, parameters.HitterAbilities!);
      UpdatePitcherAbilities(player.PitcherAbilities, parameters.PitcherAbilities!);
      UpdateSpecialAbilities(player.SpecialAbilities, parameters.SpecialAbilities!);
    }

    private void UpdatePersonalDetails(Player player, PlayerPersonalDetailsParameters parameters)
    {
      player.IsCustomPlayer = parameters.IsCustomPlayer;
      player.FirstName = parameters.FirstName!;
      player.LastName = parameters.LastName!;

      if (!parameters.KeepSpecialSavedName)
        player.SavedName = parameters.SavedName!;

      player.UniformNumber = parameters.UniformNumber!;
      player.PrimaryPosition = parameters.Position;
      player.PitcherType = parameters.PitcherType;
      player.VoiceId = parameters.VoiceId!.Value;
      player.BattingSide = parameters.BattingSide;
      player.BattingStanceId = parameters.BattingStanceId!.Value;
      player.ThrowingArm = parameters.ThrowingArm;
      player.PitchingMechanicsId = parameters.PitchingMechanicsId!.Value;
    }

    private void UpdatePositionCapabilities(PositionCapabilities positionCapabitlies, PlayerPositionCapabilitiesParameters parameters)
    {
      positionCapabitlies.Pitcher = parameters.Pitcher;
      positionCapabitlies.Catcher = parameters.Catcher;
      positionCapabitlies.FirstBase = parameters.FirstBase;
      positionCapabitlies.SecondBase = parameters.SecondBase;
      positionCapabitlies.ThirdBase = parameters.ThirdBase;
      positionCapabitlies.Shortstop = parameters.Shortstop;
      positionCapabitlies.LeftField = parameters.LeftField;
      positionCapabitlies.CenterField = parameters.CenterField;
      positionCapabitlies.RightField = parameters.RightField;
    }

    private void UpdateAppearance(Appearance appearance, PlayerAppearanceParameters parameters)
    {
      var faceType = FaceTypeHelpers.GetFaceType(parameters.FaceId);

      appearance.FaceId = parameters.FaceId;
      appearance.EyebrowThickness = FaceTypeHelpers.CanChooseEyebrows(faceType)
        ? parameters.EyebrowThickness
        : null;
      appearance.SkinColor = FaceTypeHelpers.CanChooseSkinColor(faceType)
        ? parameters.SkinColor
        : null;
      appearance.EyeColor = FaceTypeHelpers.CanChooseEyes(faceType)
        ? parameters.EyeColor
        : null;
      appearance.HairStyle = parameters.HairStyle;
      appearance.HairColor = parameters.HairStyle.HasValue
        ? parameters.HairColor
        : null;
      appearance.FacialHairStyle = parameters.FacialHairStyle;
      appearance.FacialHairColor = parameters.FacialHairStyle.HasValue
        ? parameters.FacialHairColor
        : null;
      appearance.BatColor = parameters.BatColor;
      appearance.GloveColor = parameters.GloveColor;
      appearance.EyewearType = parameters.EyewearType;
      appearance.EyewearFrameColor = parameters.EyewearType.HasValue && parameters.EyewearType != EyewearType.EyeBlack
        ? parameters.EyewearFrameColor
        : null;
      appearance.EyewearLensColor = parameters.EyewearType.HasValue && parameters.EyewearType != EyewearType.EyeBlack
        ? parameters.EyewearLensColor
        : null;
      appearance.EarringSide = parameters.EarringSide;
      appearance.EarringColor = parameters.EarringSide.HasValue
        ? parameters.EarringColor
        : null;
      appearance.RightWristbandColor = parameters.RightWristbandColor;
      appearance.LeftWristbandColor= parameters.LeftWristbandColor;
    }

    private void UpdateHitterAbilities(HitterAbilities hitterAbilities, PlayerHitterAbilityParameters parameters)
    {
      var hzParams = parameters.HotZoneGridParameters!;

      hitterAbilities.Trajectory = parameters.Trajectory;
      hitterAbilities.Contact = parameters.Contact;
      hitterAbilities.Power = parameters.Power;
      hitterAbilities.RunSpeed = parameters.RunSpeed;
      hitterAbilities.ArmStrength = parameters.ArmStrength;
      hitterAbilities.Fielding = parameters.Fielding;
      hitterAbilities.ErrorResistance = parameters.ErrorResistance;

      hitterAbilities.HotZones.UpAndIn = hzParams.UpAndIn;
      hitterAbilities.HotZones.Up = hzParams.Up;
      hitterAbilities.HotZones.UpAndAway = hzParams.UpAndAway;
      hitterAbilities.HotZones.MiddleIn = hzParams.MiddleIn;
      hitterAbilities.HotZones.Middle = hzParams.Middle;
      hitterAbilities.HotZones.MiddleAway = hzParams.MiddleAway;
      hitterAbilities.HotZones.DownAndIn = hzParams.DownAndIn;
      hitterAbilities.HotZones.Down = hzParams.Down;
      hitterAbilities.HotZones.DownAndAway = hzParams.DownAndAway;
    }

    private void UpdatePitcherAbilities(PitcherAbilities pitcherAbilities, PlayerPitcherAbilitiesParameters parameters)
    {
      pitcherAbilities.TopSpeedMph = parameters.TopSpeed;
      pitcherAbilities.Control = parameters.Control;
      pitcherAbilities.Stamina = parameters.Stamina;

      pitcherAbilities.HasTwoSeam = parameters.HasTwoSeam;
      pitcherAbilities.TwoSeamMovement = parameters.HasTwoSeam
        ? parameters.TwoSeamMovement
        : null;

      pitcherAbilities.Slider1Type = parameters.Slider1Type;
      pitcherAbilities.Slider1Movement = parameters.Slider1Type != null
        ? parameters.Slider1Movement
        : null;

      pitcherAbilities.Slider2Type = parameters.Slider2Type;
      pitcherAbilities.Slider2Movement = parameters.Slider2Type != null
        ? parameters.Slider2Movement
        : null;

      pitcherAbilities.Curve1Type = parameters.Curve1Type;
      pitcherAbilities.Curve1Movement = parameters.Curve1Type != null 
        ? parameters.Curve1Movement 
        : null;

      pitcherAbilities.Curve2Type = parameters.Curve2Type;
      pitcherAbilities.Curve2Movement = parameters.Curve2Type != null
        ? parameters.Curve2Movement
        : null;

      pitcherAbilities.Fork1Type = parameters.Fork1Type;
      pitcherAbilities.Fork1Movement = parameters.Fork1Type != null 
        ? parameters.Fork1Movement 
        : null;

      pitcherAbilities.Fork2Type = parameters.Fork2Type;
      pitcherAbilities.Fork2Movement = parameters.Fork2Type != null
        ? parameters.Fork2Movement
        : null;

      pitcherAbilities.Sinker1Type = parameters.Sinker1Type;
      pitcherAbilities.Sinker1Movement = parameters.Sinker1Type != null
        ? parameters.Sinker1Movement
        : null;

      pitcherAbilities.Sinker2Type = parameters.Sinker2Type;
      pitcherAbilities.Sinker2Movement = parameters.Sinker2Type != null
        ? parameters.Sinker2Movement
        : null;

      pitcherAbilities.SinkingFastball1Type = parameters.SinkingFastball1Type;
      pitcherAbilities.SinkingFastball1Movement = parameters.SinkingFastball1Type != null
        ? parameters.SinkingFastball1Movement
        : null;

      pitcherAbilities.SinkingFastball2Type = parameters.SinkingFastball2Type;
      pitcherAbilities.SinkingFastball2Movement = parameters.SinkingFastball2Type != null
        ? parameters.SinkingFastball2Movement
        : null;
    }

    private void UpdateSpecialAbilities(SpecialAbilities specialAbilities, SpecialAbilitiesParameters parameters)
    {
      UpdateGeneralSpecialAbilities(specialAbilities.General, parameters.General);
      UpdateHitterSpecialAbilities(specialAbilities.Hitter, parameters.Hitter);
      UpdatePitcherSpecialAbilities(specialAbilities.Pitcher, parameters.Pitcher);
    }

    private void UpdateGeneralSpecialAbilities(GeneralSpecialAbilities general, GeneralSpecialAbilitiesParameters parameters)
    {
      general.IsStar = parameters.IsStar;
      general.Durability = parameters.Durability;
      general.Morale = parameters.Morale;
    }

    private void UpdateHitterSpecialAbilities(HitterSpecialAbilities hitter, HitterSpecialAbilitiesParameters parameters)
    {
      UpdateSituationalHittingSpecialAbilities(hitter.SituationalHitting, parameters.SituationalHitting);
      UpdateHittingApproachSpecialAbilities(hitter.HittingApproach, parameters.HittingApproach);
      UpdateSmallBallSpecialAbilities(hitter.SmallBall, parameters.SmallBall);
      UpdateBaseRunningSpecialAbilities(hitter.BaseRunning, parameters.BaseRunning);
      UpdateFieldingSpecialAbilities(hitter.Fielding, parameters.Fielding);
    }

    private void UpdateSituationalHittingSpecialAbilities(SituationalHittingSpecialAbilities situational, SituationalHittingSpecialAbilitiesParameters parameters)
    {
      situational.Consistency = parameters.Consistency;
      situational.VersusLefty = parameters.VersusLefty;
      situational.IsTableSetter = parameters.IsTableSetter;
      situational.IsBackToBackHitter = parameters.IsBackToBackHitter;
      situational.IsHotHitter = parameters.IsHotHitter;
      situational.IsRallyHitter = parameters.IsRallyHitter;
      situational.IsGoodPinchHitter = parameters.IsGoodPinchHitter;
      situational.BasesLoadedHitter = parameters.BasesLoadedHitter;
      situational.WalkOffHitter = parameters.WalkOffHitter;
      situational.ClutchHitter = parameters.ClutchHitter;
    }

    private void UpdateHittingApproachSpecialAbilities(HittingApproachSpecialAbilities approach, HittingApproachSpecialAbilitiesParameters parameters)
    {
      approach.IsContactHitter = parameters.IsContactHitter;
      approach.IsPowerHitter = parameters.IsPowerHitter;
      approach.SluggerOrSlapHitter = parameters.SluggerOrSlapHitter;
      approach.IsPushHitter = parameters.IsPushHitter;
      approach.IsPullHitter = parameters.IsPullHitter;
      approach.IsSprayHitter = parameters.IsSprayHitter;
      approach.IsFirstballHitter = parameters.IsFirstballHitter;
      approach.AggressiveOrPatientHitter = parameters.AggressiveOrPatientHitter;
      approach.IsRefinedHitter = parameters.IsRefinedHitter;
      approach.IsToughOut = parameters.IsToughOut;
      approach.IsIntimidator = parameters.IsIntimidator;
      approach.IsSparkplug = parameters.IsSparkplug;
    }

    private void UpdateSmallBallSpecialAbilities(SmallBallSpecialAbilities smallBall, SmallBallSpecialAbilitiesParameters parameters)
    {
      smallBall.SmallBall = parameters.SmallBall;
      smallBall.Bunting = parameters.Bunting;
      smallBall.InfieldHitting = parameters.InfieldHitting;
    }

    private void UpdateBaseRunningSpecialAbilities(BaseRunningSpecialAbilities baseRunning, BaseRunningSpecialAbilitiesParameters parameters)
    {
      baseRunning.BaseRunning = parameters.BaseRunning;
      baseRunning.Stealing = parameters.Stealing;
      baseRunning.IsAggressiveRunner = parameters.IsAggressiveRunner;
      baseRunning.AggressiveOrCautiousBaseStealer = parameters.AggressiveOrCautiousBaseStealer;
      baseRunning.IsToughRunner = parameters.IsToughRunner;
      baseRunning.WillBreakupDoublePlay = parameters.WillBreakupDoublePlay;
      baseRunning.WillSlideHeadFirst = parameters.WillSlideHeadFirst;
    }

    private void UpdateFieldingSpecialAbilities(FieldingSpecialAbilities fielding, FieldingSpecialAbilitiesParameters parameters)
    {
      fielding.IsGoldGlover = parameters.IsGoldGlover;
      fielding.CanSpiderCatch = parameters.CanSpiderCatch;
      fielding.CanBarehandCatch = parameters.CanBarehandCatch;
      fielding.IsAggressiveFielder = parameters.IsAggressiveFielder;
      fielding.IsPivotMan = parameters.IsPivotMan;
      fielding.IsErrorProne = parameters.IsErrorProne;
      fielding.IsGoodBlocker = parameters.IsGoodBlocker;
      fielding.Catching = parameters.Catching;
      fielding.Throwing = parameters.Throwing;
      fielding.HasCannonArm = parameters.HasCannonArm;
      fielding.IsTrashTalker = parameters.IsTrashTalker;
    }

    private void UpdatePitcherSpecialAbilities(PitcherSpecialAbilities pitcher, PitcherSpecialAbilitiesParameters parameters)
    {
      UpdateSituationalPitchingAbilities(pitcher.SituationalPitching, parameters.SituationalPitching);
      UpdatePitchingDemeanorSpecialAbilities(pitcher.Demeanor, parameters.Demeanor);
      UpdatePitchingMechanicsSpecialAbilities(pitcher.PitchingMechanics, parameters.PitchingMechanics);
      UpdatePitchQualitiesSpecialAbilities(pitcher.PitchQuailities, parameters.PitchQuailities);
    } 

    private void UpdateSituationalPitchingAbilities(SituationalPitchingSpecialAbilities situational, SituationalPitchingSpecialAbilitiesParameters parameters)
    {
      situational.Consistency = parameters.Consistency;
      situational.VersusLefty = parameters.VersusLefty;
      situational.Poise = parameters.Poise;
      situational.PoorVersusRunner = parameters.PoorVersusRunner;
      situational.WithRunnersInSocringPosition = parameters.WithRunnersInSocringPosition;
      situational.IsSlowStarter = parameters.IsSlowStarter;
      situational.IsStarterFinisher = parameters.IsStarterFinisher;
      situational.IsChokeArtist = parameters.IsChokeArtist;
      situational.IsSandbag = parameters.IsSandbag;
      situational.DoctorK = parameters.DoctorK;
      situational.IsWalkProne = parameters.IsWalkProne;
      situational.Luck = parameters.Luck;
      situational.Recovery = parameters.Recovery;
    }
    
    private void UpdatePitchingDemeanorSpecialAbilities(PitchingDemeanorSpecialAbilities demeanor, PitchingDemeanorSpecialAbilitiesParameters parameters)
    {
      demeanor.IsIntimidator = parameters.IsIntimidator;
      demeanor.BattlerPokerFace = parameters.BattlerPokerFace;
      demeanor.IsHotHead = parameters.IsHotHead;
    }

    private void UpdatePitchingMechanicsSpecialAbilities(PitchingMechanicsSpecialAbilities mechanics, PitchingMechanicsSpecialAbilitiesParameters parameters)
    {
      mechanics.GoodDelivery = parameters.GoodDelivery;
      mechanics.Release = parameters.Release;
      mechanics.GoodPace = parameters.GoodPace;
      mechanics.GoodReflexes = parameters.GoodReflexes;
      mechanics.GoodPickoff = parameters.GoodPickoff;
    }

    private void UpdatePitchQualitiesSpecialAbilities(PitchQualitiesSpecialAbilities pitchQualities, PitchQualitiesSpecialAbilitiesParameters parameters)
    {
      pitchQualities.PowerOrBreakingBallPitcher = parameters.PowerOrBreakingBallPitcher;
      pitchQualities.FastballLife = parameters.FastballLife;
      pitchQualities.Spin = parameters.Spin;
      pitchQualities.SafeOrFatPitch = parameters.SafeOrFatPitch;
      pitchQualities.GroundBallOrFlyBallPitcher = parameters.GroundBallOrFlyBallPitcher;
      pitchQualities.GoodLowPitch = parameters.GoodLowPitch;
      pitchQualities.Gyroball = parameters.Gyroball;
      pitchQualities.ShuttoSpin = parameters.ShuttoSpin;
    }
  }
}
