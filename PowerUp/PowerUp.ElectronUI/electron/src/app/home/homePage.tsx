import styled from "styled-components";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { IAppContext } from "../appContext";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface HomePageProps {
  appContext: IAppContext
}

export function HomePage(props: HomePageProps) {
  return <PowerUpLayout>
    <ContentWrapper>
      <AppTitle/>
    </ContentWrapper>
  </PowerUpLayout>
}

function AppTitle() {
  return <AppTitleWrapper>
    <OutlineHeader 
      textColor={COLORS.white.regular_100} 
      strokeColor={COLORS.primaryBlue.regular_45}
      strokeWeight={4}
      fontSize={FONT_SIZES._128}
      slanted
    >
      PowerUp
    </OutlineHeader>
    <Subheader>MLB PowerPros Roster Editor</Subheader>
  </AppTitleWrapper>
}

const ContentWrapper = styled.div`
  height: 100%;
  padding: 64px;
`

const AppTitleWrapper = styled.div`
  margin-left: auto;
  margin-right: auto;
  width: fit-content;
`

const Subheader = styled.h1`
  color: ${COLORS.secondaryRed.regular_44};
  margin-top: -32px;
`