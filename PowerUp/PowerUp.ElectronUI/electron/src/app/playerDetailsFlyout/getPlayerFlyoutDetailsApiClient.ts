import { Grade } from "../../components/gradeLetter/gradeLetter";
import { CommandFetcher } from "../../utils/commandFetcher";
import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";


export interface PlayerFlyoutDetailsRequest {
  playerId: number;
}

export interface PlayerFlyoutDetailsResponse {
  playerId: number;
  sourceType: EntitySourceType;
  primaryPosition: Position;
  savedName: string;
  informalDisplayName: string;
  overall: number;
  uniformNumber: string;
  hitterDetails: HitterDetailsDto;
  pitcherDetails: PitcherDetailsDto;
  positionCapabilities: PositionCapabilitiesDto;
}

export interface HitterDetailsDto {
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  trajectory: number;
  errorResistance: number;
  positions: string;
  batsAndThrows: string;
}

export interface PitcherDetailsDto {
  topSpeed: number;
  throwingArm: string;
  pitcherType: string;
  control: number;
  stamina: number;

  twoSeamType: string | null;
  twoSeamMovement: number | null;
  slider1Type: string | null;
  slider1Movement: number | null;
  slider2Type: string | null;
  slider2Movement: number | null;
  curve1Type: string | null;
  curve1Movement: number | null;
  curve2Type: string | null;
  curve2Movement: number | null;
  fork1Type: string | null;
  fork1Movement: number | null;
  fork2Type: string | null;
  fork2Movement: number | null;
  sinker1Type: string | null;
  sinker1Movement: number | null;
  sinker2Type: string | null;
  sinker2Movement: number | null;
  sinkingFastball1Type: string | null;
  sinkingFastball1Movement: number | null;
  sinkingFastball2Type: string | null;
  sinkingFastball2Movement: number | null;
}

export interface PositionCapabilitiesDto {
  pitcher: Grade;
  catcher: Grade;
  firstBase: Grade;
  secondBase: Grade;
  thirdBase: Grade;
  shortstop: Grade;
  leftField: Grade;
  centerField: Grade;
  rightField: Grade;
}

export class GetPlayerFlyoutDetailsApiClient {
  private readonly commandName = 'GetPlayerFlyoutDetails';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: PlayerFlyoutDetailsRequest): Promise<PlayerFlyoutDetailsResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}