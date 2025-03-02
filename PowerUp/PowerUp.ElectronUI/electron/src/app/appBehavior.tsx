import { PropsWithChildren, useEffect, useReducer } from "react";
import { AppState, AppStateReducer, ModalDefinition } from "./appState";
import { useDelayedActivation } from "../components/hooks/useDelayedActivation";
import { CommandFetcher } from "../utils/commandFetcher";
import { AppContext, AppContextProvider, AsyncRenderModalCallback, RenderModalCallback } from "./appContext";
import { AppStartupProps } from "./app";
import { FullPageSpinner } from "../components/fullPageSpinner/fullPageSpinner";
import { GenerateId } from "../utils/generateId";
import { ModalPageCover } from "../components/modal/modal";
import { InitializeBaseRosterApiClient } from "./rosterEditor/importBaseRosterApiClient";
import { PageLoadDefinition, pageRegistry } from "./pages";
import { useNavigate } from "react-router-dom/dist";

export function AppBehavior({ appConfig, commandUrl, children }: PropsWithChildren<AppStartupProps>) {
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
  
  // Initializes Base Roster
  useEffect(() => { new InitializeBaseRosterApiClient(appContext.commandFetcher).execute() }, []);
  
  return <AppContextProvider appContext={appContext}>
    {children}
    {state.currentPage.renderPage(appContext)}
    {state.modals.length > 0 && state.modals.map(toRenderedModal)}
    {showSpinner && <FullPageSpinner/>}
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
}