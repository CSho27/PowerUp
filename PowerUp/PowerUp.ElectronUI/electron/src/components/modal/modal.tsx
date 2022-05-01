import { DialogContent as ReachDialog, DialogOverlay as ReachDialogOverlay } from '@reach/dialog';
import { COLORS } from '../../style/constants';
import '@reach/dialog/styles.css';
import './modal.css';

export interface ModalProps {
  ariaLabel: string;
  children: React.ReactNode;
  distanceFromTop?: string;
  fullHeight?: boolean;
}

export function Modal(props: ModalProps) {
  const distanceFromTop = props.distanceFromTop ?? '104px';

  return <ReachDialog 
    aria-label={props.ariaLabel}
    style={{ 
      minWidth: '600px',
      backgroundColor: COLORS.jet.superlight_90, 
      boxShadow: '0px 3px 16px -8px',
      marginBottom: 0,
      marginTop: distanceFromTop,
      height: props.fullHeight 
        ? `calc(100% - ${distanceFromTop})`
        : undefined
    }}>
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