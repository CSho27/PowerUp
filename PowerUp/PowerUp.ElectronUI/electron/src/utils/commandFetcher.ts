
export class CommandFetcher {
  private readonly commandUrl: string;
  private readonly setIsLoading: (isLoading: boolean) => void;

  constructor(commandUrl: string, setIsLoading: (isLoading: boolean) => void) {
    this.commandUrl = commandUrl;
    this.setIsLoading = setIsLoading;
  }

  readonly execute = async (commandName: string, request: any): Promise<any> => {
    try {
      this.setIsLoading(true);
      const response = await fetch('/command', {
        method: 'POST',
        mode: 'same-origin',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ commandName: commandName, request: request })
      });
      const responseJson = await response.json(); 
      this.setIsLoading(false);
      return responseJson;
    } catch (error) {
      this.setIsLoading(false);
      console.error(error);
      return new Promise((_, reject) => reject(error));
    }
  }
}