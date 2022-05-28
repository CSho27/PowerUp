import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceTeamWithNewTeamRequest {
  rosterId: number
  mlbPPTeam: string;
}

export class ReplaceTeamWithNewTeamApiClient {
  private readonly commandName = 'ReplaceTeamWithNewTeam';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceTeamWithNewTeamRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}