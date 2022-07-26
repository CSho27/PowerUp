import { CommandFetcher } from "../../utils/commandFetcher";

export interface PlayerInfoRequest {
  lsPlayerId: number;
}

export interface PlayerInfoResponse {
  startYear: number | null;
  endYear: number | null;
}

export class PlayerInfoApiClient {
  private readonly commandName = 'GetPlayerInfo';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: PlayerInfoRequest): Promise<PlayerInfoResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}