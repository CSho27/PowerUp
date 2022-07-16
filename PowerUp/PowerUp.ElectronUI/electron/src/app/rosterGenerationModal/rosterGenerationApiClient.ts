import { CommandFetcher } from "../../utils/commandFetcher";

export interface RosterGenerationRequest {
  year: number;
}

export interface RosterGenerationResponse {
  generationStatusId: number
}

export class RosterGenerationApiClient {
  private readonly commandName = 'RosterGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: RosterGenerationRequest): Promise<RosterGenerationResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}