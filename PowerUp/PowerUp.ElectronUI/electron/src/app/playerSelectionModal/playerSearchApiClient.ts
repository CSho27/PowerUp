import { CommandFetcher } from "../../utils/commandFetcher";
import { EntitySourceType } from "../shared/entitySourceType";

export interface PlayerSearchRequest {
  searchText: string;
}

export interface PlayerSearchResponse {
  results: PlayerSearchResultDto[];
}

export interface PlayerSearchResultDto {
  playerId: number;
  sourceType: EntitySourceType;
  uniformNumber: string;
  name: string;
  position: string;
  batAndThrows: string;
  overall: number;
}

export class PlayerSearchApiClient {
  private readonly commandName = 'PlayerSearch';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: PlayerSearchRequest): Promise<PlayerSearchResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}