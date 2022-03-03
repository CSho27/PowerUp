import React from "react";
import styled from "styled-components"
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
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
import { LoadPlayerEditorApiClient, PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { getInitialStateFromResponse, getPersonalDetailsReducer, PlayerEditorStateReducer, PlayerPersonalDetailsContext } from "./playerEditorState";
import { PlayerPersonalDetailsEditor } from "./playerPersonalDetailsEditor";
import { SavePlayerApiClient, SavePlayerRequest } from "./savePlayerApiClient";

export interface PlayerEditorPageProps {
  appContext: AppContext;
  playerKey: string;
  editorResponse: PlayerEditorResponse 
}

const tabs = [
  'Personal',
//'Appearance',
  'Positions',
  'Hitter',
  'Pitcher',
//'Special'
]

const tabOptions: KeyedCode[] = tabs.map(t => ({ key: t, name: t }));

export function PlayerEditorPage(props: PlayerEditorPageProps) {
  const { appContext, playerKey, editorResponse } = props;
  const { options } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const reducerContext: PlayerPersonalDetailsContext = {
    swingManRole: options.pitcherTypes.find(t => t.key === 'SwingMan') as KeyedCode,
    starterRole: options.pitcherTypes.find(t => t.key === 'Starter') as KeyedCode
  }
  const [state, update] = useReducerWithContext(PlayerEditorStateReducer, getInitialStateFromResponse(editorResponse), reducerContext);
  const [personalDetails, updatePersonalDetails] = getPersonalDetailsReducer(state, update);

  const savedName = personalDetails.useSpecialSavedName
    ? editorResponse.personalDetails.savedName
    : personalDetails.savedName;

  const positionType = getPositionType(state.personalDetails.position.key as Position);

  const header = <>
    <Breadcrumbs>
      <Crumb key='Home' onClick={() => {}}>Home</Crumb>
      <Crumb key='RosterEditor' onClick={() => {}}>Roster</Crumb>
    </Breadcrumbs>
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
      selectedTab={{ key: 'Personal', name: 'Personal' }}
      tabOptions={tabOptions}
      onChange={() => {}}
    />
  </> 

  return <PowerUpLayout headerText='Edit Player'>
    <ContentWithHangingHeader header={header} headerHeight='128px'>
      <PlayerPersonalDetailsEditor
        options={options}
        initiallyHadSpecialSavedName={editorResponse.personalDetails.isSpecialSavedName}
        details={state.personalDetails}
        update={updatePersonalDetails}      
      />
    </ContentWithHangingHeader>
  </PowerUpLayout>

  async function savePlayer() {
    const { personalDetails } = state;
    
    const request: SavePlayerRequest = {
      playerKey: playerKey,
      firstName: personalDetails.firstName,
      lastName: personalDetails.lastName,
      useSpecialSavedName: personalDetails.useSpecialSavedName,
      savedName: personalDetails.savedName,
      uniformNumber: personalDetails.uniformNumber,
      positionKey: personalDetails.position.key,
      pitcherTypeKey: personalDetails.pitcherType.key,
      voiceId: editorResponse.personalDetails.voiceId.id, // TODO: Implement Voice
      battingSideKey: personalDetails.battingSide.key,
      battingStanceId: personalDetails.battingStance.id,
      throwingArmKey: personalDetails.throwingArm.key,
      pitchingMechanicsId: personalDetails.pitchingMechanics.id,
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
  const response = await apiClient.execute({ playerKey: pageDef.playerKey });
  return <PlayerEditorPage appContext={appContext} playerKey={pageDef.playerKey} editorResponse={response} />;
}