import { useRef, useState } from "react";
import styled from "styled-components";
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { KeyedCode } from "../shared/keyedCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { RosterDetails, TeamDetails } from "./rosterEditorDTOs";
import { TeamGrid } from "./teamGrid";

export interface RosterEditorPageProps {
  appContext: AppContext;
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, divisionOptions, rosterDetails } = props;
  const { name: rosterName, teams } = rosterDetails;

  const [selectedDivision, setSelectedDivision] = useState(divisionOptions[0]);

  const header = <>
    <Breadcrumbs>
      <Crumb key='Home' onClick={returnHome}>Home</Crumb>
      <Crumb key='RosterEditor'>{rosterName}</Crumb>
    </Breadcrumbs>
    <RosterHeader>{rosterName}</RosterHeader>
    <TabButtonNav 
      selectedTab={selectedDivision}
      tabOptions={divisionOptions}
      onChange={handleDivisionChange}
    />
  </> 

  const teamsRef = useRef<HTMLElement | null>(null);

  return <PowerUpLayout headerText='Edit Roster'>
    <ContentWithHangingHeader header={header} headerHeight='128px' contentRef={teamsRef}>
      <TeamsContainer>
        {teams.filter(t => t.division === selectedDivision.key).map(toTeamGrid)}
      </TeamsContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toTeamGrid(team: TeamDetails) {
    return <TeamWrapper key={team.teamId}>
      <TeamGrid appContext={appContext} team={team} />
    </TeamWrapper>
  }

  function handleDivisionChange(division: KeyedCode) {
    setSelectedDivision(division);
    teamsRef.current?.scrollTo({ top: 0, behavior: 'auto' })
  }

  function returnHome() {
    appContext.setPage({ page: 'HomePage', importUrl: '' })
  }
}

const RosterHeader = styled.h1`
  padding-bottom: 8px;
  font-size: ${FONT_SIZES._32};
  font-style: italic;
  white-space: nowrap;
`

const TeamsContainer = styled.div`
  padding-left: 16px;
`

const TeamWrapper = styled.div`
  margin-top: 16px;
`


export const loadRosterEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition ) => {
  if(pageDef.page !== 'RosterEditorPage') throw '';
  
  return <RosterEditorPage 
    appContext={appContext} 
    divisionOptions={pageDef.response.divisionOptions}
    rosterDetails={pageDef.response.rosterDetails} 
  />
}