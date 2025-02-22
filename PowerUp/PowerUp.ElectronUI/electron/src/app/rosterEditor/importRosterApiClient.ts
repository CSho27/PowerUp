import { CommandFetcher } from "../../utils/commandFetcher";
import { PerformWithSpinnerCallback } from "../app";

export interface ImportRosterRequest {
  file: File;
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

  execute = async (request: ImportRosterRequest): Promise<ImportRosterResponse> => {
    return this.performWithSpinner(async () => {
      try {
        const formData = new FormData();
        formData.append("data", request.file);
        formData.append("importSource", request.importSource);
        const response = await fetch('./Import', {
          method: 'POST',
          mode: 'same-origin',
          body: formData
        });
        const responseJson = await response.json(); 
        return responseJson;
      } catch (error) {
        this.commandFetcher.log('Error', error);
        return new Promise((_, reject) => reject(error));
      }
    })
  }

  executeCsv = (request: ImportRosterRequest): Promise<ImportRosterResponse> => {
    return this.performWithSpinner(async () => {
      try {
        const formData = new FormData();
        formData.append("data", request.file);
        formData.append("importSource", request.importSource);
        const response = await fetch('./csv/import', {
          method: 'POST',
          mode: 'same-origin',
          body: formData
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