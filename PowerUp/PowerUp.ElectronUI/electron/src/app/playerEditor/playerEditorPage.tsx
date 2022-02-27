import React from "react";
import styled from "styled-components"
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { TabButtonNav } from "../../components/tabButton/tabButton";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { TextBubble } from "../../components/textBubble/textBubble";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { KeyedCode } from "../shared/keyedCode";
import { getPositionType } from "../shared/positionCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { LoadPlayerEditorApiClient, PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PlayerEditorStateReducer } from "./playerEditorState";
import { PlayerPersonalDetailsEditor } from "./playerPersonalDetailsEditor";
import { SavePlayerApiClient } from "./savePlayerApiClient";

export interface PlayerEditorPageProps {
  appContext: AppContext;
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
  const { appContext, editorResponse } = props;
  const { options, personalDetails } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const [state, update] = React.useReducer(PlayerEditorStateReducer, {
    firstName: personalDetails.firstName,
    lastName: personalDetails.lastName,
    savedName: personalDetails.savedName,
    playerNumber: personalDetails.uniformNumber
  });

  const positionType = getPositionType(personalDetails.position.key);

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
          {state.savedName}
        </PlayerNameBubble>
      </div>
      <PositionBubble
        positionType={positionType}
        size='Large'
      >
        {personalDetails.position.name}
      </PositionBubble>
      <OutlineHeader fontSize={FONT_SIZES._48} strokeWeight={2} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>
        {personalDetails.uniformNumber}
      </OutlineHeader>
    </PlayerHeaderContainer>
    <TabButtonNav 
      selectedTab={{ key: 'Personal', name: 'Personal' }}
      tabOptions={tabOptions}
      onChange={() => {}}
    />
  </> 

  return <PowerUpLayout headerText='Edit Player'>
    <ContentWithHangingHeader header={header} headerHeight='128px'>
      <PlayerPersonalDetailsEditor />
    </ContentWithHangingHeader>
  </PowerUpLayout>
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
  return <PlayerEditorPage appContext={appContext} editorResponse={response} />;
}