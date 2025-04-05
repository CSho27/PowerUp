import { GlobalStyles } from './globalStyles';
import { AppConfig } from './appConfig';
import { AppBehavior } from './appBehavior';
import { AppRouter } from './appRouter';
import { PropsWithChildren, useContext } from 'react';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

export function App(props: AppStartupProps) {
  return <AppBehavior 
    appConfig={props.appConfig} 
    commandUrl={props.commandUrl}
  >
    <AppRouter /> 
    <GlobalStyles />
  </AppBehavior> 
};

export function Page({ children }: PropsWithChildren<{}>) {
  return <>{children}</>; 
}