import { CommandFetcher } from "../../utils/commandFetcher";
import { KeyedCode } from "../shared/keyedCode";
import { PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode";

export interface LoadPlayerEditorRequest {
  playerId: number;
}

export interface PlayerEditorResponse {
  options: PlayerEditorOptions; 
  personalDetails: PlayerPersonalDetailsDto;
  positionCapabilityDetails: PositionCapabilityDetailsDto;
}

export interface PlayerEditorOptions {
  voiceOptions: SimpleCode[];
  positions: PositionCode[];
  pitcherTypes: KeyedCode[];
  battingSideOptions: KeyedCode[];
  battingStanceOptions: SimpleCode[];
  throwingArmOptions: KeyedCode[];
  pitchingMechanicsOptions: SimpleCode[];
  positionCapabilityOptions: KeyedCode[];
}

export interface PlayerPersonalDetailsDto {
  firstName: string;
  lastName: string;
  isSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  position: PositionCode;
  pitcherType: KeyedCode;
  voice: SimpleCode;
  battingSide: KeyedCode;
  battingStance: SimpleCode;
  throwingArm: KeyedCode;
  pitchingMechanics: SimpleCode;
}

export interface PositionCapabilityDetailsDto {
  pitcher: KeyedCode;
  catcher: KeyedCode;
  firstBase: KeyedCode;
  secondBase: KeyedCode;
  thirdBase: KeyedCode;
  leftField: KeyedCode;
  centerField: KeyedCode;
  rightField: KeyedCode;
}

export type Grade = 
| 'A'
| 'B'
| 'C'
| 'D'
| 'E'
| 'F'
| 'G'

export class LoadPlayerEditorApiClient {
  private readonly commandName = 'LoadPlayerEditor';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: LoadPlayerEditorRequest): Promise<PlayerEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}