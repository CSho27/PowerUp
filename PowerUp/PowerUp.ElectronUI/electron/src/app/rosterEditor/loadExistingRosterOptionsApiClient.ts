import { CommandFetcher } from "../../utils/commandFetcher";
import { SimpleCode } from "../shared/simpleCode";

export class LoadExistingRosterOptionsApiClient {
  private readonly commandName = 'LoadExistingRosterOptions';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (): Promise<SimpleCode[]> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}