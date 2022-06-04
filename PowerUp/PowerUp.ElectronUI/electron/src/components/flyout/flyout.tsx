import Portal from "@reach/portal";
import { PropsWithChildren, ReactNode, useEffect, useRef, useState } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { GenerateId } from "../../utils/generateId";
import { isPromise } from "../../utils/isPromise";
import { Spinner } from "../spinner/spinner";

export interface FlyoutStateManagementProps {
  isOpen: boolean;
  onOpenTrigger: () => void;
  onCloseTrigger: () => void;
}

export interface FlyoutProps extends FlyoutStateManagementProps {
  flyout: FlyoutContent;
  positioning?: FlyoutPositioning;
  trigger?: FlyoutTrigger;
}

export type FlyoutContent = 
| ReactNode
| (() => ReactNode)
| (() => Promise<ReactNode>)

export type FlyoutPositioning =
| 'RightBottom' 
| 'RightTop' 
| 'BottomRight' 
| 'BottomLeft' 
| 'LeftTop' 
| 'LeftBottom'
| 'TopRight'
| 'TopLeft'

export type FlyoutTrigger = 'hover' | 'click';

interface FlyoutPosition {
  top?: number
  bottom?: number;
  left?: number;
  right?: number;
}

export function FlyoutAnchor(props: PropsWithChildren<FlyoutProps>) {
  const { isOpen, children, flyout, onOpenTrigger: triggerOpen, onCloseTrigger: triggerClose } = props;
  const positioning = props.positioning ?? 'TopRight';
  const isClickTrigger = props.trigger === 'click';

  const flyoutIdRef = useRef(`flyout-${GenerateId()}`);
  const [flyoutContent, setFlyoutContent] = useState<ReactNode|null>(flyout instanceof Function ? null : flyout)
  const [anchorElement, setAnchorElement] = useState<HTMLDivElement|null>(null);
  const [flyoutElement, setFlyoutElement] = useState<HTMLDivElement|null>(null);
  const [flyoutPosition, setFlyoutPosition] = useState<FlyoutPosition>({});

  useEffect(() => {
    updateFlyoutPosition();
    window.removeEventListener('scroll', triggerClose, true);
    window.removeEventListener('mousedown', closeIfOutsideOfFlyout);

    if(isOpen) {
      updateFlyoutContent();
      window.addEventListener('mousedown', closeIfOutsideOfFlyout);
      window.addEventListener('scroll', triggerClose, true);
    }
  }, [anchorElement, flyoutElement, isOpen, flyoutContent])

  return <div 
    ref={setAnchorElement} 
    onMouseUp={isClickTrigger
      ? handleAnchorClick
      : undefined}
    onMouseEnter={!isClickTrigger
      ? handleAnchorMouseEnter
      : undefined}
    onMouseLeave={!isClickTrigger
      ? handleAnchorMouseLeave
      : undefined}>
      {children}
      {isOpen && <Portal>
        <Flyout id={flyoutIdRef.current} ref={setFlyoutElement} style={flyoutPosition}>
          {!flyoutContent && <Spinner />}
          {!!flyoutContent && flyoutContent}
        </Flyout>
      </Portal>}
  </div>

  function handleAnchorClick() {
    if(!isOpen)
      triggerOpen();
  }

  function handleAnchorMouseEnter() {
    if(!isOpen)
      triggerOpen();
  }

  function handleAnchorMouseLeave() {
    setTimeout(() => {
      window.addEventListener('mousemove', closeIfOutsideOfFlyout);
    }, 10)
  }

  function closeIfOutsideOfFlyout(event: MouseEvent) {
    if(!isInsideFlyout(event.target as HTMLElement)) {
      window.removeEventListener('mousemove', closeIfOutsideOfFlyout);
      triggerClose();
    }
  }

  function isInsideFlyout(element: HTMLElement): boolean {
    if(element.id === flyoutIdRef.current)
      return true;
    else if(element.parentElement === null)
      return false;
    else
      return isInsideFlyout(element.parentElement);
  }

  function updateFlyoutPosition() {
    const anchorBoundingRect = anchorElement?.getBoundingClientRect();
    const flyoutBoundingRect = flyoutElement?.getBoundingClientRect();
    if(!anchorBoundingRect || !flyoutBoundingRect)
      return;
    
    setFlyoutPosition(getFlyoutPosition(anchorBoundingRect, flyoutBoundingRect));
  }

  async function updateFlyoutContent() {
    if(flyoutContent || !(flyout instanceof Function))
      return;
    
    const result = flyout();
    const content = isPromise(result)
      ? await result
      : result;

    setFlyoutContent(content);
  }

  function getFlyoutPosition(anchorRect: DOMRect, flyoutRect: DOMRect): FlyoutPosition {
    switch(positioning) {
      case 'RightBottom':
        return { top: anchorRect.top, left: anchorRect.right };
      case 'RightTop':
        return { top: anchorRect.bottom - flyoutRect.height, left: anchorRect.right };
      case 'BottomRight':
        return { top: anchorRect.bottom, left: anchorRect.left };
      case 'BottomLeft':
        return { top: anchorRect.bottom, left: anchorRect.right - flyoutRect.width };
      case 'LeftTop':
        return { top: anchorRect.bottom - flyoutRect.height, left: anchorRect.left - flyoutRect.width };
      case 'LeftBottom':
        return { top: anchorRect.top, left: anchorRect.left - flyoutRect.width };
      case 'TopRight':
        return { top: anchorRect.top - flyoutRect.height, left: anchorRect.left };
      case 'TopLeft':
        return { top: anchorRect.top - flyoutRect.height, left: anchorRect.right - flyoutRect.width };
    }
  }
}

const Flyout = styled.div`
  position: fixed;
  background-color: ${COLORS.white.regular_100};
  color: initial;
  box-shadow: 0px 3px 16px -8px;
`

export function useFlyoutState(): FlyoutStateManagementProps {
  const [isOpen, setIsOpen] = useState(false);
  return {
    isOpen: isOpen,
    onOpenTrigger: () => setIsOpen(true),
    onCloseTrigger: () => setIsOpen(false)
  }
}