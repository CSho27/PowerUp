import styled from "styled-components";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";

export interface GameSaveManagerModalProps {
  appContext: AppContext;
}

export function GameSaveManagerModal(props: GameSaveManagerModalProps) {
  return <Modal 
    ariaLabel='Game Save Manager' 
    fullHeight
    width='800px'>
      <Heading>Game Save Manager</Heading>
  </Modal>
}

const Heading = styled.h2`
  font-style: italic;
`