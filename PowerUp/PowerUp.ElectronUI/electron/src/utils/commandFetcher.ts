
export class CommandFetcher {
  private readonly commandUrl: string;

  constructor(commandUrl: string) {
    this.commandUrl = commandUrl;
  }

  readonly execute = async (commandName: string, request: any): Promise<any> => {
    try {
      const response = await fetch('/command', {
        method: 'POST',
        mode: 'same-origin',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ commandName: commandName, request: request })
      });
      return response.json();
    } catch (error) {
      console.error(error);
      return new Promise((_, reject) => reject(error));
    }
  }
}