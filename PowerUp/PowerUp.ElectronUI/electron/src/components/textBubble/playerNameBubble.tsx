import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { TextBubble } from "./textBubble";

export const PlayerNameBubble = styled(TextBubble)`
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
`
