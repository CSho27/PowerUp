import { GlobalStyles } from './globalStyles';
import { AppConfig } from './appConfig';
import { AppBehavior } from './appBehavior';
import { AppRouter } from './appRouter';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

export function App(props: AppStartupProps) {
  const { commandUrl, appConfig } = props;

  return <AppRouter renderPage={page => 
      <AppBehavior appConfig={appConfig} commandUrl={commandUrl}>
        {page}
        <GlobalStyles />
      </AppBehavior>
    }>
  </AppRouter> 
};