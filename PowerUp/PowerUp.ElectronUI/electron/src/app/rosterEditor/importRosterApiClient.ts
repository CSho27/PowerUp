import { CommandFetcher } from "../../utils/commandFetcher";

export interface ImportRosterRequest {
  importSource: string;
}

export interface ImportRosterResponse {
  rosterId: number;
}

export class ImportRosterApiClient {
  private readonly commandFetcher: CommandFetcher;

  constructor(
    commandFetcher: CommandFetcher,
  ) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: ImportRosterRequest, file: File): Promise<ImportRosterResponse> => {
    return this.commandFetcher.executeWithFile('ImportGameSave', request, file);
  }

  executeCsv = (request: ImportRosterRequest, file: File): Promise<ImportRosterResponse> => {
    return this.commandFetcher.executeWithFile('ImportCsv', request, file);
  }
}