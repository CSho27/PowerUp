import * as React from 'react';

export function Dashboard() {
  const [state, setState] = React.useState<string|undefined>(undefined)

  const indexResponseRef = React.useRef(getIndexResponse());
  console.log(indexResponseRef);

  return <div>
    <h1>Content</h1>
    <div>{state}</div>
    <button onClick={fetchContent}>Test Fetch Content</button>
  </div>;

  async function fetchContent() {
    const response = await fetch('./Test');
    const responseJson = await response.json();
    setState(responseJson.result);
  }

  function getIndexResponse() {
    const dataString = document.getElementById('index-response-json-data')?.getAttribute('data')?.replaceAll("'", '"');
    return !!dataString
      ? JSON.parse(dataString)
      : undefined;
  }
};