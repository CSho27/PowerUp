import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { PlayerRoleDefinitionDto } from "./loadTeamEditorApiClient";
import { toPlayerDetails } from "./playerDetailsResponse";

export interface PlayerRoleState {
  playerDetails: PlayerDetails;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
  pitcherRole: PitcherRole;
}

export type PitcherRole = 
| 'Starter'
| 'SwingMan'
| 'LongReliever'
| 'MiddleReliever'
| 'SituationalLefty'
| 'MopUpMan'
| 'SetupMan'
| 'Closer'

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
  pitcherType: string;
  throwingArm: string;
  topSpeed: string;
  control: string;
  stamina: string;
}

export type PlayerRoleAction =
| { type: 'replacePlayer', playerDetails: PlayerDetails }
| { type: 'toggleIsPinchHitter' }
| { type: 'toggleIsPinchRunner'}
| { type: 'toggleIsDefensiveReplacement' }
| { type: 'toggleIsDefensiveLiability' }

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

export function toPlayerRoleState(roleDef: PlayerRoleDefinitionDto): PlayerRoleState {
  return {
    playerDetails: toPlayerDetails(roleDef.details),
    isPinchHitter: roleDef.isPinchHitter,
    isPinchRunner: roleDef.isPinchRunner,
    isDefensiveReplacement: roleDef.isDefensiveReplacement,
    isDefensiveLiability: roleDef.isDefensiveLiability,
    pitcherRole: roleDef.pitcherRole
  }
}

export function toDefaultRole(playerDetails: PlayerDetails): PlayerRoleState {
  return {
    playerDetails: playerDetails,
    isPinchHitter: false,
    isPinchRunner: false,
    isDefensiveReplacement: false,
    isDefensiveLiability: false,
    pitcherRole: 'MopUpMan'
  }
}