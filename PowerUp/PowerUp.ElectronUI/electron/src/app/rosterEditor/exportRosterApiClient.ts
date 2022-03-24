import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";


export interface ExportRosterRequest {
  rosterId: number;
  directoryPath: string;
}

export class ExportRosterApiClient {
  private readonly commandName = 'ExportRoster';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ExportRosterRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}