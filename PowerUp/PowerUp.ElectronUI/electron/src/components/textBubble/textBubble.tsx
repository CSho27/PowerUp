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

  return <BubbleWrapper positionType={positionType} bubbleWidth={width} bubbleHeight={height} style={style}>
    {children}
  </BubbleWrapper>
}

const colors: { [key in PositionType]: string } = {
  Catcher: `
    --bubble-color: ${COLORS.positions.catcherBlue.regular_55};
    --border-color: ${COLORS.positions.catcherBlue.dark_40};
  `,
  Infielder: `
    --bubble-color: ${COLORS.positions.infielderYellow.regular_58};
    --border-color: ${COLORS.positions.infielderYellow.dark_40};
  `,
  Outfielder: `
    --bubble-color: ${COLORS.positions.outfielderGreen.regular_45};
    --border-color: ${COLORS.positions.outfielderGreen.dark_33};
  `,
  Pitcher: `
    --bubble-color: ${COLORS.positions.pitcherRed.regular_48};
    --border-color: ${COLORS.positions.pitcherRed.dark_30};
  `
}

const bubbleText = styled.div<{ fontSize?: string }>`
  font-size: ${p => p.fontSize ?? FONT_SIZES._48};
  color: ${COLORS.white.regular_100};
  white-space: nowrap;
  line-height: 1;
`

export const PlayerName = styled(bubbleText)`
  ${textOutline('1px', COLORS.richBlack.regular_5)}
`

export const Position = styled(bubbleText)`
  ${textOutline('2px', 'var(--border-color)')}
  font-weight: bold;
  font-style: italic;
`

const BubbleWrapper = styled.div<{ positionType: PositionType, bubbleWidth: string | undefined, bubbleHeight: string | undefined }>`
  ${p => colors[p.positionType]}
  background-color: var(--bubble-color);
  border: solid 3px var(--border-color);
  border-radius: 8px;
  width: ${p => p.bubbleWidth ?? 'fit-content'};
  height: ${p => p.bubbleHeight ?? 'fit-content'};
`

