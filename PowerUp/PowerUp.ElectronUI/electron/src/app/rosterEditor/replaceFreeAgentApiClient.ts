import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceFreeAgentRequest {
  rosterId: number;
  playerToReplaceId: number;
  playerToInsertId: number;
}

export class ReplaceFreeAgentApiClient {
  private readonly commandName = 'ReplaceFreeAgent';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceFreeAgentRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}