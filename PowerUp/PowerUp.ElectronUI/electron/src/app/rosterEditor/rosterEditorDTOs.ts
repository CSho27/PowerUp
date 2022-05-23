import { EntitySourceType } from "../shared/entitySourceType";
import { KeyedCode } from "../shared/keyedCode";
import { Position } from "../shared/positionCode";

export interface RosterEditorResponse {
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export interface RosterDetails {
  rosterId: number;
  name: string
  teams: TeamDetails[];
}

export interface TeamDetails {
  teamId: number;
  name: string;
  powerProsName: string;
  powerProsTeam: string;
  division: string;
  hitters: HitterDetails[];
  pitchers: PitcherDetails[];
}

export interface PlayerDetails {
  playerId: number;
  savedName: string;
  uniformNumber: string;
  position: Position;
  positionAbbreviation: string;
  overall: string;
  batsAndThrows: string;
  sourceType: EntitySourceType;
  canEdit: boolean;
}

export interface HitterDetails extends PlayerDetails {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
}

export interface PitcherDetails extends PlayerDetails {
  pitcherType: string;
  topSpeed: number;
  control: number;
  stamina: number;
  breakingBall1: string;
  breakingBall2: string;
  breakingBall3: string;
}