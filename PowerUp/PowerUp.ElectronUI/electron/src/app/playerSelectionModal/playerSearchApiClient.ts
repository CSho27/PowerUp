import { CommandFetcher } from "../../utils/commandFetcher";
import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";

export interface PlayerSearchRequest {
  searchText: string;
}

export interface PlayerSearchResponse {
  results: PlayerSearchResultDto[];
}

export interface PlayerSearchResultDto {
  playerId: number;
  sourceType: EntitySourceType;
  canEdit: boolean;
  uniformNumber: string;
  savedName: string;
  formalDisplayName: string;
  informalDisplayName: string;
  position: Position;
  positionAbbreviation: string;
  batsAndThrows: string;
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