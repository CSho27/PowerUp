import React from 'react';
import { Button } from '../components/button/button';
import { MaxWidthWrapper } from '../components/maxWidthWrapper/maxWidthWrapper';
import { CommandFetcher } from '../utils/commandFetcher';
import { IAppContext } from './appContext';
import { GlobalStyles } from './globalStyles';
import { PlayerEditor } from './playerEditor/playerEditor';

export interface ApplicationStartupData {
  commandUrl: string;
}

export function App(props: ApplicationStartupData) {
  const appContext: IAppContext = {
    commandFetcher: new CommandFetcher(props.commandUrl)
  } 

  const [isLoading, setIsLoading] = React.useState(false);

  return <MaxWidthWrapper maxWidth='100%'>
    {!isLoading && 
    <Button 
      variant='Fill' 
      size={'Large'} 
      onClick={loadBaseGameSave}
    >Load Base Game Save</Button>}
    {isLoading &&
    <div>Base Save is Loading!</div>}
    <GlobalStyles />
  </MaxWidthWrapper>

  async function loadBaseGameSave() {
    setIsLoading(true);
    const response = await appContext.commandFetcher.execute('LoadBaseGameSave', { throwaway: 1 })
    setIsLoading(false);
    if(!response.success)
      throw response;
  }

  const pe = PlayerEditor;
};