
import * as ReactDOM from 'react-dom';
import * as React from 'react'; // DO NOT REMOVE. THIS IMPORT IS REQUIRED FOR REACT TO BE ON PAGE IN TIME!
import { App } from "./app";

ReactDOM.render(<App {...getIndexResponse()}/>, document.getElementById('renderer'));

function getIndexResponse() {
  const dataString = document.getElementById('index-response-json-data')?.getAttribute('data')?.replaceAll("'", '"');
  return !!dataString
    ? JSON.parse(dataString)
    : undefined;
}