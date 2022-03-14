import { PerformWithSpinnerCallback } from "../app/app";

export class CommandFetcher {
  private readonly commandUrl: string;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(commandUrl: string, performWithSpinner: PerformWithSpinnerCallback) {
    this.commandUrl = commandUrl;
    this.performWithSpinner = performWithSpinner;
  }

  readonly execute = async (commandName: string, request: any): Promise<any> => {
    return this.performWithSpinner(async () => {
      try {
        const response = await fetch(this.commandUrl, {
          method: 'POST',
          mode: 'same-origin',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ commandName: commandName, request: request })
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