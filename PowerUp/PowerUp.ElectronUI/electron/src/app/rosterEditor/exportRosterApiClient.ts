import { CommandFetcher } from "../../utils/commandFetcher";

export interface ExportRosterRequest {
  rosterId: number;
}

export class ExportRosterApiClient {
  private readonly commandName = 'ExportRoster';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ExportRosterRequest, file: File | null): Promise<File | null> => {
    return this.commandFetcher.executeWithFile(this.commandName, request, file);
  }

  executeCsv = (request: ExportRosterRequest): Promise<File> => {
    return this.commandFetcher.execute('ExportCsv', request);
  }
}

