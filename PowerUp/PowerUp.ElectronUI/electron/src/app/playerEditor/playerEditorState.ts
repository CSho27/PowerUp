import { Dispatch } from "react"
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient"

export interface PlayerEditorState {
  personalDetails: PlayerPersonalDetails;
}

export type PlayerEditorAction =
| { type: 'updatePersonalDetails', action: PlayerPersonalDetailsAction }

export function PlayerEditorStateReducer(state: PlayerEditorState, action: PlayerEditorAction): PlayerEditorState {
  switch(action.type) {
    case 'updatePersonalDetails':
      return {
        ...state,
        personalDetails: PlayerPersonalDetailReducer(state.personalDetails, action.action)
      }
  }
}

export interface PlayerPersonalDetails {
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
}

export type PlayerPersonalDetailsAction =
| { type: 'updateFirstName', firstName: string }
| { type: 'updateLastName', lastName: string }
| { type: 'toggleUseSpecialSavedName' }
| { type: 'updateSavedName', savedName: string }
| { type: 'updateUniformNumber', uniformNumber: string }

export function PlayerPersonalDetailReducer(state: PlayerPersonalDetails, action: PlayerPersonalDetailsAction): PlayerPersonalDetails {
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
      uniformNumber: personalDetails.uniformNumber
    }
  }
}