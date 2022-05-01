import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplacePlayerWithCopyRequest {
  teamId: number;
  playerId: number
}

export class ReplacePlayerWithCopyApiClient {
  private readonly commandName = 'ReplacePlayerWithCopy';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplacePlayerWithCopyRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}