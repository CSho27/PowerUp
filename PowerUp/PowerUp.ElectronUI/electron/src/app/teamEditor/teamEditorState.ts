import { Dispatch } from "react";
import { LoadTeamEditorResponse } from "./loadTeamEditorApiClient";
import { getInitialStateFromTeamRosterDetails, TeamManagementEditorAction, TeamManagementEditorReducer, TeamManagementEditorState } from "./teamManagementEditorState";

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
  teamManagmentState: TeamManagementEditorState;
}

export type TeamEditorDetailsAction =
| { type: 'updateTeamName', teamName: string }
| { type: 'updateTeamManagement', managementAction: TeamManagementEditorAction }

export function TeamEditorDetailsReducer(state: TeamEditorDetails, action: TeamEditorDetailsAction): TeamEditorDetails {
  switch(action.type) {
    case 'updateTeamName':
      return {
        ...state,
        teamName: action.teamName
      }
    case 'updateTeamManagement':
      return {
        ...state,
        teamManagmentState: TeamManagementEditorReducer(state.teamManagmentState, action.managementAction)
      }
  }
}

export function getDetailsReducer(state: TeamEditorState, update: Dispatch<TeamEditorAction>): [TeamEditorDetails, Dispatch<TeamEditorDetailsAction>] {
  return [
    state.currentDetails,
    (action: TeamEditorDetailsAction) => update({ type: 'updateDetails', detailsAction: action })
  ]
}

export function getTeamManagementReducer(state: TeamEditorDetails, update: Dispatch<TeamEditorDetailsAction>): [TeamManagementEditorState, Dispatch<TeamManagementEditorAction>] {
  return [
    state.teamManagmentState,
    (action: TeamManagementEditorAction) => update({ type: 'updateTeamManagement', managementAction: action })
  ]
}

export function getInitialStateFromResponse(response: LoadTeamEditorResponse): TeamEditorState {
  const details: TeamEditorDetails = {
    teamName: response.name,
    teamManagmentState: getInitialStateFromTeamRosterDetails(response.teamRosterDetails)
  }

  return {
    lastSavedDetails: details,
    currentDetails: details,
    dateLastSaved: undefined,
    selectedTab: 'Management',
    isEditingName: false
  }
}