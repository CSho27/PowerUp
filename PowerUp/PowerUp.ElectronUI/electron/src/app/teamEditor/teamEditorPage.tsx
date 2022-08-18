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
import { PitcherRoleDefinition, PitcherRolesEditor } from "./pitcherRolesEditor";
import { LineupEditor, LineupSlotDefinition } from "./lineupEditor";
import { DisabledCriterion, toDisabledProps } from "../../utils/disabledProps";
import { toIdentifier } from "../../utils/getIdentifier";

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
  const editorDisabled: DisabledCriterion = { isDisabled: !canEdit, tooltipIfDisabled: 'Teams of this type cannot be edited' };
  const noUnsavedChangesDisabled: DisabledCriterion = { isDisabled: !hasUnsavedChanges, tooltipIfDisabled: 'No changes' };
  const actionsDisabled: DisabledCriterion[] = [editorDisabled, noUnsavedChangesDisabled]

const header = <>
    <Breadcrumbs appContext={appContext}/>
    <TeamHeaderContainer>
      <div style={{ flex: '0 0 450px', display: 'flex', gap: '16px', alignItems: 'center' }}>
        <div style={{ flex: 'auto' }}>
          {!state.isEditingName && <TeamHeading>
            {currentDetails.teamName} - {toIdentifier('Team', teamId)}
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
          {...toDisabledProps('Edit team name', editorDisabled)}
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
              {...toDisabledProps('Undo changes', ...actionsDisabled)}>
                Undo Changes
            </Button>
            <Button 
              variant='Fill' 
              size='Small' 
              onClick={() => saveTeam(true)} 
              icon='floppy-disk'
              {...toDisabledProps('Save chages', ...actionsDisabled)}>
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

  return <PowerUpLayout appContext={appContext} headerText='Edit Team'>
    <ContentWithHangingHeader header={header} headerHeight='130px'>
      <EditorContainer>
        {state.selectedTab === 'Management' &&
        <TeamManagementEditor 
          appContext={appContext}
          mlbPlayers={mlbPlayers}
          aaaPlayers={aaaPlayers}
          disabled={[editorDisabled]}
          update={updateCurrentDetails} 
          saveTemp={() => saveTeam(false)}/>}
        {state.selectedTab === 'Pitcher Roles' &&
        <PitcherRolesEditor 
          appContext={appContext}
          pitchers={pitchers.map(toPitcherDetails)}
          disabled={[editorDisabled]}
          updateRole={(id, role, index) => updateCurrentDetails({ type: 'updatePitcherRole', playerId: id, role: role, orderInRole: index })}
        />}
        {state.selectedTab === 'No DH Lineup' &&
        <LineupEditor 
          appContext={appContext}
          players={mlbPlayers.map(p => toHitterDetails(p, false))}
          useDh={false}
          disabled={[editorDisabled]}
          updateLineupOrder={(id, currentOrder, newOrder) => updateCurrentDetails({ type: 'reorderNoDHLineup', playerIdentifier: id, currentOrderInLineup: currentOrder, newOrderInLineup: newOrder })}
          swapPositions={(p1, p2) => updateCurrentDetails({ type: 'swapPositionInNoDHLineup', position1: p1, position2: p2 })}
          swapPlayers={(p1, p2) => updateCurrentDetails({ type: 'swapPlayersInNoDHLineup', playerId1: p1, playerId2: p2 })}
        />}
        {state.selectedTab === 'DH Lineup' &&
        <LineupEditor 
          appContext={appContext}
          players={mlbPlayers.map(p => toHitterDetails(p, true))}
          useDh={true}
          disabled={[editorDisabled]}
          updateLineupOrder={(id, currentOrder, newOrder) => updateCurrentDetails({ type: 'reorderDHLineup', playerIdentifier: id, currentOrderInLineup: currentOrder, newOrderInLineup: newOrder })}
          swapPositions={(p1, p2) => updateCurrentDetails({ type: 'swapPositionInDHLineup', position1: p1, position2: p2 })}
          swapPlayers={(p1, p2) => updateCurrentDetails({ type: 'swapPlayersInDHLineup', playerId1: p1, playerId2: p2 })}
        />}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toPitcherDetails(player: PlayerRoleState): PitcherRoleDefinition {
    const { playerDetails, pitcherRole, orderInPitcherRole } = player;

    return {
      role: pitcherRole,
      orderInRole: orderInPitcherRole,
      details: {
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
  }

  function toHitterDetails(player: PlayerRoleState, useDh: boolean): LineupSlotDefinition {
    const { playerDetails } = player;
    
    return {
      orderInLineup: useDh
        ? player.orderInDHLineup
        : player.orderInNoDHLineup,
      position: useDh 
        ? player.positionInDHLineup 
        : player.positionInNoDHLineup,
      details: {
        sourceType: playerDetails.sourceType,
        playerId: playerDetails.playerId,
        savedName: playerDetails.savedName,
        fullName: playerDetails.fullName,
        position: playerDetails.position,
      }
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
      isDefensiveLiability: roleState.isDefensiveLiability,
      pitcherRole: roleState.pitcherRole,
      orderInPitcherRole: roleState.orderInPitcherRole,
      orderInNoDHLineup: roleState.orderInNoDHLineup ?? null,
      positionInNoDHLineup: roleState.positionInNoDHLineup ?? null,
      orderInDHLineup: roleState.orderInDHLineup ?? null,
      positionInDHLineup: roleState.positionInDHLineup ?? null
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