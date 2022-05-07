import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "./playerDetailsResponse";


export interface CopyPlayerRequest {
  playerId: number;
}

export class CopyPlayerApiClient {
  private readonly commandName = 'CopyPlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: CopyPlayerRequest): Promise<PlayerDetailsResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}