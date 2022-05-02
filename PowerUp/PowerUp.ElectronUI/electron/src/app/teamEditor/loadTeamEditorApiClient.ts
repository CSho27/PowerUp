import { CommandFetcher } from "../../utils/commandFetcher";

export interface LoadTeamEditorRequest {
  teamId: number;
}

export interface TeamEditorResponse {

}

export class LoadTeamEditorApiClient {
  private readonly commandName = 'LoadTeamEditor';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: LoadTeamEditorRequest): Promise<TeamEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}