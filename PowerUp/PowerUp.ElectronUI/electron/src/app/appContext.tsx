import { createContext, PropsWithChildren, ReactElement, useContext } from "react";
import { CommandFetcher } from "../utils/commandFetcher";
import { FileSelectionFn, OpenInNewTabFn } from "./appConfig";
import { BreadcrumbDefinition } from "./appState";
import { PageLoadDefinition } from "./pages";
import { ModalProps } from "../components/modal/modal";

export interface AppContext {
  commandFetcher: CommandFetcher;
  breadcrumbs: BreadcrumbDefinition[];
  setPage: (pageDef: PageLoadDefinition) => void;
  reloadCurrentPage: () => void;
  popBreadcrumb: (breadcrumbId: number) => void;
  openModal: (renderModal: RenderModalCallback) => void;
  openModalAsync: <T>(renderModal: AsyncRenderModalCallback<T>) => Promise<T>;
  openFileSelector: FileSelectionFn;
  openInNewTab: OpenInNewTabFn;
  performWithSpinner: PerformWithSpinnerCallback;
}

export type RenderModalCallback = (closeDialog: () => void) => ReactElement<ModalProps>;
export type AsyncRenderModalCallback<T> = (closeDialog: (value: T) => void) => ReactElement<ModalProps>;
export type PerformWithSpinnerCallback = <T>(action: () => Promise<T>) => Promise<T>;

const AppContext = createContext<AppContext|null>(null);

export interface AppContextProviderProps {
  appContext: AppContext;
}

export function AppContextProvider(props: PropsWithChildren<AppContextProviderProps>) {
  return <AppContext.Provider value={props.appContext}>
    {props.children}
  </AppContext.Provider>
}

export function useAppContext(): AppContext {
  const appContext = useContext(AppContext);
  if(!appContext) throw new Error('AppContext can only be accessed from children of an AppContextProvider');
  return appContext;
}