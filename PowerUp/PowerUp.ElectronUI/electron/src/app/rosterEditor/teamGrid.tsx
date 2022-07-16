import { useRef } from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { CenteringWrapper } from "../../components/centeringWrapper/cetneringWrapper";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { SourceTypeStamp } from "../../components/sourceTypeStamp/sourceTypeStamp";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants"
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { toIdentifier } from "../../utils/getIdentifier";
import { AppContext } from "../app"
import { PlayerGenerationApiClient } from "../playerGenerationModal/playerGenerationApiClient";
import { PlayerGenerationModal } from "../playerGenerationModal/playerGenerationModal";
import { PlayerSearchResultDto } from "../playerSelectionModal/playerSearchApiClient";
import { PlayerSelectionModal } from "../playerSelectionModal/playerSelectionModal";
import { DisableResult } from "../shared/disableResult";
import { getPositionType } from "../shared/positionCode";
import { TeamSelectionModal } from "../teamSelectionModal/teamSelectionModal";
import { ReplacePlayerWithCopyApiClient } from "./replacePlayerWithCopyApiClient";
import { ReplaceTeamWithCopyApiClient } from "./replaceTeamWithCopyApiClient";
import { ReplaceTeamWithExistingApiClient } from "./replaceTeamWithExistingApiClient";
import { ReplaceTeamWithNewTeamApiClient } from "./replaceTeamWithNewTeamApiClient";
import { ReplaceWithExistingPlayerApiClient } from "./replaceWithExistingPlayerApiClient";
import { ReplaceWithNewPlayerApiClient } from "./replaceWithNewPlayerApiClient";
import { PlayerDetails, TeamDetails } from "./rosterEditorDTOs";

interface TeamGridProps {
  appContext: AppContext;
  rosterId: number;
  disableRosterEdit: DisabledCriteria;
  team: TeamDetails
}

export function TeamGrid(props: TeamGridProps) {
  const { appContext, rosterId, disableRosterEdit, team } = props;
  const { name, powerProsName, hitters, pitchers } = team;

  const replaceTeamWithCopyApiClientRef = useRef(new ReplaceTeamWithCopyApiClient(appContext.commandFetcher));
  const replaceTeamWithExistingApiClientRef = useRef(new ReplaceTeamWithExistingApiClient(appContext.commandFetcher));
  const replaceTeamWithNewApiClientRef = useRef(new ReplaceTeamWithNewTeamApiClient(appContext.commandFetcher));

  const replacePlayerWithCopyApiClientRef = useRef(new ReplacePlayerWithCopyApiClient(appContext.commandFetcher));
  const replacePlayerWithExistingApiClientRef = useRef(new ReplaceWithExistingPlayerApiClient(appContext.commandFetcher));
  const replaceWithNewApiClientRef = useRef(new ReplaceWithNewPlayerApiClient(appContext.commandFetcher));

  const generatePlayerApiClientRef = useRef(new PlayerGenerationApiClient(appContext.commandFetcher));

  const teamIdentifier = toIdentifier('Team', team.teamId);
  const teamDisplayName = name === powerProsName
      ? `${name} - ${teamIdentifier}`
      : `${name} - ${teamIdentifier} (${powerProsName})` 

  const disableManageTeam: DisabledCriteria = [
    { isDisabled: !team.canEdit, tooltipIfDisabled: 'Teams of this type cannot be edited' }
  ]

  return <TeamGridTable>
    <TeamGridCaption>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end' }}>
        <div style={{ flex: 'auto', display: 'flex', gap: '16px', alignItems: 'center' }}>
          <TeamHeader>
            {teamDisplayName}
          </TeamHeader>
          <SourceTypeStamp 
            theme='Light'
            size='Medium'
            sourceType={team.sourceType}
          />
          <TeamStatGrid>
            <TeamStatRow>
              <TeamStatLabel>Hitting</TeamStatLabel>
              <TeamStatValue>{team.hittingRating}</TeamStatValue>
            </TeamStatRow>
            <TeamStatRow>
              <TeamStatLabel>Pitching</TeamStatLabel>
              <TeamStatValue>{team.pitchingRating}</TeamStatValue>
            </TeamStatRow>
            <TeamStatRow>
              <TeamStatLabel>Overall</TeamStatLabel>
              <TeamStatValue>{team.overallRating}</TeamStatValue>
            </TeamStatRow>            
          </TeamStatGrid>
        </div>
        <Button 
          size='Medium'
          variant='Outline'
          squarePadding
          onClick={editTeam}
          title={team.canEdit
            ? 'Edit team'
            : 'View team'}
          icon={team.canEdit
            ? 'pen-to-square'
            : 'eye'}/>
        <ContextMenuButton
          size='Medium'
          variant='Outline'
          squarePadding
          icon='right-left'
          {...toDisabledProps('Replace team', ...disableRosterEdit)}
          menuItems={<>
            <ContextMenuItem 
              icon='copy'
              onClick={replaceTeamWithCopy}>
                Replace with copy
            </ContextMenuItem>
            <ContextMenuItem 
              icon='box-archive'
              onClick={replaceTeamWithExisting}>
                Replace with existing
            </ContextMenuItem>
            <ContextMenuItem 
              icon='circle-plus'
              onClick={replaceWithNewTeam}>
                Replace with new
            </ContextMenuItem>
          </>}/>
      </div>
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
            ? 'Edit player'
            : 'View player'}
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
          icon='right-left'
          {...toDisabledProps('Replace player', ...disableManageTeam)}
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
              icon='wand-magic-sparkles'
              onClick={() => replaceWithGeneratedPlayer(playerId)}>
                Replace with generated
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
            squarePadding>
              {details.positionAbbreviation}
          </PositionBubble>
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <PlayerNameBubble 
            appContext={appContext}
            sourceType={details.sourceType}
            playerId={details.playerId}
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
      isPlayerDisabled={isPlayerDisabled}
      closeDialog={playerToInsertId => { 
        closeDialog(); 
        if(!!playerToInsertId)
          executeReplacePlayer(playerToReplaceId, playerToInsertId); 
      }} 
    />)
  }

  function replaceWithGeneratedPlayer(playerToReplaceId: number) {
    appContext.openModal(closeDialog => <PlayerGenerationModal
      appContext={appContext}
      closeDialog={async generatedPlayerId => {
        closeDialog();
        if(!!generatedPlayerId) {
          executeReplacePlayer(playerToReplaceId, generatedPlayerId); 
        }
      }}
    />);
  }

  async function executeReplacePlayer(playerToReplaceId: number, playerToInsertId: number) {
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

  async function editTeam() {
    appContext.setPage({ page: 'TeamEditorPage', teamId: team.teamId });
  }

  async function replaceTeamWithCopy() {
    const response = await replaceTeamWithCopyApiClientRef.current.execute({ rosterId: rosterId, mlbPPTeam: team.powerProsTeam });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  async function replaceWithNewTeam() {
    const response = await replaceTeamWithNewApiClientRef.current.execute({ rosterId: rosterId, mlbPPTeam: team.powerProsTeam });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  function replaceTeamWithExisting(): void {
    appContext.openModal(closeDialog => <TeamSelectionModal 
      appContext={appContext} 
      closeDialog={teamToInsertId => { 
        closeDialog(); 
        if(!!teamToInsertId)
          executeReplaceTeam(teamToInsertId);
      }} 
    />)
  }

  async function executeReplaceTeam(teamToInsertId: number) {
    const response = await replaceTeamWithExistingApiClientRef.current.execute({ 
      rosterId: rosterId,
      mlbPPTeamToReplace: team.powerProsTeam,
      teamToInsertId: teamToInsertId
    });
    if(response.success)
      appContext.reloadCurrentPage();
  }


  function isPlayerDisabled(player: PlayerSearchResultDto): DisableResult {
    const isDisabled = hitters.some(h => h.playerId === player.playerId) 
      || pitchers.some(p => p.playerId === player.playerId);

    return {
      disabled: isDisabled,
      message: isDisabled
        ? 'Player is already on this team'
        : undefined
    }

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
  position: sticky;
  top: 0;
  height: 64px;
  z-index: 1;
  padding: 8px 16px;
`

const TeamHeader = styled.h1`
  font-size: ${FONT_SIZES._32};
  font-weight: 600;
  font-style: italic;
  text-align: left;
`

const TeamStatGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 0px 16px;
`

const TeamStatRow = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  gap: 8px;
`

const TeamStatLabel = styled.div`
  font-weight: 600;
  font-style: italic;
  text-align: left;
`

const TeamStatValue = styled.div`
  font-style: italic;
  text-align: right;
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
