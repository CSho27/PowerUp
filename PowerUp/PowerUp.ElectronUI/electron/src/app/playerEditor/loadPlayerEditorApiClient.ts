import { CommandFetcher } from "../../utils/commandFetcher";
import { KeyedCode } from "../shared/keyedCode";
import { PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode";

export interface LoadPlayerEditorRequest {
  playerKey: string;
}

export interface PlayerEditorResponse {
  options: PlayerEditorOptions; 
  personalDetails: PlayerPersonalDetails;
}

export interface PlayerEditorOptions {
  voiceOptions: SimpleCode[];
  positions: PositionCode[];
  pitcherTypes: KeyedCode[];
  battingSideOptions: KeyedCode[];
  battingStanceOptions: SimpleCode[];
  throwingArmOptions: KeyedCode[];
  pitchingMechanicsOptions: SimpleCode[];
}

export interface PlayerPersonalDetails {
  firstName: string;
  lastName: string;
  isSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  position: PositionCode;
  pitcherType: KeyedCode;
  voiceId: SimpleCode;
  battingSide: KeyedCode;
  battingStance: SimpleCode;
  throwingArm: KeyedCode;
  pitchingMechanics: SimpleCode;
}

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