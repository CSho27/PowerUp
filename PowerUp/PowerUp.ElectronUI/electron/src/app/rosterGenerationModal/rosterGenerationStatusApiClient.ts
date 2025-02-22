import { CommandFetcher } from "../../utils/commandFetcher";

export interface RosterGenerationStatusRequest {
  generationStatusId: number
}

export interface RosterGenerationStatusResponse {
  currentTeamAction: string;
  currentPlayerAction: string;
  percentCompletion: number;
  estimatedTimeToCompletion: string;
  isComplete: boolean;
  isFailed: boolean;
  completedRosterId: number | null;
}

export class RosterGenerationStatusApiClient {
  private readonly commandName = 'GetRosterGenerationStatus';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: RosterGenerationStatusRequest): Promise<RosterGenerationStatusResponse> => {
    return this.commandFetcher.execute(this.commandName, request, false);
  }
}