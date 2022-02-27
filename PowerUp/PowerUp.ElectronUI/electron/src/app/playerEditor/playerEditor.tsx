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
import { KeyedCode } from "../shared/keyedCode";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PlayerEditorStateReducer } from "./playerEditorState";
import { SavePlayerApiClient } from "./savePlayerApiClient";

export interface PlayerEditorProps {
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

export function PlayerEditor(props: PlayerEditorProps) {
  const { appContext, editorResponse } = props;
  const { options, personalDetails } = editorResponse;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const [state, update] = React.useReducer(PlayerEditorStateReducer, {
    firstName: personalDetails.firstName,
    lastName: personalDetails.lastName,
    savedName: personalDetails.savedName,
    playerNumber: personalDetails.uniformNumber
  });

  const header = <>
    <Breadcrumbs>
      <Crumb key='Home' onClick={() => {}}>Home</Crumb>
      <Crumb key='RosterEditor' onClick={() => {}}>Roster</Crumb>
    </Breadcrumbs>
    <PlayerHeaderContainer>
      <PlayerNameBubble 
        positionType='Outfielder'
        size='Large'
      >
        {state.savedName}
      </PlayerNameBubble>
      <PositionBubble
        positionType='Outfielder'
        size='Large'
      >
        {personalDetails.position.name}
      </PositionBubble>
      <OutlineHeader fontSize={FONT_SIZES._64} strokeWeight={2} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>
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
    <ContentWithHangingHeader header={header} headerHeight='192px'>
      Personal Details
    </ContentWithHangingHeader>
  </PowerUpLayout>
}

const PlayerHeaderContainer = styled.div`
  display: flex;
  gap: 16px;
  align-items: center;
  padding-bottom: 8px;
`


const HeaderWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 24px;
`

const ContentRow = styled.div<{ gap?: string }>`
  display: flex;
  align-items: baseline;
  gap: ${p => p.gap ?? '8px'}
`

const ContentRowItem = styled.div<{ flex?: string }>`
  flex: ${p => p.flex ?? '1 1 auto'};
`

const FooterButtonsWrapper = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  padding: 8px;
`