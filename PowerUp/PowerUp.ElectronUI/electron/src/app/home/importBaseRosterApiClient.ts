import { PositionType } from "../../components/textBubble/textBubble";
import { CommandFetcher } from "../../utils/commandFetcher";

export interface LoadBaseRequest {
  throwaway: number;
}

export interface LoadBaseResponse {
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export interface KeyedCode {
  key: string;
  name: string;
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

export class ImportBaseRosterApiClient {
  private readonly commandName = 'LoadBaseGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: LoadBaseRequest): Promise<LoadBaseResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}