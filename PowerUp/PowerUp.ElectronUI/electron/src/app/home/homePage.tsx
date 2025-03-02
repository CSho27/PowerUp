import { useRef } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { MaxWidthWrapper } from "../../components/maxWidthWrapper/maxWidthWrapper";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { AppContext } from "../appContext";
import { PagePropsLoadFunction } from "../pages";
import { CopyExistingRosterApiClient } from "../rosterEditor/copyExistingRosterApiClient";
import { LoadExistingRosterOptionsApiClient } from "../rosterEditor/loadExistingRosterOptionsApiClient";
import { RosterGenerationModal } from "../rosterGenerationModal/rosterGenerationModal";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { ExistingRostersModal } from "./existingRostersModal";
import { ImportRosterModal } from "./importRosterModal";

export interface HomePageProps {
  appContext: AppContext;
}

export function HomePage(props: HomePageProps) {
  const { appContext } = props;
  const existingOptionsApiClientRef = useRef(new LoadExistingRosterOptionsApiClient(appContext.commandFetcher));
  const copyExistingRosterApiClientRef = useRef(new CopyExistingRosterApiClient(appContext.commandFetcher));

  return <PowerUpLayout>
    <ContentWrapper maxWidth='800px'>
      <AppTitle/>
      <ButtonSectionWrapper>
        <Button 
          variant='Fill' 
          size='Large'
          icon='folder-open'
          textAlign='left'
          onClick={openExistingModal}
        >
          Open Existing Roster
        </Button>
        <Button 
          variant='Fill' 
          size='Large'
          icon='copy'
          textAlign='left'
          onClick={openCopyExistingRosterModal}
        >
          Copy Existing Roster
        </Button> 
        <Button 
          variant='Fill' 
          size='Large'
          icon='wand-magic-sparkles'
          textAlign='left'
          onClick={openRosterGenerationModal}
        >
          Generate Roster
        </Button>
        <Button 
          variant='Fill' 
          size='Large'
          icon='upload'
          textAlign='left'
          onClick={openImportModal}
        >
          Import Roster
        </Button> 
      </ButtonSectionWrapper>
    </ContentWrapper>
  </PowerUpLayout>

  async function openExistingModal() {
    const response = await existingOptionsApiClientRef.current.execute();
    appContext.openModal(closeDialog => <ExistingRostersModal 
      appContext={appContext} 
      options={response} 
      okLabel='Open'
      closeDialog={closeDialog}
      onRosterSelected={loadExisting}
    />)
  }

  function loadExisting(rosterId: number) {
    appContext.setPage({ page: 'RosterEditorPage', rosterId: rosterId }); 
  }

  async function openCopyExistingRosterModal() {
    const response = await existingOptionsApiClientRef.current.execute();
    appContext.openModal(closeDialog => <ExistingRostersModal 
      appContext={appContext} 
      options={response} 
      okLabel='Create Copy'
      closeDialog={closeDialog}
      onRosterSelected={copyAndLoadRoster}
    />)
  }

  function openRosterGenerationModal() {
    appContext.openModal(closeDialog => <RosterGenerationModal 
      appContext={appContext}
      closeDialog={rosterId => {
        closeDialog();
        if(!!rosterId)
          loadExisting(rosterId);
      }}
    />)
  }

  async function copyAndLoadRoster(rosterId: number) {
    const response = await copyExistingRosterApiClientRef.current.execute({ rosterId: rosterId });
    appContext.setPage({ page: 'RosterEditorPage', rosterId: response.rosterId });
  }

  function openImportModal() {
    appContext.openModal(closeDialog => <ImportRosterModal
      appContext={appContext}
      closeDialog={closeDialog}
    />);
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
  margin-top: 32px;
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
export const loadHomePageProps: PagePropsLoadFunction<HomePageProps> = async (appContext, pageDef) => {
  if(pageDef.page !== 'HomePage')
    throw 'Wrong page def';
  return { title: 'Home' }
}