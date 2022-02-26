import { PositionType } from "../../components/textBubble/textBubble";
import { KeyedCode } from "../shared/keyedCode";

export interface RosterEditorResponse {
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export interface RosterDetails {
  name: string
  teams: TeamDetails[];
}

export interface TeamDetails {
  teamKey: string;
  name: string;
  powerProsName: string;
  division: string;
  hitters: HitterDetails[];
  pitchers: PitcherDetails[];
}

export interface PlayerDetails {
  playerKey: string;
  savedName: string;
  uniformNumber: string;
  positionType: PositionType;
  position: string;
  overall: string;
  batsAndThrows: string;
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