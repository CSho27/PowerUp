import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { LineupSlotDto, PlayerRoleDefinitionDto } from "./loadTeamEditorApiClient";
import { toPlayerDetails } from "./playerDetailsResponse";

export interface PlayerRoleState {
  playerDetails: PlayerDetails;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
  pitcherRole: PitcherRole;
  orderInPitcherRole: number;
  orderInNoDHLineup: number | undefined;
  positionInNoDHLineup: Position | undefined;
  orderInDHLineup: number | undefined;
  positionInDHLineup: Position | undefined;
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

export function toPlayerRoleState(
  roleDef: PlayerRoleDefinitionDto, 
  allPlayers: PitcherRoleCode[], 
  noDHLineup: LineupSlotDto[],
  dhLineup: LineupSlotDto[]
): PlayerRoleState {
  const noDhIndex = noDHLineup.findIndex(l => l.playerId === roleDef.playerId);
  const noDhPosition = noDHLineup[noDhIndex]?.position;

  const dhIndex = dhLineup.findIndex(l => l.playerId === roleDef.playerId);
  const dhPosition = dhLineup[dhIndex]?.position;

  return {
    playerDetails: toPlayerDetails(roleDef.details),
    isPinchHitter: roleDef.isPinchHitter,
    isPinchRunner: roleDef.isPinchRunner,
    isDefensiveReplacement: roleDef.isDefensiveReplacement,
    isDefensiveLiability: roleDef.isDefensiveLiability,
    pitcherRole: roleDef.pitcherRole,
    orderInPitcherRole: getOrderInPitcherRole(roleDef.playerId, roleDef.pitcherRole, allPlayers),
    orderInNoDHLineup: noDhIndex !== -1
      ? noDhIndex + 1
      : undefined,
    positionInNoDHLineup: noDhPosition,
    orderInDHLineup: dhIndex !== -1
      ? dhIndex + 1
      : undefined,
    positionInDHLineup: dhPosition
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
    orderInPitcherRole: getOrderInPitcherRole(playerDetails.playerId, 'MopUpMan', allPlayers),
    orderInNoDHLineup: undefined,
    positionInNoDHLineup: undefined,
    orderInDHLineup: undefined,
    positionInDHLineup: undefined
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