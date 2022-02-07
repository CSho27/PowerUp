import React from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { ContentBox } from "../../components/contentBox.tsx/contentBox";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader"
import { PlayerName, Position, TextBubble } from "../../components/textBubble/textBubble"
import { TextField } from "../../components/textField/textField";
import { COLORS } from "../../style/constants"
import { AppIndexResponse } from "../app";
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
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })}
          autoFocus
        />
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
  <FooterButtonsWrapper>
    <Button onClick={() => {}} variant='Outline' size='Medium'>Cancel</Button>
    <Button onClick={save} variant='Fill' size='Medium'>Save</Button>
  </FooterButtonsWrapper>
  </>

  async function save() {
    const saveRequest: AppIndexResponse = {
      powerProsId: props.powerProsId,
      firstName: state.firstName,
      lastName: state.lastName,
      savedName: state.savedName,
      playerNumber: props.playerNumber,
      position: props.position
    }
    try {
      const response = await fetch('/command', {
        method: 'POST',
        mode: 'same-origin',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ commandName: 'SavePlayer', request: saveRequest })
      });
    } catch (error) {
      console.error(error);
    }
  }
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

const FooterButtonsWrapper = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  padding: 8px;
`