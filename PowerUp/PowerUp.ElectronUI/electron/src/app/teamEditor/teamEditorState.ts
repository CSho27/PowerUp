import { Dispatch } from "react";
import { remove, replace } from "../../utils/arrayUtils";
import { LoadTeamEditorResponse, PlayerRoleDefinitionDto } from "./loadTeamEditorApiClient";
import { PlayerDetails, PlayerRoleAction, PlayerRoleState, PlayerRoleStateReducer, toDefaultRole, toPlayerRoleState } from "./playerRoleState";

export interface TeamEditorState {
  lastSavedDetails: TeamEditorDetails;
  currentDetails: TeamEditorDetails;
  dateLastSaved: Date | undefined;
  selectedTab: TeamEditorTab;
  isEditingName: boolean;
}

export type TeamEditorTab = typeof teamEditorTabOptions[number];
export const teamEditorTabOptions = [
  'Management',
  'No DH Lineup',
  'DH Lineup',
  'Pitcher Roles'
] as const

export type TeamEditorAction =
| { type: 'updateDetails', detailsAction: TeamEditorDetailsAction }
| { type: 'undoChanges' }
| { type: 'updateFromSave' }
| { type: 'updateSelectedTab', selectedTab: TeamEditorTab }
| { type: 'toggleIsEditingName' }

export function TeamEditorReducer(state: TeamEditorState, action: TeamEditorAction): TeamEditorState {
  switch(action.type) {
    case 'updateDetails':
      return {
        ...state,
        currentDetails: TeamEditorDetailsReducer(state.currentDetails, action.detailsAction)
      }
    case 'undoChanges':
      return {
        ...state,
        currentDetails: state.lastSavedDetails
      }
    case 'updateFromSave':
      return {
        ...state,
        lastSavedDetails: state.currentDetails,
        dateLastSaved: new Date()
      }
    case 'updateSelectedTab':
      return {
        ...state,
        selectedTab: action.selectedTab
      }
    case 'toggleIsEditingName':
      return {
        ...state,
        isEditingName: !state.isEditingName
      }
  }
}

export interface TeamEditorDetails {
  teamName: string;
  mlbPlayers: PlayerRoleState[];
  aaaPlayers: PlayerRoleState[];  
}

export type TeamEditorDetailsAction =
| { type: 'updateTeamName', teamName: string }
| { type: 'addMLBPlayer', playerDetais: PlayerDetails }
| { type: 'addAAAPlayer', playerDetais: PlayerDetails }
| { type: 'updateMLBPlayer', playerId: number, roleAction: PlayerRoleAction  }
| { type: 'updateAAAPlayer', playerId: number, roleAction: PlayerRoleAction }
| { type: 'sendUp', playerId: number }
| { type: 'sendDown', playerId: number }

export function TeamEditorDetailsReducer(state: TeamEditorDetails, action: TeamEditorDetailsAction): TeamEditorDetails {
  switch(action.type) {
    case 'updateTeamName':
      return {
        ...state,
        teamName: action.teamName
      }
      case 'addMLBPlayer':
        return {
          ...state,
          mlbPlayers: [...state.mlbPlayers, toDefaultRole(action.playerDetais)]
        }
      case 'addAAAPlayer':
        return {
          ...state,
          aaaPlayers: [...state.aaaPlayers, toDefaultRole(action.playerDetais)]
        }
      case 'updateMLBPlayer':
        return {
          ...state,
          mlbPlayers: replace(
            state.mlbPlayers, 
            p => p.playerDetails.playerId === action.playerId,
            p => PlayerRoleStateReducer(p, action.roleAction)
          )
        }
      case 'updateAAAPlayer':
        return {
          ...state,
          aaaPlayers: replace(
            state.aaaPlayers,
            p => p.playerDetails.playerId === action.playerId,
            p => PlayerRoleStateReducer(p, action.roleAction)
          )
        }
      case 'sendUp':
        const player = state.aaaPlayers.find(p => p.playerDetails.playerId === action.playerId)!;
  
        return {
          ...state,
          aaaPlayers: remove(state.aaaPlayers, p => p.playerDetails.playerId === action.playerId),
          mlbPlayers: [...state.mlbPlayers, player]
        }
      case 'sendDown': {
        const player = state.mlbPlayers.find(p => p.playerDetails.playerId === action.playerId)!;
  
        return {
          ...state,
          aaaPlayers: [...state.aaaPlayers, player],
          mlbPlayers: remove(state.mlbPlayers, p => p.playerDetails.playerId === action.playerId)
        }
      }
  }
}

export function getDetailsReducer(state: TeamEditorState, update: Dispatch<TeamEditorAction>): [TeamEditorDetails, Dispatch<TeamEditorDetailsAction>] {
  return [
    state.currentDetails,
    (action: TeamEditorDetailsAction) => update({ type: 'updateDetails', detailsAction: action })
  ]
}


export function getInitialStateFromResponse(response: LoadTeamEditorResponse): TeamEditorState {
  const currentDetails: TeamEditorDetails = {
    teamName: response.currentDetails.name,
    mlbPlayers: response.currentDetails.mlbPlayers.map(toPlayerRoleState),
    aaaPlayers: response.currentDetails.aaaPlayers.map(toPlayerRoleState)
  }

  const lastSavedDetails: TeamEditorDetails = {
    teamName: response.lastSavedDetails.name,
    mlbPlayers: response.lastSavedDetails.mlbPlayers.map(toPlayerRoleState),
    aaaPlayers: response.lastSavedDetails.aaaPlayers.map(toPlayerRoleState)
  }

  return {
    lastSavedDetails: lastSavedDetails,
    currentDetails: currentDetails,
    dateLastSaved: response.lastSaved 
      ? new Date(response.lastSaved)
      : undefined,
    selectedTab: 'Management',
    isEditingName: false
  }
}