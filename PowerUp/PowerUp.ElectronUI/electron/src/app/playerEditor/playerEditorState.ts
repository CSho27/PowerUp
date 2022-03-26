import { Dispatch } from "react"
import { Grade } from "../../components/gradeLetter/gradeLetter";
import { KeyedCode } from "../shared/keyedCode"
import { Position, PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode"
import { HotZoneGridAction, HotZoneGridState, HotZoneGridStateReducer } from "./hotZoneGrid";
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient"

export interface PlayerEditorState {
  selectedTab: PlayerEditorTab;
  personalDetails: PlayerPersonalDetails;
  positionCapabilityDetails: PositionCapabilityDetails;
  hitterAbilities: HitterAbilities;
  pitcherAbilities: PitcherAbilities;
}

export type PlayerEditorTab = typeof playerEditorTabOptions[number];
export const playerEditorTabOptions = [
  'Personal',
  'Positions',
//'Appearance',
  'Hitter',
  'Pitcher',
//'Special'
] as const

export type PlayerEditorAction =
| { type: 'updateSelectedTab', selectedTab: PlayerEditorTab }
| { type: 'updatePersonalDetails', action: PlayerPersonalDetailsAction }
| { type: 'updatePositionCapabilityDetails', action: PositionCapabilityDetailsAction }
| { type: 'updateHitterAbilities', action: HitterAbilitiesAction }
| { type: 'updatePitcherAbilities', action: PitcherAbilitiesAction }

export function PlayerEditorStateReducer(state: PlayerEditorState, action: PlayerEditorAction, context: PlayerPersonalDetailsContext): PlayerEditorState {
  switch(action.type) {
    case 'updateSelectedTab':
      return {
        ...state,
        selectedTab: action.selectedTab
      }
    case 'updatePersonalDetails':
      return {
        ...state,
        personalDetails: PlayerPersonalDetailReducer(state.personalDetails, action.action, context),
        positionCapabilityDetails: action.action.type === 'updatePosition'
          ? getDefaultCapabilitiesForPosition(action.action.position.key)
          : state.positionCapabilityDetails
      }
    case 'updatePositionCapabilityDetails':
      return {
        ...state,
        positionCapabilityDetails: PositionCapabilityDetailsReducer(state.positionCapabilityDetails, action.action)
      }
    case 'updateHitterAbilities':
      return {
        ...state,
        hitterAbilities: HitterAbilitiesReducer(state.hitterAbilities, action.action)
      }
    case 'updatePitcherAbilities':
      return {
        ...state,
        pitcherAbilities: PitcherAbilitiesReducer(state.pitcherAbilities, action.action)
      }
  }
}

export interface PlayerPersonalDetails {
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
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

export type PlayerPersonalDetailsAction =
| { type: 'updateFirstName', firstName: string }
| { type: 'updateLastName', lastName: string }
| { type: 'toggleUseSpecialSavedName' }
| { type: 'updateSavedName', savedName: string }
| { type: 'updateUniformNumber', uniformNumber: string }
| { type: 'updatePosition', position: PositionCode }
| { type: 'updatePitcherType', pitcherType: KeyedCode }
| { type: 'updateVoice', voice: SimpleCode }
| { type: 'updateBattingSide', battingSide: KeyedCode }
| { type: 'updateBattingStance', battingStance: SimpleCode }
| { type: 'updateThrowingArm', throwingArm: KeyedCode }
| { type: 'updatePitchingMechanics', mechanics: SimpleCode }

export interface PlayerPersonalDetailsContext {
  swingManRole: KeyedCode;
  starterRole: KeyedCode;
}

export function PlayerPersonalDetailReducer(state: PlayerPersonalDetails, action: PlayerPersonalDetailsAction, context: PlayerPersonalDetailsContext): PlayerPersonalDetails {
  switch(action.type) {
    case 'updateFirstName':
      return {
        ...state,
        firstName: action.firstName
      }
    case 'updateLastName':
      return {
        ...state,
        lastName: action.lastName
      }
    case 'toggleUseSpecialSavedName':
      return {
        ...state,
        useSpecialSavedName: !state.useSpecialSavedName,
      }
    case 'updateSavedName':
      return {
        ...state,
        savedName: action.savedName
      }
    case 'updateUniformNumber':
      return {
        ...state,
        uniformNumber: action.uniformNumber
      }
    case 'updatePosition':
      const isPitcher = action.position.key === 'Pitcher'; 
      return {
        ...state,
        position: action.position,
        pitcherType: isPitcher 
          ? context.starterRole
          : context.swingManRole,
      }
    case 'updatePitcherType':
      return {
        ...state,
        pitcherType: action.pitcherType
      }
    case 'updateVoice':
      return {
        ...state,
        voice: action.voice
      }
    case 'updateBattingSide':
      return {
        ...state,
        battingSide: action.battingSide
      }
    case 'updateBattingStance':
      return {
        ...state,
        battingStance: action.battingStance
      }
    case 'updateThrowingArm':
      return {
        ...state,
        throwingArm: action.throwingArm
      }
    case 'updatePitchingMechanics':
      return {
        ...state,
        pitchingMechanics: action.mechanics
      }
  }
}

export function getPersonalDetailsReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [PlayerPersonalDetails, Dispatch<PlayerPersonalDetailsAction>] {
  return [
    state.personalDetails,
    (action: PlayerPersonalDetailsAction) => update({ type: 'updatePersonalDetails', action: action })
  ]
}

export interface PositionCapabilityDetails {
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

export type PositionCapabilityDetailsAction = { position: Position, ability: KeyedCode }

export function PositionCapabilityDetailsReducer(state: PositionCapabilityDetails, action: PositionCapabilityDetailsAction): PositionCapabilityDetails {
  switch(action.position) {
    case 'Pitcher':
      return {
        ...state,
        pitcher: action.ability
      }
    case 'Catcher':
      return {
        ...state,
        catcher: action.ability
      }
    case 'FirstBase':
      return {
        ...state,
        firstBase: action.ability
      }
    case 'SecondBase':
      return {
        ...state,
        secondBase: action.ability
      }
    case 'ThirdBase':
      return {
        ...state,
        thirdBase: action.ability
      }
    case 'Shortstop':
      return {
        ...state,
        shortstop: action.ability
      }
    case 'LeftField':
      return {
        ...state,
        leftField: action.ability
      }
    case 'CenterField':
      return {
        ...state,
        centerField: action.ability
      }
    case 'RightField':
      return {
        ...state,
        rightField: action.ability
      }
    default:
      throw 'cannot update ability for this position'      
  }
}

export function getPositionCapabilityDetailsReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [PositionCapabilityDetails, Dispatch<PositionCapabilityDetailsAction>] {
  return [
    state.positionCapabilityDetails,
    (action: PositionCapabilityDetailsAction) => update({ type: 'updatePositionCapabilityDetails', action: action })
  ]
}

export function getDefaultCapabilitiesForPosition(position: Position) : PositionCapabilityDetails {
  const a = { key: 'A', name: 'A' };
  const e = { key: 'E', name: 'E' };
  const f = { key: 'F', name: 'F' };
  const g = { key: 'G', name: 'G' };
  const baseCapabilities: PositionCapabilityDetails = {
    pitcher: g,
    catcher: g,
    firstBase: g,
    secondBase: g,
    thirdBase: g,
    shortstop: g,
    leftField: g,
    centerField: g,
    rightField: g
  } 
  
  switch(position) {
    case 'Pitcher':
      return {
        ...baseCapabilities,
        pitcher: a
      }
    case 'Catcher':
      return {
        ...baseCapabilities,
        catcher: a
      }
    case 'FirstBase':
      return {
        ...baseCapabilities,
        firstBase: a,
        secondBase: f,
        thirdBase: f,
        shortstop: f
      }
    case 'SecondBase':
      return {
        ...baseCapabilities,
        firstBase: f,
        secondBase: a,
        thirdBase: e,
        shortstop: e
      }
    case 'ThirdBase':
      return {
        ...baseCapabilities,
        firstBase: f,
        secondBase: e,
        thirdBase: a,
        shortstop: e
      }
    case 'Shortstop':
      return {
        ...baseCapabilities,
        firstBase: f,
        secondBase: e,
        thirdBase: e,
        shortstop: a
      }
    case 'LeftField':
      return {
        ...baseCapabilities,
        leftField: a,
        centerField: e,
        rightField: e
      }
    case 'CenterField':
      return {
        ...baseCapabilities,
        leftField: e,
        centerField: a,
        rightField: e
      }
    case 'RightField':
      return {
        ...baseCapabilities,
        leftField: e,
        centerField: e,
        rightField: a
      }
    default:
      throw 'Cannot update abaility for this position';
  }
}

export interface HitterAbilities {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
  hotZones: HotZoneGridState;
}

export type HitterAbilitiesAction =
| { type: 'updateTrajectory', trajectory: number }
| { type: 'updateContact', contact: number }
| { type: 'updatePower', power: number }
| { type: 'updateRunSpeed', runSpeed: number }
| { type: 'updateArmStrength', armStrength: number }
| { type: 'updateFielding', fielding: number }
| { type: 'updateErrorResistance', errorResistance: number }
| { type: 'updateHotZones', hzAction: HotZoneGridAction }

export function HitterAbilitiesReducer(state: HitterAbilities, action: HitterAbilitiesAction): HitterAbilities {
  switch(action.type) {
    case 'updateTrajectory':
      return {
        ...state,
        trajectory: action.trajectory
      }
    case 'updateContact':
      return {
        ...state,
        contact: action.contact
      }
    case 'updatePower':
      return {
        ...state,
        power: action.power
      }
    case 'updateRunSpeed':
      return {
        ...state,
        runSpeed: action.runSpeed
      }
    case 'updateArmStrength':
      return {
        ...state,
        armStrength: action.armStrength
      }
    case 'updateFielding':
      return {
        ...state,
        fielding: action.fielding
      }
    case 'updateErrorResistance':
      return {
        ...state,
        errorResistance: action.errorResistance
      }
    case 'updateHotZones':
      return {
        ...state,
        hotZones: HotZoneGridStateReducer(state.hotZones, action.hzAction)
      }
  }
}

export function getHitterAbilitiesReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [HitterAbilities, Dispatch<HitterAbilitiesAction>] {
  return [
    state.hitterAbilities,
    (action: HitterAbilitiesAction) => update({ type: 'updateHitterAbilities', action: action })
  ]
}

export function getHotZoneGridReducer(state: HitterAbilities, update: Dispatch<HitterAbilitiesAction>) : [HotZoneGridState, Dispatch<HotZoneGridAction>] {
  return [
    state.hotZones,
    (action: HotZoneGridAction) => update({ type: 'updateHotZones', hzAction: action })
  ]
}

export interface PitcherAbilities {
  topSpeed: number;
  control: number;
  stamina: number;

  twoSeamType: KeyedCode | undefined;
  twoSeamMovement: number;

  slider1Type: KeyedCode | undefined;
  slider1Movement: number;

  slider2Type: KeyedCode | undefined;
  slider2Movement: number;

  curve1Type: KeyedCode | undefined;
  curve1Movement: number;

  curve2Type: KeyedCode | undefined;
  curve2Movement: number;

  fork1Type: KeyedCode | undefined;
  fork1Movement: number;

  fork2Type: KeyedCode | undefined;
  fork2Movement: number;

  sinker1Type: KeyedCode | undefined;
  sinker1Movement: number;

  sinker2Type: KeyedCode | undefined;
  sinker2Movement: number;

  sinkingFastball1Type: KeyedCode | undefined;
  sinkingFastball1Movement: number;

  sinkingFastball2Type: KeyedCode | undefined;
  sinkingFastball2Movement: number;
}

export type PitcherAbilitiesAction = 
| { type: 'updateTopSpeed', topSpeed: number }
| { type: 'updateControl', control: number }
| { type: 'updateStamina', stamina: number }
| { type: 'updateTwoSeamType', twoSeamType: KeyedCode | undefined }
| { type: 'updateTwoSeamMovement', movement: number }
| { type: 'updateSlider1Type', sliderType: KeyedCode | undefined }
| { type: 'updateSlider1Movement', movement: number }
| { type: 'updateSlider2Type', sliderType: KeyedCode | undefined }
| { type: 'updateSlider2Movement', movement: number }
| { type: 'updateCurve1Type', curveType: KeyedCode | undefined }
| { type: 'updateCurve1Movmement', movement: number }
| { type: 'updateCurve2Type', curveType: KeyedCode | undefined }
| { type: 'updateCurve2Movement', movement: number }
| { type: 'updateFork1Type', forkType: KeyedCode | undefined }
| { type: 'updateFork1Movement', movement: number }
| { type: 'updateFork2Type', forkType: KeyedCode | undefined }
| { type: 'updateFork2Movement', movement: number }
| { type: 'updateSinker1Type', sinkerType: KeyedCode | undefined }
| { type: 'updateSinker1Movement', movement: number }
| { type: 'updateSinker2Type', sinkerType: KeyedCode | undefined }
| { type: 'updateSinker2Movement', movement: number }
| { type: 'updateSinkingFastball1Type', sinkingFastballType: KeyedCode | undefined }
| { type: 'updateSinkingFastball1Movement', movement: number }
| { type: 'updateSinkingFastball2Type', sinkingFastballType: KeyedCode | undefined }
| { type: 'updateSinkingFastball2Movement', movement: number }

export function PitcherAbilitiesReducer(state: PitcherAbilities, action: PitcherAbilitiesAction): PitcherAbilities {
  switch(action.type) {
    case 'updateTopSpeed':
      return {
        ...state,
        topSpeed: action.topSpeed
      }
    case 'updateControl':
      return {
        ...state,
        control: action.control
      }
    case 'updateStamina':
      return {
        ...state,
        stamina: action.stamina
      }
    case 'updateTwoSeamType':
      return {
        ...state,
        twoSeamType: action.twoSeamType,
      }
    case 'updateTwoSeamMovement':
      return {
        ...state,
        twoSeamMovement: action.movement
      }
    case 'updateSlider1Type':
      return {
        ...state,
        slider1Type: action.sliderType
      }
    case 'updateSlider1Movement':
      return {
        ...state,
        slider1Movement: action.movement
      }
    case 'updateSlider2Type':
      return {
        ...state,
        slider2Type: action.sliderType
      }
    case 'updateSlider2Movement':
      return {
        ...state,
        slider2Movement: action.movement
      }
    case 'updateCurve1Type':
      return {
        ...state,
        curve1Type: action.curveType
      }
    case 'updateCurve1Movmement':
      return {
        ...state,
        curve1Movement: action.movement
      }
    case 'updateCurve2Type':
      return {
        ...state,
        curve2Type: action.curveType
      }
    case 'updateCurve2Movement':
      return {
        ...state,
        curve2Movement: action.movement
      }
    case 'updateFork1Type':
      return {
        ...state,
        fork1Type: action.forkType
      }
    case 'updateFork1Movement':
      return {
        ...state,
        fork1Movement: action.movement
      } 
    case 'updateFork2Type':
      return {
        ...state,
        fork2Type: action.forkType
      }
    case 'updateFork2Movement':
      return {
        ...state,
        fork2Movement: action.movement
      }
    case 'updateSinker1Type':
      return {
        ...state,
        sinker1Type: action.sinkerType
      }
    case 'updateSinker1Movement':
      return {
        ...state,
        sinker1Movement: action.movement
      }
    case 'updateSinker2Type':
      return {
        ...state,
        sinker2Type: action.sinkerType
      }
    case 'updateSinker2Movement':
      return {
        ...state,
        sinker2Movement: action.movement
      }
    case 'updateSinkingFastball1Type':
      return {
        ...state,
        sinkingFastball1Type: action.sinkingFastballType
      }
    case 'updateSinkingFastball1Movement':
      return {
        ...state,
        sinkingFastball1Movement: action.movement
      }
    case 'updateSinkingFastball2Type':
      return {
        ...state,
        sinkingFastball2Type: action.sinkingFastballType
      }
    case 'updateSinkingFastball2Movement':
      return {
        ...state,
        sinkingFastball2Movement: action.movement
      }
  }
}

export function getPitcherAbilitiesReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [PitcherAbilities, Dispatch<PitcherAbilitiesAction>] {
  return [
    state.pitcherAbilities,
    (action: PitcherAbilitiesAction) => update({ type: 'updatePitcherAbilities', action: action })
  ]
}

export function getGradeFor0_15(value: number): Grade {
  if(value >= 14) return 'A';
  if(value >= 12) return 'B';
  if(value >= 10) return 'C';
  if(value >= 8)  return 'D';
  if(value >= 6)  return 'E';
  if(value >= 4)  return 'F';
  else            return 'G'; 
}

export function getGradeForPower(value: number): Grade {
  if(value >= 180) return 'A';
  if(value >= 140) return 'B';
  if(value >= 120) return 'C';
  if(value >= 100)  return 'D';
  if(value >= 85)  return 'E';
  if(value >= 25)  return 'F';
  else            return 'G'; 
}


export function getInitialStateFromResponse(response: PlayerEditorResponse): PlayerEditorState {
  const { 
    personalDetails, 
    positionCapabilityDetails: positionCapabilities, 
    hitterAbilityDetails: hitterAbilities,
    pitcherAbilityDetails: pitcherAbilities
  } = response
  
  return {
    selectedTab: 'Personal',
    positionCapabilityDetails: {
      pitcher: positionCapabilities.pitcher,
      catcher: positionCapabilities.catcher,
      firstBase: positionCapabilities.firstBase,
      secondBase: positionCapabilities.secondBase,
      thirdBase: positionCapabilities.thirdBase,
      shortstop: positionCapabilities.shortstop,
      leftField: positionCapabilities.leftField,
      centerField: positionCapabilities.centerField,
      rightField: positionCapabilities.rightField
    },
    personalDetails: {
      firstName: personalDetails.firstName,
      lastName: personalDetails.lastName,
      useSpecialSavedName: personalDetails.isSpecialSavedName,
      savedName: personalDetails.savedName,
      uniformNumber: personalDetails.uniformNumber,
      position: personalDetails.position,
      pitcherType: personalDetails.pitcherType,
      voice: personalDetails.voice,
      battingSide: personalDetails.battingSide,
      battingStance: personalDetails.battingStance,
      throwingArm: personalDetails.throwingArm,
      pitchingMechanics: personalDetails.pitchingMechanics
    },
    hitterAbilities: {
      trajectory: hitterAbilities.trajectory,
      contact: hitterAbilities.contact,
      power: hitterAbilities.power,
      runSpeed: hitterAbilities.runSpeed,
      armStrength: hitterAbilities.armStrength,
      fielding: hitterAbilities.fielding,
      errorResistance: hitterAbilities.errorResistance,
      hotZones: hitterAbilities.hotZones
    },
    pitcherAbilities: {
      topSpeed: pitcherAbilities.topSpeed,
      control: pitcherAbilities.control,
      stamina: pitcherAbilities.stamina,
      twoSeamType: pitcherAbilities.twoSeamType ?? undefined,
      twoSeamMovement: pitcherAbilities.twoSeamMovement ?? 1,
      slider1Type: pitcherAbilities.slider1Type ?? undefined,
      slider1Movement: pitcherAbilities.slider1Movement ?? 1,
      slider2Type: pitcherAbilities.slider2Type ?? undefined,
      slider2Movement: pitcherAbilities.slider2Movement ?? 1,
      curve1Type: pitcherAbilities.curve1Type ?? undefined,
      curve1Movement: pitcherAbilities.curve1Movement ?? 1,
      curve2Type: pitcherAbilities.curve2Type ?? undefined,
      curve2Movement: pitcherAbilities.curve1Movement ?? 1,
      fork1Type: pitcherAbilities.fork1Type ?? undefined,
      fork1Movement: pitcherAbilities.fork1Movement ?? 1,
      fork2Type: pitcherAbilities.fork2Type ?? undefined,
      fork2Movement: pitcherAbilities.fork2Movement ?? 1,
      sinker1Type: pitcherAbilities.sinker1Type ?? undefined,
      sinker1Movement: pitcherAbilities.sinker1Movement ?? 1,
      sinker2Type: pitcherAbilities.sinker1Type ?? undefined,
      sinker2Movement: pitcherAbilities.sinker2Movement ?? 1,
      sinkingFastball1Type: pitcherAbilities.sinkingFastball1Type ?? undefined,
      sinkingFastball1Movement: pitcherAbilities.sinkingFastball1Movement ?? 1,
      sinkingFastball2Type: pitcherAbilities.sinkingFastball2Type ?? undefined,
      sinkingFastball2Movement: pitcherAbilities.sinkingFastball2Movement ?? 1
    }
  }
}