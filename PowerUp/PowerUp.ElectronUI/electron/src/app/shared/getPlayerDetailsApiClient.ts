import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";


export interface GetPlayerDetailsRequest {
  playerId: number;
}

export class GetPlayerDetailsApiClient {
  private readonly commandName = 'GetPlayerDetails';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: GetPlayerDetailsRequest): Promise<PlayerDetailsResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}