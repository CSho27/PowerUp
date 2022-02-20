import styled from "styled-components";
import { Button } from "../../components/button/button";
import { Icon } from "../../components/icon/icon";
import { MaxWidthWrapper } from "../../components/maxWidthWrapper/maxWidthWrapper";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { IAppContext } from "../appContext";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface HomePageProps {
  appContext: IAppContext
}

export function HomePage(props: HomePageProps) {
  return <PowerUpLayout>
    <ContentWrapper maxWidth='800px'>
      <AppTitle/>
      <ButtonSectionWrapper>
        <Button 
          variant='Fill' 
          size='Large'
          textAlign='left'
          onClick={() => {}}
        >
          Open Existing Roster
        </Button>
        {/*
          <Button 
            variant='Fill' 
            size='Large'
            textAlign='left'
            onClick={() => {}}
          >
            Generate Roster
          </Button>
        */}
        <Button 
          variant='Fill' 
          size='Large'
          textAlign='left'
          icon='upload'
          onClick={() => {}}
        >
          Import Roster
        </Button>
        <Button 
          variant='Fill' 
          size='Large'
          textAlign='left'
          onClick={() => {}}
        >
          Start From Base Roster
        </Button>  
      </ButtonSectionWrapper>
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

const ContentWrapper = styled(MaxWidthWrapper)`
  padding: 32px 0px;
`

const ButtonSectionWrapper = styled.section`
  margin-top: 64px;
  display: flex;
  flex-direction: column;
  gap: 16px;
  width: fit-content;
  margin-left: auto;
  margin-right: auto;
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