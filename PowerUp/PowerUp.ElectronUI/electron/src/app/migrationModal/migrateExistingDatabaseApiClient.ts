import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface MigrateExistingDatabaseRequest {
  powerUpDirectory: string;
}

export class MigrateExistingDatabaseApiClient {
  private readonly commandName = 'MigrateExistingDatabase';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: MigrateExistingDatabaseRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}