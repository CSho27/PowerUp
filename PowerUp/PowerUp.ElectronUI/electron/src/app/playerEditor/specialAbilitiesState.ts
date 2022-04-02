import { KeyedCode } from "../shared/keyedCode";

export interface SpecialAbilities {
  general: GeneralSpecialAbilities;
  hitter: HitterSpecialAblities;
  pitcher: PitcherSpecialAbilities;
}

export interface GeneralSpecialAbilities {
  isStar: boolean;
  durability: KeyedCode;
  morale: KeyedCode;
}

export interface HitterSpecialAblities {
  situational: SituationalHittingSpecialAbilities;
  approach: HittingApproachSpecialAbilities;
  smallBall: SmallBallSpecialAbilities;
  baseRunning: BaseRunningSpecialAbilities;
  fielding: FieldingSpecialAbilities;
}

export interface SituationalHittingSpecialAbilities {
  hittingConsistency: KeyedCode;
  versusLefty: KeyedCode;
  isTableSetter: boolean;
  isBackToBackHitter: boolean;
  isHotHitter: boolean;
  isRallyHitter: boolean;
  isGoodPinchHitter: boolean;
  basesLoadedHitter: KeyedCode | undefined;
  walkOffHitter: KeyedCode | undefined;
  clutchHitter: KeyedCode;
}

export interface HittingApproachSpecialAbilities {
  isContactHitter: boolean;
  isPowerHitter: boolean;
  sluggerOrSlapHitter: KeyedCode | undefined;
  isPushHitter: boolean;
  isPullHitter: boolean;
  isSprayHitter: boolean;
  isFirstballHitter: boolean;
  aggressiveOrPatientHitter: KeyedCode | undefined;
  isRefinedHitter: boolean;
  isToughOut: boolean;
  isIntimidatingHitter: boolean;
  isSparkplug: boolean;
}

export interface SmallBallSpecialAbilities {
  smallBall: KeyedCode;
  bunting: KeyedCode | undefined;
  infieldHitter: KeyedCode | undefined;
}

export interface BaseRunningSpecialAbilities {
  baseRunning: KeyedCode;
  stealing: KeyedCode;
  isAggressiveRunner: boolean;
  aggressiveOrPatientBaseStealer: KeyedCode | undefined;
  isToughRunner: boolean;
  willBreakupDoublePlay: boolean;
  willSlideHeadFirst: boolean;
}

export interface FieldingSpecialAbilities {
  isGoldGlover: boolean;
  canSpiderCatch: boolean;
  canBarehandCatch: boolean;
  isAggressiveFielder: boolean;
  isPivotMan: boolean;
  isErrorProne: boolean;
  isGoodBlocker: boolean;
  catching: KeyedCode | undefined;
  throwing: KeyedCode;
  hasCannonArm: boolean;
  isTrashTalker: boolean;
}

export interface PitcherSpecialAbilities {
  situational: SituationalPitchingSpecialAbilities;
  demeanor: PitchingDemeanorSpecialAbilities;
  mechanics: PitchingMechanicsSpecialAbilities;
  pitchQualities: PitchQualitiesSpecialAbilities;
}

export interface SituationalPitchingSpecialAbilities {
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

export interface PitchingDemeanorSpecialAbilities {
  isIntimidatingPitcher: boolean;
  battlerOrPokerFace: KeyedCode | undefined;
  isHotHead: boolean;
}

export interface PitchingMechanicsSpecialAbilities {
  goodDelivery: boolean;
  release: KeyedCode;
  goodPace: boolean;
  goodReflexes: boolean;
  goodPickoff: boolean;
}

export interface PitchQualitiesSpecialAbilities {
  powerOrBreakingBallPitcher: KeyedCode | undefined;
  fastballLife: KeyedCode;
  spin: KeyedCode;
  safeOrFatPitch: KeyedCode;
  groundBallOrFlyBallPitcher: KeyedCode;
  gyroball: boolean;
  shuttoSpin: boolean;
}

export type SpecialAbilitiesAction =
| { type: 'updateGeneralAbility', generalAction: GeneralSpecialAbilitiesAction }
| { type: 'updateHitterAbility', hitterAction: HitterSpecialAbilitiesAction }
| { type: 'updatePitcherAbility', pitcherAction: PitcherSpecialAbilitiesAction }

export type GeneralSpecialAbilitiesAction = 
| { type: 'updateIsStar', isStar: boolean }
| { type: 'updateDurability', durability: KeyedCode }
| { type: 'updateMorale', morale: KeyedCode }

export type HitterSpecialAbilitiesAction =
| { type: 'updateSituationalAbility', situationalAction: SituationalHittingSpecialAbilitiesAction }
| { type: 'updateHittingApproachAbility', approachAction: HittingApproachSpecialAbilitiesAction }
| { type: 'updateSmallBallAbility', smallBallAction: SmallBallSpecialAbilitiesAction }
| { type: 'updateBaseRunningAbility', baseRunningAction: BaseRunningSpecialAbilitiesAction }
| { type: 'updateFieldingAbility', fieldingAction: FieldingSpecialAbilitiesAction }

export type SituationalHittingSpecialAbilitiesAction =
| { type: 'updateConsistency', consistency: KeyedCode }
| { type: 'updateVersusLefty', versusLefty: KeyedCode }
| { type: 'updateIsTableSetter', isTableSetter: boolean }
| { type: 'updateIsBackToBackHitter', isBackToBackHitter: boolean }
| { type: 'updateIsHotHitter', isHotHitter: boolean }
| { type: 'updateIsRallyHitter', isRallyHitter: boolean }
| { type: 'updateIsGoodPinchHitter', isGoodPinchHitter: boolean }
| { type: 'updateBasesLoadedHitter', basesLoadedHitter: KeyedCode | undefined }
| { type: 'updateWalkOffHitter', walkOffHitter: KeyedCode | undefined }
| { type: 'updateClutchHitter', clutchHitter: KeyedCode | undefined }

export type HittingApproachSpecialAbilitiesAction =
| { type: 'updateIsContactHitter', isContactHitter: boolean }
| { type: 'updateIsPowerHitter', isPowerHitter: boolean }
| { type: 'updateSluggerOrSlapHitter', sluggerOrSlapHitter: KeyedCode | undefined }
| { type: 'updateIsPushHitter', isPushHitter: boolean }
| { type: 'updateIsPullHitter', isPullHitter: boolean }
| { type: 'updateIsSprayHitter', isSprayHitter: boolean }
| { type: 'updateIsFirstballHitter', isFirstballHitter: boolean }
| { type: 'updateAggressiveOrPatientHitter', aggressiveOrPatientHitter: KeyedCode | undefined }
| { type: 'updateIsRefinedHitter', isRefinedHitter: boolean }
| { type: 'updateIsToughOut', isToughOut: boolean }
| { type: 'updateIsIntimidatingHitter', isIntimidatingHitter: boolean }
| { type: 'updateIsSparkplug', isSparkplug: boolean }

export type SmallBallSpecialAbilitiesAction =
| { type: 'updateSmallBall', smallBall: KeyedCode }
| { type: 'updateBunting', bunting: KeyedCode | undefined }
| { type: 'updateInfieldHitter', infieldHitter: KeyedCode | undefined }

export type BaseRunningSpecialAbilitiesAction =
| { type: 'updateBaseRunning', baseRunning: KeyedCode }
| { type: 'updateStealing', stealing: KeyedCode }
| { type: 'updateIsAggressiveRunner', isAggressiveRunner: boolean }
| { type: 'updateAggressiveOrPatientBaseStealer', aggressiveOrPatientBaseStealer: KeyedCode | undefined }
| { type: 'updateIsToughRunner', isToughRunner: boolean }
| { type: 'updateWillBreakupDoublePlay', willBreakupDoublePlay: boolean }
| { type: 'updateWillSlideHeadFirst', willSlideHeadFirst: boolean }

export type FieldingSpecialAbilitiesAction =
| { type: 'updateIsGoldGlover', isGoldGlover: boolean }
| { type: 'updateCanSpiderCatch', canSpiderCatch: boolean }
| { type: 'updateCanBarehandCatch', canBarehandCatch: boolean }
| { type: 'updateIsAggressiveFielder', isAggressiveFielder: boolean }
| { type: 'updateIsPivotMan', isPivotMan: boolean }
| { type: 'updateIsErrorProne', isErrorProne: boolean }
| { type: 'updateIsGoodBlocker', isGoodBlocker: boolean }
| { type: 'updateCatching', catching: KeyedCode | undefined }
| { type: 'updateThrowing', throwing: KeyedCode }
| { type: 'updateHasCannonArm', hasCannonArm: boolean }
| { type: 'updateIsTrashTalker', isTrashTalker: boolean }

export type PitcherSpecialAbilitiesAction =
| { type: 'updateSituationalAbility', situationalAction: SituationalPitchingSpecialAbilitiesAction }
| { type: 'updatePitchingDemeanor', demeanorAction: PitchingDemeanorSpecialAbilitiesAction }
| { type: 'updateMechanics', mechanicsAction: PitchingMechanicsSpecialAbilitiesAction }
| { type: 'updatePitchQualities', pitchQualitiesAction: PitchQualitiesSpecialAbilitiesAction }

export type SituationalPitchingSpecialAbilitiesAction =
| { type: 'updatePitchingConsistency', pitchingConsistency: KeyedCode }
| { type: 'updatePitchingVersusLefty', pitchingVersusLefty: KeyedCode }
| { type: 'updatePoise', poise: KeyedCode }
| { type: 'updatePoorVersusRunner', poorVersusRunner: boolean }
| { type: 'updateWithRunnersInScoringPosition', withRunnersInScoringPosition: KeyedCode }
| { type: 'updateIsSlowStarter', isSlowStarter: boolean }
| { type: 'updateIsStarterFinisher', isStarterFinisher: boolean }
| { type: 'updateIsChokeArtist', isChokeArtist: boolean }
| { type: 'updateIsSandbag', isSandbag: boolean }
| { type: 'updateDoctorK', doctorK: boolean }
| { type: 'updateIsWalkProne', isWalkProne: boolean }
| { type: 'updateLuck', luck: KeyedCode }
| { type: 'updateRecovery', recovery: KeyedCode }

export type PitchingDemeanorSpecialAbilitiesAction =
| { type: 'updateIsIntimidatingPitcher', isIntimidatingPitcher: boolean }
| { type: 'updateBattlerOrPokerFace', battlerOrPokerFace: KeyedCode | undefined }
| { type: 'updateIsHotHead', isHotHead: boolean }

export type PitchingMechanicsSpecialAbilitiesAction =
| { type: 'updateGoodDelivery', goodDelivery: boolean }
| { type: 'updateRelease', release: KeyedCode }
| { type: 'updateGoodPace', goodPace: boolean }
| { type: 'updateGoodReflexes', goodReflexes: boolean }
| { type: 'updateGoodPickoff', goodPickoff: boolean }

export type PitchQualitiesSpecialAbilitiesAction =
| { type: 'updatePowerOrBreakingBallPitcher', powerOrBreakingBallPitcher: KeyedCode | undefined }
| { type: 'updateFastballLife', fastballLife: KeyedCode }
| { type: 'updateSpin', spin: KeyedCode }
| { type: 'updateSafeOrFatPitch', safeOrFatPitch: KeyedCode }
| { type: 'updateGroundBallOrFlyBallPitcher', groundBallOrFlyBallPitcher: KeyedCode }
| { type: 'updateGyroball', gyroball: boolean }
| { type: 'updateShuttoSpin', shuttoSpin: boolean }

export function SpecialAbilitiesReducer(state: SpecialAbilities, action: SpecialAbilitiesAction): SpecialAbilities {
  switch(action.type) {
    case 'updateGeneralAbility':
      return {
        ...state,
        general: GeneralSpecialAbilitiesReducer(state.general, action.generalAction) 
      }
    case 'updateHitterAbility':
      return {
        ...state,
        hitter: HitterSpecialAbilitiesReducer(state.hitter, action.hitterAction)
      }
    case 'updatePitcherAbility':
      return {
        ...state,
        pitcher: PitcherSpecialAbilitiesReducer(state.pitcher, action.pitcherAction)
      }
  }
}

export function GeneralSpecialAbilitiesReducer(state: GeneralSpecialAbilities, action: GeneralSpecialAbilitiesAction): GeneralSpecialAbilities {
  return state;
}

export function HitterSpecialAbilitiesReducer(state: HitterSpecialAblities, action: HitterSpecialAbilitiesAction): HitterSpecialAblities {
  return state;
}

export function PitcherSpecialAbilitiesReducer(state: PitcherSpecialAbilities, action: PitcherSpecialAbilitiesAction): PitcherSpecialAbilities {
  return state;
}

export function HittingApproachSpecialAbilitiesReducer(state: HittingApproachSpecialAbilities, action: HittingApproachSpecialAbilitiesAction): HittingApproachSpecialAbilities {
  switch(action.type) {
    case 'updateIsContactHitter':
      return {
        ...state,
        isContactHitter: action.isContactHitter
      }
    case 'updateIsPowerHitter':
      return {
        ...state,
        isPowerHitter: action.isPowerHitter
      }
    case 'updateSluggerOrSlapHitter':
      return {
        ...state,
        sluggerOrSlapHitter: action.sluggerOrSlapHitter
      }
    case 'updateIsPushHitter':
      return {
        ...state,
        isPushHitter: action.isPushHitter
      }
    case 'updateIsPullHitter':
      return {
        ...state,
        isPullHitter: action.isPullHitter
      }
    case 'updateIsSprayHitter':
      return {
        ...state,
        isSprayHitter: action.isSprayHitter
      }
    case 'updateIsFirstballHitter':
      return {
        ...state,
        isFirstballHitter: action.isFirstballHitter
      }
    case 'updateAggressiveOrPatientHitter':
      return {
        ...state,
        aggressiveOrPatientHitter: action.aggressiveOrPatientHitter
      }
    case 'updateIsRefinedHitter':
      return {
        ...state,
        isRefinedHitter: action.isRefinedHitter
      }
    case 'updateIsToughOut':
      return {
        ...state,
        isToughOut: action.isToughOut
      }
    case 'updateIsIntimidatingHitter':
      return {
        ...state,
        isIntimidatingHitter: action.isIntimidatingHitter
      }
    case 'updateIsSparkplug':
      return {
        ...state,
        isSparkplug: action.isSparkplug
      }
  }
}