import { ReactNode } from "react";
import styled, { CSSProperties } from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";

export type PositionType = 'Catcher' | 'Infielder' | 'Outfielder' | 'Pitcher';

export interface TextBubbleProps {
  positionType: PositionType;
  width?: string;
  height?: string;
  style?: CSSProperties;
  children?: ReactNode;
}

export function TextBubble(props: TextBubbleProps) {
  const { positionType, width, height, style, children } = props;

  return <BubbleWrapper positionType={positionType} width={width} height={height} style={style}>
    {children}
  </BubbleWrapper>
}

const colors: { [key in PositionType]: string } = {
  Catcher: `
    --bubble-color: ${COLORS.PP_CatcherBlue};
    --border-color: ${COLORS.PP_CatcherBlue_Dark};
  `,
  Infielder: `
    --bubble-color: ${COLORS.PP_InfielderYellow};
    --border-color: ${COLORS.PP_InfielderYellowDark};
  `,
  Outfielder: `
    --bubble-color: ${COLORS.PP_OutfielderGreen};
    --border-color: ${COLORS.PP_OutfielderGreenDark};
  `,
  Pitcher: `
    --bubble-color: ${COLORS.PP_PitcherRed};
    --border-color: ${COLORS.PP_PitcherRedDark};
  `
}

const bubbleText = styled.div`
  font-size: ${FONT_SIZES._48};
  line-height: 1.2;
  margin-top: -4px;
  color: ${COLORS.white};
  white-space: nowrap;
`

export const PlayerName = styled(bubbleText)`
  ${textOutline('1px', COLORS.black)}
`

export const Position = styled(bubbleText)`
  ${textOutline('2px', COLORS.PP_InfielderYellowDark)}
  font-weight: bold;
  font-style: italic;
`

const BubbleWrapper = styled.div<{ positionType: PositionType, width: string | undefined, height: string | undefined }>`
  ${p => colors[p.positionType]}
  background-color: var(--bubble-color);
  border: solid 3px var(--border-color);
  border-radius: 8px;
  width: ${p => p.width ?? 'fit-content'};
  height: ${p => p.height ?? 'fit-content'};
  padding: 2px 4px;
`

