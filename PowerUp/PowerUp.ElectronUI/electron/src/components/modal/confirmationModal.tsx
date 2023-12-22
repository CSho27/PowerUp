import { ReactNode } from "react";
import { Modal } from "./modal";
import { Button } from "../button/button";

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
    <div>
      {message}
    </div>
    <div>
      {secondaryMessage}
    </div>
    <div 
      style={{ 
        display: 'flex', 
        gap: '4px', 
        justifyContent: 'flex-end' 
      }}
    >
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
    </div>
  </Modal>
}
