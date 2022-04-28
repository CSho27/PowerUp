import { CommandFetcher } from "../../utils/commandFetcher";

export class InitializeBaseRosterApiClient {
  private readonly commandName = 'InitializeBaseGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (): Promise<void> => {
    return this.commandFetcher.execute(this.commandName, {}, false);
  }
}