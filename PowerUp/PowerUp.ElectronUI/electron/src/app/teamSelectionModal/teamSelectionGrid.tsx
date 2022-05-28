import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { TeamSearchResultDto } from "./teamSearchApiClient";

export interface TeamSelectionGridProps {
  selectedTeam: TeamSearchResultDto | undefined;
  teams: TeamSearchResultDto[];
  noDataMessage: string;
  height?: string;
  onTeamSelected: (team: TeamSearchResultDto) => void;
}

export function TeamSelectionGrid(props: TeamSelectionGridProps) {
  const { selectedTeam, teams, noDataMessage, height, onTeamSelected } = props;

  return <GridWrapper height={height}>
    <TeamGrid>
      <thead>
        <tr>
          <TeamHeader>Type</TeamHeader>
          <TeamHeader>Name</TeamHeader>
          <TeamHeader>Ovr</TeamHeader>
        </tr>
      </thead>
      <tbody>
        {teams.map(toTeamRow)}
      </tbody>
    </TeamGrid>
    {teams.length === 0 && 
    <NoDataMessage>
      <span>{noDataMessage}</span>
    </NoDataMessage>}
  </GridWrapper>

  function toTeamRow(team: TeamSearchResultDto) {
    return <TeamRow 
      key={team.teamId} 
      selected={team.teamId === selectedTeam?.teamId}
      onClick={() => onTeamSelected(team)}>
        <TeamData>{team.sourceType}</TeamData>
        <TeamData>{team.name}</TeamData>
        <TeamData>{team.overall}</TeamData>
    </TeamRow>
  }
}

const GridWrapper = styled.div<{ height: string | undefined }>`
  width: 100%;
  height: ${p => p.height};
  overflow-y: auto;
  background-color: ${COLORS.white.regular_100};
`

const TeamGrid = styled.table`
  border-collapse: collapse;
  width: 100%;
  text-align: left;
`

const TeamHeader = styled.th`
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

const TeamRow = styled.tr<{ selected: boolean, disabled?: boolean }>`
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
const TeamData = styled.td`
  padding: 2px 8px;
`