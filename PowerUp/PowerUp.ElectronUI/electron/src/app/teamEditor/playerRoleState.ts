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
  orderInPitcherRole: number;
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

export function toPlayerRoleState(roleDef: PlayerRoleDefinitionDto, allPlayers: PitcherRoleCode[]): PlayerRoleState {
  return {
    playerDetails: toPlayerDetails(roleDef.details),
    isPinchHitter: roleDef.isPinchHitter,
    isPinchRunner: roleDef.isPinchRunner,
    isDefensiveReplacement: roleDef.isDefensiveReplacement,
    isDefensiveLiability: roleDef.isDefensiveLiability,
    pitcherRole: roleDef.pitcherRole,
    orderInPitcherRole: getOrderInPitcherRole(roleDef.playerId, roleDef.pitcherRole, allPlayers)
  }
}

export function toDefaultRole(playerDetails: PlayerDetails, allPlayers: PitcherRoleCode[]): PlayerRoleState {
  return {
    playerDetails: playerDetails,
    isPinchHitter: false,
    isPinchRunner: false,
    isDefensiveReplacement: false,
    isDefensiveLiability: false,
    pitcherRole: 'MopUpMan',
    orderInPitcherRole: getOrderInPitcherRole(playerDetails.playerId, 'MopUpMan', allPlayers)
  }
}

interface PitcherRoleCode {
  playerId: number;
  role: PitcherRole;
}

function getOrderInPitcherRole(playerId: number, role: PitcherRole, allPlayers: PitcherRoleCode[]): number {
  const pitchersInRole = allPlayers.filter(p => p.role === role);
  const playerIndex = pitchersInRole.findIndex(p => p.playerId === playerId)
  return playerIndex === -1
    ? pitchersInRole.length + 1
    : playerIndex + 1
}