import { CommandFetcher } from "../../utils/commandFetcher";
import { KeyedCode } from "../shared/keyedCode";
import { PositionCode } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode";
import { HotZoneGridDto } from "./hotZoneGridDto";

export interface LoadPlayerEditorRequest {
  playerId: number;
}

export interface PlayerEditorResponse {
  options: PlayerEditorOptions; 
  personalDetails: PlayerPersonalDetailsDto;
  positionCapabilityDetails: PositionCapabilityDetailsDto;
  hitterAbilityDetails: HitterAbilityDetailsDto;
  pitcherAbilityDetails: PitcherAbilityDetailsDto;
}

export interface PlayerEditorOptions {
  voiceOptions: SimpleCode[];
  positions: PositionCode[];
  pitcherTypes: KeyedCode[];
  battingSideOptions: KeyedCode[];
  battingStanceOptions: SimpleCode[];
  throwingArmOptions: KeyedCode[];
  pitchingMechanicsOptions: SimpleCode[];
  positionCapabilityOptions: KeyedCode[];
}

export interface PlayerPersonalDetailsDto {
  firstName: string;
  lastName: string;
  isSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  position: PositionCode;
  pitcherType: KeyedCode;
  voice: SimpleCode;
  battingSide: KeyedCode;
  battingStance: SimpleCode;
  throwingArm: KeyedCode;
  pitchingMechanics: SimpleCode;
}

export interface PositionCapabilityDetailsDto {
  pitcher: KeyedCode;
  catcher: KeyedCode;
  firstBase: KeyedCode;
  secondBase: KeyedCode;
  thirdBase: KeyedCode;
  shortstop: KeyedCode;
  leftField: KeyedCode;
  centerField: KeyedCode;
  rightField: KeyedCode;
}

export interface HitterAbilityDetailsDto {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
  hotZones: HotZoneGridDto;
}

export interface PitcherAbilityDetailsDto {
  topSpeed: number;
  control: number;
  stamina: number;

  twoSeamType: KeyedCode | null;
  twoSeamMovement: number | null;

  slider1Type: KeyedCode | null;
  slider1Movement: number | null;

  slider2Type: KeyedCode | null;
  slider2Movement: number | null;

  curve1Type: KeyedCode | null;
  curve1Movement: number | null;

  curve2Type: KeyedCode | null;
  curve2Movement: number | null;

  fork1Type: KeyedCode | null;
  fork1Movement: number | null;

  fork2Type: KeyedCode | null;
  fork2Movement: number | null;

  sinker1Type: KeyedCode | null;
  sinker1Movement: number | null;

  sinker2Type: KeyedCode | null;
  sinker2Movement: number | null;

  sinkingFastball1Type: KeyedCode | null;
  sinkingFastball1Movement: number | null;

  sinkingFastball2Type: KeyedCode | null;
  sinkingFastball2Movement: number | null;
}

export class LoadPlayerEditorApiClient {
  private readonly commandName = 'LoadPlayerEditor';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: LoadPlayerEditorRequest): Promise<PlayerEditorResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}