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
  withoutBackground?: boolean;
  borderRadius?: string;
}

export type FlyoutContent = 
| ReactNode
| (() => ReactNode)
| (() => Promise<ReactNode>)

const positioningTypes = [
  'RightBottom', 
  'RightTop', 
  'BottomRight', 
  'BottomLeft', 
  'LeftTop', 
  'LeftBottom',
  'TopRight',
  'TopLeft'
] as const;
export type FlyoutPositioning = typeof positioningTypes[number]

export type FlyoutTrigger = 'hover' | 'click';

interface FlyoutPosition {
  top: number | undefined;
  left: number | undefined;
}

export function FlyoutAnchor(props: PropsWithChildren<FlyoutProps>) {
  const { 
    isOpen, 
    children, 
    flyout, 
    withoutBackground, 
    borderRadius,
    onOpenTrigger: triggerOpen, 
    onCloseTrigger: triggerClose 
  } = props;
  const positioning = props.positioning ?? 'TopRight';
  const isClickTrigger = props.trigger === 'click';

  const flyoutIdRef = useRef(`flyout-${GenerateId()}`);
  const [flyoutContent, setFlyoutContent] = useState<ReactNode|null>(flyout instanceof Function ? null : flyout)
  const [anchorElement, setAnchorElement] = useState<HTMLDivElement|null>(null);
  const [flyoutElement, setFlyoutElement] = useState<HTMLDivElement|null>(null);
  const [flyoutPosition, setFlyoutPosition] = useState<FlyoutPosition>({ top: undefined, left: undefined });

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

  return <FlyoutAnchorWrapper
    isClickTrigger={isClickTrigger} 
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
        <Flyout id={flyoutIdRef.current} ref={setFlyoutElement} style={flyoutPosition} withoutBackground borderRadius={borderRadius}>
          {/*!flyoutContent && <Spinner />*/}
          {!!flyoutContent && flyoutContent}
        </Flyout>
      </Portal>}
  </FlyoutAnchorWrapper>

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
    const defaultPosition = getPositionFor(anchorRect, flyoutRect, positioning);
    if(isFullyOnScreen(defaultPosition, flyoutRect))
      return defaultPosition;
    
    for(let i=0; i<positioningTypes.length; i++) {
      const position = getPositionFor(anchorRect, flyoutRect, positioningTypes[i]);
      if(isFullyOnScreen(position, flyoutRect))
      return position;
    }

    return defaultPosition;
  }

  function isFullyOnScreen(flyoutPosition: FlyoutPosition, flyoutRect: DOMRect): boolean {
    return flyoutPosition.left! >= 0 &&
    flyoutPosition.left! + flyoutRect.width <= document.body.clientWidth &&
    flyoutPosition.top! >= 0 &&
    flyoutPosition.top! + flyoutRect.height <= document.body.clientHeight;
  }

  function getPositionFor(anchorRect: DOMRect, flyoutRect: DOMRect, positioning: FlyoutPositioning): FlyoutPosition {
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

const FlyoutAnchorWrapper = styled.div<{ isClickTrigger: boolean }>`
  cursor: ${p => p.isClickTrigger ? 'pointer' : undefined};
`

const Flyout = styled.div<{ withoutBackground: boolean, borderRadius?: string }>`
  position: fixed;
  background-color: ${p => p.withoutBackground ? '' : COLORS.white.regular_100};
  color: initial;
  box-shadow: 0px 3px 16px -8px;
  border-radius: ${p => p.borderRadius ? p.borderRadius :  '4px'};
`

export function useFlyoutState(): FlyoutStateManagementProps {
  const [isOpen, setIsOpen] = useState(false);
  return {
    isOpen: isOpen,
    onOpenTrigger: () => setIsOpen(true),
    onCloseTrigger: () => setIsOpen(false)
  }
}