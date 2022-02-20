import React, { useReducer } from 'react';
import { CommandFetcher } from '../utils/commandFetcher';
import { IAppContext } from './appContext';
import { AppPage, AppStateReducer, initialAppState } from './appState';
import { GlobalStyles } from './globalStyles';
import { HomePage } from './home/homePage';
import { PlayerEditor, PlayerEditorDTO } from './playerEditor/playerEditor';

export interface ApplicationStartupData {
  commandUrl: string;
}

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const [state, update] = useReducer(AppStateReducer, initialAppState);
  const appContext: IAppContext = {
    commandFetcher: new CommandFetcher(commandUrl)
  }

  const pages: { [page in AppPage] : React.ReactNode } = {
    Home: <HomePage appContext={appContext} />,
    RosterEditor: <></>,
    TeamEditor: <></>,
    PlayerEditor: <PlayerEditor appContext={appContext} editorDTO={{} as PlayerEditorDTO} />
  }


  return <>
    {pages[state.currentPage]}
    <GlobalStyles />
  </>

};