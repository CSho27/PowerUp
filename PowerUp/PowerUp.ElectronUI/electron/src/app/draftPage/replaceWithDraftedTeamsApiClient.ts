import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";

export interface ReplaceWithDraftedTeamsRequest {
  rosterId: number;
  teams: DraftedTeam[];
}

export interface DraftedTeam {
  teamToReplace: string;
  teamName: string;
  playerIds: number[];
}

export class ReplaceWithDraftedTeamsApiClient {
  private readonly commandName = 'ReplaceWithDraftedTeams';
  private readonly commandFetcher: CommandFetcher;
  
  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher
  }

  execute = (request: ReplaceWithDraftedTeamsRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}