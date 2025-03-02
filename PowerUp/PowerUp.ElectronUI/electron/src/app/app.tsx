import { GlobalStyles } from './globalStyles';
import { AppConfig } from './appConfig';
import { BrowserRouter } from 'react-router-dom';
import { AppBehavior } from './appBehavior';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

export function App(props: AppStartupProps) {
  const { commandUrl, appConfig } = props;

  return <BrowserRouter>
    <AppBehavior appConfig={appConfig} commandUrl={commandUrl}>
    </AppBehavior>
    <GlobalStyles />
  </BrowserRouter>

  
};