import { Dispatch } from "react"
import { KeyedCode } from "../shared/keyedCode"
import { PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode"
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient"

export interface PlayerEditorState {
  selectedTab: PlayerEditorTab;
  personalDetails: PlayerPersonalDetails;
  positionCapabilityDetails: PositionCapabilityDetails;
}

export type PlayerEditorTab = typeof playerEditorTabOptions[number];
export const playerEditorTabOptions = [
  'Personal',
  'Positions'
] as const

export type PlayerEditorAction =
| { type: 'updateSelectedTab', selectedTab: PlayerEditorTab }
| { type: 'updatePersonalDetails', action: PlayerPersonalDetailsAction }
| { type: 'updatePositionCapabilityDetails', action: PositionCapabilityDetailsAction }

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
        personalDetails: PlayerPersonalDetailReducer(state.personalDetails, action.action, context)
      }
    case 'updatePositionCapabilityDetails':
      return {
        ...state,
        positionCapabilityDetails: PositionCapabilityDetailsReducer(state.positionCapabilityDetails, action.action)
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

export type PositionCapabilityDetailsAction =
| { type: 'updatePitcherAbility', ability: KeyedCode }
| { type: 'updateCatcherAbility', ability: KeyedCode }
| { type: 'updateFirstBaseAbility', ability: KeyedCode }
| { type: 'updateSecondBaseAbility', ability: KeyedCode }
| { type: 'updateThirdBaseAbility', ability: KeyedCode }
| { type: 'updateShortstopAbility', ability: KeyedCode }
| { type: 'updateLeftFieldAbility', ability: KeyedCode }
| { type: 'updateCenterFieldAbility', ability: KeyedCode }
| { type: 'updateRightFieldAbility', ability: KeyedCode }

export function PositionCapabilityDetailsReducer(state: PositionCapabilityDetails, action: PositionCapabilityDetailsAction): PositionCapabilityDetails {
  switch(action.type) {
    case 'updatePitcherAbility':
      return {
        ...state,
        pitcher: action.ability
      }
    case 'updateCatcherAbility':
      return {
        ...state,
        catcher: action.ability
      }
    case 'updateFirstBaseAbility':
      return {
        ...state,
        firstBase: action.ability
      }
    case 'updateSecondBaseAbility':
      return {
        ...state,
        secondBase: action.ability
      }
    case 'updateThirdBaseAbility':
      return {
        ...state,
        thirdBase: action.ability
      }
    case 'updateShortstopAbility':
      return {
        ...state,
        shortstop: action.ability
      }
    case 'updateLeftFieldAbility':
      return {
        ...state,
        leftField: action.ability
      }
    case 'updateCenterFieldAbility':
      return {
        ...state,
        centerField: action.ability
      }
    case 'updateRightFieldAbility':
      return {
        ...state,
        rightField: action.ability
      }
  }
}

export function getPositionCapabilityDetailsReducer(state: PlayerEditorState, update: Dispatch<PlayerEditorAction>) : [PositionCapabilityDetails, Dispatch<PositionCapabilityDetailsAction>] {
  return [
    state.positionCapabilityDetails,
    (action: PositionCapabilityDetailsAction) => update({ type: 'updatePositionCapabilityDetails', action: action })
  ]
}

export function getInitialStateFromResponse(response: PlayerEditorResponse): PlayerEditorState {
  const { personalDetails, positionCapabilityDetails: positionCapabilities } = response
  
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
    }
  }
}