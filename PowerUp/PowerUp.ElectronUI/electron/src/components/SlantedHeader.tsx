import styled from 'styled-components';
import { FONT_SIZES } from '../style/constants';
import { textOutline } from '../style/outlineHelper';

export const SlantedOutlineHeader = styled.h1<{ textColor: string, strokeColor: string }>`
  font-size: ${FONT_SIZES._64};
  font-style: italic;
  font-weight: bold;
  text-transform: uppercase;
  color: ${props => props.textColor};
  ${props => textOutline('2px', props.strokeColor)}
`