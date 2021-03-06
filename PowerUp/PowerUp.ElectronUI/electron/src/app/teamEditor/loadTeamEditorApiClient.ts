import { CommandFetcher } from "../../utils/commandFetcher";
import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { PlayerDetailsResponse } from "./playerDetailsResponse";
import { PitcherRole } from "./playerRoleState";

export interface LoadTeamEditorRequest {
  teamId: number;
  tempTeamId?: number;
}

export interface LoadTeamEditorResponse {
  tempTeamId: number;
  sourceType: EntitySourceType;
  canEdit: boolean;
  lastSavedDetails: TeamDetails; 
  currentDetails: TeamDetails;
  lastSaved: string | null;
}

export interface TeamDetails {
  name: string;
  mlbPlayers: PlayerRoleDefinitionDto[];
  aaaPlayers: PlayerRoleDefinitionDto[];
  noDHLineup: LineupSlotDto[];
  dhLineup: LineupSlotDto[];
}

export interface PlayerRoleDefinitionDto {
  sourceType: EntitySourceType;
  canEdit: boolean;
  playerId: number;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
  pitcherRole: PitcherRole;
  details: PlayerDetailsResponse;
}

export interface LineupSlotDto {
  playerId: number;
  position: Position;
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