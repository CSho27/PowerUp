import { CommandFetcher } from "../../utils/commandFetcher";

export interface TeamGenerationStatusRequest {
  generationStatusId: number
}

export interface TeamGenerationStatusResponse {
  currentAction: string;
  percentCompletion: number;
  estimatedTimeToCompletion: string;
  isComplete: boolean;
  completedTeamId: number | null;
}

export class TeamGenerationStatusApiClient {
  private readonly commandName = 'GetTeamGenerationStatus';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: TeamGenerationStatusRequest): Promise<TeamGenerationStatusResponse> => {
    return this.commandFetcher.execute(this.commandName, request, false);
  }
}