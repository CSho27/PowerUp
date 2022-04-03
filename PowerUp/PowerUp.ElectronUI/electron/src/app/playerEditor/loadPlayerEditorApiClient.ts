import { CommandFetcher } from "../../utils/commandFetcher";
import { KeyedCode } from "../shared/keyedCode";
import { PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode";
import { HotZoneGridDto } from "./hotZoneGridDto";

export interface LoadPlayerEditorRequest {
  playerId: number;
}

export interface PlayerEditorResponse {
  options: PlayerEditorOptions; 
  personalDetails: PlayerPersonalDetailsDto;
  positionCapabilityDetails: PositionCapabilityDetailsDto;
  hitterAbilityDetails: HitterAbilityDetailsDto;
  pitcherAbilityDetails: PitcherAbilityDetailsDto;
  specialAbilityDetails: SpecialAbilitiesDetailsDto;
}

export interface PlayerEditorOptions {
  personalDetailsOptions: PersonalDetailsOptions;  
  positionCapabilityOptions: KeyedCode[];
  pitcherAbilitiesOptions: PitcherAbilitiesOptions;
  specialAbilitiesOptions: SpecialAbilitiesOptions;
}

export interface PersonalDetailsOptions {
  voiceOptions: SimpleCode[];
  positions: PositionCode[];
  pitcherTypes: KeyedCode[];
  battingSideOptions: KeyedCode[];
  battingStanceOptions: SimpleCode[];
  throwingArmOptions: KeyedCode[];
  pitchingMechanicsOptions: SimpleCode[];
}

export interface PitcherAbilitiesOptions {
  twoSeamOptions: KeyedCode[];
  sliderOptions: KeyedCode[];
  curveOptions: KeyedCode[];
  forkOptions: KeyedCode[];
  sinkerOptions: KeyedCode[];
  sinkingFastballOptions: KeyedCode[];
}

export interface SpecialAbilitiesOptions {
  special1_5Options: KeyedCode[];
  special2_4Options: KeyedCode[];
  specialPositive_NegativeOptions: KeyedCode[];
  basesLoadedHitterOptions: KeyedCode[];
  walkOffHitterOptions: KeyedCode[];
  sluggerOrSlapHitterOptions: KeyedCode[];
  aggressiveOrPatientHitterOptions: KeyedCode[];
  aggressiveOrCautiousBaseStealerOptions: KeyedCode[];
  buntingAbilityOptions: KeyedCode[];
  infieldHittingAbilityOptions: KeyedCode[];
  catchingAbilityOptions: KeyedCode[];
  battlerPokerFaceOptions: KeyedCode[];
  powerOrBreakingBallPitcher: KeyedCode[];
}

export interface PlayerPersonalDetailsDto {
  firstName: string;
  lastName: string;
  isSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  position: PositionCode;
  pitcherType: KeyedCode;
  voice: SimpleCode;
  battingSide: KeyedCode;
  battingStance: SimpleCode;
  throwingArm: KeyedCode;
  pitchingMechanics: SimpleCode;
}

export interface PositionCapabilityDetailsDto {
  pitcher: KeyedCode;
  catcher: KeyedCode;
  firstBase: KeyedCode;
  secondBase: KeyedCode;
  thirdBase: KeyedCode;
  shortstop: KeyedCode;
  leftField: KeyedCode;
  centerField: KeyedCode;
  rightField: KeyedCode;
}

export interface HitterAbilityDetailsDto {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
  hotZones: HotZoneGridDto;
}

export interface PitcherAbilityDetailsDto {
  topSpeed: number;
  control: number;
  stamina: number;

  twoSeamType: KeyedCode | null;
  twoSeamMovement: number | null;

  slider1Type: KeyedCode | null;
  slider1Movement: number | null;

  slider2Type: KeyedCode | null;
  slider2Movement: number | null;

  curve1Type: KeyedCode | null;
  curve1Movement: number | null;

  curve2Type: KeyedCode | null;
  curve2Movement: number | null;

  fork1Type: KeyedCode | null;
  fork1Movement: number | null;

  fork2Type: KeyedCode | null;
  fork2Movement: number | null;

  sinker1Type: KeyedCode | null;
  sinker1Movement: number | null;

  sinker2Type: KeyedCode | null;
  sinker2Movement: number | null;

  sinkingFastball1Type: KeyedCode | null;
  sinkingFastball1Movement: number | null;

  sinkingFastball2Type: KeyedCode | null;
  sinkingFastball2Movement: number | null;
}

export interface SpecialAbilitiesDetailsDto {
  general: GeneralSpecialAbilitiesDetailsDto;
  hitter: HitterSpecialAblitiesDetailsDto;
  pitcher: PitcherSpecialAbilitiesDetailsDto;
}

export interface GeneralSpecialAbilitiesDetailsDto {
  isStar: boolean;
  durability: KeyedCode;
  morale: KeyedCode;
}

export interface HitterSpecialAblitiesDetailsDto {
  situational: SituationalHittingSpecialAbilitiesDetailsDto;
  approach: HittingApproachSpecialAbilitiesDetailsDto;
  smallBall: SmallBallSpecialAbilitiesDetailsDto;
  baseRunning: BaseRunningSpecialAbilitiesDetailsDto;
  fielding: FieldingSpecialAbilitiesDetailsDto;
}

export interface SituationalHittingSpecialAbilitiesDetailsDto {
  hittingConsistency: KeyedCode;
  versusLefty: KeyedCode;
  isTableSetter: boolean;
  isBackToBackHitter: boolean;
  isHotHitter: boolean;
  isRallyHitter: boolean;
  isGoodPinchHitter: boolean;
  basesLoadedHitter: KeyedCode | null;
  walkOffHitter: KeyedCode | null;
  clutchHitter: KeyedCode;
}

export interface HittingApproachSpecialAbilitiesDetailsDto {
  isContactHitter: boolean;
  isPowerHitter: boolean;
  sluggerOrSlapHitter: KeyedCode | null;
  isPushHitter: boolean;
  isPullHitter: boolean;
  isSprayHitter: boolean;
  isFirstballHitter: boolean;
  aggressiveOrPatientHitter: KeyedCode | null;
  isRefinedHitter: boolean;
  isToughOut: boolean;
  isIntimidatingHitter: boolean;
  isSparkplug: boolean;
}

export interface SmallBallSpecialAbilitiesDetailsDto {
  smallBall: KeyedCode;
  bunting: KeyedCode | null;
  infieldHitter: KeyedCode | null;
}

export interface BaseRunningSpecialAbilitiesDetailsDto {
  baseRunning: KeyedCode;
  stealing: KeyedCode;
  isAggressiveRunner: boolean;
  aggressiveOrPatientBaseStealer: KeyedCode | null;
  isToughRunner: boolean;
  willBreakupDoublePlay: boolean;
  willSlideHeadFirst: boolean;
}

export interface FieldingSpecialAbilitiesDetailsDto {
  isGoldGlover: boolean;
  canSpiderCatch: boolean;
  canBarehandCatch: boolean;
  isAggressiveFielder: boolean;
  isPivotMan: boolean;
  isErrorProne: boolean;
  isGoodBlocker: boolean;
  catching: KeyedCode | null;
  throwing: KeyedCode;
  hasCannonArm: boolean;
  isTrashTalker: boolean;
}

export interface PitcherSpecialAbilitiesDetailsDto {
  situational: SituationalPitchingSpecialAbilitiesDetailsDto;
  demeanor: PitchingDemeanorSpecialAbilitiesDetailsDto;
  mechanics: PitchingMechanicsSpecialAbilitiesDetailsDto;
  pitchQualities: PitchQualitiesSpecialAbilitiesDetailsDto;
}

export interface SituationalPitchingSpecialAbilitiesDetailsDto {
  pitchingConsistency: KeyedCode;
  pitchingVersusLefty: KeyedCode;
  poise: KeyedCode;
  poorVersusRunner: boolean;
  withRunnersInScoringPosition: KeyedCode;
  isSlowStarter: boolean;
  isStarterFinisher: boolean;
  isChokeArtist: boolean;
  isSandbag: boolean
  doctorK: boolean
  isWalkProne: boolean;
  luck: KeyedCode
  recovery: KeyedCode
}

export interface PitchingDemeanorSpecialAbilitiesDetailsDto {
  isIntimidatingPitcher: boolean;
  battlerOrPokerFace: KeyedCode | null;
  isHotHead: boolean;
}

export interface PitchingMechanicsSpecialAbilitiesDetailsDto {
  goodDelivery: boolean;
  release: KeyedCode;
  goodPace: boolean;
  goodReflexes: boolean;
  goodPickoff: boolean;
}

export interface PitchQualitiesSpecialAbilitiesDetailsDto {
  powerOrBreakingBallPitcher: KeyedCode | null;
  fastballLife: KeyedCode;
  spin: KeyedCode;
  safeOrFatPitch: KeyedCode;
  groundBallOrFlyBallPitcher: KeyedCode;
  gyroball: boolean;
  shuttoSpin: boolean;
}

export class LoadPlayerEditorApiClient {
  private readonly commandName = 'LoadPlayerEditor';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: LoadPlayerEditorRequest): Promise<PlayerEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}