import React, { useEffect, useReducer } from 'react';
import { CommandFetcher } from '../utils/commandFetcher';
import { IAppContext } from './appContext';
import { AppPage, AppStateReducer } from './appState';
import { GlobalStyles } from './globalStyles';
import { HomePage } from './home/homePage';
import { PlayerEditor, PlayerEditorDTO } from './playerEditor/playerEditor';
import { RosterEditorPage } from './rosterEditor/rosterEditorPage';

export interface ApplicationStartupData {
  commandUrl: string;
}

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const [state, update] = useReducer(AppStateReducer, {
    currentPage: <></>
  });

  const appContext: IAppContext = {
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