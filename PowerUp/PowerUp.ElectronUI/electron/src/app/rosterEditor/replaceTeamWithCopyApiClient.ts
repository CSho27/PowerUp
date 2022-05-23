import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceTeamWithCopyRequest {
  rosterId: number
  mlbPPTeam: string;
}

export class ReplaceTeamWithCopyApiClient {
  private readonly commandName = 'ReplaceTeamWithCopy';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceTeamWithCopyRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}