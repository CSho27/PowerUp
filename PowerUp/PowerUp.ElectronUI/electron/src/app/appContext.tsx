import {
  createContext,
  PropsWithChildren,
  ReactElement,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from 'react';
import { CommandFetcher } from '../utils/commandFetcher';
import { OpenInNewTabFn } from './appConfig';
import { BreadcrumbDefinition } from './appState';
import { PageLoadDefinition } from './pages';
import { ModalProps } from '../components/modal/modal';
import { FileSelectionFn } from '../components/fileSelector/fileSelector';
import { NavigateFunction } from 'react-router-dom';

export interface AppContext {
  commandFetcher: CommandFetcher;
  breadcrumbs: BreadcrumbDefinition[];
  toolbarActions?: ReactNode;
  setPage: (pageDef: PageLoadDefinition) => void;
  reloadCurrentPage: () => void;
  popBreadcrumb: (breadcrumbId: number) => void;
  openModal: (renderModal: RenderModalCallback) => void;
  openModalAsync: <T>(renderModal: AsyncRenderModalCallback<T>) => Promise<T>;
  openFileSelector: FileSelectionFn;
  openInNewTab: OpenInNewTabFn;
  performWithSpinner: PerformWithSpinnerCallback;
}

export type RenderModalCallback = (
  closeDialog: () => void
) => ReactElement<ModalProps>;
export type AsyncRenderModalCallback<T> = (
  closeDialog: (value: T) => void
) => ReactElement<ModalProps>;
export type PerformWithSpinnerCallback = <T>(
  action: () => Promise<T>
) => Promise<T>;

export type ConfigSetter = (config: AppContextConfig) => void;
export interface AppContextConfig {
  navigate: NavigateFunction;
}

const AppContext = createContext<AppContext | null>(null);
const AppContextConfigContext = createContext<ConfigSetter | null>(null);

export interface AppContextProviderProps {
  appContext: AppContext;
  setConfig: ConfigSetter;
}

export function AppContextProvider(
  props: PropsWithChildren<AppContextProviderProps>
) {
  return (
    <AppContextConfigContext.Provider value={props.setConfig}>
      <AppContext.Provider value={props.appContext}>
        {props.children}
      </AppContext.Provider>
    </AppContextConfigContext.Provider>
  );
}

export function useAppContext(): AppContext {
  const appContext = useContext(AppContext);
  if (!appContext)
    throw new Error(
      'AppContext can only be accessed from children of an AppContextProvider'
    );
  return appContext;
}

export function useConfigureAppContext(config: AppContextConfig) {
  const configContext = useContext(AppContextConfigContext);
  if (!configContext)
    throw new Error(
      'AppContextConfigContext can only be accessed from children of an AppContextProvider'
    );
  useEffect(() => {
    configContext(config);
  }, [config]);
}
