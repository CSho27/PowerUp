import styled from "styled-components";
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { HomePage } from "../home/homePage";
import { RosterDetails, TeamDetails } from "../home/importBaseRosterApiClient";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { TeamGrid } from "./teamGrid";

export interface RosterEditorPageProps {
  appContext: AppContext;
  rosterDetails: RosterDetails;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, rosterDetails } = props;
  const { name: rosterName, teams } = rosterDetails;
  
  const header = <>
    <Breadcrumbs>
      <Crumb key='Home' onClick={returnHome}>Home</Crumb>
      <Crumb key='RosterEditor'>{rosterName}</Crumb>
    </Breadcrumbs>
    <RosterHeader>{rosterName}</RosterHeader>
  </> 

  return <PowerUpLayout headerText='Edit Roster'>
    <ContentWithHangingHeader header={header} headerHeight='88px'>
      <TeamsContainer>
        {teams.slice(0, 3).map(toTeamGrid)}
      </TeamsContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toTeamGrid(team: TeamDetails) {
    return <TeamWrapper>
      <TeamGrid appContext={appContext} team={team} />
    </TeamWrapper>
  }

  function returnHome() {
    appContext.setPage(<HomePage appContext={appContext} />)
  }
}

const RosterHeader = styled.h1`
  padding-bottom: 8px;
  font-size: ${FONT_SIZES._32};
  font-style: italic;
`

const TeamsContainer = styled.div`
  padding-left: 16px;
`

const TeamWrapper = styled.div`
  margin-top: 16px;
`
