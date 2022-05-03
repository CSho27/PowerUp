import { useReducer } from "react";
import styled from "styled-components";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { LoadTeamEditorApiClient, LoadTeamEditorResponse } from "./loadTeamEditorApiClient";
import { getDetailsReducer, getInitialStateFromResponse, TeamEditorReducer, TeamEditorTab, teamEditorTabOptions } from "./teamEditorState";

interface TeamEditorPageProps {
  appContext: AppContext;
  teamId: number;
  editorResponse: LoadTeamEditorResponse
}

function TeamEditorPage(props: TeamEditorPageProps) {
  const { appContext, teamId, editorResponse } = props;

  const [state, update] = useReducer(TeamEditorReducer, getInitialStateFromResponse(editorResponse));
  const [currentDetails, updateCurrentDetails] = getDetailsReducer(state, update);

  const actionsDisabled = false;
  const actionsDisabledTooltip = '';

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <TeamHeaderContainer>
      <div style={{ flex: '0 0 450px', display: 'flex', gap: '16px', alignItems: 'center' }}>
        <div style={{ flex: 'auto' }}>
          {!state.isEditingName && <h1>{currentDetails.teamName}</h1>}
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
              onClick={() => {}/*update({ type: 'undoChanges' })*/}
              icon='rotate-left'
              disabled={actionsDisabled}
              title={actionsDisabledTooltip}>
                Undo Changes
            </Button>
            <Button 
              variant='Fill' 
              size='Small' 
              onClick={saveTeam} 
              icon='floppy-disk' 
              disabled={actionsDisabled} 
              title={actionsDisabledTooltip}>
                  Save
            </Button>
          </TeamHeaderActionButtons>
          {/*!!state.dateLastSaved && 
          <span>Last Save: {toShortDateTimeString(state.dateLastSaved, true)}</span>*/}
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
        Roster Editor
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function saveTeam() {

  }
}

const TeamHeaderContainer = styled.div`
  display: flex;
  gap: 16px;
  align-items: stretch;
  padding-bottom: 8px;
  min-height: 48px;
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
  const response = await apiClient.execute({ teamId: pageDef.teamId });

  return {
    title: `Edit Team`,
    renderPage: appContext => <TeamEditorPage 
      appContext={appContext}
      teamId={pageDef.teamId}
      editorResponse={response}
    />
  }
}