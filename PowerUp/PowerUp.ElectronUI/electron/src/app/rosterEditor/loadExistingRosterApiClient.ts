import { CommandFetcher } from "../../utils/commandFetcher";
import { RosterEditorResponse } from "./rosterEditorDTOs";

export interface LoadExistingRosterRequest {
  rosterId: number;
}

export class LoadExistingRosterApiClient {
  private readonly commandName = 'LoadExistingRoster';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: LoadExistingRosterRequest): Promise<RosterEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}