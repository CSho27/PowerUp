import styled, { keyframes } from "styled-components"
import { COLORS } from "../../style/constants"
import { Icon } from "../icon/icon"

export function FullPageSpinner() {
  return <PageCover>
    <SpinnerWrapper>
      <Icon icon='circle-notch'/>
    </SpinnerWrapper>
  </PageCover>
}

const PageCover = styled.div`
  background-color: ${COLORS.white.grayed_80_t40};
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  display: flex;
  align-items: center;
  justify-content: center;
`


const spinAnimation = keyframes`
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
`

const SpinnerWrapper = styled.div`
  font-size: 80px;
  color: ${COLORS.primaryBlue.regular_45};
  animation-name: ${spinAnimation};
  animation-duration: 1.5s;
  animation-iteration-count: infinite;
  animation-timing-function: linear;
`