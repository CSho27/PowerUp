import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "./playerDetailsResponse";


export interface CreatePlayerRequest {
  isPitcher: boolean;
}

export class CreatePlayerApiClient {
  private readonly commandName = 'CreatePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: CreatePlayerRequest): Promise<PlayerDetailsResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}