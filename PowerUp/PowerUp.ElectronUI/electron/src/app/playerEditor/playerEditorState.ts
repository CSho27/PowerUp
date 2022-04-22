import { Dispatch } from "react"
import { Grade } from "../../components/gradeLetter/gradeLetter";
import { KeyedCode } from "../shared/keyedCode"
import { Position, PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode"
import { HotZoneGridAction, HotZoneGridState, HotZoneGridStateReducer } from "./hotZoneGrid";
import { FaceCode, PlayerEditorResponse } from "./loadPlayerEditorApiClient"
import { SavePlayerRequest } from "./savePlayerApiClient";
import { buildSpecialAbilitiesRequestFromState, getInitialSpecialAbilitiesFromResponse, SpecialAbilities, SpecialAbilitiesAction, SpecialAbilitiesReducer } from "./specialAbilitiesState";

export interface PlayerEditorState {
  selectedTab: PlayerEditorTab;
  personalDetails: PlayerPersonalDetails;
  appearance: Appearance;
  positionCapabilityDetails: PositionCapabilityDetails;
  hitterAbilities: HitterAbilities;
  pitcherAbilities: PitcherAbilities;
  specialAbilities: SpecialAbilities;
}

export type PlayerEditorTab = typeof playerEditorTabOptions[number];
export const playerEditorTabOptions = [
  'Personal',
  'Appearance',
  'Positions',
  'Hitter',
  'Pitcher',
  'Special'
] as const

export type PlayerEditorAction =
| { type: 'updateSelectedTab', selectedTab: PlayerEditorTab }
| { type: 'updatePersonalDetails', action: PlayerPersonalDetailsAction }
| { type: 'updateAppearance', action: AppearanceAction }
| { type: 'updatePositionCapabilityDetails', action: PositionCapabilityDetailsAction }
| { type: 'updateHitterAbilities', action: HitterAbilitiesAction }
| { type: 'updatePitcherAbilities', action: PitcherAbilitiesAction }
| { type: 'updateSpecialAbilities', action: SpecialAbilitiesAction }

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
    case 'updateAppearance':
      return {
        ...state,
        appearance: AppearanceReducer(state.appearance, action.action) 
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
    case 'updateSpecialAbilities':
      return {
        ...state,
        specialAbilities: SpecialAbilitiesReducer(state.specialAbilities, action.action)
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

export interface Appearance {
  face: FaceCode;
  eyebrows: KeyedCode;
  skinColor: KeyedCode;
  eyeColor: KeyedCode;
  hairStyle: KeyedCode | undefined;
  hairColor: KeyedCode;
  facialHairStyle: KeyedCode | undefined;
  facialHairColor: KeyedCode;
  batColor: KeyedCode;
  gloveColor: KeyedCode;
  eyewearType: KeyedCode | undefined;
  eyewearFrameColor: KeyedCode;
  eyewearLensColor: KeyedCode;
  earringSide: KeyedCode | undefined;
  earringColor: KeyedCode;
  rightWristbandColor: KeyedCode | undefined;
  leftWristbandColor: KeyedCode | undefined;
}

export type AppearanceAction =
| { type: 'updateFace', face: FaceCode }
| { type: 'updateEyebrows', eyebrows: KeyedCode }
| { type: 'updateSkinColor', skinColor: KeyedCode }
| { type: 'updateEyeColor', eyeColor: KeyedCode }
| { type: 'updateHairStyle', hairStyle: KeyedCode | undefined }
| { type: 'updateHairColor', hairColor: KeyedCode }
| { type: 'updateFacialHairStyle', facialHairStyle: KeyedCode | undefined }
| { type: 'updateFacialHairColor', facialHairColor: KeyedCode }
| { type: 'updateBatColor', batColor: KeyedCode }
| { type: 'updateGloveColor', gloveColor: KeyedCode }
| { type: 'updateEyewearType', eyewearType: KeyedCode | undefined }
| { type: 'updateEyewearFrameColor', frameColor: KeyedCode }
| { type: 'updateEyewearLensColor', lensColor: KeyedCode }
| { type: 'updateEarringSide', earringSide: KeyedCode | undefined }
| { type: 'updateEarringColor', earringColor: KeyedCode }
| { type: 'updateRightWristbandColor', color: KeyedCode | undefined }
| { type: 'updateLeftWristbandColor', color: KeyedCode | undefined }

export function AppearanceReducer(state: Appearance, action: AppearanceAction): Appearance {
  switch(action.type) {
    case 'updateFace':
      return {
        ...state,
        face: action.face
      }
    case 'updateEyebrows':
      return {
        ...state,
        eyebrows: action.eyebrows
      }
    case 'updateSkinColor':
      return {
        ...state,
        skinColor: action.skinColor
      }
    case 'updateEyeColor':
      return {
        ...state,
        eyeColor: action.eyeColor
      }
    case 'updateHairStyle':
      return {
        ...state,
        hairStyle: action.hairStyle
      }
    case 'updateHairColor':
      return {
        ...state,
        hairColor: action.hairColor
      }
    case 'updateFacialHairStyle':
      return {
        ...state,
        facialHairStyle: action.facialHairStyle
      } 
    case 'updateFacialHairColor':
      return {
        ...state,
        facialHairColor: action.facialHairColor
      }
    case 'updateBatColor':
      return {
        ...state,
        batColor: action.batColor
      }
    case 'updateGloveColor':
      return {
        ...state,
        gloveColor: action.gloveColor
      }
    case 'updateEyewearType':
      return {
        ...state,
        eyewearType: action.eyewearType
      }
    case 'updateEyewearFrameColor': 
      return  {
        ...state,
        eyewearFrameColor: action.frameColor
      }
    case 'updateEyewearLensColor':
      return {
        ...state,
        eyewearLensColor: action.lensColor
      }
    case 'updateEarringSide':
      return {
        ...state,
        earringSide: action.earringSide
      }
    case 'updateEarringColor':
      return {
        ...state,
        earringColor: action.earringColor
      }
    case 'updateRightWristbandColor':
      return {
        ...state,
        rightWristbandColor: action.color
      }
    case 'updateLeftWristbandColor':
      return {
        ...state,
        leftWristbandColor: action.color
      }
  }
}

export function getAppearanceReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [Appearance, Dispatch<AppearanceAction>] {
  return [
    state.appearance,
    (action: AppearanceAction) => update({ type: 'updateAppearance', action: action })
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

export function getSepcialAbilitiesReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [SpecialAbilities, Dispatch<SpecialAbilitiesAction>] {
  return [
    state.specialAbilities,
    (action: SpecialAbilitiesAction) => update({ type: 'updateSpecialAbilities', action: action })
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
  if(value >= 100) return 'D';
  if(value >= 85)  return 'E';
  if(value >= 25)  return 'F';
  else             return 'G'; 
}

export function getGradeForControl(value: number): Grade {
  if(value >= 180) return 'A';
  if(value >= 155) return 'B';
  if(value >= 135) return 'C';
  if(value >= 120) return 'D';
  if(value >= 110) return 'E';
  if(value >= 100) return 'F';
  else             return 'G'; 
}

export function getGradeForStamina(value: number): Grade {
  if(value >= 150) return 'A';
  if(value >= 110) return 'B';
  if(value >= 80)  return 'C';
  if(value >= 60)  return 'D';
  if(value >= 30)  return 'E';
  if(value >= 15)  return 'F';
  else             return 'G'; 
}


export function getInitialStateFromResponse(response: PlayerEditorResponse): PlayerEditorState {
  const { 
    personalDetails, 
    appearanceDetails: appearance,
    positionCapabilityDetails: positionCapabilities, 
    hitterAbilityDetails: hitterAbilities,
    pitcherAbilityDetails: pitcherAbilities
  } = response

  const { appearanceOptions  } = response.options
  
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
    appearance: {
      face: appearance.face,
      eyebrows: appearance.eyebrows ?? appearanceOptions.eyebrowThicknessOptions[0],
      skinColor: appearance.skinColor ?? appearanceOptions.skinColorOptions[0],
      eyeColor: appearance.eyeColor ?? appearanceOptions.eyeColorOptions[0],
      hairStyle: appearance.hairStyle ?? undefined,
      hairColor: appearance.hairColor ?? appearanceOptions.hairColorOptions[0],
      facialHairStyle: appearance.facialHairStyle ?? undefined,
      facialHairColor: appearance.facialHairColor ?? appearanceOptions.hairColorOptions[0],
      batColor: appearance.batColor,
      gloveColor: appearance.gloveColor,
      eyewearType: appearance.eyewearType ?? undefined,
      eyewearFrameColor: appearance.eyewearFrameColor ?? appearanceOptions.eyewearFrameColorOptions[0],
      eyewearLensColor: appearance.eyewearLensColor ?? appearanceOptions.eyewearLensColorOptions[0],
      earringSide: appearance.earringSide ?? undefined,
      earringColor: appearance.earringColor ?? appearanceOptions.accessoryColorOptions[0],
      rightWristbandColor : appearance.rightWristbandColor ?? undefined,
      leftWristbandColor: appearance.leftWristbandColor ?? undefined
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
      curve2Movement: pitcherAbilities.curve2Movement ?? 1,
      
      fork1Type: pitcherAbilities.fork1Type ?? undefined,
      fork1Movement: pitcherAbilities.fork1Movement ?? 1,
      
      fork2Type: pitcherAbilities.fork2Type ?? undefined,
      fork2Movement: pitcherAbilities.fork2Movement ?? 1,
      
      sinker1Type: pitcherAbilities.sinker1Type ?? undefined,
      sinker1Movement: pitcherAbilities.sinker1Movement ?? 1,
      
      sinker2Type: pitcherAbilities.sinker2Type ?? undefined,
      sinker2Movement: pitcherAbilities.sinker2Movement ?? 1,
      
      sinkingFastball1Type: pitcherAbilities.sinkingFastball1Type ?? undefined,
      sinkingFastball1Movement: pitcherAbilities.sinkingFastball1Movement ?? 1,

      sinkingFastball2Type: pitcherAbilities.sinkingFastball2Type ?? undefined,
      sinkingFastball2Movement: pitcherAbilities.sinkingFastball2Movement ?? 1
    },
    specialAbilities: getInitialSpecialAbilitiesFromResponse(response.specialAbilityDetails)
  }
}

export function buildSavePlayerRequestFromState(state: PlayerEditorState, playerId: number): SavePlayerRequest {
  const { 
    personalDetails, 
    appearance,
    positionCapabilityDetails, 
    pitcherAbilities, 
    hitterAbilities, 
    specialAbilities 
  } = state;

  return {
    playerId: playerId,
    personalDetails: {
      firstName: personalDetails.firstName,
      lastName: personalDetails.lastName,
      useSpecialSavedName: personalDetails.useSpecialSavedName,
      savedName: personalDetails.savedName,
      uniformNumber: personalDetails.uniformNumber,
      positionKey: personalDetails.position.key,
      pitcherTypeKey: personalDetails.pitcherType.key,
      voiceId: personalDetails.voice.id,
      battingSideKey: personalDetails.battingSide.key,
      battingStanceId: personalDetails.battingStance.id,
      throwingArmKey: personalDetails.throwingArm.key,
      pitchingMechanicsId: personalDetails.pitchingMechanics.id,
    },
    appearance: {
      faceId: appearance.face.id,
      eyebrowThicknessKey: appearance.face.canChooseEyebrows
        ? appearance.eyebrows.key
        : null,
      skinColorKey: appearance.face.canChooseSkin
        ? appearance.skinColor.key
        : null,
      eyeColorKey: appearance.face.canChooseEyes
        ? appearance.eyeColor.key
        : null,
      hairStyleKey: appearance.hairStyle?.key ?? null,
      hairColorKey: !!appearance.hairStyle
        ? appearance.hairColor.key
        : null,
      facialHairStyleKey: appearance.facialHairStyle?.key ?? null,
      facialHairColorKey: !!appearance.facialHairStyle
        ? appearance.facialHairColor.key
        : null,
      batColorKey: appearance.batColor.key,
      gloveColorKey: appearance.gloveColor.key,
      eyewearTypeKey: appearance.eyewearType?.key ?? null,
      eyewearFrameColorKey: !!appearance.eyewearType && appearance.eyewearType.key != 'EyeBlack'   
        ? appearance.eyewearFrameColor.key
        : null,
      eyewearLensColorKey: !!appearance.eyewearType && appearance.eyewearType.key != 'EyeBlack'
        ? appearance.eyewearLensColor.key
        : null,
      earringSideKey: appearance.earringSide?.key ?? null,
      earringColorKey: !!appearance.earringSide
        ? appearance.earringColor.key
        : null,
      rightWristbandColorKey: appearance.rightWristbandColor?.key ?? null,
      leftWristbandColorKey: appearance.leftWristbandColor?.key ?? null
    },
    positionCapabilities: {
      pitcher: positionCapabilityDetails.pitcher.key,
      catcher: positionCapabilityDetails.catcher.key,
      firstBase: positionCapabilityDetails.firstBase.key,
      secondBase: positionCapabilityDetails.secondBase.key,
      thirdBase: positionCapabilityDetails.thirdBase.key,
      shortstop: positionCapabilityDetails.shortstop.key,
      leftField: positionCapabilityDetails.leftField.key,
      centerField: positionCapabilityDetails.centerField.key,
      rightField: positionCapabilityDetails.rightField.key
    },
    hitterAbilities: {
      trajectory: hitterAbilities.trajectory,
      contact: hitterAbilities.contact,
      power: hitterAbilities.power,
      runSpeed: hitterAbilities.runSpeed,
      armStrength: hitterAbilities.armStrength,
      fielding: hitterAbilities.fielding,
      errorResistance: hitterAbilities.errorResistance,
      hotZoneGrid: hitterAbilities.hotZones
    },
    pitcherAbilities: {
      topSpeed: pitcherAbilities.topSpeed,
      control: pitcherAbilities.control,
      stamina: pitcherAbilities.stamina,

      twoSeamTypeKey: pitcherAbilities.twoSeamType?.key ?? null,
      twoSeamMovement: pitcherAbilities.twoSeamType
        ? pitcherAbilities.twoSeamMovement
        : null,

      slider1TypeKey: pitcherAbilities.slider1Type?.key ?? null,
      slider1Movement: pitcherAbilities.slider1Type
        ? pitcherAbilities.slider1Movement
        : null,
      
      slider2TypeKey: pitcherAbilities.slider1Type
        ? pitcherAbilities.slider2Type?.key ?? null
        : null,
      slider2Movement: pitcherAbilities.slider1Type && pitcherAbilities.slider2Type
        ? pitcherAbilities.slider2Movement
        : null,

      curve1TypeKey: pitcherAbilities.curve1Type?.key ?? null,
      curve1Movement: pitcherAbilities.curve1Type
        ? pitcherAbilities.curve1Movement
        : null,

      curve2TypeKey: pitcherAbilities.curve1Type
        ? pitcherAbilities.curve2Type?.key ?? null
        : null,
      curve2Movement: pitcherAbilities.curve1Type && pitcherAbilities.curve2Type
        ? pitcherAbilities.curve2Movement
        : null,

      fork1TypeKey: pitcherAbilities.fork1Type?.key ?? null,
      fork1Movement: pitcherAbilities.fork1Type
        ? pitcherAbilities.fork1Movement
        : null,

      fork2TypeKey: pitcherAbilities.fork1Type
        ? pitcherAbilities.fork2Type?.key ?? null
        : null,
      fork2Movement: pitcherAbilities.fork1Type && pitcherAbilities.fork2Type
        ? pitcherAbilities.fork2Movement
        : null,

      sinker1TypeKey: pitcherAbilities.sinker1Type?.key ?? null,
      sinker1Movement: pitcherAbilities.sinker1Type
        ? pitcherAbilities.sinker1Movement
        : null,

      sinker2TypeKey: pitcherAbilities.sinker1Type
        ? pitcherAbilities.sinker2Type?.key ?? null
        : null,
      sinker2Movement: pitcherAbilities.sinker1Type && pitcherAbilities.sinker2Type
        ? pitcherAbilities.sinker2Movement
        : null,

      sinkingFastball1TypeKey: pitcherAbilities.sinkingFastball1Type?.key ?? null,
      sinkingFastball1Movement: pitcherAbilities.sinkingFastball1Type
        ? pitcherAbilities.sinkingFastball1Movement
        : null,
      
      sinkingFastball2TypeKey: pitcherAbilities.sinkingFastball1Type
        ? pitcherAbilities.sinkingFastball2Type?.key ?? null
        : null,
      sinkingFastball2Movement: pitcherAbilities.sinkingFastball1Type && pitcherAbilities.sinkingFastball2Type
        ? pitcherAbilities.sinkingFastball2Movement
        : null
    },
    specialAbilities: buildSpecialAbilitiesRequestFromState(specialAbilities)
  }
}