import { useMemo, useRef, useState } from "react";
import styled from "styled-components";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { SourceTypeStamp } from "../../components/sourceTypeStamp/sourceTypeStamp";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { TextField } from "../../components/textField/textField";
import { FONT_SIZES } from "../../style/constants";
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { toIdentifier } from "../../utils/getIdentifier";
import { AppContext } from "../appContext";
import { PageLoadDefinition, PageLoadFunction, PageProps, PagePropsLoadFunction } from "../pages";
import { KeyedCode } from "../shared/keyedCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { EditRosterNameApiClient } from "./editRosterNameApiClient";
import { LoadExistingRosterApiClient } from "./loadExistingRosterApiClient";
import { PlayerGrid } from "./playerGrid";
import { ReplaceFreeAgentApiClient } from "./replaceFreeAgentApiClient";
import { RosterDetails, TeamDetails } from "./rosterEditorDTOs";
import { openRosterExportModal } from "./rosterExportModal";
import { TeamGrid } from "./teamGrid";
import { DraftPoolApiClient } from "../draftPage/draftPoolApiClient";

export interface RosterEditorPageProps {
  appContext: AppContext;
  divisionOptions: KeyedCode[];
  rosterDetails: RosterDetails;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, rosterDetails } = props;
  const { rosterId, name, teams, freeAgentHitters, freeAgentPitchers } = rosterDetails;

  const rosterNameApiClientRef = useRef(new EditRosterNameApiClient(appContext.commandFetcher));
  const replaceFreeAgentApiClientRef = useRef(new ReplaceFreeAgentApiClient(appContext.commandFetcher));

  const divisionOptions: KeyedCode[] = [...props.divisionOptions, { key: 'FreeAgents', name: 'Free Agents' }];
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
        <RosterHeader>{rosterName} - {toIdentifier('Roster', rosterId)}</RosterHeader>
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
          {...toDisabledProps('Edit roster name', ...disableRosterEdit)}
          icon={isEditingRosterName ? 'lock' : 'pen-to-square'}
          onClick={handleRosterNameToggle}
        />
        
      </div>
      <div style={{ display: 'flex', gap: '16px', alignItems: 'center' }}>
        <Button 
          size='Medium' 
          variant='Fill' 
          squarePadding
          icon='list-ol'
          onClick={() => appContext.setPage({ page: 'DraftPage', rosterId: rosterId })} 
        />
        <Button 
          size='Medium' 
          variant='Fill' 
          squarePadding
          icon='download'
          onClick={() => openRosterExportModal(appContext, rosterId)} 
        />
      </div>
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
        {selectedDivision.key === 'FreeAgents' &&

        <FreeAgentTable>
          <PlayerGrid
            appContext={appContext}
            hitters={freeAgentHitters}
            pitchers={freeAgentPitchers}
            disableManagement={disableRosterEdit}
            replacePlayer={replacePlayer}
          />
        </FreeAgentTable>}
      </TeamsContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toTeamGrid(team: TeamDetails) {
    return <TeamWrapper key={`${team.teamId} - ${team.powerProsTeam}`}>
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

  async function replacePlayer(playerToReplaceId: number, playerToInsertId: number) {
    const response = await replaceFreeAgentApiClientRef.current.execute({
      rosterId: rosterId,
      playerToReplaceId: playerToReplaceId,
      playerToInsertId: playerToInsertId
    });
    return response.success;
  }

  function handleDivisionChange(division: KeyedCode) {
    setSelectedDivision(division);
    teamsRef.current?.scrollTo({ top: 0, behavior: 'auto' })
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

const FreeAgentTable = styled.table`
  width: 100%;
  border-collapse: collapse;
  isolation: isolate;
`

export const loadRosterEditorPageProps: PagePropsLoadFunction<RosterEditorPageProps> = async (
  appContext: AppContext, 
  pageDef: PageLoadDefinition
): Promise<PageProps<RosterEditorPageProps>> => {
  if(pageDef.page !== 'RosterEditorPage') throw '';

  const apiClient = new LoadExistingRosterApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ rosterId: pageDef.rosterId });
  return {
    title: response.rosterDetails.name,
    divisionOptions: response.divisionOptions,
    rosterDetails: response.rosterDetails,
  }
}

export const loadRosterEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  const props = await loadRosterEditorPageProps(appContext, pageDef);
  return {
    title: props.title,
    renderPage: (appContext) => <RosterEditorPage 
      appContext={appContext} 
      divisionOptions={props.divisionOptions}
      rosterDetails={props.rosterDetails} 
    />
  }
}

      