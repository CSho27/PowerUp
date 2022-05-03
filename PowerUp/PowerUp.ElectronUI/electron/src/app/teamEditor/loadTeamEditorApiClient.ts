import { CommandFetcher } from "../../utils/commandFetcher";

export interface LoadTeamEditorRequest {
  teamId: number;
}

export interface LoadTeamEditorResponse {
  name: string;
  teamRosterDetails: TeamRosterDetails; 
}

export interface TeamRosterDetails {
  mlbPlayers: PlayerRoleDefinitionDto[];
  aaaPlayers: PlayerRoleDefinitionDto[];
}

export interface PlayerRoleDefinitionDto {
  playerId: number;
  fullName: string;
  savedName: string;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
}

export class LoadTeamEditorApiClient {
  private readonly commandName = 'LoadTeamEditor';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: LoadTeamEditorRequest): Promise<LoadTeamEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}