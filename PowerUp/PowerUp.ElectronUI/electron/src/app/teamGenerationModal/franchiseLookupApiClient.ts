import { CommandFetcher } from "../../utils/commandFetcher";

export interface FranchiseLookupRequest {
  searchText: string;
}

export interface FranchiseLookupResponse {
  results: FranchiseLookupResultDto[];
}

export interface FranchiseLookupResultDto {
  lsTeamId: number;
  name: string;
  beginYear: number;
  endYear: number | null;
}

export class FranchiseLookupApiClient {
  private readonly commandName = 'FranchiseLookup';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: FranchiseLookupRequest): Promise<FranchiseLookupResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}