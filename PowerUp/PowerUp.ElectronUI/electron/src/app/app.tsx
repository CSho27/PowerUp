import { GlobalStyles } from './globalStyles';
import { AppConfig } from './appConfig';
import { AppBehavior } from './appBehavior';
import { AppRouter } from './appRouter';
import { createContext, PropsWithChildren, useContext } from 'react';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

const AppStartupContext = createContext<AppStartupProps|null>(null);

export function App(props: AppStartupProps) {
  return <AppStartupContext.Provider value={props}>
    <AppRouter /> 
    <GlobalStyles />
  </AppStartupContext.Provider>
};

export function Page({ children }: PropsWithChildren<{}>) {
  const startupContext = useContext(AppStartupContext);
  if(!startupContext) return <>ERRROR</>;

  return <AppBehavior 
    appConfig={startupContext.appConfig} 
    commandUrl={startupContext.commandUrl}
  >
    {children}
  </AppBehavior> 
}