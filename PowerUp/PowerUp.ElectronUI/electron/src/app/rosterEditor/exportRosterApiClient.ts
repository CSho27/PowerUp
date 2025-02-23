import { CommandFetcher } from "../../utils/commandFetcher";
import { trim } from "../../utils/stringUtils";
import { PerformWithSpinnerCallback } from "../appContext";
import { ResultResponse } from "../shared/resultResponse";
import { ContentDisposition } from "../../utils/ContentDisposition";

export interface ExportRosterRequest {
  rosterId: number;
  sourceGameSavePath?: string;
  directoryPath: string;
}

export class ExportRosterApiClient {
  private readonly commandName = 'ExportRoster';
  private readonly commandFetcher: CommandFetcher;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(commandFetcher: CommandFetcher, performWithSpinner: PerformWithSpinnerCallback) {
    this.commandFetcher = commandFetcher;
    this.performWithSpinner = performWithSpinner;
  }

  execute = (request: ExportRosterRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }

  executeCsv = (rosterId: number): Promise<File> => {
    return this.performWithSpinner(async () => {
      try {
        const response = await fetch(`./csv/export?rosterId=${rosterId}`, {
          method: 'GET',
          mode: 'same-origin',
          headers: {
            'Content-Type': 'application/json'
          }
        });
        
        const responseType = response.headers.get('Content-Type');
        if(!responseType || !responseType.includes('text/csv'))
          throw await response.text();

        const disposition = new ContentDisposition(response.headers.get('content-disposition'));
        const fileName = disposition['filename']
          ? trim(disposition['filename'], '"')
          : null;
        const fileBytes = await response.blob();
        return new File([fileBytes], fileName ?? "Untitled");
      } catch (error) {
        this.commandFetcher.log('Error', error);
        return new Promise((_, reject) => reject(error));
      }
    })
  }
}

