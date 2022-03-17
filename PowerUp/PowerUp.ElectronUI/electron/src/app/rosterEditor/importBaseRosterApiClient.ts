import { CommandFetcher } from "../../utils/commandFetcher";

export interface LoadBaseRosterResponse {
  rosterId: number;
}

export class ImportBaseRosterApiClient {
  private readonly commandName = 'LoadBaseGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (): Promise<LoadBaseRosterResponse> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}