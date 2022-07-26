import styled from 'styled-components';
import { COLORS } from '../../style/constants';

export interface ProgressBarProps {
  progress: number;
  size: ProgressBarSize;
}

export type ProgressBarSize = 'Small' | 'Medium' | 'Large';

export function ProgressBar(props: ProgressBarProps) {
  const { progress, size } = props;
  const numericValue = sanitizeValue(progress);

  return <Background 
    role='progressbar'
    aria-valuenow={numericValue} 
    aria-valuemin={0} 
    aria-valuemax={100}
    size={size}>
      <Bar progress={numericValue}/>
  </Background>
  
  function sanitizeValue(value: number) {
    if(!value) return 0;
    if(value < 0) return 0;
    if(value > 100) return 100;
    else return value;
  }
};

const sizes = {
  Small: `
    --background-height: 8px;
    --background-radius: 4px;
  `,
  Medium: `
    --background-height: 12px;
    --background-radius: 4px;
  `,
  Large: `
    --background-height: 24px;
    --background-radius: 8px;
    --background-padding: 4px;
  `
}

const Background = styled.div<{ size: ProgressBarSize }>`
  ${p => sizes[p.size]}
  background-color: ${COLORS.jet.regular_25_t20};
  height: var(--background-height);
  width: 100%;
  border-radius: var(--background-radius);
  box-shadow: inset 0px 2px 4px ${COLORS.jet.regular_18_t70};
  padding: var(--background-padding);
`

const Bar = styled.div<{ progress: number }>`
  --bar-radius: 4px;
  --percent-progress: ${p => p.progress}%;
  --right-edge-radius: calc(var(--bar-radius) + var(--percent-progress) - 100%);
  background-color: ${COLORS.primaryBlue.regular_45};
  width: var(--percent-progress);
  height: 100%;
  border-top-left-radius: var(--bar-radius);
  border-bottom-left-radius: var(--bar-radius);
  border-top-right-radius: var(--right-edge-radius);
  border-bottom-right-radius: var(--right-edge-radius);
`
