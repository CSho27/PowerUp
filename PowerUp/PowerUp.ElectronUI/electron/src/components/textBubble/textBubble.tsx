import styled from "styled-components";
import { PositionType } from "../../app/shared/positionCode";
import { COLORS, FONT_SIZES } from "../../style/constants";

export type Size = 'Medium' | 'Large';

const sizingStyles = {
  Medium: `
    --horizontal-padding: 4px;
    --vertical-padding: 2px;
    --font-size: ${FONT_SIZES._24};
  `,
  Large: `
    --horizontal-padding: 8px;
    --vertical-padding: 4px;
    --font-size: ${FONT_SIZES._32};
  `
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

export const TextBubble = styled.div<{ positionType: PositionType, size: Size, squarePadding?: boolean, fullWidth?: boolean,  }>`
  ${p => colors[p.positionType]}
  ${p => sizingStyles[p.size]}
  background-color: var(--bubble-color);
  border: solid 3px var(--border-color);
  border-radius: 8px;
  width: ${p => p.fullWidth ? '100%' : 'fit-content'};
  padding: var(--vertical-padding) ${p => p.squarePadding ? '' : 'var(--horizontal-padding)'};
  font-size: var(--font-size);
  color: ${COLORS.white.regular_100};
  white-space: nowrap;
  line-height: 1;
`

