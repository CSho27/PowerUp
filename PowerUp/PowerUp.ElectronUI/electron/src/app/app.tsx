import { useEffect, useReducer } from 'react';
import { FullPageSpinner } from '../components/fullPageSpinner/fullPageSpinner';
import { CommandFetcher } from '../utils/commandFetcher';
import { AppStateReducer } from './appState';
import { GlobalStyles } from './globalStyles';
import { HomePage } from './home/homePage';
import { PageLoadDefinition, pageRegistry } from './pages';

export interface ApplicationStartupData {
  commandUrl: string;
}

export interface AppContext {
  commandFetcher: CommandFetcher;
  setPage: (pageDef: PageLoadDefinition) => void;
}

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const [state, update] = useReducer(AppStateReducer, {
    currentPage: <></>,
    isLoading: false
  });

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl, isLoading => update({ type: 'updateIsLoading', isLoading: isLoading })),
    setPage: setPage 
  };

  useEffect(() => {
    setPage({ page: 'HomePage' });
  }, [])

  return <>
    {state.currentPage}
    {state.isLoading && <FullPageSpinner/>}
    <GlobalStyles />
  </>

  async function setPage(pageDef: PageLoadDefinition) {
    const pageLoader = pageRegistry[pageDef.page];
    const newPage = await pageLoader(appContext, pageDef);
    update({type: 'updatePage', newPage: newPage });
  }
};