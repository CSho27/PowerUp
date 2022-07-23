import { EntitySourceType } from "../shared/entitySourceType";
import { GeneratorWarningDto } from "../shared/generatorWarning";
import { Position } from "../shared/positionCode";
import { PlayerDetails } from "./playerRoleState";

export interface PlayerDetailsResponse {
  sourceType: EntitySourceType;
  canEdit: boolean;
  playerId: number;
  fullName: string;
  savedName: string;
  generatedPlayer_Warnings: GeneratorWarningDto[];
  generatedPlayer_IsUnedited: boolean;
  position: Position;
  positionAbbreviation: string;
  overall: number;
  pitcherType: string;
  batsAndThrows: string;
  throwingArm: string;
  topSpeed: string;
  control: string;
  stamina: string;
}

export function toPlayerDetails(detailsResponse: PlayerDetailsResponse): PlayerDetails {
  return {
    sourceType: detailsResponse.sourceType,
    canEdit: detailsResponse.canEdit,
    playerId: detailsResponse.playerId,
    fullName: detailsResponse.fullName,
    savedName: detailsResponse.savedName,
    generatedPlayer_Warnings: detailsResponse.generatedPlayer_Warnings,
    generatedPlayer_IsUnedited: detailsResponse.generatedPlayer_IsUnedited,
    position: detailsResponse.position,
    positionAbbreviation: detailsResponse.positionAbbreviation,
    overall: detailsResponse.overall,
    batsAndThrows: detailsResponse.batsAndThrows,
    pitcherType: detailsResponse.pitcherType,
    throwingArm: detailsResponse.throwingArm,
    topSpeed: detailsResponse.topSpeed,
    control: detailsResponse.control,
    stamina: detailsResponse.stamina
  }
}