import React, { cloneElement, PropsWithChildren, ReactElement, ReactNode } from "react";
import styled from "styled-components"

export type FlexRowGap =
| '4px'
| '8px'
| '16px'

export interface FlexRowProps {
  gap: FlexRowGap,
  withBottomPadding?: boolean;
  vAlignCenter?: boolean;
}

export function FlexRow(props:  PropsWithChildren<FlexRowProps>) {
  const childCount = React.Children.count(props.children);
  const gap = Number.parseInt(props.gap.replace('px', ''));
  const totalGapWidth = (childCount - 1) * gap;
  const gapAdjustment = totalGapWidth / childCount;
  
  return <FlexRowWrapper {...props}>
    {React.Children.map(props.children, mapWithGap)}
  </FlexRowWrapper>

  function mapWithGap(child: ReactNode) {
    if(!isFlexFracItem(child))
      return child;

    const newProps: FlexFracItemProps = {
      ...child.props,
      gapAdjustment: `${gapAdjustment}px`
    }
    return cloneElement(child, newProps, child.props.children)
  }

  function isFlexFracItem(child: ReactNode): child is ReactElement<PropsWithChildren<FlexFracItemProps>> {
    return !!(child as ReactElement<FlexFracItemProps>)?.props?.frac;
  }
}

const FlexRowWrapper = styled.div<{ gap: FlexRowGap, withBottomPadding?: boolean, vAlignCenter?: boolean }>`
  display: flex;
  gap: ${p => p.gap};
  align-items: ${p => p.vAlignCenter ? 'center' : undefined};
  padding-bottom: ${p => p.withBottomPadding ? '16px' : undefined};
`

export type FlexFrac = 
| '1/2'

| '1/3'
| '2/3'

| '1/4'
| '1/5'
| '1/6'
| '1/8'
| '1/16'
| '1/24'

export interface FlexFracItemProps {
  frac: FlexFrac;
  /** LEAVE EMPTY, WILL BE OVERWRITTEN BY SURROUNDING FLEX ROW */
  gapAdjustment?: string;
}

export const FlexFracItem = styled.div<FlexFracItemProps>`
  --width: ${p => `calc((100% * (${p.frac})) - ${p.gapAdjustment})`};
  width: var(--width);
  flex: 0 0 var(--width);
`

export const FlexAutoItem = styled.div<{ allowShrink?: boolean }>`
  flex: 1 ${p => p.allowShrink ? '1' : '0'} auto;
`