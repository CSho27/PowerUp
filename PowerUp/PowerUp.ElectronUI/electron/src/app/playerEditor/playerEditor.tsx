import React from "react";
import styled from "styled-components"
import { ContentBox } from "../../components/contentBox.tsx/contentBox";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader"
import { PlayerName, Position, TextBubble } from "../../components/textBubble/textBubble"
import { TextField } from "../../components/textField/textField";
import { COLORS } from "../../style/constants"
import { PlayerEditorStateReducer } from "./playerEditorState";

export interface PlayerEditorProps {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function PlayerEditor(props: PlayerEditorProps) {
  const [state, update] = React.useReducer(PlayerEditorStateReducer, {
    firstName: props.firstName,
    lastName: props.lastName,
    savedName: props.savedName,
    playerNumber: props.playerNumber
  });

  return <>
    <HeaderWrapper>
    <OutlineHeader textColor={COLORS.PP_Blue70} strokeColor={COLORS.PP_Blue45} slanted>Edit Player</OutlineHeader>
    <TextBubble positionType='Infielder' width='250px' style={{ position: 'relative', bottom: '-2px' }}>
      <PlayerName>{state.savedName}</PlayerName>
    </TextBubble>
    <TextBubble positionType='Infielder' style={{ position: 'relative', bottom: '-2px' }}>
      <Position>{props.position}</Position>
    </TextBubble>
    <OutlineHeader textColor={COLORS.PP_Blue45} strokeColor={COLORS.white}>{state.playerNumber}</OutlineHeader>
  </HeaderWrapper>
  <ContentBox>
    <ContentRow>
      <ContentRowItem>
        <FieldLabel>First Name</FieldLabel>
        <TextField 
          value={state.firstName} 
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })} />
      </ContentRowItem> 
      <ContentRowItem>
        <FieldLabel>Last Name</FieldLabel>
        <TextField 
          value={state.lastName} 
          onChange={lastName => update({ type: 'updateLastName', lastName: lastName })} />
      </ContentRowItem> 
      <ContentRowItem>
        <FieldLabel>Saved Name</FieldLabel>
        <TextField 
          value={state.savedName} 
          onChange={savedName => update({ type: 'updateSavedName', savedName: savedName })} />
      </ContentRowItem> 
    </ContentRow>
    
  </ContentBox>
  </>
}

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