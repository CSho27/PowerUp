import { ReactElement, useEffect, useReducer } from 'react';
import { ModalPageCover, ModalProps } from '../components/modal/modal';
import { FullPageSpinner } from '../components/fullPageSpinner/fullPageSpinner';
import { useGlobalBindings } from '../nimbleKey/globalBinding';
import { CommandFetcher } from '../utils/commandFetcher';
import { GenerateId } from '../utils/generateId';
import { AppStateReducer, ModalDefinition } from './appState';
import { GlobalStyles } from './globalStyles';
import { PageLoadDefinition, pageRegistry } from './pages';

export interface ApplicationStartupData {
  commandUrl: string;
}

export interface AppContext {
  commandFetcher: CommandFetcher;
  setPage: (pageDef: PageLoadDefinition) => void;
  openModal: (dialog: ReactElement<ModalProps>) => () => void;
}

export function App(props: ApplicationStartupData) {
  const { commandUrl } = props;
  
  const [state, update] = useReducer(AppStateReducer, {
    currentPage: <></>,
    modals: [],
    isLoading: false
  });

  const appContext: AppContext = {
    commandFetcher: new CommandFetcher(commandUrl, isLoading => update({ type: 'updateIsLoading', isLoading: isLoading })),
    setPage: setPage,
    openModal: openModal
  };

  useEffect(() => {
    setPage({ page: 'HomePage' });
  }, [])

  useGlobalBindings(
    { keys: ['Control', 'Alt', 'Shift', 'P'], callbackFn: () => setPage({ page: 'PlayerEditorPage', playerId: 1 }) }
  )

  return <>
    {state.currentPage}
    {state.modals.length > 0 && 
    state.modals.map(toRenderedModal)}
    {state.isLoading && <FullPageSpinner/>}
    <GlobalStyles />
  </>

  async function setPage(pageDef: PageLoadDefinition) {
    const pageLoader = pageRegistry[pageDef.page];
    const newPage = await pageLoader(appContext, pageDef);
    update({ type: 'updatePage', newPage: newPage });
  }

  function openModal(modal: ReactElement<ModalProps>) {
    var key = GenerateId().toString();
    update({ type: 'openModal', modal: { key: key, modal: modal } });
    return () => update({ type: 'closeModal', modalKey: key });
  }

  function toRenderedModal(modalDef: ModalDefinition, index: number) {
    return index === state.modals.length - 1
      ? <ModalPageCover key={modalDef.key}>{modalDef.modal}</ModalPageCover>
      : <ModalPageCover transparent key={modalDef.key}>{modalDef.modal}</ModalPageCover>
  }
};