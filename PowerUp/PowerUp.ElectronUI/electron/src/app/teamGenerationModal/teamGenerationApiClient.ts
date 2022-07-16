import { CommandFetcher } from "../../utils/commandFetcher";

export interface TeamGenerationRequest {
  lsTeamId: number;
  year: number;
  teamName: string;
}

export interface TeamGenerationResponse {
  generationStatusId: number
}

export class TeamGenerationApiClient {
  private readonly commandName = 'TeamGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: TeamGenerationRequest): Promise<TeamGenerationResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}