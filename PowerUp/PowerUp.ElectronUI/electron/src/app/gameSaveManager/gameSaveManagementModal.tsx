import { useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";
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

  return <Modal 
    ariaLabel='Game Save Manager' 
    fullHeight
    width='800px'>
      <Heading>Game Save Manager</Heading>
      <div>{activeGameSaveId}</div>
      {gameSaveOptions.map(o => <div key={o.id}>{o.name}</div>)}
      <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
        <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      </div>
  </Modal>
}

const Heading = styled.h2`
  font-style: italic;
`