import React from "react";
import styled from "styled-components"
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { Button } from "../../components/button/button";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { KeyedCode } from "../shared/keyedCode";
import { getPositionType, Position } from "../shared/positionCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { AppearanceEditor } from "./appearanceEditor";
import { HitterAbilitiesEditor } from "./hitterAbilitiesEditor";
import { BattingSide } from "./hotZoneGrid";
import { LoadPlayerEditorApiClient, PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PitcherAbilitiesEditor } from "./pitcherAbilitiesEditor";
import { buildSavePlayerRequestFromState, getAppearanceReducer, getHitterAbilitiesReducer, getInitialStateFromResponse, getPersonalDetailsReducer, getPitcherAbilitiesReducer, getPositionCapabilityDetailsReducer, getSepcialAbilitiesReducer, PlayerEditorStateReducer, PlayerEditorTab, playerEditorTabOptions, PlayerPersonalDetailsContext } from "./playerEditorState";
import { PlayerPersonalDetailsEditor } from "./playerPersonalDetailsEditor";
import { PositionCapabilitiesEditor } from "./positionCapabilitiesEditor";
import { SavePlayerApiClient, SavePlayerRequest } from "./savePlayerApiClient";
import { SpecialAbilitiesEditor } from "./specialAbilitiesEditor";

export interface PlayerEditorPageProps {
  appContext: AppContext;
  playerId: number;
  editorResponse: PlayerEditorResponse 
}

export function PlayerEditorPage(props: PlayerEditorPageProps) {
  const { appContext, playerId, editorResponse } = props;
  const { sourceType, canEdit, options } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const reducerContext: PlayerPersonalDetailsContext = {
    swingManRole: options.personalDetailsOptions.pitcherTypes.find(t => t.key === 'SwingMan') as KeyedCode,
    starterRole: options.personalDetailsOptions.pitcherTypes.find(t => t.key === 'Starter') as KeyedCode
  }
  const [state, update] = useReducerWithContext(PlayerEditorStateReducer, getInitialStateFromResponse(editorResponse), reducerContext);
  const [personalDetails, updatePersonalDetails] = getPersonalDetailsReducer(state, update);
  const [apperance, updateAppearance] = getAppearanceReducer(state, update);
  const [positionCapabilityDetails, updatePositionCapabilities] = getPositionCapabilityDetailsReducer(state, update);
  const [hitterAbilities, updateHitterAbilities] = getHitterAbilitiesReducer(state, update);
  const [pitcherAbilities, updatePitcherAbilities] = getPitcherAbilitiesReducer(state, update);
  const [specialAbilities, updateSpecialAbilities] = getSepcialAbilitiesReducer(state, update);

  const savedName = personalDetails.useSpecialSavedName
    ? editorResponse.personalDetails.savedName
    : personalDetails.savedName;

  const positionType = getPositionType(state.personalDetails.position.key as Position);

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <PlayerHeaderContainer>
      <div style={{ flex: '0 0 250px'}}>
        <PlayerNameBubble 
          sourceType={sourceType}
          positionType={positionType}
          size='Large'
          fullWidth
        >
          {savedName}
        </PlayerNameBubble>
      </div>
      <PositionBubble
        positionType={positionType}
        size='Large'
      >
        {state.personalDetails.position.name}
      </PositionBubble>
      <OutlineHeader fontSize={FONT_SIZES._40} strokeWeight={2} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>
        {personalDetails.uniformNumber}
      </OutlineHeader>
      <div style={{ flex: 'auto', display: 'flex', justifyContent: 'flex-end' }}>
        <Button variant='Fill' size='Medium' onClick={savePlayer} icon='floppy-disk'>Save</Button>
      </div>
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
          primaryPosition={state.personalDetails.position}
          options={options.positionCapabilityOptions}
          details={positionCapabilityDetails}
          update={updatePositionCapabilities}
        />}
        {state.selectedTab === 'Hitter' &&
        <HitterAbilitiesEditor
          battingSide={state.personalDetails.battingSide.key as BattingSide}
          details={hitterAbilities}
          update={updateHitterAbilities}
        />}
        {state.selectedTab === 'Pitcher' &&
        <PitcherAbilitiesEditor
          options={options.pitcherAbilitiesOptions}
          details={pitcherAbilities}
          update={updatePitcherAbilities}
        />}
        {state.selectedTab === 'Special' &&
        <SpecialAbilitiesEditor
          options={options.specialAbilitiesOptions}
          details={specialAbilities}
          isPitcher={positionType === 'Pitcher'}
          update={updateSpecialAbilities}
        />}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function savePlayer() {
    const response = await apiClientRef.current.execute(buildSavePlayerRequestFromState(state, playerId));
    console.log(response);
  } 
}

const PlayerHeaderContainer = styled.div`
  display: flex;
  gap: 16px;
  align-items: center;
  padding-bottom: 8px;
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

const EditorContainer = styled.div`
padding: 16px;
`
