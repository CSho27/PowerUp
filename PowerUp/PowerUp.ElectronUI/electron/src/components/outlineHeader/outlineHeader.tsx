import styled from 'styled-components';
import { FONT_SIZES } from '../../style/constants';
import { textOutline } from '../../style/outlineHelper';

export const OutlineHeader = styled.h1<{ textColor: string, strokeColor: string, slanted?: boolean }>`
  font-size: ${FONT_SIZES._64};
  font-style: ${p => p.slanted ? 'italic' : undefined};
  font-weight: bold;
  white-space: nowrap;
  text-transform: uppercase;
  color: ${props => props.textColor};
  ${props => textOutline('2px', props.strokeColor)}
`