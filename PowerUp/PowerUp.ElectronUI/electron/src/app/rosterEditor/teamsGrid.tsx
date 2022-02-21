import styled from "styled-components"
import { COLORS, FONT_SIZES } from "../../style/constants"
import { TeamDetails } from "../home/importBaseRosterApiClient"

export interface TeamsGridProps {
  teams: TeamDetails[];  
}

export function TeamsGrid(props: TeamsGridProps) {
  const { teams } = props
  
  return <TeamsContainer>
    {teams.map(toTeamSection)}
  </TeamsContainer>
  
  function toTeamSection(team: TeamDetails) {
    const teamDisplayName = team.name === team.powerProsName
      ? team.name
      : `${team.name} (${team.powerProsName})` 

    return <TeamWrapper key={team.powerProsName}> 
      <TeamHeaderWrapper>
        <TeamHeader>{teamDisplayName}</TeamHeader>
      </TeamHeaderWrapper>
      <PlayerGridHeaderWrapper>
        <PlayerGridHeader>Hitters</PlayerGridHeader>
      </PlayerGridHeaderWrapper>
      {team.hitters.map(h => <div key={h.savedName}>{h.savedName}</div>)}
      <PlayerGridHeaderWrapper>
        <PlayerGridHeader>Pitchers</PlayerGridHeader>
      </PlayerGridHeaderWrapper>
      {team.pitchers.map(h => <div key={h.savedName}>{h.savedName}</div>)}
    </TeamWrapper>
  }
}

const TeamsContainer = styled.div`
  padding-left: 16px;
`

const TeamWrapper = styled.div`
  margin-top: 8px;
`

const TeamHeaderWrapper = styled.div`
  background-color: ${COLORS.primaryBlue.regular_45};
  color: ${COLORS.white.regular_100};
  width: 100%;
  height: 64px;
  position: sticky;
  top: 0;
`

const TeamHeader = styled.h1`
  padding: 8px 16px;
  font-size: ${FONT_SIZES._32};
  font-weight: 600;
  font-style: italic;
  width: 100%;
`

const PlayerGridHeaderWrapper = styled.div`
  padding: 0px 16px;
  background-color: ${COLORS.jet.light_39};
  color: ${COLORS.white.regular_100};
  width: 100%;
  position: sticky;
  top: 64px;
`

const PlayerGridHeader = styled.h3`
`