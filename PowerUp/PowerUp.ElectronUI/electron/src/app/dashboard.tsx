import * as React from 'react';

export interface DashboardProps {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function Dashboard(props: DashboardProps) {
  const { powerProsId, firstName, lastName, savedName, position, playerNumber } = props;
  const [state, setState] = React.useState<string|undefined>(undefined)

  return <div>
    <div style={{ display: 'flex' }}>
      <h1>Edit Player</h1>
      <div>{savedName}</div>
      <div>{playerNumber}</div>
      <div>{position}</div>
    </div>
  </div>

  async function fetchContent() {
    const response = await fetch('./Test');
    const responseJson = await response.json();
    setState(responseJson.result);
  }
};