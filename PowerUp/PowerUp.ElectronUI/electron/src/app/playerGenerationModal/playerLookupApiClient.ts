import { CommandFetcher } from "../../utils/commandFetcher";

export interface PlayerLookupRequest {
  searchText: string;
}

export interface PlayerLookupResponse {
  results: PlayerLookupResultDto[];
}

export interface PlayerLookupResultDto {
  lsPlayerId: number;
  informalDisplayName: string;
  position: string;
  mostRecentTeam: string;
  debutYear: number;
}

export class PlayerLookupApiClient {
  private readonly commandName = 'PlayerLookup';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: PlayerLookupRequest): Promise<PlayerLookupResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}