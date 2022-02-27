import styled from "styled-components";
import { textOutline } from "../../style/outlineHelper";
import { TextBubble } from "./textBubble";

const sizingStyles = {
  Medium: `
    --width: 38px;
    --height: 36px;
  `,
  Large: `
    --width: 72px;
    --height: 62px;
  `
}

export const PositionBubble = styled(TextBubble)`
  ${textOutline('2px', 'var(--border-color)')}
  ${p => sizingStyles[p.size]}
  font-weight: bold;
  font-style: italic;
  text-align: center;
  padding: var(--vertical-padding);
  width: ${p => p.fullWidth ? '100%' : 'var(--width)'};
  height: var(--height);
`