import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceWithNewPlayerRequest {
  teamId: number;
  playerToReplaceId: number
}

export class ReplaceWithNewPlayerApiClient {
  private readonly commandName = 'ReplaceWithNewPlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceWithNewPlayerRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}