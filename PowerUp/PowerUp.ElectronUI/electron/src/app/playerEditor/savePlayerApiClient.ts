import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerEditorDTO } from "./playerEditor";

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: PlayerEditorDTO): Promise<any> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}