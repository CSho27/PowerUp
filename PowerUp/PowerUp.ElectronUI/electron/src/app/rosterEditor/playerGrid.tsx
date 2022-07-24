import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { CenteringWrapper } from "../../components/centeringWrapper/cetneringWrapper";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { FlyoutAnchor } from "../../components/flyout/flyout";
import { Icon } from "../../components/icon/icon";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { distinctBy } from "../../utils/arrayUtils";
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { AppContext } from "../app";
import { PlayerGenerationModal } from "../playerGenerationModal/playerGenerationModal";
import { PlayerSearchResultDto } from "../playerSelectionModal/playerSearchApiClient";
import { PlayerSelectionModal } from "../playerSelectionModal/playerSelectionModal";
import { DisableResult } from "../shared/disableResult";
import { getPositionType, positionCompare } from "../shared/positionCode";
import { ReplacePlayerWithCopyApiClient } from "./replacePlayerWithCopyApiClient";
import { ReplaceWithExistingPlayerApiClient } from "./replaceWithExistingPlayerApiClient";
import { ReplaceWithNewPlayerApiClient } from "./replaceWithNewPlayerApiClient";
import { HitterDetails, PitcherDetails, PlayerDetails } from "./rosterEditorDTOs";

export interface PlayerGridProps {
  appContext: AppContext;
  rosterId: number;
  teamId: number | null;
  hitters: HitterDetails[];
  pitchers: PitcherDetails[];
  disableManagement: DisabledCriteria;
}

export function PlayerGrid(props: PlayerGridProps) {
  const { appContext, rosterId, teamId, pitchers, disableManagement } = props;
  const hitters = props.hitters.slice().sort((p1, p2) => positionCompare(p1.position, p2.position));

  const [warningsOpenPlayerId, setWarningsOpenPlayerId] = useState<number|null>(null);

  const replacePlayerWithCopyApiClientRef = useRef(new ReplacePlayerWithCopyApiClient(appContext.commandFetcher));
  const replacePlayerWithExistingApiClientRef = useRef(new ReplaceWithExistingPlayerApiClient(appContext.commandFetcher));
  const replaceWithNewApiClientRef = useRef(new ReplaceWithNewPlayerApiClient(appContext.commandFetcher));
  const hasTeamHeader = !!teamId;

  return <>
    <thead>
    <tr>
      <PlayerGroupHeader hasTeamHeader={hasTeamHeader} colSpan={'100%' as any}>
        <PlayerGroupH3>
          Hitters
        </PlayerGroupH3>
      </PlayerGroupHeader>
    </tr>
    <tr>
      {getPlayerDetailsHeaders()}
      <StatHeader hasTeamHeader={hasTeamHeader}>Trj</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Con</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Pwr</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Run</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}><CenteringWrapper>Arm</CenteringWrapper></StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Fld</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>E-Res</StatHeader>
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
      <PlayerGroupHeader hasTeamHeader={!!teamId} colSpan={'100%' as any}>
        <PlayerGroupH3>
          Pitchers
        </PlayerGroupH3>
      </PlayerGroupHeader>
    </tr>
    <tr>
      {getPlayerDetailsHeaders()}
      <StatHeader hasTeamHeader={hasTeamHeader}>Type</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Top Spd</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Ctrl</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Stam</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='64px'>Brk 1</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='64px'>Brk 2</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='64px'>Brk 3</StatHeader>
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
  </>

  function getPlayerDetailsHeaders() {
    return <>
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='1px' />
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='1px' />
      <IconHeader hasTeamHeader={hasTeamHeader}><Icon icon='asterisk' /></IconHeader>
      <IconHeader hasTeamHeader={hasTeamHeader}><Icon icon='triangle-exclamation' /></IconHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Num</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Pos</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader} columnWidth='100px' style={{ textAlign: 'left' }}>Name</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>Ovr</StatHeader>
      <StatHeader hasTeamHeader={hasTeamHeader}>B/T</StatHeader>
    </>
  }

  function getPlayerDetailsColumns(details: PlayerDetails) {
    const positionType = getPositionType(details.position)
    const { playerId } = details;
    const warnings = distinctBy(details.generatedPlayer_Warnings, w => w.errorKey);

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
          {...toDisabledProps('Replace player', ...disableManagement)}
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
        {details.generatedPlayer_IsUnedited &&
          <Icon icon='asterisk' title='Generated player has not yet been edited' />}
      </PlayerCell>
      <PlayerCell>
        {details.generatedPlayer_Warnings.length > 0 &&
          <FlyoutAnchor
            isOpen={warningsOpenPlayerId == playerId}
            onCloseTrigger={() => setWarningsOpenPlayerId(null)}
            onOpenTrigger={() => setWarningsOpenPlayerId(playerId)}
            flyout={<PlayerMessageFlyout>
              {warnings.map(w => <div key={w.errorKey}>{w.message}</div>)}
            </PlayerMessageFlyout>}>
            <Icon icon='triangle-exclamation' style={{ color: COLORS.attentionYellow.regular_45 }} />
          </FlyoutAnchor>}
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
    const response = await replacePlayerWithCopyApiClientRef.current.execute({ teamId: teamId!, playerId: playerId });
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
      teamId: teamId!, 
      playerToReplaceId: playerToReplaceId,
      playerToInsertId: playerToInsertId
    });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  async function replaceWithNewPlayer(playerId: number) {
    const response = await replaceWithNewApiClientRef.current.execute({ teamId: teamId!, playerToReplaceId: playerId });
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

const PlayerGroupHeader = styled.th<{ hasTeamHeader: boolean }>`
  padding: 0px 16px;
  background-color: ${COLORS.jet.light_39};
  color: ${COLORS.white.regular_100};
  position: sticky;
  top: ${p => p.hasTeamHeader ? '64px' : '0px'};
  height: 24px;
`


const PlayerGroupH3 = styled.h3`
  text-align: left;
  font-style: italic;
  font-weight: 600;
`

const IconHeader = styled.th<{ hasTeamHeader: boolean }>`
  background-color: ${COLORS.jet.lighter_71};
  position: sticky;
  top: ${p => p.hasTeamHeader ? '88px' : '24px'};
  height: 24px;
  width: 1px;
  padding: 0 8px;
`

const StatHeader = styled.th<{ hasTeamHeader: boolean, columnWidth?: string }>`
  background-color: ${COLORS.jet.lighter_71};
  font-style: italic;
  position: sticky;
  top: ${p => p.hasTeamHeader ? '88px' : '24px'};
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

const PlayerMessageFlyout = styled.div`
  background-color: ${COLORS.white.regular_100};
  padding: 8px;
  font-size: ${FONT_SIZES._14};
`