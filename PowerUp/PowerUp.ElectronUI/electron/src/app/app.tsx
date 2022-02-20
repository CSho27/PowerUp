import { useEffect, useReducer } from 'react';
import { FullPageSpinner } from '../components/fullPageSpinner/fullPageSpinner';
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
    currentPage: <></>,
    isLoading: false
  });

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl, isLoading => update({ type: 'updateIsLoading', isLoading: isLoading })),
    setPage: newPage => update({type: 'updatePage', newPage: newPage })
  };

  useEffect(() => {
    update({ type: 'updatePage', newPage: <HomePage appContext={appContext} /> });
  }, [])

  return <>
    {state.currentPage}
    {state.isLoading && <FullPageSpinner/>}
    <GlobalStyles />
  </>
};