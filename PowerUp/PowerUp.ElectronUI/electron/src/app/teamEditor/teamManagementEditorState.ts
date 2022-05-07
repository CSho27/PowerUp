import { Dispatch } from "react";
import { remove, replace } from "../../utils/arrayUtils";
import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { PlayerRoleDefinitionDto, TeamRosterDetails } from "./loadTeamEditorApiClient";

export interface TeamManagementEditorState {
  mlbPlayers: PlayerRoleState[];
  aaaPlayers: PlayerRoleState[];  
}

export interface PlayerRoleState {
  playerDetails: PlayerDetails;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
}

export interface PlayerDetails {
  sourceType: EntitySourceType;
  canEdit: boolean;
  playerId: number;
  fullName: string;
  savedName: string;
  position: Position;
  positionAbbreviation: string;
  overall: number;
  batsAndThrows: string;
}

export type TeamManagementEditorAction =
| { type: 'addMLBPlayer', playerDetais: PlayerDetails }
| { type: 'addAAAPlayer', playerDetais: PlayerDetails }
| { type: 'updateMLBPlayer', playerId: number, roleAction: PlayerRoleAction  }
| { type: 'updateAAAPlayer', playerId: number, roleAction: PlayerRoleAction }
| { type: 'sendUp', playerId: number }
| { type: 'sendDown', playerId: number }

export type PlayerRoleAction =
| { type: 'replacePlayer', playerDetails: PlayerDetails }
| { type: 'toggleIsPinchHitter' }
| { type: 'toggleIsPinchRunner'}
| { type: 'toggleIsDefensiveReplacement' }
| { type: 'toggleIsDefensiveLiability' }

export function TeamManagementEditorReducer(state: TeamManagementEditorState, action: TeamManagementEditorAction): TeamManagementEditorState {
  switch(action.type) {
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

export function PlayerRoleStateReducer(state: PlayerRoleState, action: PlayerRoleAction): PlayerRoleState {
  switch(action.type) {
    case 'replacePlayer':
      return {
        ...state,
        playerDetails: action.playerDetails
      }
    case 'toggleIsPinchHitter':
      return {
        ...state,
        isPinchHitter: !state.isPinchHitter
      }
    case 'toggleIsPinchRunner':
      return {
        ...state,
        isPinchRunner: !state.isPinchRunner
      }
    case 'toggleIsDefensiveReplacement':
      return {
        ...state,
        isDefensiveReplacement: !state.isDefensiveReplacement
      }
    case 'toggleIsDefensiveLiability':
      return {
        ...state,
        isDefensiveLiability: !state.isDefensiveLiability
      }
  }
}

export function getInitialStateFromTeamRosterDetails(rosterDetails: TeamRosterDetails): TeamManagementEditorState {
  return {
    mlbPlayers: rosterDetails.mlbPlayers.map(toPlayerRoleState),
    aaaPlayers: rosterDetails.aaaPlayers.map(toPlayerRoleState)
  }
}

function toPlayerRoleState(roleDef: PlayerRoleDefinitionDto): PlayerRoleState {
  return {
    playerDetails: {
      sourceType: roleDef.sourceType,
      canEdit: roleDef.canEdit,
      playerId: roleDef.playerId,
      savedName: roleDef.savedName,
      fullName: roleDef.fullName,
      position: roleDef.position,
      positionAbbreviation: roleDef.positionAbbreviation,
      overall: roleDef.overall,
      batsAndThrows : roleDef.batsAndThrows
    },
    isPinchHitter: roleDef.isPinchHitter,
    isPinchRunner: roleDef.isPinchRunner,
    isDefensiveReplacement: roleDef.isDefensiveReplacement,
    isDefensiveLiability: roleDef.isDefensiveLiability
  }
}

function toDefaultRole(playerDetails: PlayerDetails): PlayerRoleState {
  return {
    playerDetails: playerDetails,
    isPinchHitter: false,
    isPinchRunner: false,
    isDefensiveReplacement: false,
    isDefensiveLiability: false
  }
}