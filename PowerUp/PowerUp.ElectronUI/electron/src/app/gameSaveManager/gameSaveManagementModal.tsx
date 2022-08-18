import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { IconButton } from "../../components/icon/iconButton";
import { Modal } from "../../components/modal/modal";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { ActivateGameSaveApiClient } from "./activateGameSaveApiClient";
import { GameSaveDto } from "./openGameSaveManagerApiClient";

export interface GameSaveManagerModalProps {
  appContext: AppContext;
  initialActiveGameSaveId: number | null;
  gameSaveOptions: GameSaveDto[];
  closeDialog: () => void;
}

export function GameSaveManagerModal(props: GameSaveManagerModalProps) {
  const { appContext, initialActiveGameSaveId, gameSaveOptions, closeDialog } = props;
  const [activeGameSaveId, setActiveGameSaveId] = useState<number|null>(initialActiveGameSaveId);
  const activeGameSaveApiClientRef = useRef(new ActivateGameSaveApiClient(appContext.commandFetcher));

  return <Modal 
    ariaLabel='Game Save Manager' 
    fullHeight
    width='800px'>
      <Wrapper>
      <Heading>Game Save Manager</Heading>
      <GridWrapper>
        {gameSaveOptions.map(toGridRow)}
      </GridWrapper>
      <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
        <Button variant='Fill' size='Small' onClick={closeDialog}>Close</Button>
      </div>
      </Wrapper>
  </Modal>

  function toGridRow(gameSave: GameSaveDto) {
    const isSelected = gameSave.id === activeGameSaveId
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
                onClick={() => activeGameSave(gameSave.id)} 
              >Activate</Button>}
        </CenteredCell>
        <div>{gameSave.name}</div>
      </GameSaveRow>
  }

  async function activeGameSave(gameSaveId: number) {
    const response = await activeGameSaveApiClientRef.current.execute({
      gameSaveId: gameSaveId
    });

    if(response.success)
      setActiveGameSaveId(gameSaveId);
  }
}

const Wrapper = styled.div`
  display: grid;
  grid-template-rows: min-content auto min-content;
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
  grid-template-columns: 128px auto;
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