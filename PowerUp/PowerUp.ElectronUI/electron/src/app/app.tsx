import * as React from 'react';
import styled from 'styled-components';
import { MaxWidthWrapper } from '../components/maxWidthWrapper/maxWidthWrapper';
import { TextBubble, PlayerName, Position } from '../components/textBubble/textBubble';
import { OutlineHeader } from '../components/outlineHeader/outlineHeader';
import { COLORS } from '../style/constants';
import { GlobalStyles } from './globalStyles';

export interface AppIndexResponse {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function App(props: AppIndexResponse) {
  const { powerProsId, firstName, lastName, savedName, position, playerNumber } = props;
  const [state, setState] = React.useState<string|undefined>(undefined)

  return <MaxWidthWrapper maxWidth='100%'>
    <HeaderWrapper>
      <OutlineHeader textColor={COLORS.PP_Blue70} strokeColor={COLORS.PP_Blue45} slanted>Edit Player</OutlineHeader>
      <TextBubble positionType='Infielder' width='250px' style={{ position: 'relative', bottom: '-2px' }}>
        <PlayerName>{savedName}</PlayerName>
      </TextBubble>
      <TextBubble positionType='Infielder' style={{ position: 'relative', bottom: '-2px' }}>
        <Position>{position}</Position>
      </TextBubble>
      <OutlineHeader textColor={COLORS.PP_Blue45} strokeColor={COLORS.white}>{playerNumber}</OutlineHeader>
    </HeaderWrapper>
    <GlobalStyles />
  </MaxWidthWrapper>

  async function fetchContent() {
    const response = await fetch('./Test');
    const responseJson = await response.json();
    setState(responseJson.result);
  }
};

const HeaderWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 24px;
`