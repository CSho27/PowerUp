import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { PlayerDetails } from "./teamManagementEditorState";

export interface PlayerDetailsResponse {
  sourceType: EntitySourceType;
  canEdit: boolean;
  playerId: number;
  fullName: string;
  savedName: string;
  position: Position;
  positionAbbreviation: string;
  overall: number;
  batsAndThrows: string;
}

export function toPlayerDetails(detailsResponse: PlayerDetailsResponse): PlayerDetails {
  return {
    sourceType: detailsResponse.sourceType,
    canEdit: detailsResponse.canEdit,
    playerId: detailsResponse.playerId,
    fullName: detailsResponse.fullName,
    savedName: detailsResponse.savedName,
    position: detailsResponse.position,
    positionAbbreviation: detailsResponse.positionAbbreviation,
    overall: detailsResponse.overall,
    batsAndThrows: detailsResponse.batsAndThrows
  }
}