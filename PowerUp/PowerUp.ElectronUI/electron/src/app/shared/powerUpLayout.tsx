import styled from "styled-components"
import { Icon } from "../../components/icon/icon";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";

export interface PowerUpLayoutProps {
  headerText?: string;
  sidebar?: React.ReactNode;
  children?: React.ReactNode;
}

export function PowerUpLayout(props: PowerUpLayoutProps) {
  const { headerText, sidebar, children } = props;
  
  return <LayoutWrapper>
    <HeaderWrapper>
      <LogoCorner>
        <LogoP>P</LogoP>
        <Icon icon='arrow-up'/>
      </LogoCorner>
      <HeaderTextWrapper>
        <OutlineHeader textColor={COLORS.secondaryRed.regular_44} strokeColor={COLORS.white.regular_100} fontSize={FONT_SIZES._80} slanted>{headerText}</OutlineHeader>
        <HelpIconSectionWrapper>
          <HelpIconWrapper href='https://github.com/CSho27/PowerUp#use-guide' target='_blank' title='View Use Guide'>
            Help
            <Icon icon='circle-question' />
          </HelpIconWrapper>
        </HelpIconSectionWrapper>
      </HeaderTextWrapper>
    </HeaderWrapper>
    <PageContent>
      <Sidebar>{sidebar}</Sidebar>
      <MainContent>{children}</MainContent>
    </PageContent>
  </LayoutWrapper>
}

const LayoutWrapper = styled.div`
  height: 100%;
  display: flex;
  flex-direction: column;
`

const HeaderWrapper = styled.header`
  height: 104px;
  background-color: ${COLORS.primaryBlue.regular_45};
  display: flex;
  align-items: center;
`

const HelpIconSectionWrapper = styled.div`
  flex: auto;
`

const HelpIconWrapper = styled.a`    
  flex: auto;
  font-size: ${FONT_SIZES.default_16};
  color: white;
  display: flex;
  justify-content: flex-end;
  align-items: baseline;
  gap: 4px;
  text-decoration: none;
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
  flex: auto;
  padding: 0px 36px;
  margin-top: -9px;
  display: flex;
  align-items: center;
  justify-content: flex-end;
`

const PageContent = styled.div`
  flex: 1 0 auto;
  display: flex;
`

const Sidebar = styled.aside`
  background-color: ${COLORS.jet.superlight_85};
  height: 100%;
  flex: 0 0 200px;
`

const MainContent = styled.main`
  flex: 1 0 auto;
  position: absolute;
  top: 100px;
  bottom: 0px;
  left: 200px;
  right: 0px;
  overflow-y: auto;
`