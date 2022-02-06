import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";

export const FieldLabel = styled.label<{ inline?: boolean }>`
  color: ${COLORS.PP_Blue23};
  font-weight: 500;
  font-size: ${FONT_SIZES._24};
  display: ${p => p.inline ? undefined : 'block'};
`;