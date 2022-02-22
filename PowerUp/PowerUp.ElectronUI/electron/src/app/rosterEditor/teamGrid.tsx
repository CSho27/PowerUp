import styled from "styled-components"
import { COLORS, FONT_SIZES } from "../../style/constants"
import { AppContext } from "../app"
import { TeamDetails } from "../home/importBaseRosterApiClient"

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
        <StatHeader>Num</StatHeader>
        <StatHeader>Pos</StatHeader>
        <StatHeader>Name</StatHeader>
        <StatHeader>Ovr</StatHeader>
        <StatHeader>B/T</StatHeader>
        <StatHeader>Trj</StatHeader>
        <StatHeader>Con</StatHeader>
        <StatHeader>Pwr</StatHeader>
        <StatHeader>Run</StatHeader>
        <StatHeader>Arm</StatHeader>
        <StatHeader>Fld</StatHeader>
        <StatHeader>E-Res</StatHeader>
      </tr>
    </thead>
    <PlayerTableBody>
    {hitters.map(h => 
      <tr key={h.savedName}>
        <td>{h.uniformNumber}</td>
        <td>{h.position}</td>
        <td>{h.savedName}</td>
        <td>{h.overall}</td>
        <td>{h.batsAndThrows}</td>
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
        <StatHeader>Num</StatHeader>
        <StatHeader>Pos</StatHeader>
        <StatHeader>Name</StatHeader>
        <StatHeader>Ovr</StatHeader>
        <StatHeader>B/T</StatHeader>
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
        <td>{p.uniformNumber}</td>
        <td>{p.position}</td>
        <td>{p.savedName}</td>
        <td>{p.overall}</td>
        <td>{p.batsAndThrows}</td>
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

const StatHeader = styled.th`
  background-color: ${COLORS.jet.lighter_71};
  font-style: italic;
  position: sticky;
  top: 88px;
  height: 24px;
`

const PlayerTableBody = styled.tbody`
  text-align: center;
`
