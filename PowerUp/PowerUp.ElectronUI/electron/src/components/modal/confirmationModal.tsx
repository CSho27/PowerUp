import { ReactNode } from "react";
import { Modal } from "./modal";
import { Button } from "../button/button";
import styled from "styled-components";
import { FONT_SIZES } from "../../style/constants";

export interface ConfirmationModalProps {
  message: ReactNode;
  secondaryMessage: ReactNode;
  withDeny?: boolean;
  confirmVerb?: string;
  denyVerb?: string;
  closeDialog: (didConfirm: boolean) => void;
}

export function ConfirmationModal({ 
  message,
  secondaryMessage,
  withDeny,
  confirmVerb = 'Continue',
  denyVerb = 'Cancel',
  closeDialog
}: ConfirmationModalProps) {
  return <Modal ariaLabel='confirm'>
    <MainMessage>
      {message}
    </MainMessage>
    <SecondaryMessage>
      {secondaryMessage}
    </SecondaryMessage>
    <Actions>
      {withDeny && <Button
        size='Small'
        variant='Outline'
        onClick={() => closeDialog(false)}
      >
        {denyVerb}
      </Button>}
      <Button
        size='Small'
        variant='Fill'
        onClick={() => closeDialog(true)}
      >
        {confirmVerb}
      </Button>
    </Actions>
  </Modal>
}

const MainMessage = styled.div`
  font-weight: 500;
  font-size: ${FONT_SIZES._24};
`

const SecondaryMessage = styled.div`
  font-weight: 300;
  font-size: ${FONT_SIZES._18};
`

const Actions = styled.div`
  display: flex; 
  gap: 4px; 
  justify-content: flex-end; 
`
