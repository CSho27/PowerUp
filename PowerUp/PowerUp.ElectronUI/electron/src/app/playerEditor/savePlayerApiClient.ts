import { CommandFetcher } from "../../utils/commandFetcher";

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: any): Promise<any> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}