import React from "react";
import styled from "styled-components";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { Link } from "../../components/link/link";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { AppContext } from "../appContext";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { toShortDateTimeString } from "../shared/dateUtils";
import { deepEquals } from "../shared/deepEquals";
import { KeyedCode } from "../shared/keyedCode";
import { getPositionType, Position } from "../shared/positionCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { AppearanceEditor } from "./appearanceEditor";
import { HitterAbilitiesEditor } from "./hitterAbilitiesEditor";
import { BattingSide } from "./hotZoneGrid";
import { LoadPlayerEditorApiClient, PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PitcherAbilitiesEditor } from "./pitcherAbilitiesEditor";
import { buildSavePlayerRequestFromState, getAppearanceReducer, getDetailsReducer, getHitterAbilitiesReducer, getInitialStateFromResponse, getPersonalDetailsReducer, getPitcherAbilitiesReducer, getPositionCapabilityDetailsReducer, getSepcialAbilitiesReducer, PlayerEditorReducer, PlayerEditorTab, playerEditorTabOptions, PlayerPersonalDetailsContext } from "./playerEditorState";
import { PlayerPersonalDetailsEditor } from "./playerPersonalDetailsEditor";
import { PositionCapabilitiesEditor } from "./positionCapabilitiesEditor";
import { SavePlayerApiClient } from "./savePlayerApiClient";
import { SpecialAbilitiesEditor } from "./specialAbilitiesEditor";

interface PlayerEditorPageProps {
  appContext: AppContext;
  playerId: number;
  editorResponse: PlayerEditorResponse 
}

function PlayerEditorPage(props: PlayerEditorPageProps) {
  const { appContext, playerId, editorResponse } = props;
  const { sourceType, canEdit, options } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const reducerContext: PlayerPersonalDetailsContext = {
    swingManRole: options.personalDetailsOptions.pitcherTypes.find(t => t.key === 'SwingMan') as KeyedCode,
    starterRole: options.personalDetailsOptions.pitcherTypes.find(t => t.key === 'Starter') as KeyedCode
  }

  const [state, update] = useReducerWithContext(PlayerEditorReducer, getInitialStateFromResponse(editorResponse), reducerContext);
  const [currentDetails, updateCurrentDetails] = getDetailsReducer(state, update);
  const [personalDetails, updatePersonalDetails] = getPersonalDetailsReducer(currentDetails, updateCurrentDetails);
  const [apperance, updateAppearance] = getAppearanceReducer(currentDetails, updateCurrentDetails);
  const [positionCapabilityDetails, updatePositionCapabilities] = getPositionCapabilityDetailsReducer(currentDetails, updateCurrentDetails);
  const [hitterAbilities, updateHitterAbilities] = getHitterAbilitiesReducer(currentDetails, updateCurrentDetails);
  const [pitcherAbilities, updatePitcherAbilities] = getPitcherAbilitiesReducer(currentDetails, updateCurrentDetails);
  const [specialAbilities, updateSpecialAbilities] = getSepcialAbilitiesReducer(currentDetails, updateCurrentDetails);

  const savedName = personalDetails.useSpecialSavedName
    ? editorResponse.personalDetails.savedName
    : personalDetails.savedName;

  const positionType = getPositionType(currentDetails.personalDetails.position.key as Position);

  const hasUnsavedChanges = !deepEquals(state.lastSavedDetails, state.currentDetails);
  const actionsDisabled = !canEdit || !hasUnsavedChanges;
  const actionsDisabledTooltip = !canEdit
    ? 'Players of this type cannot be edited'
    : !hasUnsavedChanges
      ? 'No changes'
      : undefined;

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <PlayerHeaderContainer>
      <div style={{ flex: '0 0 375px'}}>
        <PlayerNameBubble 
          appContext={appContext}
          sourceType={sourceType}
          playerId={playerId}
          positionType={positionType}
          size='Large'
          fullWidth
          withoutInfoFlyout>
            {savedName}
        </PlayerNameBubble>
      </div>
      <div>
      <PositionBubble
        positionType={positionType}
        size='Large'>
          {currentDetails.personalDetails.position.name}
      </PositionBubble>
      </div>
      <div>
      <OutlineHeader fontSize={FONT_SIZES._40} strokeWeight={2} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>
        {personalDetails.uniformNumber}
      </OutlineHeader>
      </div>
      <PlayerHeaderActions>
        <div>
          <PlayerHeaderActionButtons>
            <Link icon='up-right-from-square' url={editorResponse.baseballReferenceUrl}>BBRef</Link>
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
              onClick={savePlayer} 
              icon='floppy-disk' 
              disabled={actionsDisabled} 
              title={actionsDisabledTooltip}>
                  Save
            </Button>
          </PlayerHeaderActionButtons>
          {!!state.dateLastSaved && 
          <span>Last Save: {toShortDateTimeString(state.dateLastSaved, true)}</span>}
        </div>
      </PlayerHeaderActions>
    </PlayerHeaderContainer>
    <TabButtonNav 
      selectedTab={state.selectedTab}
      tabOptions={playerEditorTabOptions.slice()}
      onChange={t => update({ type: 'updateSelectedTab', selectedTab: t as PlayerEditorTab })}
    />
  </> 

  return <PowerUpLayout headerText='Edit Player'>
    <ContentWithHangingHeader header={header} headerHeight='128px'>
      <EditorContainer>
        {state.selectedTab === 'Personal' && 
        <PlayerPersonalDetailsEditor
          appContext={appContext}
          options={options.personalDetailsOptions}
          initiallyHadSpecialSavedName={editorResponse.personalDetails.isSpecialSavedName}
          details={personalDetails}
          disabled={!canEdit}
          update={updatePersonalDetails}      
        />}
        {state.selectedTab === 'Appearance' &&
        <AppearanceEditor 
          options={options.appearanceOptions}
          details={apperance}
          disabled={!canEdit}
          update={updateAppearance}
        />}
        {state.selectedTab === 'Positions' &&
        <PositionCapabilitiesEditor 
          primaryPosition={currentDetails.personalDetails.position}
          options={options.positionCapabilityOptions}
          details={positionCapabilityDetails}
          disabled={!canEdit}
          update={updatePositionCapabilities}
        />}
        {state.selectedTab === 'Hitter' &&
        <HitterAbilitiesEditor
          battingSide={currentDetails.personalDetails.battingSide.key as BattingSide}
          details={hitterAbilities}
          disabled={!canEdit}
          update={updateHitterAbilities}
        />}
        {state.selectedTab === 'Pitcher' &&
        <PitcherAbilitiesEditor
          options={options.pitcherAbilitiesOptions}
          details={pitcherAbilities}
          disabled={!canEdit}
          update={updatePitcherAbilities}
        />}
        {state.selectedTab === 'Special' &&
        <SpecialAbilitiesEditor
          options={options.specialAbilitiesOptions}
          details={specialAbilities}
          isPitcher={positionType === 'Pitcher'}
          disabled={!canEdit}
          update={updateSpecialAbilities}
        />}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function savePlayer() {
    const response = await apiClientRef.current.execute(buildSavePlayerRequestFromState(currentDetails, playerId));
    if(response.success)
      update({ type: 'updateFromSave' });
  } 
}

const PlayerHeaderContainer = styled.div`
  display: flex;
  gap: 16px;
  align-items: stretch;
  padding-top: 4px;
  padding-bottom: 8px;
  min-height: 64px;
`

const PlayerHeaderActions = styled.div`
  flex: auto;
  display: flex;
  gap: 8px;
  justify-content: flex-end;
`

const PlayerHeaderActionButtons = styled.div`
  display: flex;
  gap: 8px;
  justify-content: flex-end;
`

const EditorContainer = styled.div`
  padding: 16px;
`

export const loadPlayerEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'PlayerEditorPage') throw '';
  
  const apiClient = new LoadPlayerEditorApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ playerId: pageDef.playerId });

  return {
    title: `${response.personalDetails.firstName} ${response.personalDetails.lastName}`,
    renderPage: (appContext) => <PlayerEditorPage appContext={appContext} playerId={pageDef.playerId} editorResponse={response} />
  }
}