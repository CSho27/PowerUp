import styled from "styled-components"

export type FlexRowGap =
| '4px'
| '8px'
| '16px'

export const FlexRow = styled.div<{ gap: FlexRowGap, withBottomPadding?: boolean, vAlignCenter?: boolean }>`
  display: flex;
  gap: ${p => p.gap};
  align-items: ${p => p.vAlignCenter ? 'center' : undefined};
  padding-bottom: ${p => p.withBottomPadding ? '16px' : undefined};
`

export type FlexFrac = 
| '1/4'

export const FlexFracItem = styled.div<{ frac: FlexFrac }>`
  --width: calc(100% * ${p => `(${p.frac})`});
  width: var(--width);
  flex: 0 1 var(--width);
`