import styled from "styled-components"
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";

export interface PowerUpLayoutProps {
  headerText?: string;
}

export function PowerUpLayout(props: PowerUpLayoutProps) {
  const { headerText } = props;
  
  return <LayoutWrapper>
    <HeaderWrapper>
      <LogoCorner>
        <LogoP>P</LogoP>
        <i className='fa-solid fa-arrow-up'/>
      </LogoCorner>
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
  font-size: ${FONT_SIZES._64};
  font-weight: bold;
  font-style: italic;
  letter-spacing: 3px;
  display: flex;
  align-items: center;
  justify-content: center;
  ${textOutline('2px', COLORS.jet.lighter_71)}
`

const LogoP = styled.span`
  font-size: calc(4rem * 1.3);
  margin-top: -9px;
`

const HeaderTextWrapper = styled.div`
  padding: 36px;
`