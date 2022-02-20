import React from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { ContentBox } from "../../components/contentBox/contentBox";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader"
import { PlayerName, Position, TextBubble } from "../../components/textBubble/textBubble"
import { TextField } from "../../components/textField/textField";
import { COLORS } from "../../style/constants"
import { AppContext } from "../app";
import { PlayerEditorStateReducer } from "./playerEditorState";
import { SavePlayerApiClient } from "./savePlayerApiClient";

export interface PlayerEditorProps {
  appContext: AppContext;
  editorDTO: PlayerEditorDTO;
}

export interface PlayerEditorDTO {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function PlayerEditor(props: PlayerEditorProps) {
  const { appContext, editorDTO } = props;

  const apiClientRef = React.useRef(new SavePlayerApiClient(appContext.commandFetcher));

  const [state, update] = React.useReducer(PlayerEditorStateReducer, {
    firstName: editorDTO.firstName,
    lastName: editorDTO.lastName,
    savedName: editorDTO.savedName,
    playerNumber: editorDTO.playerNumber
  });

  return <>
    <HeaderWrapper>
    <OutlineHeader textColor={COLORS.primaryBlue.lighter_69} strokeColor={COLORS.primaryBlue.regular_45} slanted>Edit Player</OutlineHeader>
    <TextBubble positionType='Infielder' width='250px' style={{ position: 'relative', bottom: '-2px' }}>
      <PlayerName>{state.savedName}</PlayerName>
    </TextBubble>
    <TextBubble positionType='Infielder' style={{ position: 'relative', bottom: '-2px' }}>
      <Position>{editorDTO.position}</Position>
    </TextBubble>
    <OutlineHeader textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>{state.playerNumber}</OutlineHeader>
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
    const saveRequest: PlayerEditorDTO = {
      powerProsId: editorDTO.powerProsId,
      firstName: state.firstName,
      lastName: state.lastName,
      savedName: state.savedName,
      playerNumber: editorDTO.playerNumber,
      position: editorDTO.position
    }
    var response = await apiClientRef.current.execute(saveRequest);
    console.log(response);
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