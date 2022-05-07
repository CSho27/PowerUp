
import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";


export interface DiscardTempTeamRequest {
  teamId: number;
  tempTeamId: number;
}

export class DiscardTempTeamApiClient {
  private readonly commandName = 'DiscardTempTeam';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: DiscardTempTeamRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}