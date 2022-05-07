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
import { PowerUpLayout } from "../shared/powerUpLayout";
import { DiscardTempTeamApiClient } from "./discardTempTeamApiClient";
import { LoadTeamEditorApiClient, LoadTeamEditorResponse } from "./loadTeamEditorApiClient";
import { PlayerRoleRequest, SaveTeamApiClient } from "./saveTeamApiClient";
import { getDetailsReducer, getInitialStateFromResponse, getTeamManagementReducer, TeamEditorReducer, TeamEditorTab, teamEditorTabOptions } from "./teamEditorState";
import { TeamManagementEditor } from "./teamManagementEditor";
import { PlayerRoleState } from "./teamManagementEditorState";

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
  const [managementState, updateManagementState] = getTeamManagementReducer(currentDetails, updateCurrentDetails);

  const actionsDisabled = false;
  const actionsDisabledTooltip = '';

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
              onClick={() => update({ type: 'undoChanges' })}
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
    <ContentWithHangingHeader header={header} headerHeight='120px'>
      <EditorContainer>
        {state.selectedTab === 'Management' &&
        <TeamManagementEditor 
          appContext={appContext}
          teamId={teamId}
          state={managementState} 
          disabled={false /*!canEdit*/}
          update={updateManagementState} 
          saveTemp={() => saveTeam(false)}/>}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function saveTeam(persist: boolean) {
    apiClientRef.current.execute({
      teamId: teamId,
      tempTeamId: tempTeamId,
      persist: persist,
      name: currentDetails.teamName,
      mlbPlayers: currentDetails.teamManagmentState.mlbPlayers.map(toPlayerRoleRequest),
      aaaPlayers: currentDetails.teamManagmentState.aaaPlayers.map(toPlayerRoleRequest)
    });
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
    title: response.name,
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