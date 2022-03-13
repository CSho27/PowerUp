import { DialogContent as ReachDialog, DialogOverlay as ReachDialogOverlay } from '@reach/dialog';
import { COLORS } from '../../style/constants';
import '@reach/dialog/styles.css';
import './modal.css';

export interface ModalProps {
  children: React.ReactNode;
}

export function Modal(props: ModalProps) {
  return <ReachDialog aria-label='Existing Rosters'>
    {props.children}
  </ReachDialog>
}

export interface ModalCoverProps {
  transparent?: boolean;
  children: React.ReactNode;
}

export function ModalPageCover(props: ModalCoverProps) {
  return <ReachDialogOverlay
    style={{ backgroundColor: props.transparent 
      ? COLORS.transparent.regular_100
      : COLORS.white.grayed_80_t40
    }}>
    {props.children}
  </ReachDialogOverlay>
}