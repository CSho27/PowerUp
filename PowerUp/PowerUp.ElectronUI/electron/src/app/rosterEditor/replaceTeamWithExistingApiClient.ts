import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceTeamWithExistingRequest {
  rosterId: number
  mlbPPTeamToReplace: string;
  teamToInsertId: number;
}

export class ReplaceTeamWithExistingApiClient {
  private readonly commandName = 'ReplaceTeamWithExisting';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceTeamWithExistingRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}