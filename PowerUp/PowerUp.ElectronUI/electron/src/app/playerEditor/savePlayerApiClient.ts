import { CommandFetcher } from "../../utils/commandFetcher";

export interface SavePlayerRequest {
  playerId: number;
  personalDetails: PersonalDetailsRequest;
  positionCapabilities: PositionCapabilitiesRequest;
}

export interface PersonalDetailsRequest {
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  positionKey: string;
  pitcherTypeKey: string;
  voiceId: number;
  battingSideKey: string;
  battingStanceId: number;
  throwingArmKey: string;
  pitchingMechanicsId: number;
}

export interface PositionCapabilitiesRequest {
  pitcher: string;
  catcher: string;
  firstBase: string;
  secondBase: string;
  thirdBase: string;
  shortstop: string;
  leftField: string;
  centerField: string;
  rightField: string;
}

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: SavePlayerRequest): Promise<any> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}