import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";

export interface DraftPoolGenerationRequest {
  size: number;
}

export interface DraftPoolGenerationResponse {
  players: PlayerDetailsResponse[];
}


export class DraftPoolApiClient {
  private readonly commandName = 'DraftPoolGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: DraftPoolGenerationRequest): Promise<DraftPoolGenerationResponse> => {
    //return new Promise(r => r(testPage));
    return this.commandFetcher.execute(this.commandName, request, false);
  }
}