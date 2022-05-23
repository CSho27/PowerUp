import { useRef, useState } from "react";
import styled from "styled-components";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { SourceTypeStamp } from "../../components/sourceTypeStamp/sourceTypeStamp";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { TextField } from "../../components/textField/textField";
import { FONT_SIZES } from "../../style/constants";
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { KeyedCode } from "../shared/keyedCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { EditRosterNameApiClient } from "./editRosterNameApiClient";
import { LoadExistingRosterApiClient } from "./loadExistingRosterApiClient";
import { RosterDetails, TeamDetails } from "./rosterEditorDTOs";
import { RosterExportModal } from "./rosterExportModal";
import { TeamGrid } from "./teamGrid";

export interface RosterEditorPageProps {
  appContext: AppContext;
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, divisionOptions, rosterDetails } = props;
  const { rosterId, name, teams } = rosterDetails;

  const rosterNameApiClientRef = useRef(new EditRosterNameApiClient(appContext.commandFetcher));

  const [selectedDivision, setSelectedDivision] = useState(divisionOptions[0]);
  const [isEditingRosterName, setIsEditingRosterName] = useState(false);
  const [rosterName, setRosterName] = useState(name);

  const disableRosterEdit: DisabledCriteria = [
    { isDisabled: !rosterDetails.canEdit, tooltipIfDisabled: 'Roster of this type cannot be edited' }
  ]

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
      <div style={{ flex: 'auto', display: 'flex', gap: '16px', alignItems: 'center' }}>
        {!isEditingRosterName && <>
        <RosterHeader>{rosterName}</RosterHeader>
        <SourceTypeStamp 
        theme='Dark'
        size='Medium'
        sourceType={rosterDetails.sourceType}
        /> 
        </>}
        {isEditingRosterName && 
        <div style={{ width: '25rem' }}>
          <TextField 
            value={rosterName} 
            onChange={name => setRosterName(name)}        
          />
        </div>}
        <Button 
          variant='Outline'
          size='Small'
          {...toDisabledProps('Edit roster name', ...[] /*disableRosterEdit*/)}
          icon={isEditingRosterName ? 'lock' : 'pen-to-square'}
          onClick={handleRosterNameToggle}
        />
      </div>
      <Button 
        size='Medium' 
        variant='Fill' 
        squarePadding
        icon='download'
        onClick={openExportModal} 
      />
    </div>
    <TabButtonNav 
      selectedTab={selectedDivision.name}
      tabOptions={divisionOptions.map(o => o.name)}
      onChange={t => handleDivisionChange(divisionOptions.find(o => o.name === t)!) }
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
      <TeamGrid appContext={appContext} rosterId={rosterId} disableRosterEdit={disableRosterEdit} team={team} />
    </TeamWrapper>
  }

  async function handleRosterNameToggle() {
    if(!isEditingRosterName) {
      setIsEditingRosterName(true);
      return;
    }

    const response = await rosterNameApiClientRef.current.execute({ rosterId: rosterId, rosterName: rosterName });
    if(response.success)
       setIsEditingRosterName(false);
  } 

  function handleDivisionChange(division: KeyedCode) {
    setSelectedDivision(division);
    teamsRef.current?.scrollTo({ top: 0, behavior: 'auto' })
  }

  function openExportModal() {
    appContext.openModal(closeDialog => <RosterExportModal 
      appContext={appContext} 
      rosterId={rosterId}
      closeDialog={closeDialog} 
    />)
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


export const loadRosterEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'RosterEditorPage') throw '';

  const apiClient = new LoadExistingRosterApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ rosterId: pageDef.rosterId });
  return {
    title: response.rosterDetails.name,
    renderPage: (appContext) => <RosterEditorPage 
      appContext={appContext} 
      divisionOptions={response.divisionOptions}
      rosterDetails={response.rosterDetails} 
    />
  }
}

      