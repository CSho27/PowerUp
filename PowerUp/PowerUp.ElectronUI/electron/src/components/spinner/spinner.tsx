import styled from "styled-components"
import { spinAnimation } from "../../style/animations"
import { COLORS } from "../../style/constants"
import { Icon } from "../icon/icon"

export interface SpinnerProps {
  size?: SpinnerSize;
}

export type SpinnerSize = 'Medium' | 'Large'

export function Spinner(props: SpinnerProps) {
  return <SpinnerWrapper isLarge={props.size === 'Large'}>
    <Icon icon='circle-notch' style={{ display: 'block' }}/>
  </SpinnerWrapper>
}

const SpinnerWrapper = styled.div<{ isLarge: boolean }>`
  font-size: ${p => p.isLarge? '80px' : '40px'};
  color: ${COLORS.primaryBlue.regular_45};
  animation-name: ${spinAnimation};
  animation-duration: 1.5s;
  animation-iteration-count: infinite;
  animation-timing-function: linear;
`