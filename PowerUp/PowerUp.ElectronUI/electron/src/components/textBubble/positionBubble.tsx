import styled from "styled-components";
import { textOutline } from "../../style/outlineHelper";
import { TextBubble } from "./textBubble";

const sizingStyles = {
  Medium: `
    --width: 42px;
  `,
  Large: `
    --width: 56px;
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
`