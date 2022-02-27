import React from "react";
import styled from "styled-components"
import { AppContext } from "../app";
import { PlayerEditorResponse } from "./loadPlayerEditorApiClient";
import { PlayerEditorStateReducer } from "./playerEditorState";
import { SavePlayerApiClient } from "./savePlayerApiClient";

export interface PlayerEditorProps {
  appContext: AppContext;
  editorResponse: PlayerEditorResponse 
}

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

  return <div>
    playerEditor
  </div>
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