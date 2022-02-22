import styled from "styled-components"
import { PlayerName, Position, TextBubble } from "../../components/textBubble/textBubble";
import { COLORS, FONT_SIZES } from "../../style/constants"
import { AppContext } from "../app"
import { PlayerDetails, TeamDetails } from "../home/importBaseRosterApiClient"

interface TeamGridProps {
  appContext: AppContext;
  team: TeamDetails
}

export function TeamGrid(props: TeamGridProps) {
  const { appContext, team } = props;
  const { name, powerProsName, hitters, pitchers } = team;

  const teamDisplayName = name === powerProsName
      ? name
      : `${name} (${powerProsName})` 

  return <TeamGridTable key={powerProsName}>
    <TeamGridCaption>
      <TeamHeader>
        {teamDisplayName}
      </TeamHeader>
    </TeamGridCaption>
    <thead>
      <tr>
        <PlayerGroupHeader colSpan={'100%' as any}>
          <PlayerGroupH3>
            Hitters
          </PlayerGroupH3>
        </PlayerGroupHeader>
      </tr>
      <tr>
        {getPlayerDetailsHeaders()}
        <StatHeader>Trj</StatHeader>
        <StatHeader>Con</StatHeader>
        <StatHeader>Pwr</StatHeader>
        <StatHeader>Run</StatHeader>
        <StatHeader><CenteringWrapper>Arm</CenteringWrapper></StatHeader>
        <StatHeader>Fld</StatHeader>
        <StatHeader>E-Res</StatHeader>
      </tr>
    </thead>
    <PlayerTableBody>
    {hitters.map(h => 
      <tr key={h.savedName}>
        {getPlayerDetailsColumns(h)}
        <td>{h.trajectory}</td>
        <td>{h.contact}</td>
        <td>{h.power}</td>
        <td>{h.runSpeed}</td>
        <td>{h.armStrength}</td>
        <td>{h.fielding}</td>
        <td>{h.errorResistance}</td>
      </tr>)}
    </PlayerTableBody>
    <thead>
      <tr>
        <PlayerGroupHeader colSpan={'100%' as any}>
          <PlayerGroupH3>
            Pitchers
          </PlayerGroupH3>
        </PlayerGroupHeader>
      </tr>
      <tr>
        {getPlayerDetailsHeaders()}
        <StatHeader>Type</StatHeader>
        <StatHeader>Top Spd</StatHeader>
        <StatHeader>Ctrl</StatHeader>
        <StatHeader>Stam</StatHeader>
        <StatHeader>Brk 1</StatHeader>
        <StatHeader>Brk 2</StatHeader>
        <StatHeader>Brk 3</StatHeader>
      </tr>
    </thead>
    <PlayerTableBody>
    {pitchers.map(p => 
      <tr key={p.savedName}>
        {getPlayerDetailsColumns(p)}
        <td>{p.pitcherType}</td>
        <td>{p.topSpeed} mph</td>
        <td>{p.control}</td>
        <td>{p.stamina}</td>
        <td>{p.breakingBall1}</td>
        <td>{p.breakingBall2}</td>
        <td>{p.breakingBall3}</td>
      </tr>)}
    </PlayerTableBody>
  </TeamGridTable>;

  function getPlayerDetailsHeaders() {
    return <>
      <StatHeader>Num</StatHeader>
      <StatHeader>Pos</StatHeader>
      <StatHeader columnWidth='100px' style={{ textAlign: 'left' }}>Name</StatHeader>
      <StatHeader>Ovr</StatHeader>
      <StatHeader>B/T</StatHeader>
    </>
  }

  function getPlayerDetailsColumns(details: PlayerDetails) {
    return <>
      <td>{details.uniformNumber}</td>
      <td>
        <CenteringWrapper>
          <TextBubble positionType={details.positionType} height='32px' width='38px'>
            <Position fontSize={FONT_SIZES._24} >{details.position}</Position>
          </TextBubble>
        </CenteringWrapper>
      </td>
      <td>
        <CenteringWrapper>
          <TextBubble positionType={details.positionType} width='100%' style={{ textAlign: 'left' }}>
            <TeamGridPlayerName fontSize={FONT_SIZES._24}>{details.savedName}</TeamGridPlayerName>
          </TextBubble>
        </CenteringWrapper>
      </td>
      <td>{details.overall}</td>
      <td>{details.batsAndThrows}</td>
    </>
  }
}

const TeamGridTable = styled.table`
  width: 100%;
  border-collapse: collapse;
  isolation: isolate;
`

const TeamGridCaption = styled.caption`
  background-color: ${COLORS.primaryBlue.regular_45};
  color: ${COLORS.white.regular_100};
  text-align: left;
  position: sticky;
  top: 0;
  height: 64px;
  z-index: 1;
`

const TeamHeader = styled.h1`
  padding: 8px 16px;
  font-size: ${FONT_SIZES._32};
  font-weight: 600;
  font-style: italic;
`

const PlayerGroupHeader = styled.th`
  padding: 0px 16px;
  background-color: ${COLORS.jet.light_39};
  color: ${COLORS.white.regular_100};
  position: sticky;
  top: 64px;
  height: 24px;
`

const PlayerGroupH3 = styled.h3`
  text-align: left;
  font-style: italic;
  font-weight: 600;
`

const StatHeader = styled.th<{ columnWidth?: string | number }>`
  background-color: ${COLORS.jet.lighter_71};
  font-style: italic;
  position: sticky;
  top: 88px;
  height: 24px;
  width: ${p => p.columnWidth ?? '32px'};
  white-space: nowrap;
`

const PlayerTableBody = styled.tbody`
  text-align: center;
`

const CenteringWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`

const TeamGridPlayerName = styled(PlayerName)`
  padding: 2px 4px;
`
