import { useEffect, useReducer } from 'react';
import { CommandFetcher } from '../utils/commandFetcher';
import { AppStateReducer } from './appState';
import { GlobalStyles } from './globalStyles';
import { HomePage } from './home/homePage';

export interface ApplicationStartupData {
  commandUrl: string;
}

export interface AppContext {
  commandFetcher: CommandFetcher;
  setPage: (newPage: React.ReactNode) => void;
}

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const [state, update] = useReducer(AppStateReducer, {
    currentPage: <></>
  });

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl),
    setPage: newPage => update({type: 'updatePage', newPage: newPage })
  };

  useEffect(() => {
    update({ type: 'updatePage', newPage: <HomePage appContext={appContext} /> });
  }, [])

  return <>
    {state.currentPage}
    <GlobalStyles />
  </>
};