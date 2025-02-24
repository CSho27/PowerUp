import * as React from 'react'; // DO NOT REMOVE. THIS IMPORT IS REQUIRED FOR REACT TO BE ON PAGE IN TIME!
import ReactDOM from "react-dom";
import { App } from "../app/app";
import { FileSelectionFn } from '../components/fileSelector/fileSelector';
import { OpenInNewTabFn } from '../app/appConfig';

export interface ElectronApi {
  openFileSelector: FileSelectionFn;
  openInNewTab: OpenInNewTabFn;
  env: Env;
}

export interface Env {
  BASE_URL: string;
}

const electronApi: ElectronApi = (window as any).electronApi;
const base = document.createElement('base');
base.href = electronApi.env.BASE_URL;
document.head.append(base);

ReactDOM.render(
  <App
    commandUrl={'/command'}
    appConfig={{
      openFileSelector: electronApi.openFileSelector,
      openInNewTab: electronApi.openInNewTab,
    }} 
  />, 
  document.getElementById('renderer')
);
