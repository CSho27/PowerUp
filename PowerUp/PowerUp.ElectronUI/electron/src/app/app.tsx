import * as React from 'react';
import { SlantedOutlineHeader } from '../components/SlantedHeader';
import { COLORS } from '../style/colors';
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
    <div style={{ display: 'flex' }}>
      <SlantedOutlineHeader textColor={COLORS.BB_Blue70} strokeColor={COLORS.PP_Blue45}>Edit Player</SlantedOutlineHeader>
      <div>{savedName}</div>
      <div>{playerNumber}</div>
      <div>{position}</div>
    </div>
    <GlobalStyles />
  </div>

  async function fetchContent() {
    const response = await fetch('./Test');
    const responseJson = await response.json();
    setState(responseJson.result);
  }
};