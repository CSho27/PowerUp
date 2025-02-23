import * as React from 'react'; // DO NOT REMOVE. THIS IMPORT IS REQUIRED FOR REACT TO BE ON PAGE IN TIME!
import ReactDOM from "react-dom";
import { App } from "../app/app";
import { FileSelectionFn } from '../components/fileSelector/fileSelector';
import { OpenInNewTabFn } from '../app/appConfig';

export interface ElectronApi {
  openFileSelector: FileSelectionFn;
  openInNewTab: OpenInNewTabFn;
}

const electronApi: ElectronApi = (window as any).electronApi;
ReactDOM.render(
  <App
    commandUrl={'https://localhost:44388/command'}
    appConfig={{
      openFileSelector: electronApi.openFileSelector,
      openInNewTab: electronApi.openInNewTab,
    }} 
  />, 
  document.getElementById('renderer')
);
