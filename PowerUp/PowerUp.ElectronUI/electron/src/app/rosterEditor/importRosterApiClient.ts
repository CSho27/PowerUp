import { CommandFetcher, getDefaultRequestOptions } from "../../utils/commandFetcher";
import { PerformWithSpinnerCallback } from "../appContext";

export interface ImportRosterRequest {
  importSource: string;
}

export interface ImportRosterResponse {
  rosterId: number;
}

export class ImportRosterApiClient {
  private readonly commandFetcher: CommandFetcher;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(
    commandFetcher: CommandFetcher,
    performWithSpinner: PerformWithSpinnerCallback
  ) {
    this.commandFetcher = commandFetcher;
    this.performWithSpinner = performWithSpinner;
  }

  execute = async (request: ImportRosterRequest, file: File): Promise<ImportRosterResponse> => {
    return await this.commandFetcher.executeWithFile('ImportGameSave', request, file);
  }

  executeCsv = (request: ImportRosterRequest, file: File): Promise<ImportRosterResponse> => {
    return this.performWithSpinner(async () => {
      try {
        const formData = new FormData();
        formData.append("data", file);
        formData.append("importSource", request.importSource);
        const response = await fetch('./csv/import', {
          method: 'POST',
          body: formData,
          ...getDefaultRequestOptions()
        });
        const responseJson = await response.json(); 
        return responseJson;
      } catch (error) {
        this.commandFetcher.log('Error', error);
        return new Promise((_, reject) => reject(error));
      }
    })
  }
}