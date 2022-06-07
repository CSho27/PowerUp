import { Children, PropsWithChildren, useState } from "react";
import styled from "styled-components"
import { Icon } from "../../components/icon/icon";
import { FONT_SIZES } from "../../style/constants";
import { PitchBreakMeter } from "./pitchBreakMeter"

export interface PitchArsenalDisplayProps {
  isLefty: boolean;

  twoSeamType: string | null;
  twoSeamMovement: number | null;
  slider1Type: string | null;
  slider1Movement: number | null;
  slider2Type: string | null;
  slider2Movement: number | null;
  curve1Type: string | null;
  curve1Movement: number | null;
  curve2Type: string | null;
  curve2Movement: number | null;
  fork1Type: string | null;
  fork1Movement: number | null;
  fork2Type: string | null;
  fork2Movement: number | null;
  sinker1Type: string | null;
  sinker1Movement: number | null;
  sinker2Type: string | null;
  sinker2Movement: number | null;
  sinkingFastball1Type: string | null;
  sinkingFastball1Movement: number | null;
  sinkingFastball2Type: string | null;
  sinkingFastball2Movement: number | null;
}

export function PitchArsenalDisplay(props: PitchArsenalDisplayProps) {
  const { isLefty } = props;
  const minusIfLefty = isLefty
    ? '-'
    : '';
  const minusIfRighty = !isLefty
    ? '-'
    : '';
  
  return <PitchArsenalDisplayWrapper isLefty={props.isLefty}>
    <TwoSeamMovementContainer>
      <PositioningContainer position='center'>
        <PitchLabel top={-18}>{props.twoSeamType}</PitchLabel>
        <PitchBreakMeter movement={props.twoSeamMovement ?? 0} maxMovement={3} />
      </PositioningContainer>
    </TwoSeamMovementContainer>
    <PositioningContainer position='right'>
      <PitchLabel top={props.sinkingFastball2Type ? -28 : -10} right={!isLefty ? 20 : undefined} left={isLefty ? 20 : undefined}>
        {props.sinkingFastball1Type}
      </PitchLabel>
      {props.sinkingFastball2Type && 
      <PitchLabel top={-14} right={!isLefty ? 20 : undefined} left={isLefty ? 20 : undefined}>
        {props.sinkingFastball2Type}
      </PitchLabel>}
      <RotationWrapper rotation={`${minusIfRighty}90deg`}>
        <FlexWrapper reverse>
          <PitchBreakMeter movement={props.sinkingFastball1Movement ?? 0} maxMovement={7} />
          {props.sinkingFastball2Movement && <PitchBreakMeter movement={props.sinkingFastball2Movement} maxMovement={7} alternateColor />}
        </FlexWrapper>
      </RotationWrapper>
    </PositioningContainer>
    <PositioningContainer position='center'>
      <RotationWrapper rotation='-45deg'>
        <Icon icon='baseball' />
      </RotationWrapper>
    </PositioningContainer>
    <PositioningContainer position='left'>
      <PitchLabel top={props.slider2Type ? -28 : -10} right={isLefty ? 20 : undefined} left={!isLefty ? 20 : undefined}>
        {props.slider1Type}
      </PitchLabel>
      {props.slider2Type && 
      <PitchLabel top={-14} right={isLefty ? 20 : undefined} left={!isLefty ? 20 : undefined}>
        {props.slider2Type}
      </PitchLabel>}
      <RotationWrapper rotation={`${minusIfLefty}90deg`}>
        <FlexWrapper>
          <PitchBreakMeter movement={props.slider1Movement ?? 0} maxMovement={7} />
          {props.slider2Movement && <PitchBreakMeter movement={props.slider2Movement} maxMovement={7} alternateColor />}
        </FlexWrapper>
      </RotationWrapper>
    </PositioningContainer>
    <PositioningContainer position='right' top={-8} right={!isLefty ? -4 : undefined} left={isLefty ? -4 : undefined}>
      <PitchLabel top={48} right={!isLefty ? 30 : undefined} left={isLefty ? 30 : undefined}>
        {props.sinker1Type}
      </PitchLabel>
      {props.sinker2Type && 
      <PitchLabel top={62} right={!isLefty ? 30 : undefined} left={isLefty ? 30 : undefined}>
        {props.sinker2Type}
      </PitchLabel>}
      <RotationWrapper rotation={`${minusIfRighty}135deg`}>
        <FlexWrapper reverse>
          <PitchBreakMeter movement={props.sinker1Movement ?? 0} maxMovement={7} />
          {props.sinker2Movement && <PitchBreakMeter movement={props.sinker2Movement} maxMovement={7} alternateColor />}
        </FlexWrapper>
      </RotationWrapper>
    </PositioningContainer>
    <PositioningContainer position='center'>
      <PitchLabel top={56}>
        {props.fork1Type}
      </PitchLabel>
      {props.fork2Type && 
      <PitchLabel top={70}>
        {props.fork2Type}
      </PitchLabel>}
      <RotationWrapper rotation='180deg'>
        <FlexWrapper reverse={!isLefty}>
          <PitchBreakMeter movement={props.fork1Movement ?? 0} maxMovement={7} />
          {props.fork2Movement && <PitchBreakMeter movement={props.fork2Movement} maxMovement={7} alternateColor />}
        </FlexWrapper>
      </RotationWrapper>
    </PositioningContainer>
    <PositioningContainer position='left' top={-8} left={!isLefty ? -4 : undefined} right={isLefty ? -4 : undefined}>
      <PitchLabel top={48} right={isLefty ? 30 : undefined} left={!isLefty ? 30 : undefined}>
        {props.curve1Type}
      </PitchLabel>
      {props.curve2Type && 
      <PitchLabel top={62} right={isLefty ? 30 : undefined} left={!isLefty ? 30 : undefined}>
        {props.curve2Type}
      </PitchLabel>}
      <RotationWrapper rotation={`${minusIfLefty}135deg`}>
        <FlexWrapper>
          <PitchBreakMeter movement={props.curve1Movement ?? 0} maxMovement={7} />
          {props.curve2Movement && <PitchBreakMeter movement={props.curve2Movement} maxMovement={7} alternateColor />}
        </FlexWrapper>
      </RotationWrapper>
    </PositioningContainer>
  </PitchArsenalDisplayWrapper>
}

const PositioningContainer = styled.div<{ position: 'left' | 'center' | 'right', top?: number, left?: number, right?: number }>`
  position: relative;
  top: ${p => p.top}px;
  left: ${p => p.left}px;
  right: ${p => p.right}px;
  display: flex;
  align-items: center;
  justify-content: ${p => p.position === 'left'
    ? 'flex-start'
    : p.position === 'center'
      ? 'center'
      : 'flex-end'};
`

const FlexWrapper = styled.div<{ reverse?: boolean }>`
  display: flex;
  flex-direction: ${p => p.reverse ? 'row-reverse' : undefined};
`

const PitchLabel = styled.div<{ top?: number, left?: number, right?: number, bottom?: number }>`
  position: absolute;
  top: ${p => p.top}px;
  left: ${p => p.left}px;
  right: ${p => p.right}px;
  bottom: ${p => p.bottom}px;
  font-size: ${FONT_SIZES._14};
  font-style: italic;
  text-transform: uppercase;
`

interface RotationWrapperProps {
  rotation: string;
}

function RotationWrapper(props: PropsWithChildren<RotationWrapperProps>) {
  const [rotatedElement, setRotatedElement] = useState<HTMLElement|null>(null);
  const boundingClientRect = rotatedElement?.getBoundingClientRect();
  const width = boundingClientRect
    ? Math.abs(boundingClientRect.right - boundingClientRect.left)
    : undefined;
  const height = boundingClientRect
    ? Math.abs(boundingClientRect.top - boundingClientRect.bottom)
    : undefined; 

  return <RotationContainer width={width} height={height}>
    <RotatedElement ref={setRotatedElement} rotation={props.rotation}>
        {props.children}
    </RotatedElement>
  </RotationContainer>
}

const RotationContainer = styled.div<{ height?: number, width?: number }>`
  box-sizing: content-box;
  height: ${p => p.height}px;
  width: ${p => p.width}px;
  display: flex;
  align-items: center;
  justify-content: center; 
`

const RotatedElement = styled.div<{ rotation: string }>`
  transform: rotate(${p => p.rotation});
`

const PitchArsenalDisplayWrapper = styled.div<{ isLefty: boolean }>`
  padding: 24px;
  padding-bottom: 0px;
  display: grid;
  grid-template-columns: 1fr auto 1fr;
  direction: ${p => p.isLefty ? 'rtl' : 'ltr'};
`

const TwoSeamMovementContainer = styled.div`
  grid-column: span 3;
  display: flex;
  justify-content: center;
`