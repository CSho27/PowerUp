import { CommandFetcher } from "../../utils/commandFetcher";
import { RosterEditorResponse } from "./rosterEditorDTOs";

export class ImportBaseRosterApiClient {
  private readonly commandName = 'LoadBaseGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (): Promise<RosterEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}