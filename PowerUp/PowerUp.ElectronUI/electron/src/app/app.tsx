import * as React from 'react';
import styled from 'styled-components';
import { PlayerBubble } from '../components/PlayerBubble';
import { SlantedOutlineHeader } from '../components/SlantedHeader';
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

  return <div>
    <HeaderWrapper>
      <SlantedOutlineHeader textColor={COLORS.PP_Blue70} strokeColor={COLORS.PP_Blue45}>Edit Player</SlantedOutlineHeader>
      <PlayerBubble savedName={savedName} positionType='Infielder' />
      <div>{playerNumber}</div>
      <div>{position}</div>
    </HeaderWrapper>
    <GlobalStyles />
  </div>

  async function fetchContent() {
    const response = await fetch('./Test');
    const responseJson = await response.json();
    setState(responseJson.result);
  }
};

const HeaderWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 16px;
`