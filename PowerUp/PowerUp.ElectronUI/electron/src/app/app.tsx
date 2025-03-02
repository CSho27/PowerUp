import { useEffect, useReducer } from 'react';
import { ModalPageCover } from '../components/modal/modal';
import { FullPageSpinner } from '../components/fullPageSpinner/fullPageSpinner';
import { useGlobalBindings } from '../nimbleKey/globalBinding';
import { CommandFetcher } from '../utils/commandFetcher';
import { GenerateId } from '../utils/generateId';
import { AppState, AppStateReducer, ModalDefinition } from './appState';
import { GlobalStyles } from './globalStyles';
import { PageLoadDefinition, pageRegistry } from './pages';
import { InitializeBaseRosterApiClient } from './rosterEditor/importBaseRosterApiClient';
import { AppConfig } from './appConfig';
import { AppContext, AppContextProvider, AsyncRenderModalCallback, RenderModalCallback } from './appContext';
import { useDelayedActivation } from '../components/hooks/useDelayedActivation';

export interface AppStartupProps {
  appConfig: AppConfig;
  commandUrl: string;
}

export function App(props: AppStartupProps) {
  const { commandUrl, appConfig } = props;
  
  const initialState: AppState = {
    breadcrumbs: [],
    currentPage: { title: '', renderPage: () => <></> },
    modals: [],
    isLoading: false
  }

  const [state, update] = useReducer(AppStateReducer, initialState);
  const showSpinner = useDelayedActivation(state.isLoading, 500);

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl, performWithSpinner),
    breadcrumbs: state.breadcrumbs,
    setPage: setPage,
    reloadCurrentPage: reloadCurrentPage,
    popBreadcrumb: popBreadcrumb,
    openModal: openModal,
    openModalAsync: openModalAsync,
    openFileSelector: appConfig.openFileSelector,
    openInNewTab: appConfig.openInNewTab,
    performWithSpinner: performWithSpinner
  };

  useEffect(() => {
    setPage({ page: 'HomePage' });
  }, [])

  useGlobalBindings(
    { keys: ['Control', 'Alt', 'Shift', 'R'], callbackFn: () => setPage({ page: 'RosterEditorPage', rosterId: 1 }) },
    { keys: ['Control', 'Alt', 'Shift', 'H'], callbackFn: () => setPage({ page: 'PlayerEditorPage', playerId: 1 }) },
    { keys: ['Control', 'Alt', 'Shift', 'P'], callbackFn: () => setPage({ page: 'PlayerEditorPage', playerId: 16 }) },
    { keys: ['Control', 'Alt', 'Shift', 'T'], callbackFn: () => setPage({ page: 'TeamEditorPage', teamId: 8 }) },
    { keys: ['Control', 'Alt', 'Shift', 'Q'], callbackFn: () => setPage({ page: 'TestPage' }) },
  )

  // Initializes Base Roster
  useEffect(() => { new InitializeBaseRosterApiClient(appContext.commandFetcher).execute() }, []);

  return <AppContextProvider appContext={appContext}>
    {state.currentPage.renderPage(appContext)}
    {state.modals.length > 0 && state.modals.map(toRenderedModal)}
    {showSpinner && <FullPageSpinner/>}
    <GlobalStyles />
  </AppContextProvider>

  async function setPage(pageDef: PageLoadDefinition) {
    const pageEntry = pageRegistry[pageDef.page];
    const loadedPage = await pageEntry.load(appContext, pageDef);
    update({ type: 'updatePage', pageLoadDef: loadedPage.updatedPageLoadDef ?? pageDef, pageDef: loadedPage });
  }

  async function reloadCurrentPage() {
    popBreadcrumb(state.breadcrumbs[state.breadcrumbs.length-1].id);
  }

  async function popBreadcrumb(breadcrumbId: number) {
    const pageIndex = state.breadcrumbs.findIndex(c => c.id === breadcrumbId);
    const pageLoadDef = state.breadcrumbs[pageIndex].pageLoadDef!;
    const pageEntry = pageRegistry[pageLoadDef.page];
    const newPage = await pageEntry.load(appContext, pageLoadDef);

    const pagesToCleanUp = state.breadcrumbs.slice(pageIndex+1);
    pagesToCleanUp.forEach(p => {
      const entry = pageRegistry[p.pageLoadDef.page];
      if(!!entry.cleanup)
        entry.cleanup(appContext, p.pageLoadDef);
    })

    update({ type: 'updatePageFromBreadcrumb', breadcrumbId: breadcrumbId, pageDef: newPage });
  }

  function openModalAsync<T>(renderModal: AsyncRenderModalCallback<T>): Promise<T> {
    return new Promise(resolve => 
        openModal(closeDialog => {
          return renderModal(closeAndResolve)
          
          function closeAndResolve(value: T) {
            closeDialog();
            resolve(value);
          }
        })
    )
  }

  function openModal(renderModal: RenderModalCallback) {
    var key = GenerateId().toString();
    var closeDialog = () => update({ type: 'closeModal', modalKey: key });
    update({ type: 'openModal', modal: { key: key, modal: renderModal(closeDialog) } });
  }

  async function performWithSpinner<T>(action: () => Promise<T>): Promise<T> {
    update({ type: 'updateIsLoading', isLoading: true });
    var returnValue = await action().catch(e => {
      alert('Unexpected Error: If this error persists please create a GitHub issue and contact the creator.')
      update({ type: 'updateIsLoading', isLoading: false });
      throw e;
    });
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