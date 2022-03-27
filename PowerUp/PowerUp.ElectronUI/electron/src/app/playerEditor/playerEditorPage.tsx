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
import { HitterAbilitiesEditor } from "./hitterAbilitiesEditor";
import { BattingSide } from "./hotZoneGrid";
import { LoadPlayerEditorApiClient, PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PitcherAbilitiesEditor } from "./pitcherAbilitiesEditor";
import { getHitterAbilitiesReducer, getInitialStateFromResponse, getPersonalDetailsReducer, getPitcherAbilitiesReducer, getPositionCapabilityDetailsReducer, PlayerEditorStateReducer, PlayerEditorTab, playerEditorTabOptions, PlayerPersonalDetailsContext } from "./playerEditorState";
import { PlayerPersonalDetailsEditor } from "./playerPersonalDetailsEditor";
import { PositionCapabilitiesEditor } from "./positionCapabilitiesEditor";
import { SavePlayerApiClient, SavePlayerRequest } from "./savePlayerApiClient";

export interface PlayerEditorPageProps {
  appContext: AppContext;
  playerId: number;
  editorResponse: PlayerEditorResponse 
}

export function PlayerEditorPage(props: PlayerEditorPageProps) {
  const { appContext, playerId, editorResponse } = props;
  const { options } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const reducerContext: PlayerPersonalDetailsContext = {
    swingManRole: options.pitcherTypes.find(t => t.key === 'SwingMan') as KeyedCode,
    starterRole: options.pitcherTypes.find(t => t.key === 'Starter') as KeyedCode
  }
  const [state, update] = useReducerWithContext(PlayerEditorStateReducer, getInitialStateFromResponse(editorResponse), reducerContext);
  const [personalDetails, updatePersonalDetails] = getPersonalDetailsReducer(state, update);
  const [positionCapabilityDetails, updatePositionCapabilities] = getPositionCapabilityDetailsReducer(state, update);
  const [hitterAbilities, updateHitterAbilities] = getHitterAbilitiesReducer(state, update);
  const [pitcherAbilities, updatePitcherAbilities] = getPitcherAbilitiesReducer(state, update);

  const savedName = personalDetails.useSpecialSavedName
    ? editorResponse.personalDetails.savedName
    : personalDetails.savedName;

  const positionType = getPositionType(state.personalDetails.position.key as Position);

  const header = <>
    <Breadcrumbs appContext={appContext}/>
    <PlayerHeaderContainer>
      <div style={{ flex: '0 0 250px'}}>
        <PlayerNameBubble 
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
          options={options}
          initiallyHadSpecialSavedName={editorResponse.personalDetails.isSpecialSavedName}
          details={state.personalDetails}
          update={updatePersonalDetails}      
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
          options={options}
          details={pitcherAbilities}
          update={updatePitcherAbilities}
        />}
      </EditorContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function savePlayer() {
    const request: SavePlayerRequest = {
      playerId: playerId,
      personalDetails: {
        firstName: personalDetails.firstName,
        lastName: personalDetails.lastName,
        useSpecialSavedName: personalDetails.useSpecialSavedName,
        savedName: personalDetails.savedName,
        uniformNumber: personalDetails.uniformNumber,
        positionKey: personalDetails.position.key,
        pitcherTypeKey: personalDetails.pitcherType.key,
        voiceId: personalDetails.voice.id,
        battingSideKey: personalDetails.battingSide.key,
        battingStanceId: personalDetails.battingStance.id,
        throwingArmKey: personalDetails.throwingArm.key,
        pitchingMechanicsId: personalDetails.pitchingMechanics.id,
      },
      positionCapabilities: {
        pitcher: positionCapabilityDetails.pitcher.key,
        catcher: positionCapabilityDetails.catcher.key,
        firstBase: positionCapabilityDetails.firstBase.key,
        secondBase: positionCapabilityDetails.secondBase.key,
        thirdBase: positionCapabilityDetails.thirdBase.key,
        shortstop: positionCapabilityDetails.shortstop.key,
        leftField: positionCapabilityDetails.leftField.key,
        centerField: positionCapabilityDetails.centerField.key,
        rightField: positionCapabilityDetails.rightField.key
      },
      hitterAbilities: {
        trajectory: hitterAbilities.trajectory,
        contact: hitterAbilities.contact,
        power: hitterAbilities.power,
        runSpeed: hitterAbilities.runSpeed,
        armStrength: hitterAbilities.armStrength,
        fielding: hitterAbilities.fielding,
        errorResistance: hitterAbilities.errorResistance,
        hotZoneGrid: hitterAbilities.hotZones
      },
      pitcherAbilities: {
        topSpeed: pitcherAbilities.topSpeed,
        control: pitcherAbilities.control,
        stamina: pitcherAbilities.stamina,

        twoSeamTypeKey: pitcherAbilities.twoSeamType?.key ?? null,
        twoSeamMovement: pitcherAbilities.twoSeamType
          ? pitcherAbilities.twoSeamMovement
          : null,

        slider1TypeKey: pitcherAbilities.slider1Type?.key ?? null,
        slider1Movement: pitcherAbilities.slider1Type
          ? pitcherAbilities.slider1Movement
          : null,
        
        slider2TypeKey: pitcherAbilities.slider1Type
          ? pitcherAbilities.slider2Type?.key ?? null
          : null,
        slider2Movement: pitcherAbilities.slider1Type && pitcherAbilities.slider2Type
          ? pitcherAbilities.slider2Movement
          : null,

        curve1TypeKey: pitcherAbilities.curve1Type?.key ?? null,
        curve1Movement: pitcherAbilities.curve1Type
          ? pitcherAbilities.curve1Movement
          : null,

        curve2TypeKey: pitcherAbilities.curve1Type
          ? pitcherAbilities.curve2Type?.key ?? null
          : null,
        curve2Movement: pitcherAbilities.curve1Type && pitcherAbilities.curve2Type
          ? pitcherAbilities.curve2Movement
          : null,

        fork1TypeKey: pitcherAbilities.fork1Type?.key ?? null,
        fork1Movement: pitcherAbilities.fork1Type
          ? pitcherAbilities.fork1Movement
          : null,

        fork2TypeKey: pitcherAbilities.fork1Type
          ? pitcherAbilities.fork2Type?.key ?? null
          : null,
        fork2Movement: pitcherAbilities.fork1Type && pitcherAbilities.fork2Type
          ? pitcherAbilities.fork2Movement
          : null,

        sinker1TypeKey: pitcherAbilities.sinker1Type?.key ?? null,
        sinker1Movement: pitcherAbilities.sinker1Type
          ? pitcherAbilities.sinker1Movement
          : null,

        sinker2TypeKey: pitcherAbilities.sinker1Type
          ? pitcherAbilities.sinker2Type?.key ?? null
          : null,
        sinker2Movement: pitcherAbilities.sinker1Type && pitcherAbilities.sinker2Type
          ? pitcherAbilities.sinker2Movement
          : null,

        sinkingFastball1TypeKey: pitcherAbilities.sinkingFastball1Type?.key ?? null,
        sinkingFastball1Movement: pitcherAbilities.sinkingFastball1Type
          ? pitcherAbilities.sinkingFastball1Movement
          : null,
        
        sinkingFastball2TypeKey: pitcherAbilities.sinkingFastball1Type
          ? pitcherAbilities.sinkingFastball2Type?.key ?? null
          : null,
        sinkingFastball2Movement: pitcherAbilities.sinkingFastball1Type && pitcherAbilities.sinkingFastball2Type
          ? pitcherAbilities.sinkingFastball2Movement
          : null
      }
    }
    const response = await apiClientRef.current.execute(request);
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
