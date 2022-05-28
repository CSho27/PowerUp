import { CommandFetcher } from "../../utils/commandFetcher";
import { EntitySourceType } from "../shared/entitySourceType";

export interface TeamSearchRequest {
  searchText: string;
}

export interface TeamSearchResponse {
  results: TeamSearchResultDto[];
}

export interface TeamSearchResultDto {
  teamId: number;
  sourceType: EntitySourceType;
  name: string;
  overall: number;
}

export class TeamSearchApiClient {
  private readonly commandName = 'TeamSearch';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: TeamSearchRequest): Promise<TeamSearchResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}