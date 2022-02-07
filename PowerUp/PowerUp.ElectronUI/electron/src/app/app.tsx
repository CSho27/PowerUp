import { MaxWidthWrapper } from '../components/maxWidthWrapper/maxWidthWrapper';
import { CommandFetcher } from '../utils/commandFetcher';
import { IAppContext } from './appContext';
import { GlobalStyles } from './globalStyles';
import { PlayerEditor, PlayerEditorDTO } from './playerEditor/playerEditor';

export interface ApplicationStartupData {
  commandUrl: string;
  indexResponse: PlayerEditorDTO;
}

export function App(props: ApplicationStartupData) {
  const appContext: IAppContext = {
    commandFetcher: new CommandFetcher(props.commandUrl)
  } 

  return <MaxWidthWrapper maxWidth='100%'>
    <PlayerEditor appContext={appContext} editorDTO={props.indexResponse} />    
    <GlobalStyles />
  </MaxWidthWrapper>
};