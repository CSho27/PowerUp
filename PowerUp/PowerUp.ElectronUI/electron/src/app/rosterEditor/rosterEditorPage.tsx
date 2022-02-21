import styled from "styled-components";
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { HomePage } from "../home/homePage";
import { RosterDetails } from "../home/importBaseRosterApiClient";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface RosterEditorPageProps {
  appContext: AppContext;
  rosterDetails: RosterDetails;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, rosterDetails } = props;
  const { name: rosterName, teams } = rosterDetails;
  
  const header = <>
    <Breadcrumbs>
      <Crumb key='Home' onClick={returnHome}>Home</Crumb>
      <Crumb key='RosterEditor'>{rosterName}</Crumb>
    </Breadcrumbs>
    <RosterHeader>{rosterName}</RosterHeader>
  </> 

  return <PowerUpLayout headerText='Edit Roster'>
    <ContentWithHangingHeader header={header} headerHeight='88px'>
      <GridContainer>
        <GridScrollContainer>
          <div>
            {teams.concat(teams).concat(teams).map(t => <div key={t.name}>{t.name}</div>)}
          </div>
        </GridScrollContainer>
      </GridContainer>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function returnHome() {
    appContext.setPage(<HomePage appContext={appContext} />)
  }
}

const RosterHeader = styled.h1`
  padding-bottom: 8px;
  font-size: ${FONT_SIZES._32};
  font-style: italic;
`

const GridContainer = styled.section`
  flex: 1 0 auto;
`

const GridScrollContainer = styled.div`
  height: 100%;
  overflow-y: auto;
  display: flex;
`
