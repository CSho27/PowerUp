import { GlobalStyles } from './globalStyles';
import { AppConfig } from './appConfig';
import { AppBehavior } from './appBehavior';
import { AppRouter } from './appRouter';
import { PropsWithChildren } from 'react';
import { useNavigate } from 'react-router-dom/dist';
import { useConfigureAppContext } from './appContext';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

export function App(props: AppStartupProps) {
  return (
    <AppBehavior appConfig={props.appConfig} commandUrl={props.commandUrl}>
      <AppRouter />
      <GlobalStyles />
    </AppBehavior>
  );
}

export function Page({ children }: PropsWithChildren<{}>) {
  const navigate = useNavigate();
  useConfigureAppContext({ navigate: navigate });
  return <>{children}</>;
}
