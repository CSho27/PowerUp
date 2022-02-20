import styled from "styled-components";
import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadcrumbs";
import { HangingHeader } from "../../components/hangingHeader/hangingHeader";
import { PositionType } from "../../components/textBubble/textBubble";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { HomePage } from "../home/homePage";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface RosterEditorPageProps {
  appContext: AppContext;
  rosterName: string;
  teams: TeamDetails[]
}

export interface TeamDetails {
  name: string;
  powerProsName: string;
  hitters: HitterDetails[];

}

export interface PlayerDetails {
  savedName: string;
  uniformNumber: string;
  positionType: PositionType;
  position: string;
  overall: string;
  batsAndThrows: string;
}

export interface HitterDetails extends PlayerDetails {
  trajectory: number;
  contact: number;
  power: number;
  runSpeed: number;
  armStrength: number;
  fielding: number;
  errorResistance: number;
}

export interface PitcherDetails extends PlayerDetails {
  topSpeed: number;
  control: number;
  stamina: number;
  breakingBall1: string;
  breakingBall2: string;
  breakingBall3: string;
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, rosterName, teams } = props;
  
  return <PowerUpLayout headerText='Edit Roster'>
    <HangingHeader>
      <Breadcrumbs>
        <Crumb key='Home' onClick={returnHome}>Home</Crumb>
        <Crumb key='RosterEditor'>{rosterName}</Crumb>
      </Breadcrumbs>
      <RosterHeader>{rosterName}</RosterHeader>
    </HangingHeader>
    {teams.map(t => <div key={t.name}>{t.name}</div>)}
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