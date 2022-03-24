import { PerformWithSpinnerCallback } from "../app";

export interface ImportRosterRequest {
  file: File;
  importSource: string;
}

export interface ImportRosterResponse {
  rosterId: number;
}

export class ImportRosterApiClient {
  private readonly importUrl: string;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(importUrl: string, performWithSpinner: PerformWithSpinnerCallback) {
    this.importUrl = importUrl;
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
}