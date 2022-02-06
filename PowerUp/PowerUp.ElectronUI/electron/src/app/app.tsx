import { MaxWidthWrapper } from '../components/maxWidthWrapper/maxWidthWrapper';
import { GlobalStyles } from './globalStyles';
import { PlayerEditor } from './playerEditor/playerEditor';

export interface AppIndexResponse {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function App(props: AppIndexResponse) {
  return <MaxWidthWrapper maxWidth='100%'>
    <PlayerEditor {...props} />    
    <GlobalStyles />
  </MaxWidthWrapper>
};