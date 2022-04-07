import { CommandFetcher } from "../../utils/commandFetcher";
import { ResultResponse } from "../shared/resultResponse";
import { HotZoneGridDto } from "./hotZoneGridDto";

export interface SavePlayerRequest {
  playerId: number;
  personalDetails: PersonalDetailsRequest;
  positionCapabilities: PositionCapabilitiesRequest;
  hitterAbilities: HitterAbilitiesSaveRequest;
  pitcherAbilities: PitcherAbilitiesSaveRequest;
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

export interface PitcherAbilitiesSaveRequest {
  topSpeed: number;
  control: number;
  stamina: number;
  
  twoSeamTypeKey: string | null;
  twoSeamMovement: number | null;
  
  slider1TypeKey: string | null;
  slider1Movement: number | null;
  
  slider2TypeKey: string | null;
  slider2Movement:  number | null;

  curve1TypeKey: string | null;
  curve1Movement: number | null;

  curve2TypeKey: string | null;
  curve2Movement: number | null;

  fork1TypeKey: string | null;
  fork1Movement: number | null;

  fork2TypeKey: string | null;
  fork2Movement: number | null;

  sinker1TypeKey: string | null;
  sinker1Movement: number | null;
  
  sinker2TypeKey: string | null;
  sinker2Movement: number | null;

  sinkingFastball1TypeKey: string | null;
  sinkingFastball1Movement: number | null;
  
  sinkingFastball2TypeKey: string | null;
  sinkingFastball2Movement: number | null;
}

export interface SpecialAbilitiesRequest {
  generalSpecialAbilitiesRequest: GeneralSpecialAbilitiesRequest;
  hitterSpecialAbilitiesRequest: HitterSpecialAbilitiesRequest;
  pitcherSpecialAbilitiesRequest: PitcherSpecialAbilitiesRequest;
}

export interface GeneralSpecialAbilitiesRequest {
  isStar: boolean;
  durabilityKey: string; 
  moraleKey: string;
}

export interface HitterSpecialAbilitiesRequest {
  situational: SituationalHittingSpecialAbilitiesRequest;
  approach: HittingApproachSpecialAbilitiesRequest;
  smallBall: SmallBallSpecialAbilitiesRequest;
  baseRunning: BaseRunningSpecialAbilitiesRequest;
  fielding: FieldingSpecialAbilitiesRequest;
}

export interface SituationalHittingSpecialAbilitiesRequest {
  
}

export interface HittingApproachSpecialAbilitiesRequest {

}

export interface SmallBallSpecialAbilitiesRequest {

}

export interface BaseRunningSpecialAbilitiesRequest {

}

export interface FieldingSpecialAbilitiesRequest {

}

export interface PitcherSpecialAbilitiesRequest {
  situational: SituationalPitchingSpecialAbilitiesRequest; 
  demeanor: PitchingDemeanorSpecialAbilitiesRequest; 
  mechanics: PitchingMechanicsSpecialAbilitiesRequest; 
  pitchQualities: PitchQualitiesSpecialAbilitiesRequest; 
}

export interface SituationalPitchingSpecialAbilitiesRequest {

}

export interface PitchingDemeanorSpecialAbilitiesRequest {

}

export interface PitchingMechanicsSpecialAbilitiesRequest {

}

export interface PitchQualitiesSpecialAbilitiesRequest {
  
}

export class SavePlayerApiClient {
  private readonly commandName = 'SavePlayer';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: SavePlayerRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}