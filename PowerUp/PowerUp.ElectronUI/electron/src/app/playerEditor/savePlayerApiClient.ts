import { CommandFetcher } from "../../utils/commandFetcher";
import { HotZoneGridDto } from "./hotZoneGridDto";

export interface SavePlayerRequest {
  playerId: number;
  personalDetails: PersonalDetailsRequest;
  positionCapabilities: PositionCapabilitiesRequest;
  hitterAbilities: HitterAbilitiesSaveRequest;
}

export interface PersonalDetailsRequest {
  firstName: string;
  lastName: string;
  useSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  positionKey: string;
  pitcherTypeKey: string;
  voiceId: number;
  battingSideKey: string;
  battingStanceId: number;
  throwingArmKey: string;
  pitchingMechanicsId: number;
}

export interface PositionCapabilitiesRequest {
  pitcher: string;
  catcher: string;
  firstBase: string;
  secondBase: string;
  thirdBase: string;
  shortstop: string;
  leftField: string;
  centerField: string;
  rightField: string;
}

export interface HitterAbilitiesSaveRequest {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
  hotZoneGrid: HotZoneGridDto;
}

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: SavePlayerRequest): Promise<any> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}