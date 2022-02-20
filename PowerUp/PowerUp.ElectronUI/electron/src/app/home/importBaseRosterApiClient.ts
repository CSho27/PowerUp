import { CommandFetcher } from "../../utils/commandFetcher";

export interface LoadBaseRequest {
  throwaway: number;
}

export interface LoadBaseResponse {
  success: boolean;
}

export class ImportBaseRosterApiClient {
  private readonly commandName = 'LoadBaseGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: LoadBaseRequest): Promise<LoadBaseResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}