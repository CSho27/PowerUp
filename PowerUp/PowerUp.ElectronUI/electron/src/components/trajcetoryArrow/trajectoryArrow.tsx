import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { Icon } from "../icon/icon";

export type TrajectoryValue = 1 | 2 | 3 | 4;

export interface TrajectoryArrowProps {
  value: TrajectoryValue;
  size?: 'Small' | 'Medium';
}

export function TrajectoryArrow(props: TrajectoryArrowProps) {
  return <ArrowWrapper value={props.value} size={props.size}>
    <Icon icon='arrow-right'/>
  </ArrowWrapper>
}

const arrowStyles: { [value in TrajectoryValue]: string } = {
  '1': `
    --arrow-angle: 0deg;
    --arrow-color: ${COLORS.grades.D_yellow_49};
  `,
  '2': `
  --arrow-angle: -25deg;
  --arrow-color: ${COLORS.grades.C_orange_59};
  `,
  '3': `
    --arrow-angle: -50deg;
    --arrow-color: ${COLORS.grades.B_red_46};
  `,
  '4': `
    --arrow-angle: -75deg;
    --arrow-color: ${COLORS.grades.A_pink_51};
  `
}

const ArrowWrapper = styled.div<{ value: TrajectoryValue, size?: 'Small' | 'Medium' }>`
  ${p => arrowStyles[p.value]}
  line-height: 1;
  width: fit-content;
  transform: rotate(var(--arrow-angle));
  color: var(--arrow-color);
  font-size: ${p => p.size === 'Small' ? FONT_SIZES._24 : FONT_SIZES._32};
`