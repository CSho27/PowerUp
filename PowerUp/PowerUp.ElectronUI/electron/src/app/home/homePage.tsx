import styled from "styled-components";
import { Button } from "../../components/button/button";
import { MaxWidthWrapper } from "../../components/maxWidthWrapper/maxWidthWrapper";
import { Modal } from "../../components/modal/modal";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { PageLoadFunction } from "../pages";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface HomePageProps {
  appContext: AppContext
}

export function HomePage(props: HomePageProps) {
  const {appContext } = props;

  return <PowerUpLayout>
    <ContentWrapper maxWidth='800px'>
      <AppTitle/>
      <ButtonSectionWrapper>
        <Button 
          variant='Fill' 
          size='Large'
          icon='folder-open'
          textAlign='left'
          onClick={openExisting}
        >
          Open Existing Roster
        </Button>
        {/*
          <Button 
            variant='Fill' 
            size='Large'
            icon='wand-magic-sparkles'
            textAlign='left'
            onClick={() => {}}
          >
            Generate Roster
          </Button>
        */}
        <Button 
          variant='Fill' 
          size='Large'
          icon='upload'
          textAlign='left'
          onClick={() => {}}
        >
          Import Roster
        </Button>
        <Button 
          variant='Fill' 
          size='Large'
          icon='box-archive'
          textAlign='left'
          onClick={startFromBase}
        >
          Start From Base Roster
        </Button>  
      </ButtonSectionWrapper>
    </ContentWrapper>
  </PowerUpLayout>

  function openExisting() {
    appContext.openModal(closeDialog => <Modal>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Close</Button>
      <Button variant='Fill' size='Small' onClick={openExisting}>Open Another</Button>
    </Modal>)
  }

  function startFromBase() {
    appContext.setPage({ page: 'RosterEditorPage', rosterId: 1 });  
  }
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
`

export const loadHomePage: PageLoadFunction = async (appContext: AppContext) => {
  return <HomePage appContext={appContext}/>
}
