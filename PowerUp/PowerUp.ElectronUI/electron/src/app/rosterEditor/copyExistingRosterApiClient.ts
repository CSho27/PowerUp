import { CommandFetcher } from "../../utils/commandFetcher";

export interface CopyExistingRosterRequest {
  rosterId: number;
}

export interface CopyExistingRosterResponse {
  rosterId: number;
}

export class CopyExistingRosterApiClient {
  private readonly commandName = 'CopyExistingRoster';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: CopyExistingRosterRequest): Promise<CopyExistingRosterResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}