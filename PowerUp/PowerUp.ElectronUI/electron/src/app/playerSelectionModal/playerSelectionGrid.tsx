import styled from "styled-components"
import { COLORS } from "../../style/constants";
import { DisableResult } from "../shared/disableResult";
import { EntitySourceType } from "../shared/entitySourceType";
import { Position } from "../shared/positionCode";
import { SimpleCode } from "../shared/simpleCode";

export interface PlayerSelectionGridProps {
  selectedPlayer: PlayerSelectionGridPlayer | undefined;
  players: PlayerSelectionGridPlayer[];
  noDataMessage: string;
  height?: string;
  onPlayerSelected: (player: PlayerSelectionGridPlayer) => void;
  isPlayerDisabled?: (player: PlayerSelectionGridPlayer) => DisableResult;
}

export interface PlayerSelectionGridPlayer {
  playerId: number;
  sourceType: EntitySourceType;
  canEdit: boolean;
  uniformNumber: string;
  savedName: string;
  formalDisplayName: string;
  informalDisplayName: string;
  position: Position;
  positionAbbreviation: string;
  batsAndThrows: string;
  overall: number;
}

export function PlayerSelectionGrid(props: PlayerSelectionGridProps) {
  const { selectedPlayer, players, isPlayerDisabled, noDataMessage, height, onPlayerSelected } = props;
  
  return <GridWrapper height={height}>
    <PlayerGrid >
      <thead>
        <tr>
          <PlayerHeader>Type</PlayerHeader>
          <PlayerHeader>Num</PlayerHeader>
          <PlayerHeader>Pos</PlayerHeader>
          <PlayerHeader>Name</PlayerHeader>
          <PlayerHeader>Ovr</PlayerHeader>
          <PlayerHeader>B/T</PlayerHeader>
        </tr>
      </thead>
      <tbody>
        {players.map(toPlayerRow)}
      </tbody>
    </PlayerGrid>
    {players.length === 0 && 
    <NoDataMessage>
      <span>{noDataMessage}</span>
    </NoDataMessage>}
  </GridWrapper>

  function toPlayerRow(player: PlayerSelectionGridPlayer) {
    const disabledResult = !!isPlayerDisabled
      ? isPlayerDisabled(player)
      : undefined;
    
    return <PlayerRow 
      key={player.playerId} 
      selected={player.playerId === selectedPlayer?.playerId}
      disabled={!!disabledResult && disabledResult.disabled}
      title={disabledResult?.message}
      onClick={disabledResult?.disabled 
        ? () => {}
        : () => onPlayerSelected(player)}>
      <PlayerData>{player.sourceType}</PlayerData>
      <PlayerData>{player.uniformNumber}</PlayerData>
      <PlayerData>{player.positionAbbreviation}</PlayerData>
      <PlayerData>{player.formalDisplayName}</PlayerData>
      <PlayerData>{player.overall}</PlayerData>
      <PlayerData>{player.batsAndThrows}</PlayerData>
    </PlayerRow>
  }
}

const GridWrapper = styled.div<{ height: string | undefined }>`
  width: 100%;
  height: ${p => p.height};
  overflow-y: auto;
  background-color: ${COLORS.white.regular_100};
`

const PlayerGrid = styled.table`
  border-collapse: collapse;
  width: 100%;
  text-align: left;
`

const PlayerHeader = styled.th`
  position: sticky;
  top: 0;
  background-color: ${COLORS.jet.lighter_71};
  padding: 2px 8px;
  font-style: italic;
`

const NoDataMessage = styled.div`
  padding: 2px 8px;
  color: ${COLORS.jet.light_39};
  display: flex;
  align-items: center;
  justify-content: center;
`

const PlayerRow = styled.tr<{ selected: boolean, disabled: boolean }>`
  padding: 2px 8px;
  cursor: ${p => p.disabled ? 'undefined' : 'pointer' };
  background-color: ${p => p.disabled
    ? COLORS.white.grayed_80_t40
    : p.selected 
      ? COLORS.white.offwhite_85 
      : undefined};

  &:nth-child(even) {
    background-color: ${p => p.disabled
      ? COLORS.white.grayed_80_t40
      : p.selected 
        ? COLORS.white.offwhite_85 
        : COLORS.white.offwhite_97};
  } 

  &:hover {
    background-color: ${p => !p.selected && !p.disabled ? COLORS.white.offwhite_92 : undefined};
  }
`
const PlayerData = styled.td`
  padding: 2px 8px;
`