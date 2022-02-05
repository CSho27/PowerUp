import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../style/constants";
import { textOutline } from "../style/outlineHelper";

export type PositionType = 'Catcher' | 'Infielder' | 'Outfielder' | 'Pitcher';

export interface PlayerBubbleProps {
  savedName: string;
  positionType: PositionType;
  width?: string;
}

export function PlayerBubble(props: PlayerBubbleProps) {
  const { savedName, positionType, width } = props;

  return <BubbleWrapper positionType={positionType} width={width}>
    <PlayerName>{savedName}</PlayerName>
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

const BubbleWrapper = styled.div<{ positionType: PositionType, width?: string; }>`
  ${p => colors[p.positionType]}
  background-color: var(--bubble-color);
  border: solid 3px var(--border-color);
  border-radius: 8px;
  height: fit-content;
  width: 200px;
  padding: 2px 4px;
`

const PlayerName = styled.div`
  ${textOutline('1px', COLORS.black)}
  color: ${COLORS.white};
  font-size: ${FONT_SIZES._48};
  line-height: 1;
  margin-top: -4px;
`