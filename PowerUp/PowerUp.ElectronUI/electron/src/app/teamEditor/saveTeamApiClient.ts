import { CommandFetcher } from "../../utils/commandFetcher";
import { Position } from "../shared/positionCode";
import { ResultResponse } from "../shared/resultResponse";
import { PitcherRole } from "./playerRoleState";

export interface SaveTeamRequest {
  teamId: number;
  tempTeamId: number;
  persist: boolean;
  
  name: string;
  mlbPlayers: PlayerRoleRequest[];
  aaaPlayers: PlayerRoleRequest[];
}

export interface PlayerRoleRequest {
  playerId: number;
  isPinchHitter: boolean;
  isPinchRunner: boolean;
  isDefensiveReplacement: boolean;
  isDefensiveLiability: boolean;
  pitcherRole: PitcherRole;
  orderInPitcherRole: number;
  orderInNoDHLineup: number | null;
  positionInNoDHLineup: Position | null;
  orderInDHLineup: number | null;
  positionInDHLineup: Position | null;
}

export class SaveTeamApiClient {
  private readonly commandName = 'SaveTeam';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: SaveTeamRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}