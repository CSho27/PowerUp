import { Dispatch } from "react"
import { KeyedCode } from "../shared/keyedCode"
import { SimpleCode } from "../shared/simpleCode"
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient"

export interface PlayerEditorState {
  personalDetails: PlayerPersonalDetails;
}

export type PlayerEditorAction =
| { type: 'updatePersonalDetails', action: PlayerPersonalDetailsAction }

export function PlayerEditorStateReducer(state: PlayerEditorState, action: PlayerEditorAction, context: PlayerPersonalDetailsContext): PlayerEditorState {
  switch(action.type) {
    case 'updatePersonalDetails':
      return {
        ...state,
        personalDetails: PlayerPersonalDetailReducer(state.personalDetails, action.action, context)
      }
  }
}

export interface PlayerPersonalDetails {
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  position: KeyedCode;
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
| { type: 'updatePosition', position: KeyedCode }
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
          : context.swingManRole
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

export function getInitialStateFromResponse(response: PlayerEditorResponse): PlayerEditorState {
  const { personalDetails } = response
  
  return {
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