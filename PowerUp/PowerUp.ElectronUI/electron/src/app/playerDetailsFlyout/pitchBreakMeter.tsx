import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface PitchBreakMeterProps {
  movement: number;
  maxMovement: number;
  alternateColor?: boolean;
}

export function PitchBreakMeter(props: PitchBreakMeterProps) {
  const { movement, maxMovement, alternateColor } = props;

  const pitchBreakSquares = [];
  for(let i=0; i<maxMovement; i++) {
    pitchBreakSquares.push(<PitchBreakSquare key={i} isFilled={i<movement} alternateColor={!!alternateColor} />)
  }

  return <PitchBreakMeterWrapper>
    {pitchBreakSquares}
  </PitchBreakMeterWrapper>
}

const PitchBreakMeterWrapper = styled.div`
  display: flex;
  flex-direction: column-reverse;
  align-items: stretch;
`

const PitchBreakSquare = styled.div<{ isFilled: boolean, alternateColor: boolean }>`
  width: 8px;
  height: 8px;
  border: 1px solid ${COLORS.richBlack.regular_5};
  background-color: ${p => p.isFilled 
    ? p.alternateColor 
      ? COLORS.primaryBlue.light_50 
      : COLORS.secondaryRed.light_69 
    : undefined};

  &:last-of-type {
    border-radius: 50% 50% 0 0
  }
`