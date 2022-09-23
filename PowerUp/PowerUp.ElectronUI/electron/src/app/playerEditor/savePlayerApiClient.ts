import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";
import { HotZoneGridDto } from "./hotZoneGridDto";

export interface SavePlayerRequest {
  playerId: number;
  personalDetails: PersonalDetailsRequest;
  appearance: AppearanceRequest;
  positionCapabilities: PositionCapabilitiesRequest;
  hitterAbilities: HitterAbilitiesSaveRequest;
  pitcherAbilities: PitcherAbilitiesSaveRequest;
  specialAbilities: SpecialAbilitiesRequest;
}

export interface PersonalDetailsRequest {
  isCustomPlayer: boolean;
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  positionKey: string;
  pitcherTypeKey: string;
  voiceId: number;
  battingSideKey: string;
  battingStanceId: number;
  throwingArmKey: string;
  pitchingMechanicsId: number;
  battingAverage: number | null;
  runsBattedIn: number | null;
  homeRuns: number | null;
  earnedRunAverage: number | null;
}

export interface AppearanceRequest {
  faceId: number;
  eyebrowThicknessKey: string | null;
  skinColorKey: string | null;
  eyeColorKey: string | null;
  hairStyleKey: string | null;
  hairColorKey: string | null;
  facialHairStyleKey: string | null;
  facialHairColorKey: string | null;
  batColorKey: string | null;
  gloveColorKey: string | null;
  eyewearTypeKey: string | null;
  eyewearFrameColorKey: string | null;
  eyewearLensColorKey: string | null;
  earringSideKey: string | null;
  earringColorKey: string | null;
  rightWristbandColorKey: string | null;
  leftWristbandColorKey: string | null;
}

export interface PositionCapabilitiesRequest {
  pitcher: string;
  catcher: string;
  firstBase: string;
  secondBase: string;
  thirdBase: string;
  shortstop: string;
  leftField: string;
  centerField: string;
  rightField: string;
}

export interface HitterAbilitiesSaveRequest {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
  hotZoneGrid: HotZoneGridDto;
}

export interface PitcherAbilitiesSaveRequest {
  topSpeed: number;
  control: number;
  stamina: number;
  
  twoSeamTypeKey: string | null;
  twoSeamMovement: number | null;
  
  slider1TypeKey: string | null;
  slider1Movement: number | null;
  
  slider2TypeKey: string | null;
  slider2Movement:  number | null;

  curve1TypeKey: string | null;
  curve1Movement: number | null;

  curve2TypeKey: string | null;
  curve2Movement: number | null;

  fork1TypeKey: string | null;
  fork1Movement: number | null;

  fork2TypeKey: string | null;
  fork2Movement: number | null;

  sinker1TypeKey: string | null;
  sinker1Movement: number | null;
  
  sinker2TypeKey: string | null;
  sinker2Movement: number | null;

  sinkingFastball1TypeKey: string | null;
  sinkingFastball1Movement: number | null;
  
  sinkingFastball2TypeKey: string | null;
  sinkingFastball2Movement: number | null;
}

export interface SpecialAbilitiesRequest {
  generalSpecialAbilitiesRequest: GeneralSpecialAbilitiesRequest;
  hitterSpecialAbilitiesRequest: HitterSpecialAbilitiesRequest;
  pitcherSpecialAbilitiesRequest: PitcherSpecialAbilitiesRequest;
}

export interface GeneralSpecialAbilitiesRequest {
  isStar: boolean;
  durabilityKey: string; 
  moraleKey: string;
  dayGameAbilityKey: string;
  inRainAbilityKey: string;
}

export interface HitterSpecialAbilitiesRequest {
  situational: SituationalHittingSpecialAbilitiesRequest;
  approach: HittingApproachSpecialAbilitiesRequest;
  smallBall: SmallBallSpecialAbilitiesRequest;
  baseRunning: BaseRunningSpecialAbilitiesRequest;
  fielding: FieldingSpecialAbilitiesRequest;
}

export interface SituationalHittingSpecialAbilitiesRequest {
  consistencyKey: string;
  versusLeftyKey: string;
  isTableSetter: boolean;
  isBackToBackHitter: boolean;
  isHotHitter: boolean;
  isRallyHitter: boolean;
  isGoodPinchHitter: boolean;
  basesLoadedHitterKey: string | null;
  walkOffHitterKey: string | null;
  clutchHitterKey: string;
}

export interface HittingApproachSpecialAbilitiesRequest {
  isContactHitter: boolean;
  isPowerHitter: boolean;
  sluggerOrSlapHitterKey: string | null;
  isPushHitter: boolean;
  isPullHitter: boolean;
  isSprayHitter: boolean;
  isFirstballHitter: boolean;
  aggressiveOrPatientHitterKey: string | null;
  isRefinedHitter: boolean;
  isFreeSwinger: boolean;
  isToughOut: boolean;
  isIntimidator: boolean;
  isSparkplug: boolean;
}

export interface SmallBallSpecialAbilitiesRequest {
  smallBallKey: string;
  buntingKey: string | null;
  infieldHittingKey: string | null;
}

export interface BaseRunningSpecialAbilitiesRequest {
  baseRunningKey: string;
  stealingKey: string;
  isAggressiveRunner: boolean;
  aggressiveOrCautiousBaseStealerKey: string | null;
  isToughRunner: boolean;
  willBreakupDoublePlay: boolean;
  willSlideHeadFirst: boolean;
}

export interface FieldingSpecialAbilitiesRequest {
  isGoldGlover: boolean;
  canSpiderCatch: boolean;
  canBarehandCatch: boolean;
  isAggressiveFielder: boolean;
  isPivotMan: boolean;
  isErrorProne: boolean;
  isGoodBlocker: boolean;
  catchingKey: string | null;
  throwingKey: string;
  hasCannonArm: boolean;
  isTrashTalker: boolean;
}

export interface PitcherSpecialAbilitiesRequest {
  situational: SituationalPitchingSpecialAbilitiesRequest; 
  demeanor: PitchingDemeanorSpecialAbilitiesRequest; 
  mechanics: PitchingMechanicsSpecialAbilitiesRequest; 
  pitchQualities: PitchQualitiesSpecialAbilitiesRequest; 
}

export interface SituationalPitchingSpecialAbilitiesRequest {
  consistencyKey: string;
  versusLeftyKey: string;
  poiseKey: string;
  poorVersusRunner: boolean;
  withRunnersInScoringPositionKey: string;
  isSlowStarter: boolean;
  isStarterFinisher: boolean;
  isChokeArtist: boolean;
  isSandbag: boolean;
  doctorK: boolean;
  isWalkProne: boolean;
  luckKey: string;
  recoveryKey: string;
}

export interface PitchingDemeanorSpecialAbilitiesRequest {
  isIntimidator: boolean;
  battlerPokerFaceKey: string | null;
  isHotHead: boolean;
}

export interface PitchingMechanicsSpecialAbilitiesRequest {
  goodDelivery: boolean;
  releaseKey: string;
  goodPace: boolean;
  goodReflexes: boolean;
  goodPickoff: boolean;
}

export interface PitchQualitiesSpecialAbilitiesRequest {
  powerOrBreakingBallPitcherKey: string | null;
  fastballLifeKey: string;
  spinKey: string;
  safeOrFatPitchKey: string;
  groundBallOrFlyBallPitcherKey: string;
  goodLowPitch: boolean;
  gyroball: boolean;
  shuttoSpin: boolean;
}

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: SavePlayerRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}