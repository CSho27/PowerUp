import { CommandFetcher } from "../../utils/commandFetcher";

export interface SavePlayerRequest {
  playerKey: string;
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