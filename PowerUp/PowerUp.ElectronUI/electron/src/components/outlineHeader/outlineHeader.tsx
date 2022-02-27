import styled from 'styled-components';
import { FONT_SIZES } from '../../style/constants';
import { textOutline } from '../../style/outlineHelper';

export interface OutlineheaderProps {
  textColor: string;
  strokeColor: string; 
  slanted?: boolean;
  fontSize?: string;
  strokeWeight?: number;
  allCaps?: boolean;
}

export const OutlineHeader = styled.h1<OutlineheaderProps>`
  line-height: 1;
  font-size: ${p => p.fontSize ?? FONT_SIZES._64};
  font-style: ${p => p.slanted ? 'italic' : undefined};
  font-weight: bold;
  white-space: nowrap;
  text-transform: ${p => p.allCaps ? 'uppercase' : undefined};
  color: ${p => p.textColor};
  ${p => textOutline(`${p.strokeWeight ?? 2}px`, p.strokeColor)}
`