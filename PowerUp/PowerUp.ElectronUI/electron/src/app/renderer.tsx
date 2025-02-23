
import * as ReactDOM from 'react-dom';
import * as React from 'react'; // DO NOT REMOVE. THIS IMPORT IS REQUIRED FOR REACT TO BE ON PAGE IN TIME!
import { App } from "./app";

interface ApplicationStartupData {
  commandUrl: string;
  indexResponse: object | null;
}

const pageJsonData = getPageJsonData();
ReactDOM.render(
  <App
    commandUrl={pageJsonData.commandUrl}
    appConfig={{
      openInNewTab: openInNewTab,
      openFileSelector: openFileSelector
    }} 
  />, 
  document.getElementById('renderer')
);

function getPageJsonData(): ApplicationStartupData {
  const dataString = document.getElementById('index-response-json-data')?.getAttribute('data')?.replaceAll("'", '"');
  return !!dataString
    ? JSON.parse(dataString)
    : undefined;
}

function openInNewTab(url: string) {
  window.open(url, '_blank');
}

function openFileSelector(): Promise<File|null> {
  return new Promise(resolve => {
    const fileInput = document.createElement('input');
    fileInput.type = 'file';
    fileInput.style.display = 'none';
  
    fileInput.addEventListener('change', (event) => {
      const typedTarget = event.target as HTMLInputElement | undefined;
      const files = typedTarget?.files ?? [];
      if (files.length > 0) resolve(files[0])
      else resolve(null)
      document.body.removeChild(fileInput);
    });
  
    document.body.appendChild(fileInput);
    fileInput.click();
  })
  
}