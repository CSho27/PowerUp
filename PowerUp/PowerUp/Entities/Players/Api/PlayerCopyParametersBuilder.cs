namespace PowerUp.Entities.Players.Api
{
  public static class PlayerCopyParametersBuilder
  {
    public static PlayerParameters Build(Player player)
    {
      var apperance = player.Appearance;
      var positionCapabilities = player.PositionCapabilities;
      var hitterAbilities = player.HitterAbilities;
      var pitcherAbilities = player.PitcherAbilities;

      var generalSpecialAbilities = player.SpecialAbilities.General;
      
      var situationalHittingSpecialAbilities = player.SpecialAbilities.Hitter.SituationalHitting;
      var hittingApproachSpecialAbilities = player.SpecialAbilities.Hitter.HittingApproach;
      var smallBallSpecialAbilities = player.SpecialAbilities.Hitter.SmallBall;
      var baseRunningSpecialAbilities = player.SpecialAbilities.Hitter.BaseRunning;
      var fieldingSpecialAbilities = player.SpecialAbilities.Hitter.Fielding;

      var situationalPitchingSpecialAbilities = player.SpecialAbilities.Pitcher.SituationalPitching;
      var pitchingDemeanorSpecialAbilities = player.SpecialAbilities.Pitcher.Demeanor;
      var pitchingMechanicsSpecialAbilities = player.SpecialAbilities.Pitcher.PitchingMechanics;
      var pitchQualitiesSpecialAbilities = player.SpecialAbilities.Pitcher.PitchQuailities;

      return new PlayerParameters
      {
        PersonalDetails = new PlayerPersonalDetailsParameters
        {
          IsCustomPlayer = false,
          FirstName = player.FirstName,
          LastName = player.LastName,
          KeepSpecialSavedName = player.SpecialSavedNameId.HasValue,
          SavedName = player.SpecialSavedNameId.HasValue
            ? null
            : player.SavedName,
          UniformNumber = player.UniformNumber,
          Position = player.PrimaryPosition,
          PitcherType = player.PitcherType,
          VoiceId = player.VoiceId,
          BattingSide = player.BattingSide,
          BattingStanceId = player.BattingStanceId,
          ThrowingArm = player.ThrowingArm,
          PitchingMechanicsId = player.PitchingMechanicsId,
        },
        Appearance = new PlayerAppearanceParameters
        {
          FaceId = apperance.FaceId,
          EyebrowThickness = apperance.EyebrowThickness,
          SkinColor = apperance.SkinColor,
          EyeColor = apperance.EyeColor,
          HairStyle = apperance.HairStyle,
          HairColor = apperance.HairColor,
          FacialHairStyle = apperance.FacialHairStyle,
          FacialHairColor = apperance.FacialHairColor,
          BatColor = apperance.BatColor,
          GloveColor = apperance.GloveColor,
          EyewearType = apperance.EyewearType,
          EyewearFrameColor = apperance.EyewearFrameColor,
          EyewearLensColor = apperance.EyewearLensColor,
          EarringSide = apperance.EarringSide,
          EarringColor = apperance.EarringColor,
          RightWristbandColor = apperance.RightWristbandColor,
          LeftWristbandColor = apperance.LeftWristbandColor
        },
        PositionCapabilities = new PlayerPositionCapabilitiesParameters 
        {
          Pitcher = positionCapabilities.Pitcher,
          Catcher = positionCapabilities.Catcher,
          FirstBase = positionCapabilities.FirstBase,
          SecondBase = positionCapabilities.SecondBase,
          ThirdBase = positionCapabilities.ThirdBase,
          Shortstop = positionCapabilities.Shortstop,
          LeftField = positionCapabilities.LeftField,
          CenterField = positionCapabilities.CenterField,
          RightField = positionCapabilities.RightField
        },
        HitterAbilities = new PlayerHitterAbilityParameters
        {
          Trajectory = hitterAbilities.Trajectory,
          Contact = hitterAbilities.Contact,
          Power = hitterAbilities.Power,
          RunSpeed = hitterAbilities.RunSpeed,
          ArmStrength = hitterAbilities.ArmStrength,
          Fielding = hitterAbilities.Fielding,
          ErrorResistance = hitterAbilities.ErrorResistance,
          HotZoneGridParameters = new HotZoneGridParameters
          {
            UpAndIn = hitterAbilities.HotZones.UpAndIn,
            Up = hitterAbilities.HotZones.Up,
            UpAndAway = hitterAbilities.HotZones.UpAndAway,
            MiddleIn = hitterAbilities.HotZones.MiddleIn,
            Middle = hitterAbilities.HotZones.Middle,
            MiddleAway = hitterAbilities.HotZones.MiddleAway,
            DownAndIn = hitterAbilities.HotZones.DownAndIn,
            Down = hitterAbilities.HotZones.Down,
            DownAndAway = hitterAbilities.HotZones.DownAndAway
          }
        },
        PitcherAbilities = new PlayerPitcherAbilitiesParameters
        {
          TopSpeed = (int)pitcherAbilities.TopSpeedMph,
          Control = pitcherAbilities.Control,
          Stamina = pitcherAbilities.Stamina,
          HasTwoSeam = pitcherAbilities.HasTwoSeam,
          TwoSeamMovement = pitcherAbilities.TwoSeamMovement,
          Slider1Type = pitcherAbilities.Slider1Type,
          Slider1Movement = pitcherAbilities.Slider1Movement,
          Slider2Type = pitcherAbilities.Slider2Type,
          Slider2Movement = pitcherAbilities.Slider2Movement,
          Curve1Type = pitcherAbilities.Curve1Type,
          Curve1Movement = pitcherAbilities.Curve1Movement,
          Curve2Type = pitcherAbilities.Curve2Type,
          Curve2Movement = pitcherAbilities.Curve2Movement,
          Fork1Type = pitcherAbilities.Fork1Type,
          Fork1Movement = pitcherAbilities.Fork1Movement,
          Fork2Type = pitcherAbilities.Fork2Type,
          Fork2Movement = pitcherAbilities.Fork2Movement,
          Sinker1Type = pitcherAbilities.Sinker1Type,
          Sinker1Movement = pitcherAbilities.Sinker1Movement,
          Sinker2Type = pitcherAbilities.Sinker2Type,
          Sinker2Movement = pitcherAbilities.Sinker2Movement,
          SinkingFastball1Type = pitcherAbilities.SinkingFastball1Type,
          SinkingFastball1Movement = pitcherAbilities.SinkingFastball1Movement,
          SinkingFastball2Type = pitcherAbilities.SinkingFastball2Type,
          SinkingFastball2Movement = pitcherAbilities.SinkingFastball2Movement
        },
        SpecialAbilities = new SpecialAbilitiesParameters
        {
          General = new GeneralSpecialAbilitiesParameters
          {
            IsStar = generalSpecialAbilities.IsStar,
            Durability = generalSpecialAbilities.Durability,
            Morale = generalSpecialAbilities.Morale
          },
          Hitter = new HitterSpecialAbilitiesParameters
          {
            SituationalHitting = new SituationalHittingSpecialAbilitiesParameters
            {
              Consistency = situationalHittingSpecialAbilities.Consistency,
              VersusLefty = situationalHittingSpecialAbilities.VersusLefty,
              IsTableSetter = situationalHittingSpecialAbilities.IsTableSetter,
              IsBackToBackHitter = situationalHittingSpecialAbilities.IsBackToBackHitter,
              IsHotHitter = situationalHittingSpecialAbilities.IsHotHitter,
              IsRallyHitter = situationalHittingSpecialAbilities.IsRallyHitter,
              IsGoodPinchHitter = situationalHittingSpecialAbilities.IsGoodPinchHitter,
              BasesLoadedHitter = situationalHittingSpecialAbilities.BasesLoadedHitter,
              WalkOffHitter = situationalHittingSpecialAbilities.WalkOffHitter,
              ClutchHitter = situationalHittingSpecialAbilities.ClutchHitter
            },
            HittingApproach = new HittingApproachSpecialAbilitiesParameters 
            { 
              IsContactHitter = hittingApproachSpecialAbilities.IsContactHitter,
              IsPowerHitter = hittingApproachSpecialAbilities.IsPowerHitter,
              SluggerOrSlapHitter = hittingApproachSpecialAbilities.SluggerOrSlapHitter,
              IsPushHitter = hittingApproachSpecialAbilities.IsPushHitter,
              IsPullHitter = hittingApproachSpecialAbilities.IsPullHitter,
              IsSprayHitter = hittingApproachSpecialAbilities.IsSprayHitter,
              IsFirstballHitter = hittingApproachSpecialAbilities.IsFirstballHitter,
              AggressiveOrPatientHitter = hittingApproachSpecialAbilities.AggressiveOrPatientHitter,
              IsRefinedHitter = hittingApproachSpecialAbilities.IsRefinedHitter,
              IsToughOut = hittingApproachSpecialAbilities.IsToughOut,
              IsIntimidator = hittingApproachSpecialAbilities.IsIntimidator,
              IsSparkplug = hittingApproachSpecialAbilities.IsSparkplug
            },
            SmallBall = new SmallBallSpecialAbilitiesParameters
            {
              SmallBall = smallBallSpecialAbilities.SmallBall,
              Bunting = smallBallSpecialAbilities.Bunting,
              InfieldHitting = smallBallSpecialAbilities.InfieldHitting
            },
            BaseRunning = new BaseRunningSpecialAbilitiesParameters
            {
              BaseRunning = baseRunningSpecialAbilities.BaseRunning,
              Stealing = baseRunningSpecialAbilities.Stealing,
              IsAggressiveRunner = baseRunningSpecialAbilities.IsAggressiveRunner,
              AggressiveOrCautiousBaseStealer = baseRunningSpecialAbilities.AggressiveOrCautiousBaseStealer,
              IsToughRunner = baseRunningSpecialAbilities.IsToughRunner,
              WillBreakupDoublePlay = baseRunningSpecialAbilities.WillBreakupDoublePlay,
              WillSlideHeadFirst = baseRunningSpecialAbilities.WillSlideHeadFirst
            },
            Fielding = new FieldingSpecialAbilitiesParameters
            {
              IsGoldGlover = fieldingSpecialAbilities.IsGoldGlover,
              CanSpiderCatch = fieldingSpecialAbilities.CanSpiderCatch,
              CanBarehandCatch = fieldingSpecialAbilities.CanBarehandCatch,
              IsAggressiveFielder = fieldingSpecialAbilities.IsAggressiveFielder,
              IsPivotMan = fieldingSpecialAbilities.IsPivotMan,
              IsErrorProne = fieldingSpecialAbilities.IsErrorProne,
              IsGoodBlocker = fieldingSpecialAbilities.IsGoodBlocker,
              Catching = fieldingSpecialAbilities.Catching,
              Throwing = fieldingSpecialAbilities.Throwing,
              HasCannonArm = fieldingSpecialAbilities.HasCannonArm,
              IsTrashTalker = fieldingSpecialAbilities.IsTrashTalker
            }
          },
          Pitcher = new PitcherSpecialAbilitiesParameters
          {
            SituationalPitching = new SituationalPitchingSpecialAbilitiesParameters
            {
              Consistency = situationalPitchingSpecialAbilities.Consistency,
              VersusLefty = situationalPitchingSpecialAbilities.VersusLefty,
              Poise = situationalPitchingSpecialAbilities.Poise,
              PoorVersusRunner = situationalPitchingSpecialAbilities.PoorVersusRunner,
              WithRunnersInSocringPosition = situationalPitchingSpecialAbilities.WithRunnersInSocringPosition,
              IsSlowStarter = situationalPitchingSpecialAbilities.IsSlowStarter,
              IsStarterFinisher = situationalPitchingSpecialAbilities.IsStarterFinisher,
              IsChokeArtist = situationalPitchingSpecialAbilities.IsChokeArtist,
              IsSandbag = situationalPitchingSpecialAbilities.IsSandbag,
              DoctorK = situationalPitchingSpecialAbilities.DoctorK,
              IsWalkProne = situationalPitchingSpecialAbilities.IsWalkProne,
              Luck = situationalPitchingSpecialAbilities.Luck,
              Recovery = situationalPitchingSpecialAbilities.Recovery
            },
            Demeanor = new PitchingDemeanorSpecialAbilitiesParameters
            {
              IsIntimidator = pitchingDemeanorSpecialAbilities.IsIntimidator,
              BattlerPokerFace = pitchingDemeanorSpecialAbilities.BattlerPokerFace,
              IsHotHead = pitchingDemeanorSpecialAbilities.IsHotHead
            },
            PitchingMechanics = new PitchingMechanicsSpecialAbilitiesParameters
            {
              GoodDelivery = pitchingMechanicsSpecialAbilities.GoodDelivery,
              Release = pitchingMechanicsSpecialAbilities.Release,
              GoodPace = pitchingMechanicsSpecialAbilities.GoodPace,
              GoodReflexes = pitchingMechanicsSpecialAbilities.GoodReflexes,
              GoodPickoff = pitchingMechanicsSpecialAbilities.GoodPickoff
            },
            PitchQuailities = new PitchQualitiesSpecialAbilitiesParameters
            {
              PowerOrBreakingBallPitcher = pitchQualitiesSpecialAbilities.PowerOrBreakingBallPitcher,
              FastballLife = pitchQualitiesSpecialAbilities.FastballLife,
              Spin = pitchQualitiesSpecialAbilities.Spin,
              SafeOrFatPitch = pitchQualitiesSpecialAbilities.SafeOrFatPitch,
              GroundBallOrFlyBallPitcher = pitchQualitiesSpecialAbilities.GroundBallOrFlyBallPitcher,
              GoodLowPitch = pitchQualitiesSpecialAbilities.GoodLowPitch,
              Gyroball = pitchQualitiesSpecialAbilities.Gyroball,
              ShuttoSpin = pitchQualitiesSpecialAbilities.ShuttoSpin
            }
          }
        }
      };
    }
  }
}
