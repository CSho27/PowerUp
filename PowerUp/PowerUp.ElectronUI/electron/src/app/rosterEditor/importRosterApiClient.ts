import { PerformWithSpinnerCallback } from "../app";

export interface ImportRosterRequest {
  file: File;
  importSource: string;
}

export interface ImportRosterResponse {
  rosterId: number;
}

export class ImportRosterApiClient {
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(performWithSpinner: PerformWithSpinnerCallback) {
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
        console.error(error);
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
        console.error(error);
        return new Promise((_, reject) => reject(error));
      }
    })
  }
}