import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { PlayerLookupResultDto } from "./playerLookupApiClient";


export interface PlayerLookupGridProps {
  selectedPlayer: PlayerLookupResultDto | undefined;
  players: PlayerLookupResultDto[];
  noDataMessage: string;
  height?: string;
  onPlayerSelected: (player: PlayerLookupResultDto) => void;
}

export function PlayerLookupGrid(props: PlayerLookupGridProps) {
  const { selectedPlayer, players, noDataMessage, height, onPlayerSelected } = props;

  return <GridWrapper height={height}>
  <PlayerGrid>
    <thead>
      <tr>
        <PlayerHeader>Name</PlayerHeader>
        <PlayerHeader>Position</PlayerHeader>
        <PlayerHeader>Most Recent Team</PlayerHeader>
        <PlayerHeader>Debut year</PlayerHeader>
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

function toPlayerRow(player: PlayerLookupResultDto) {
  return <PlayerRow 
    key={player.lsPlayerId} 
    selected={player.lsPlayerId === selectedPlayer?.lsPlayerId}
    onClick={() => onPlayerSelected(player)}>
      <PlayerData>{player.informalDisplayName}</PlayerData>
      <PlayerData>{player.position}</PlayerData>
      <PlayerData>{player.mostRecentTeam}</PlayerData>
      <PlayerData>{player.debutYear}</PlayerData>
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

const PlayerRow = styled.tr<{ selected: boolean, disabled?: boolean }>`
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