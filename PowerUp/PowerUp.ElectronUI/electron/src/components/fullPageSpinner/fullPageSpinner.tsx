import styled from "styled-components"
import { COLORS } from "../../style/constants"
import { Spinner } from "../spinner/spinner"
import { Portal } from "@reach/portal"

export function FullPageSpinner() {
  return <Portal>
    <PageCover>
      <Spinner size='Large' />
    </PageCover>
  </Portal>
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