import { useRef } from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants"
import { AppContext } from "../app"
import { PlayerSelectionModal } from "../playerSelectionModal/playerSelectionModal";
import { getPositionType } from "../shared/positionCode";
import { ReplacePlayerWithCopyApiClient } from "./replacePlayerWithCopyApiClient";
import { ReplaceWithExistingPlayerApiClient } from "./replaceWithExistingPlayerApiClient";
import { ReplaceWithNewPlayerApiClient } from "./replaceWithNewPlayerApiClient";
import { PlayerDetails, TeamDetails } from "./rosterEditorDTOs";

interface TeamGridProps {
  appContext: AppContext;
  team: TeamDetails
}

export function TeamGrid(props: TeamGridProps) {
  const { appContext, team } = props;
  const { name, powerProsName, hitters, pitchers } = team;

  const replacePlayerWithCopyApiClientRef = useRef(new ReplacePlayerWithCopyApiClient(appContext.commandFetcher));
  const replacePlayerWithExistingApiClientRef = useRef(new ReplaceWithExistingPlayerApiClient(appContext.commandFetcher));
  const replaceWithNewApiClientRef = useRef(new ReplaceWithNewPlayerApiClient(appContext.commandFetcher));

  const teamDisplayName = name === powerProsName
      ? name
      : `${name} (${powerProsName})` 

  return <TeamGridTable>
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
      <PlayerRow key={h.playerId}>
        {getPlayerDetailsColumns(h)}
        <PlayerCell>{h.trajectory}</PlayerCell>
        <PlayerCell>{h.contact}</PlayerCell>
        <PlayerCell>{h.power}</PlayerCell>
        <PlayerCell>{h.runSpeed}</PlayerCell>
        <PlayerCell>{h.armStrength}</PlayerCell>
        <PlayerCell>{h.fielding}</PlayerCell>
        <PlayerCell>{h.errorResistance}</PlayerCell>
      </PlayerRow>)}
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
        <StatHeader columnWidth='64px'>Brk 1</StatHeader>
        <StatHeader columnWidth='64px'>Brk 2</StatHeader>
        <StatHeader columnWidth='64px'>Brk 3</StatHeader>
      </tr>
    </thead>
    <PlayerTableBody>
    {pitchers.map(p => 
      <PlayerRow key={p.playerId}>
        {getPlayerDetailsColumns(p)}
        <PlayerCell>{p.pitcherType}</PlayerCell>
        <PlayerCell>{p.topSpeed} mph</PlayerCell>
        <PlayerCell>{p.control}</PlayerCell>
        <PlayerCell>{p.stamina}</PlayerCell>
        <PlayerCell>{p.breakingBall1}</PlayerCell>
        <PlayerCell>{p.breakingBall2}</PlayerCell>
        <PlayerCell>{p.breakingBall3}</PlayerCell>
      </PlayerRow>)}
    </PlayerTableBody>
  </TeamGridTable>;

  function getPlayerDetailsHeaders() {
    return <>
      <StatHeader columnWidth='1px' />
      <StatHeader columnWidth='1px' />
      <StatHeader>Num</StatHeader>
      <StatHeader>Pos</StatHeader>
      <StatHeader columnWidth='100px' style={{ textAlign: 'left' }}>Name</StatHeader>
      <StatHeader>Ovr</StatHeader>
      <StatHeader>B/T</StatHeader>
    </>
  }

  function getPlayerDetailsColumns(details: PlayerDetails) {
    const positionType = getPositionType(details.position)
    const { playerId } = details;

    return <>
      <PlayerCell>
        <Button
          size='Small'
          variant='Outline'
          title={details.canEdit
            ? 'Edit'
            : 'View'}
          icon={details.canEdit
            ? 'user-pen'
            : 'eye'}
          squarePadding
          onClick={() => editPlayer(playerId)}
        />
      </PlayerCell>
      <PlayerCell>
        <ContextMenuButton
          size='Small'
          variant='Outline'
          title='Replace'
          icon='right-left'
          squarePadding
          menuItems={<>
            <ContextMenuItem 
              icon='copy'
              onClick={() => replacePlayerWithCopy(playerId)}>
                Replace with copy
            </ContextMenuItem>
            <ContextMenuItem 
              icon='box-archive'
              onClick={() => replacePlayerWithExisting(playerId)}>
                Replace with existing
            </ContextMenuItem>
            <ContextMenuItem 
              icon='user-plus'
              onClick={() => replaceWithNewPlayer(playerId)}>
                Replace with new
            </ContextMenuItem>
          </>}
        />
      </PlayerCell>
      <PlayerCell>
        <OutlineHeader fontSize={FONT_SIZES._24} strokeWeight={1} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.white.regular_100}>
          {details.uniformNumber}
        </OutlineHeader>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <PositionBubble 
            positionType={positionType} 
            size='Medium' 
            squarePadding
          >
            {details.positionAbbreviation}
          </PositionBubble>
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <PlayerNameBubble 
            sourceType={details.sourceType}
            positionType={positionType} 
            size='Medium'
            fullWidth
          >
            {details.savedName}
          </PlayerNameBubble>
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>{details.overall}</PlayerCell>
      <PlayerCell>{details.batsAndThrows}</PlayerCell>
    </>
  }

  function editPlayer(playerId: number) {
    appContext.setPage({ page: 'PlayerEditorPage', playerId: playerId });
  }

  async function replacePlayerWithCopy(playerId: number) {
    const response = await replacePlayerWithCopyApiClientRef.current.execute({ teamId: team.teamId, playerId: playerId });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  function replacePlayerWithExisting(playerToReplaceId: number) {
    appContext.openModal(closeDialog => <PlayerSelectionModal 
      appContext={appContext} 
      closeDialog={playerToInsertId => { 
        closeDialog(); 
        if(!!playerToInsertId)
          executeReplace(playerToReplaceId, playerToInsertId); 
      }} 
    />)
  }

  async function executeReplace(playerToReplaceId: number, playerToInsertId: number) {
    const response = await replacePlayerWithExistingApiClientRef.current.execute({ 
      teamId: team.teamId, 
      playerToReplaceId: playerToReplaceId,
      playerToInsertId: playerToInsertId
    });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  async function replaceWithNewPlayer(playerId: number) {
    const response = await replaceWithNewApiClientRef.current.execute({ teamId: team.teamId, playerToReplaceId: playerId });
    if(response.success)
      appContext.reloadCurrentPage();
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

const StatHeader = styled.th<{ columnWidth?: string }>`
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

const PlayerRow = styled.tr`
  &:nth-child(even) {
    background-color: ${COLORS.jet.superlight_85};
  } 
`

const PlayerCell = styled.td`
  white-space: nowrap;
`

const CenteringWrapper = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`