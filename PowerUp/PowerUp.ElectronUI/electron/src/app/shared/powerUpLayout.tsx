import styled from "styled-components"
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS } from "../../style/constants";

export interface PowerUpLayoutProps {
  headerText?: string;
}

export function PowerUpLayout(props: PowerUpLayoutProps) {
  const { headerText } = props;
  
  return <LayoutWrapper>
    <HeaderWrapper>
      <LogoCorner>P</LogoCorner>
      <HeaderTextWrapper>
        <OutlineHeader textColor={COLORS.secondaryRed.regular_44} strokeColor={COLORS.white.regular_100} slanted>{headerText}</OutlineHeader>
      </HeaderTextWrapper>
    </HeaderWrapper>
    <main>
    </main>
  </LayoutWrapper>
}

const LayoutWrapper = styled.div`
  height: 100%
`

const HeaderWrapper = styled.header`
  height: 100px;
  background-color: ${COLORS.primaryBlue.regular_45};
  display: flex;
  align-items: center;
`

const LogoCorner = styled.div`
  height: 100%;
  width: 200px;
  background-color: ${COLORS.white.regular_100};
  color: ${COLORS.secondaryRed.regular_44};
`

const HeaderTextWrapper = styled.div`
  padding: 36px;
`