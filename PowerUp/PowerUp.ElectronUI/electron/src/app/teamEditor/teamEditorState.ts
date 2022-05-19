import { Dispatch } from "react";
import { insert, remove, replace } from "../../utils/arrayUtils";
import { Position } from "../shared/positionCode";
import { LoadTeamEditorResponse, PlayerRoleDefinitionDto } from "./loadTeamEditorApiClient";
import { PitcherRole, PlayerDetails, PlayerRoleAction, PlayerRoleState, PlayerRoleStateReducer, toDefaultRole, toPlayerRoleState } from "./playerRoleState";

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
| { type: 'updatePitcherRole', playerId: number, role: PitcherRole, orderInRole: number }
| { type: 'reorderNoDHLineup', playerIdentifier: number | 'Pitcher', currentOrderInLineup: number, newOrderInLineup: number }
| { type: 'reorderDHLineup', playerIdentifier: number | 'Pitcher', currentOrderInLineup: number, newOrderInLineup: number }

export function TeamEditorDetailsReducer(state: TeamEditorDetails, action: TeamEditorDetailsAction): TeamEditorDetails {
  switch(action.type) {
    case 'updateTeamName':
      return {
        ...state,
        teamName: action.teamName
      }
    case 'addMLBPlayer': {
      const pitcherRoleList = state.mlbPlayers.map(r => ({ playerId: r.playerDetails.playerId, role: r.pitcherRole }));
      return {
        ...state,
        mlbPlayers: [...state.mlbPlayers, toDefaultRole(action.playerDetais, pitcherRoleList)]
      }
    }
    case 'addAAAPlayer': {
      const pitcherRoleList = state.aaaPlayers.map(r => ({ playerId: r.playerDetails.playerId, role: r.pitcherRole }));
      return {
        ...state,
        aaaPlayers: [...state.aaaPlayers, toDefaultRole(action.playerDetais, pitcherRoleList)]
      }
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
    case 'sendUp': {
      const player = state.aaaPlayers.find(p => p.playerDetails.playerId === action.playerId)!;

      return {
        ...state,
        aaaPlayers: remove(state.aaaPlayers, p => p.playerDetails.playerId === action.playerId),
        mlbPlayers: [...state.mlbPlayers, player]
      }
    }
    case 'sendDown': {
      const player = state.mlbPlayers.find(p => p.playerDetails.playerId === action.playerId)!;

      return {
        ...state,
        aaaPlayers: [...state.aaaPlayers, player],
        mlbPlayers: remove(state.mlbPlayers, p => p.playerDetails.playerId === action.playerId)
      }
    }
    case 'updatePitcherRole': {
      const player = state.mlbPlayers.find(p => p.playerDetails.playerId === action.playerId)!;
      return {
        ...state,
        mlbPlayers: state.mlbPlayers.map(p => updateRoleAndOrder(p, player, action.role, action.orderInRole))
      }
    }
    case 'reorderNoDHLineup': {
      const playerId = action.playerIdentifier === 'Pitcher'
        ? undefined
        : action.playerIdentifier

      return {
        ...state,
        mlbPlayers: state.mlbPlayers.map(p => updateLineupOrder(p, playerId, action.currentOrderInLineup, action.newOrderInLineup, false))
      };
    }
    case 'reorderDHLineup': {
      const playerId = action.playerIdentifier === 'Pitcher'
        ? undefined
        : action.playerIdentifier

      return {
        ...state,
        mlbPlayers: state.mlbPlayers.map(p => updateLineupOrder(p, playerId, action.currentOrderInLineup, action.newOrderInLineup, true))
      };
    }
  }
}

function updateRoleAndOrder(player: PlayerRoleState, movingPlayer: PlayerRoleState, movingPlayerNewRole: PitcherRole, movingPlayerNewOrder: number): PlayerRoleState {
  if(player.playerDetails.playerId === movingPlayer.playerDetails.playerId)
    return { ...player, pitcherRole: movingPlayerNewRole, orderInPitcherRole: movingPlayerNewOrder };

  if(player.pitcherRole !== movingPlayer.pitcherRole && player.pitcherRole !== movingPlayerNewRole)
    return player;

  const movingPlayerCurrentOrder = movingPlayer.orderInPitcherRole;
  const isBeingAddedToRole = player.pitcherRole !== movingPlayer.pitcherRole
    && player.pitcherRole === movingPlayerNewRole;
  const isBeingRemovedFromRole = player.pitcherRole === movingPlayer.pitcherRole
    && player.pitcherRole !== movingPlayerNewRole;

  if(isBeingAddedToRole) {
    return movingPlayerNewOrder <= player.orderInPitcherRole
      ? incrementOrderInRole(player)
      : player;
  } else if(isBeingRemovedFromRole) {
    return movingPlayerCurrentOrder < player.orderInPitcherRole
      ? decrementOrderInRole(player)
      : player;
  } else if(movingPlayerCurrentOrder > player.orderInPitcherRole && movingPlayerNewOrder <= player.orderInPitcherRole) {
    return incrementOrderInRole(player);
  } else if(movingPlayerCurrentOrder < player.orderInPitcherRole && movingPlayerNewOrder >= player.orderInPitcherRole) {
    return decrementOrderInRole(player);
  } else {
    return player;
  }
}

function incrementOrderInRole(player: PlayerRoleState): PlayerRoleState {
  return { ...player, orderInPitcherRole: player.orderInPitcherRole + 1 };
}

function decrementOrderInRole(player: PlayerRoleState): PlayerRoleState {
  return { ...player, orderInPitcherRole: player.orderInPitcherRole - 1 };
}

function updateLineupOrder(player: PlayerRoleState, movingPlayerId: number | undefined, movingPlayerCurrentOrder: number, movingPlayerNewOrder: number, useDh: boolean): PlayerRoleState {
  if(player.playerDetails.playerId === movingPlayerId) {
    return useDh
    ? {...player, orderInDHLineup: movingPlayerNewOrder }
    : {...player, orderInNoDHLineup: movingPlayerNewOrder }
  }

  const thisPlayerCurrentOrder = useDh
    ? player.orderInDHLineup!
    : player.orderInNoDHLineup!;

  if(movingPlayerCurrentOrder > thisPlayerCurrentOrder && movingPlayerNewOrder <= thisPlayerCurrentOrder) {
    return incrementOrderInLineup(player, useDh);
  } else if(movingPlayerCurrentOrder < thisPlayerCurrentOrder && movingPlayerNewOrder >= thisPlayerCurrentOrder) {
    return decrementOrderInLineup(player, useDh);
  } else {
    return player;
  }
}

function incrementOrderInLineup(player: PlayerRoleState, useDh: boolean): PlayerRoleState {
  return useDh
    ? { ...player, orderInDHLineup: player.orderInDHLineup! + 1 }
    : { ...player, orderInNoDHLineup: player.orderInNoDHLineup! + 1 };
}

function decrementOrderInLineup(player: PlayerRoleState, useDh: boolean): PlayerRoleState {
  return useDh
    ? { ...player, orderInDHLineup: player.orderInDHLineup! - 1 }
    : { ...player, orderInNoDHLineup: player.orderInNoDHLineup! - 1 };
}

export function getDetailsReducer(state: TeamEditorState, update: Dispatch<TeamEditorAction>): [TeamEditorDetails, Dispatch<TeamEditorDetailsAction>] {
  return [
    state.currentDetails,
    (action: TeamEditorDetailsAction) => update({ type: 'updateDetails', detailsAction: action })
  ]
}


export function getInitialStateFromResponse(response: LoadTeamEditorResponse): TeamEditorState {
  const currentMLBPitcherRoleList = response.currentDetails.mlbPlayers.map(r => ({ playerId: r.playerId, role: r.pitcherRole }));
  const currentAAAPitcherRoleList = response.currentDetails.aaaPlayers.map(r => ({ playerId: r.playerId, role: r.pitcherRole }));
  const currentDetails: TeamEditorDetails = {
    teamName: response.currentDetails.name,
    mlbPlayers: response.currentDetails.mlbPlayers.map(p => toPlayerRoleState(p, currentMLBPitcherRoleList, response.currentDetails.noDHLineup, response.currentDetails.dhLineup)),
    aaaPlayers: response.currentDetails.aaaPlayers.map(p => toPlayerRoleState(p, currentAAAPitcherRoleList, response.currentDetails.noDHLineup, response.currentDetails.dhLineup))
  }

  const lastSavedMLBPitcherRoleList = response.currentDetails.mlbPlayers.map(r => ({ playerId: r.playerId, role: r.pitcherRole }));
  const lastSavedAAAPitcherRoleList = response.currentDetails.aaaPlayers.map(r => ({ playerId: r.playerId, role: r.pitcherRole }));
  const lastSavedDetails: TeamEditorDetails = {
    teamName: response.lastSavedDetails.name,
    mlbPlayers: response.lastSavedDetails.mlbPlayers.map(p => toPlayerRoleState(p, lastSavedMLBPitcherRoleList, response.lastSavedDetails.noDHLineup, response.lastSavedDetails.dhLineup)),
    aaaPlayers: response.lastSavedDetails.aaaPlayers.map(p => toPlayerRoleState(p, lastSavedAAAPitcherRoleList, response.lastSavedDetails.noDHLineup, response.lastSavedDetails.dhLineup))
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