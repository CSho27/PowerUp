import { KeyedCode } from '../shared/keyedCode';

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
  consistency: KeyedCode;
  versusLefty: KeyedCode;
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
| { type: 'updateClutchHitter', clutchHitter: KeyedCode }

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
| { type: 'updateConsistency', consistency: KeyedCode }
| { type: 'updateVersusLefty', versusLefty: KeyedCode }
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
  switch(action.type) {
    case 'updateIsStar':
      return {
        ...state,
        isStar: action.isStar
      }
    case 'updateDurability':
      return {
        ...state,
        durability: action.durability
      }
    case 'updateMorale':
      return {
        ...state,
        morale: action.morale
      }
  }
}

export function HitterSpecialAbilitiesReducer(state: HitterSpecialAblities, action: HitterSpecialAbilitiesAction): HitterSpecialAblities {
  switch(action.type) {
    case 'updateSituationalAbility':
      return {
        ...state,
        situational: SituationalHittingSpecialAbilitiesReducer(state.situational, action.situationalAction)
      }
    case 'updateHittingApproachAbility':
      return {
        ...state,
        approach: HittingApproachSpecialAbilitiesReducer(state.approach, action.approachAction)
      }
    case 'updateSmallBallAbility':
      return {
        ...state,
        smallBall: SmallBallSpecialAbilitiesReducer(state.smallBall, action.smallBallAction)
      }
    case 'updateBaseRunningAbility':
      return {
        ...state,
        baseRunning: BaseRunningSpecialAbilitiesReducer(state.baseRunning, action.baseRunningAction)
      }
    case 'updateFieldingAbility':
      return {
        ...state,
        fielding: FieldingSpecialAbilitiesReducer(state.fielding, action.fieldingAction)
      }
  }
}

export function SituationalHittingSpecialAbilitiesReducer(state: SituationalHittingSpecialAbilities, action: SituationalHittingSpecialAbilitiesAction): SituationalHittingSpecialAbilities {
  switch(action.type) {
    case 'updateConsistency':
      return {
        ...state,
        hittingConsistency: action.consistency
      }
    case 'updateVersusLefty':
      return {
        ...state,
        versusLefty: action.versusLefty
      }
    case 'updateIsTableSetter':
      return {
        ...state,
        isTableSetter: action.isTableSetter
      }
    case 'updateIsBackToBackHitter':
      return {
        ...state,
        isBackToBackHitter: action.isBackToBackHitter
      }
    case 'updateIsHotHitter':
      return {
        ...state,
        isHotHitter: action.isHotHitter
      }
    case 'updateIsRallyHitter':
      return {
        ...state,
        isRallyHitter: action.isRallyHitter
      }
    case 'updateIsGoodPinchHitter':
      return {
        ...state,
        isGoodPinchHitter: action.isGoodPinchHitter
      }
    case 'updateBasesLoadedHitter':
      return {
        ...state,
        basesLoadedHitter: action.basesLoadedHitter
      }
    case 'updateWalkOffHitter':
      return {
        ...state,
        walkOffHitter: action.walkOffHitter
      }
    case 'updateClutchHitter':
      return {
        ...state,
        clutchHitter: action.clutchHitter
      }
  }
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

export function SmallBallSpecialAbilitiesReducer(state: SmallBallSpecialAbilities, action: SmallBallSpecialAbilitiesAction): SmallBallSpecialAbilities {
  switch(action.type) {
    case 'updateSmallBall':
      return {
        ...state,
        smallBall: action.smallBall
      }
    case 'updateBunting':
      return {
        ...state,
        bunting: action.bunting
      }
    case 'updateInfieldHitter':
      return {
        ...state,
        infieldHitter: action.infieldHitter
      }
  }
}

export function BaseRunningSpecialAbilitiesReducer(state: BaseRunningSpecialAbilities, action: BaseRunningSpecialAbilitiesAction): BaseRunningSpecialAbilities {
  switch(action.type) {
    case 'updateBaseRunning':
      return {
        ...state,
        baseRunning: action.baseRunning
      }
    case 'updateStealing':
      return {
        ...state,
        stealing: action.stealing
      }
    case 'updateIsAggressiveRunner':
      return {
        ...state,
        isAggressiveRunner: action.isAggressiveRunner
      }
    case 'updateAggressiveOrPatientBaseStealer':
      return {
        ...state,
        aggressiveOrPatientBaseStealer: action.aggressiveOrPatientBaseStealer
      }
    case 'updateIsToughRunner':
      return {
        ...state,
        isToughRunner: action.isToughRunner
      }
    case 'updateWillBreakupDoublePlay':
      return {
        ...state,
        willBreakupDoublePlay: action.willBreakupDoublePlay
      }
    case 'updateWillSlideHeadFirst':
      return {
        ...state,
        willSlideHeadFirst: action.willSlideHeadFirst
      }
  }
}

export function FieldingSpecialAbilitiesReducer(state: FieldingSpecialAbilities, action: FieldingSpecialAbilitiesAction): FieldingSpecialAbilities {
  switch(action.type) {
    case 'updateIsGoldGlover':
      return {
        ...state,
        isGoldGlover: action.isGoldGlover
      }
    case 'updateCanSpiderCatch':
      return {
        ...state,
        canSpiderCatch: action.canSpiderCatch
      }
    case 'updateCanBarehandCatch':
      return {
        ...state,
        canBarehandCatch: action.canBarehandCatch
      }
    case 'updateIsAggressiveFielder':
      return {
        ...state,
        isAggressiveFielder: action.isAggressiveFielder
      }
    case 'updateIsPivotMan':
      return {
        ...state,
        isPivotMan: action.isPivotMan
      }
    case 'updateIsErrorProne':
      return {
        ...state,
        isErrorProne: action.isErrorProne
      }
    case 'updateIsGoodBlocker':
      return {
        ...state,
        isGoodBlocker: action.isGoodBlocker
      }
    case 'updateCatching':
      return {
        ...state,
        catching: action.catching
      }
    case 'updateThrowing':
      return {
        ...state,
        throwing: action.throwing
      }
    case 'updateHasCannonArm':
      return {
        ...state,
        hasCannonArm: action.hasCannonArm
      }
    case 'updateIsTrashTalker':
      return {
        ...state,
        isTrashTalker: action.isTrashTalker
      }
  }
}

export function PitcherSpecialAbilitiesReducer(state: PitcherSpecialAbilities, action: PitcherSpecialAbilitiesAction): PitcherSpecialAbilities {
  switch(action.type) {
    case 'updateSituationalAbility':
      return {
        ...state,
        situational: SituationalPitchingSpecialAbilitiesReducer(state.situational, action.situationalAction)
      }
    case 'updatePitchingDemeanor':
      return {
        ...state,
        demeanor: PitchingDemeanorSpecialAbilitiesReducer(state.demeanor, action.demeanorAction)
      }
    case 'updateMechanics':
      return {
        ...state,
        mechanics: PitchingMechanicsSpecialAbilitiesReducer(state.mechanics, action.mechanicsAction)
      }
    case 'updatePitchQualities':
      return {
        ...state,
        pitchQualities: PitchQualitiesSpecialAbilitiesReducer(state.pitchQualities, action.pitchQualitiesAction)
      }
  }
}

export function SituationalPitchingSpecialAbilitiesReducer(state: SituationalPitchingSpecialAbilities, action: SituationalPitchingSpecialAbilitiesAction): SituationalPitchingSpecialAbilities {
  switch(action.type) {
    case 'updateConsistency':
      return {
        ...state,
        consistency: action.consistency
      }
    case 'updateVersusLefty':
      return {
        ...state,
        versusLefty: action.versusLefty
      }
    case 'updatePoise':
      return {
        ...state,
        poise: action.poise
      }
    case 'updatePoorVersusRunner':
      return {
        ...state,
        poorVersusRunner: action.poorVersusRunner
      }
    case 'updateWithRunnersInScoringPosition':
      return {
        ...state,
        withRunnersInScoringPosition: action.withRunnersInScoringPosition
      }
    case 'updateIsSlowStarter':
      return {
        ...state,
        isSlowStarter: action.isSlowStarter
      }
    case 'updateIsStarterFinisher':
      return {
        ...state,
        isStarterFinisher: action.isStarterFinisher
      }
    case 'updateIsChokeArtist':
      return {
        ...state,
        isChokeArtist: action.isChokeArtist
      }
    case 'updateIsSandbag':
      return {
        ...state,
        isSandbag: action.isSandbag
      }
    case 'updateDoctorK':
      return {
        ...state,
        doctorK: action.doctorK
      }
    case 'updateIsWalkProne':
      return {
        ...state,
        isWalkProne: action.isWalkProne
      }
    case 'updateLuck':
      return {
        ...state,
        luck: action.luck
      }
    case 'updateRecovery':
      return {
        ...state,
        recovery: action.recovery
      }
  }
}

export function PitchingDemeanorSpecialAbilitiesReducer(state: PitchingDemeanorSpecialAbilities, action: PitchingDemeanorSpecialAbilitiesAction): PitchingDemeanorSpecialAbilities {
  switch(action.type) {
    case 'updateIsIntimidatingPitcher':
      return {
        ...state,
        isIntimidatingPitcher: action.isIntimidatingPitcher
      }
    case 'updateBattlerOrPokerFace':
      return {
        ...state,
        battlerOrPokerFace: action.battlerOrPokerFace
      }
    case 'updateIsHotHead':
      return {
        ...state,
        isHotHead: action.isHotHead
      }
  }
}

export function PitchingMechanicsSpecialAbilitiesReducer(state: PitchingMechanicsSpecialAbilities, action: PitchingMechanicsSpecialAbilitiesAction): PitchingMechanicsSpecialAbilities {
  switch(action.type) {
    case 'updateGoodDelivery':
      return {
        ...state,
        goodDelivery: action.goodDelivery
      }
    case 'updateRelease':
      return {
        ...state,
        release: action.release
      }
    case 'updateGoodPace':
      return {
        ...state,
        goodPace: action.goodPace
      }
    case 'updateGoodReflexes':
      return {
        ...state,
        goodReflexes: action.goodReflexes
      }
    case 'updateGoodPickoff':
      return {
        ...state,
        goodPickoff: action.goodPickoff
      }
  }
}

export function PitchQualitiesSpecialAbilitiesReducer(state: PitchQualitiesSpecialAbilities, action: PitchQualitiesSpecialAbilitiesAction): PitchQualitiesSpecialAbilities {
  switch(action.type) {
    case 'updatePowerOrBreakingBallPitcher':
      return {
        ...state,
        powerOrBreakingBallPitcher: action.powerOrBreakingBallPitcher
      }
    case 'updateFastballLife':
      return {
        ...state,
        fastballLife: action.fastballLife
      }
    case 'updateSpin':
      return {
        ...state,
        spin: action.spin
      }
    case 'updateSafeOrFatPitch':
      return {
        ...state,
        safeOrFatPitch: action.safeOrFatPitch
      }
    case 'updateGroundBallOrFlyBallPitcher':
      return {
        ...state,
        groundBallOrFlyBallPitcher: action.groundBallOrFlyBallPitcher
      }
    case 'updateGyroball':
      return {
        ...state,
        gyroball: action.gyroball
      }
    case 'updateShuttoSpin':
      return {
        ...state,
        shuttoSpin: action.shuttoSpin
      }
  }
}