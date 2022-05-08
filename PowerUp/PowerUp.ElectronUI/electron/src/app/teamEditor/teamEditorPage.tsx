import { useReducer, useRef } from "react";
import styled from "styled-components";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { SourceTypeStamp } from "../../components/sourceTypeStamp/sourceTypeStamp";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { TextField } from "../../components/textField/textField";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { PageCleanupFunction, PageLoadDefinition, PageLoadFunction } from "../pages";
import { toShortDateTimeString } from "../shared/dateUtils";
import { deepEquals } from "../shared/deepEquals";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { DiscardTempTeamApiClient } from "./discardTempTeamApiClient";
import { LoadTeamEditorApiClient, LoadTeamEditorResponse } from "./loadTeamEditorApiClient";
import { PlayerRoleRequest, SaveTeamApiClient, SaveTeamRequest } from "./saveTeamApiClient";
import { getDetailsReducer, getInitialStateFromResponse, TeamEditorDetails, TeamEditorReducer, TeamEditorTab, teamEditorTabOptions } from "./teamEditorState";
import { TeamManagementEditor } from "./teamManagementEditor";
import { PlayerRoleState } from "./playerRoleState";
import { PitcherDetails, PitcherRolesEditor } from "./pitcherRolesEditor";

interface TeamEditorPageProps {
  appContext: AppContext;
  teamId: number;
  editorResponse: LoadTeamEditorResponse
}

function TeamEditorPage(props: TeamEditorPageProps) {
  const { appContext, teamId, editorResponse } = props;
  const { sourceType, canEdit, tempTeamId } = editorResponse;

  const apiClientRef = useRef(new SaveTeamApiClient(appContext.commandFetcher));

  const [state, update] = useReducer(TeamEditorReducer, getInitialStateFromResponse(editorResponse));
  const [currentDetails, updateCurrentDetails] = getDetailsReducer(state, update);
  const { mlbPlayers, aaaPlayers } = currentDetails
  const pitchers = mlbPlayers.filter(p => p.playerDetails.position === 'Pitcher');

  const hasUnsavedChanges = !deepEquals(state.lastSavedDetails, state.currentDetails);
  const actionsDisabled = /*!canEdit ||*/ !hasUnsavedChanges;
  const actionsDisabledTooltip = /*!canEdit
    ? 'Teams of this type cannot be edited'
    : */!hasUnsavedChanges
      ? 'No changes'
      : undefined;

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <TeamHeaderContainer>
      <div style={{ flex: '0 0 450px', display: 'flex', gap: '16px', alignItems: 'center' }}>
        <div style={{ flex: 'auto' }}>
          {!state.isEditingName && <TeamHeading>
            {currentDetails.teamName}
            <SourceTypeStamp 
              theme='Dark'
              size='Medium'
              sourceType={sourceType}
            />
          </TeamHeading>}
          {state.isEditingName && 
          <TextField 
            value={currentDetails.teamName} 
            onChange={name => updateCurrentDetails({ type: 'updateTeamName', teamName: name })}        
          />}
        </div>
        <Button 
          variant='Outline'
          size='Small'
          icon={state.isEditingName ? 'lock' : 'pen-to-square'}
          onClick={() => update({ type: 'toggleIsEditingName' })}
        />
      </div>
      <TeamHeaderActions>
        <div>
          <TeamHeaderActionButtons>
            <Button
              variant='Outline'
              size='Small'
              onClick={undoChanges}
              icon='rotate-left'
              disabled={actionsDisabled}
              title={actionsDisabledTooltip}>
                Undo Changes
            </Button>
            <Button 
              variant='Fill' 
              size='Small' 
              onClick={() => saveTeam(true)} 
              icon='floppy-disk' 
              disabled={actionsDisabled} 
              title={actionsDisabledTooltip}>
                  Save
            </Button>
          </TeamHeaderActionButtons>
          {!!state.dateLastSaved && 
          <span>Last Save: {toShortDateTimeString(state.dateLastSaved, true)}</span>}
        </div>
      </TeamHeaderActions>
    </TeamHeaderContainer>
    <TabButtonNav 
      selectedTab={state.selectedTab}
      tabOptions={teamEditorTabOptions.slice()}
      onChange={t => update({ type: 'updateSelectedTab', selectedTab: t as TeamEditorTab }) }
    />
  </> 

  return <PowerUpLayout headerText='Edit Team'>
    <ContentWithHangingHeader header={header} headerHeight='130px'>
      <EditorContainer>
        {state.selectedTab === 'Management' &&
        <TeamManagementEditor 
          appContext={appContext}
          mlbPlayers={mlbPlayers}
          aaaPlayers={aaaPlayers}
          disabled={false /*!canEdit*/}
          update={updateCurrentDetails} 
          saveTemp={() => saveTeam(false)}/>}
        {state.selectedTab === 'Pitcher Roles' &&
        <PitcherRolesEditor 
          appContext={appContext}
          starters={pitchers.filter(p => p.pitcherRole === 'Starter').map(toPitcherDetails)}
          swingMen={pitchers.filter(p => p.pitcherRole === 'SwingMan').map(toPitcherDetails)}
          longRelievers={pitchers.filter(p => p.pitcherRole === 'LongReliever').map(toPitcherDetails)}
          middleRelievers={pitchers.filter(p => p.pitcherRole === 'MiddleReliever').map(toPitcherDetails)}
          situationalLefties={pitchers.filter(p => p.pitcherRole === 'SituationalLefty').map(toPitcherDetails)}
          mopUpMen={pitchers.filter(p => p.pitcherRole === 'MopUpMan').map(toPitcherDetails)}
          setupMen={pitchers.filter(p => p.pitcherRole === 'SetupMan').map(toPitcherDetails)}
          closer={pitchers.filter(p => p.pitcherRole === 'Closer').map(toPitcherDetails)[0]}
        />}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toPitcherDetails(player: PlayerRoleState): PitcherDetails {
    const { playerDetails } = player;

    return {
      sourceType: playerDetails.sourceType,
      playerId: playerDetails.playerId,
      savedName: playerDetails.savedName,
      fullName: playerDetails.fullName,
      overall: playerDetails.overall,
      throwingArm: playerDetails.throwingArm,
      pitcherType: playerDetails.pitcherType,
      topSpeed: playerDetails.topSpeed,
      control: playerDetails.control,
      stamina: playerDetails.stamina
    }
  }

  async function saveTeam(persist: boolean) {
    const response = await apiClientRef.current.execute(buildSaveRequest(currentDetails, persist));
    if(persist && response.success)
      update({ type: 'updateFromSave' });
  }

  function buildSaveRequest(teamDetails: TeamEditorDetails, persist: boolean): SaveTeamRequest {
    return {
      teamId: teamId,
      tempTeamId: tempTeamId,
      persist: persist,
      name: teamDetails.teamName,
      mlbPlayers: teamDetails.mlbPlayers.map(toPlayerRoleRequest),
      aaaPlayers: teamDetails.aaaPlayers.map(toPlayerRoleRequest)
    }
  }

  function toPlayerRoleRequest(roleState: PlayerRoleState): PlayerRoleRequest {
    return {
      playerId: roleState.playerDetails.playerId,
      isPinchHitter: roleState.isPinchHitter,
      isPinchRunner: roleState.isPinchRunner,
      isDefensiveReplacement: roleState.isDefensiveReplacement,
      isDefensiveLiability: roleState.isDefensiveLiability
    }
  }

  async function undoChanges() {
    update({ type: 'undoChanges' });
    apiClientRef.current.execute(buildSaveRequest(state.lastSavedDetails, false));
  }
}

const TeamHeaderContainer = styled.div`
  display: flex;
  gap: 16px;
  align-items: stretch;
  padding-bottom: 8px;
  min-height: 48px;
`

const TeamHeading = styled.h1`
  display: flex;
  align-items: center;
  gap: 16px;
  font-size: ${FONT_SIZES._32};
  font-style: italic;
  white-space: nowrap;
`

const TeamHeaderActions = styled.div`
  flex: auto;
  display: flex;
  gap: 8px;
  justify-content: flex-end;
`

const TeamHeaderActionButtons = styled.div`
  display: flex;
  gap: 8px;
  justify-content: flex-end;
`

const EditorContainer = styled.div`
  padding: 16px;
`

export const loadTeamEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'TeamEditorPage') throw '';
  
  const apiClient = new LoadTeamEditorApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ teamId: pageDef.teamId, tempTeamId: pageDef.tempTeamId });

  return {
    title: response.lastSavedDetails.name,
    renderPage: appContext => <TeamEditorPage 
      appContext={appContext}
      teamId={pageDef.teamId}
      editorResponse={response}
    />,
    updatedPageLoadDef: {
      ...pageDef,
      tempTeamId: response.tempTeamId
    }
  }
}

export const cleanupTeamEditorPage: PageCleanupFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'TeamEditorPage') throw '';

  const apiClient = new DiscardTempTeamApiClient(appContext.commandFetcher);
  await apiClient.execute({ teamId: pageDef.teamId, tempTeamId: pageDef.tempTeamId! })
}