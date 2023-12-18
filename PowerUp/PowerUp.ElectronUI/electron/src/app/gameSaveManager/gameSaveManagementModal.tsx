import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { IconButton } from "../../components/icon/iconButton";
import { Modal } from "../../components/modal/modal";
import { fileSystemCharacters, TextField } from "../../components/textField/textField";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { replace } from "../../utils/arrayUtils";
import { AppContext } from "../app";
import { ActivateGameSaveApiClient } from "./activateGameSaveApiClient";
import { openGameSaveManagerInitializationModal } from "./gameSaveManagerInitializationModal";
import { GameSaveDto, OpenGameSaveManagerApiClient } from "./openGameSaveManagerApiClient";
import { RenameGameSaveApiClient } from "./renameGameSaveApiClient";
import { NoticeSection } from "../../components/noticeSection/noticeSection";

export async function openGameSaveManagerModal(appContext: AppContext) {
  const managerStateResponse = await new OpenGameSaveManagerApiClient(appContext.commandFetcher).execute();
  appContext.openModal(closeDialog => <GameSaveManagerModal 
    appContext={appContext}
    initialActiveGameSaveId={managerStateResponse.activeGameSaveId}
    initialGameSaveOptions={managerStateResponse.gameSaveOptions}
    closeDialog={closeDialog}
  />)
}

interface GameSaveManagerModalProps {
  appContext: AppContext;
  initialActiveGameSaveId: number | null;
  initialGameSaveOptions: GameSaveDto[];
  closeDialog: () => void;
}

interface State {
  activeGameSaveId: number | null;
  gameSaveOptions: GameSaveDto[];
  currentlyRenamingGameSaveId: number | undefined;
  currentlyRenamingGameSaveName: string | undefined;
}

function GameSaveManagerModal(props: GameSaveManagerModalProps) {
  const { appContext, initialActiveGameSaveId, initialGameSaveOptions, closeDialog } = props;
  const [state, setState] = useState<State>({
    activeGameSaveId: initialActiveGameSaveId,
    gameSaveOptions: initialGameSaveOptions,
    currentlyRenamingGameSaveId: undefined,
    currentlyRenamingGameSaveName: undefined
  });
  const otherMatchingGameSaves = state.gameSaveOptions.filter(o => o.id === state.activeGameSaveId).slice(1);
  const showNotice = otherMatchingGameSaves.length > 0;
  const activeGameSaveApiClientRef = useRef(new ActivateGameSaveApiClient(appContext.commandFetcher));
  const renameGameSaveApiClientRef = useRef(new RenameGameSaveApiClient(appContext.commandFetcher));

  return <Modal 
    ariaLabel='Game Save Manager' 
    fullHeight
    width='800px'>
      <Wrapper withNoticeSection={showNotice}>
      <Heading>Game Save Manager</Heading>
      {showNotice && <>
        <NoticeSection>
          More than one game save exists with this id. Consider deleting and regenerating the following Game Saves:
          <ul>
            {otherMatchingGameSaves.map(o => <li key={o.name}>{o.name}</li>)}
          </ul> 
        </NoticeSection>
      </>}
      <GridWrapper>
        {state.gameSaveOptions.map(toGridRow)}
      </GridWrapper>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <Button variant='Ghost' size='Small' onClick={selectNewDirectory}>Select Different Directory</Button>
        <Button variant='Fill' size='Small' onClick={closeDialog}>Close</Button>
      </div>
      </Wrapper>
  </Modal>

  function toGridRow(gameSave: GameSaveDto, index: number) {
    // Only the first match should show as being selected
    const isSelected = gameSave.id === state.activeGameSaveId 
      && state.gameSaveOptions.findIndex(o => o.id === state.activeGameSaveId) === index;
    const isRenaming = gameSave.id == state.currentlyRenamingGameSaveId;

    return <GameSaveRow 
      key={gameSave.id} 
      selected={isSelected}>
        <CenteredCell>
          {isSelected 
            ? <ActiveText>Active</ActiveText> 
            : <Button
                variant='Outline' 
                size='Small'
                icon='file-arrow-down'
                onClick={() => activateGameSave(gameSave.id)} 
              >Activate</Button>}
        </CenteredCell>
        <CenteredCell>
          {isRenaming
            ? <Button
                variant='Outline' 
                size='Small'
                icon='lock'
                onClick={() => saveRenameFor(gameSave.id)}>
                  Save
              </Button>
            : <Button
                variant='Outline' 
                size='Small'
                icon='pen-to-square'
                onClick={() => beginRenaming(gameSave.id)}>
                  Rename
              </Button>}
        </CenteredCell>
        <div>
          {isRenaming
            ? <TextField 
                value={state.currentlyRenamingGameSaveName} 
                onChange={name => setState(p => ({...p, currentlyRenamingGameSaveName: name}))} 
                autoFocus
                allowedCharacters={fileSystemCharacters}
              />
            : gameSave.name}</div>
      </GameSaveRow>
  }

  async function activateGameSave(gameSaveId: number) {
    const response = await activeGameSaveApiClientRef.current.execute({
      gameSaveId: gameSaveId
    });

    if(response.success)
      setState(p => ({...p, activeGameSaveId: gameSaveId}));
  }

  async function beginRenaming(gameSaveId: number) {
    setState(p => ({
      ...p,
      currentlyRenamingGameSaveId: gameSaveId,
      currentlyRenamingGameSaveName: state.gameSaveOptions.find(o => o.id === gameSaveId)!.name
    }))
  }

  async function saveRenameFor(gameSaveId: number) {
    const newName = state.currentlyRenamingGameSaveName!
    const response = await renameGameSaveApiClientRef.current.execute({
      gameSaveId: gameSaveId,
      name: newName
    });

    if(response.success) {
      setState(p => ({
        ...p,
        gameSaveOptions: replace(p.gameSaveOptions, o => o.id == gameSaveId, o => ({...o, name: newName })),
        currentlyRenamingGameSaveId: undefined,
        currentlyRenamingGameSaveName: undefined
      }));
    } else {
      setState(p => ({
        ...p,
        currentlyRenamingGameSaveId: undefined,
        currentlyRenamingGameSaveName: undefined
      }));
    }
  }

  async function selectNewDirectory() {
    const shouldReload = await openGameSaveManagerInitializationModal(appContext)
    if(!shouldReload)
      return;
    closeDialog();
    openGameSaveManagerModal(appContext);
  }
}

const Wrapper = styled.div<{ withNoticeSection: boolean; }>`
  display: grid;
  grid-template-rows: min-content ${p => p.withNoticeSection ? 'auto' : ''} auto min-content;
  gap: 8px;
  height: 100%;
`

const Heading = styled.h2`
  font-style: italic;
`

const GridWrapper = styled.div`
  width: 100%;
  overflow-y: auto;
  background-color: ${COLORS.white.regular_100};
`

const GameSaveRow = styled.div<{ selected: boolean }>`
  padding: 2px 8px;
  font-size: ${FONT_SIZES._18};
  background-color: ${p => p.selected 
    ? COLORS.white.offwhite_85 
    : undefined};
  display: grid;
  grid-template-columns: 128px 128px auto;
  gap: 16px;

  &:nth-child(even) {
    background-color: ${p => p.selected 
      ? COLORS.white.offwhite_85 
      : COLORS.white.offwhite_97};
  }
`

const CenteredCell = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`

const ActiveText = styled.div`
  color: ${COLORS.affirmativeGreen.regular_25};
`