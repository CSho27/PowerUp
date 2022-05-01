import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceWithExistingPlayerRequest {
  teamId: number;
  playerToReplaceId: number;
  playerToInsertId: number;
}

export class ReplaceWithExistingPlayerApiClient {
  private readonly commandName = 'ReplaceWithExistingPlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ReplaceWithExistingPlayerRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}