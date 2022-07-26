import { CommandFetcher } from "../../utils/commandFetcher";

export interface PlayerGenerationRequest {
  lsPlayerId: number;
  year: number;
}

export interface PlayerGenerationResponse {
  playerId: number
}

export class PlayerGenerationApiClient {
  private readonly commandName = 'PlayerGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: PlayerGenerationRequest): Promise<PlayerGenerationResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}