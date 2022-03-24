import { ReactElement, useEffect, useReducer } from 'react';
import { ModalPageCover, ModalProps } from '../components/modal/modal';
import { FullPageSpinner } from '../components/fullPageSpinner/fullPageSpinner';
import { useGlobalBindings } from '../nimbleKey/globalBinding';
import { CommandFetcher } from '../utils/commandFetcher';
import { GenerateId } from '../utils/generateId';
import { AppState, AppStateReducer, BreadcrumbDefinition, ModalDefinition } from './appState';
import { GlobalStyles } from './globalStyles';
import { PageLoadDefinition, pageRegistry } from './pages';

export interface ApplicationStartupData {
  commandUrl: string;
}

export interface AppContext {
  commandFetcher: CommandFetcher;
  breadcrumbs: BreadcrumbDefinition[];
  setPage: (pageDef: PageLoadDefinition) => void;
  popBreadcrumb: (breadcrumbId: number) => void;
  openModal: (renderModal: RenderModalCallback) => void;
  performWithSpinner: PerformWithSpinnerCallback;
}

export type RenderModalCallback = (closeDialog: () => void) => ReactElement<ModalProps>;
export type PerformWithSpinnerCallback = <T>(action: () => Promise<T>) => Promise<T>;

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const initialState: AppState = {
    breadcrumbs: [],
    currentPage: { title: '', renderPage: () => <></> },
    modals: [],
    isLoading: false
  }

  const [state, update] = useReducer(AppStateReducer, initialState);

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl, performWithSpinner),
    breadcrumbs: state.breadcrumbs,
    setPage: setPage,
    popBreadcrumb: popBreadcrumb,
    openModal: openModal,
    performWithSpinner: performWithSpinner
  };

  useEffect(() => {
    setPage({ page: 'HomePage' });
  }, [])

  useGlobalBindings(
    { keys: ['Control', 'Alt', 'Shift', 'P'], callbackFn: () => setPage({ page: 'PlayerEditorPage', playerId: 1 }) }
  )

  return <>
    {state.currentPage.renderPage(appContext)}
    {state.modals.length > 0 && 
    state.modals.map(toRenderedModal)}
    {state.isLoading && <FullPageSpinner/>}
    <GlobalStyles />
  </>

  async function setPage(pageDef: PageLoadDefinition) {
    const pageLoader = pageRegistry[pageDef.page];
    const loadedPage = await pageLoader(appContext, pageDef);
    update({ type: 'updatePage', pageLoadDef: pageDef, pageDef: loadedPage });
  }

  async function popBreadcrumb(breadcrumbId: number) {
    const pageLoadDef = state.breadcrumbs.find(c => c.id === breadcrumbId)!.pageLoadDef;
    const pageLoader = pageRegistry[pageLoadDef.page];
    const newPage = await pageLoader(appContext, pageLoadDef);
    update({ type: 'updatePageFromBreadcrumb', breadcrumbId: breadcrumbId, pageDef: newPage });
  }

  function openModal(renderModal: RenderModalCallback) {
    var key = GenerateId().toString();
    var closeDialog = () => update({ type: 'closeModal', modalKey: key });
    update({ type: 'openModal', modal: { key: key, modal: renderModal(closeDialog) } });
  }

  async function performWithSpinner<T>(action: () => Promise<T>): Promise<T> {
    update({ type: 'updateIsLoading', isLoading: true });
    var returnValue = await action();
    update({ type: 'updateIsLoading', isLoading: false });
    return returnValue;
  }
  
  function toRenderedModal(modalDef: ModalDefinition, index: number) {
    return <ModalPageCover 
      key={modalDef.key}
      transparent={index !== state.modals.length - 1}
    >
      {modalDef.modal}
    </ModalPageCover>
  }
};