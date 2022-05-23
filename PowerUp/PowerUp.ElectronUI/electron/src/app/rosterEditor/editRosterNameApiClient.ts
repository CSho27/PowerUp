import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";


export interface EditRosterNameRequest {
  rosterId: number;
  rosterName: string;
}

export class EditRosterNameApiClient {
  private readonly commandName = 'EditRosterName';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: EditRosterNameRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}