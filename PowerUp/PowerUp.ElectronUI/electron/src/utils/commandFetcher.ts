import { PerformWithSpinnerCallback } from "../app/app";

export class CommandFetcher {
  private readonly commandUrl: string;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(commandUrl: string, performWithSpinner: PerformWithSpinnerCallback) {
    this.commandUrl = commandUrl;
    this.performWithSpinner = performWithSpinner;
  }

  readonly execute = async (commandName: string, request: any, useSpinner?: boolean): Promise<any> => {
    const shouldUseSpinner = useSpinner ?? true; 
    return shouldUseSpinner
      ? this.performWithSpinner(() => this.performFetch(commandName, request))
      : this.performFetch(commandName, request);
  }

  private readonly performFetch = async (commandName: string, request: any) => {
    try {
      const response = await fetch(this.commandUrl, {
        method: 'POST',
        mode: 'same-origin',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ commandName: commandName, request: request })
      });
      
      const responseType = response.headers.get('Content-Type');
      if(!responseType || !responseType.includes('application/json'))
        throw await response.text();

      const responseJson = await response.json(); 
      return responseJson;
    } catch (error) {
      console.error(error);
      return new Promise((_, reject) => reject(error));
    }
  }
}