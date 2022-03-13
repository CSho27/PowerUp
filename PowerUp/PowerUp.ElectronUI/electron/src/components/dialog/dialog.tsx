import { Dialog as ReachDialog, DialogOverlay as ReachDialogOverlay } from '@reach/dialog';
import "@reach/dialog/styles.css";

export interface DialogProps {
  children: React.ReactNode;
}

export function Dialog(props: DialogProps) {
  return <ReachDialog aria-label='Existing Rosters'>
    {props.children}
  </ReachDialog>
}

export interface DialogCoverProps {
  children: React.ReactNode;
}

export function DialogPageCover(props: DialogCoverProps) {
  return <ReachDialogOverlay>
    {props.children}
  </ReachDialogOverlay>
}