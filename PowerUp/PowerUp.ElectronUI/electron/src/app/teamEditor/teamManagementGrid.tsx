import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { CenteringWrapper } from "../../components/centeringWrapper/cetneringWrapper";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { FlyoutAnchor } from "../../components/flyout/flyout";
import { Icon } from "../../components/icon/icon";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { distinctBy } from "../../utils/arrayUtils";
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { AppContext } from "../app";
import { PlayerGenerationApiClient } from "../playerGenerationModal/playerGenerationApiClient";
import { PlayerGenerationModal } from "../playerGenerationModal/playerGenerationModal";
import { PlayerSearchResultDto } from "../playerSelectionModal/playerSearchApiClient";
import { PlayerSelectionModal } from "../playerSelectionModal/playerSelectionModal";
import { DisableResult } from "../shared/disableResult";
import { GetPlayerDetailsApiClient } from "../shared/getPlayerDetailsApiClient";
import { ListDispatch } from "../shared/listDispatch";
import { getPositionType, positionCompare } from "../shared/positionCode";
import { CopyPlayerApiClient } from "./copyPlayerApiClient";
import { CreatePlayerApiClient } from "./createPlayerApiClient";
import { toPlayerDetails } from "./playerDetailsResponse";
import { PlayerDetails, PlayerRoleAction, PlayerRoleState } from "./playerRoleState";

export interface TeamManagementGridProps {
  appContext: AppContext;
  isAAA: boolean;
  mlbPlayers: PlayerRoleState[];
  aaaPlayers: PlayerRoleState[];
  startingNumber: number;
  disableManageRoster: DisabledCriteria;
  disableEditRoles: DisabledCriteria;
  disableSendUpOrDown: DisabledCriteria;
  updatePlayer: ListDispatch<number, PlayerRoleAction>;
  sendUpOrDown: (playerId: number) => void;
  addPlayer: (details: PlayerDetails) => void;
  saveTempTeam: () => void;
}

export function TeamManagementGrid(props: TeamManagementGridProps) {
  const { 
    appContext, 
    isAAA,
    mlbPlayers,
    aaaPlayers,
    startingNumber,
    disableManageRoster,
    disableEditRoles,
    disableSendUpOrDown,
    updatePlayer,
    sendUpOrDown,
    addPlayer,
    saveTempTeam
  } = props;

  const [warningsOpenPlayerId, setWarningsOpenPlayerId] = useState<number|null>(null);

  const copyingApiClientRef = useRef(new CopyPlayerApiClient(appContext.commandFetcher));
  const creationApiClientRef = useRef(new CreatePlayerApiClient(appContext.commandFetcher));
  
  const detailsApiClientRef = useRef(new GetPlayerDetailsApiClient(appContext.commandFetcher));

  const allPlayers = [...mlbPlayers, ...aaaPlayers];
  const thisGridPlayers = isAAA 
    ? aaaPlayers.slice().sort((p1, p2) => positionCompare(p1.playerDetails.position, p2.playerDetails.position))
    : mlbPlayers.slice().sort((p1, p2) => positionCompare(p1.playerDetails.position, p2.playerDetails.position));

  const addDisabled: DisabledCriteria = [
    ...disableManageRoster,
    { isDisabled: isAAA && allPlayers.length >= 40, tooltipIfDisabled: 'Cannot add player. Roster already has 40 people on it' },
    { isDisabled: !isAAA && mlbPlayers.length >= 25, tooltipIfDisabled: 'Cannot add player. Roster already has 25 people on it' }
  ]

  return <Wrapper>
    <PlayerGroupHeader>
      <h2>{isAAA ? 'AAA' : 'MLB'}</h2>
      <ContextMenuButton
        size='Small'
        variant='Outline'
        {...toDisabledProps('Add Player', ...addDisabled)}
        icon='plus'
        squarePadding
        menuItems={<>
          <ContextMenuItem 
            icon='box-archive'
            onClick={addExistingPlayer}>
              Add existing
          </ContextMenuItem>
          <ContextMenuItem 
            icon='wand-magic-sparkles'
            onClick={addGeneratedPlayer}>
              Add generated player
          </ContextMenuItem>
          <ContextMenuItem 
            icon='user-plus'
            onClick={() => addNewPlayer(false)}>
              Add new hitter
          </ContextMenuItem>
          <ContextMenuItem 
            icon='user-plus'
            onClick={() => addNewPlayer(true)}>
              Add new pitcher
          </ContextMenuItem>
        </>}
      />
    </PlayerGroupHeader>
    <TeamManagementTable>
      <thead>
        <tr>
          <StatHeader columnWidth='1px'>Slot</StatHeader>
          <StatHeader columnWidth='1px' />
          <StatHeader columnWidth='1px' />
          <StatHeader columnWidth='1px' />
          <IconHeader><Icon icon='asterisk'/></IconHeader>
          <IconHeader><Icon icon='triangle-exclamation'/></IconHeader>
          <StatHeader>Pos</StatHeader>
          <StatHeader columnWidth='100px' style={{ textAlign: 'left' }}>Name</StatHeader>
          <StatHeader>Ovr</StatHeader>
          <StatHeader>B/T</StatHeader>
          <StatHeader columnWidth='5px'>Pinch Hitter</StatHeader>
          <StatHeader columnWidth='5px'>Pinch Runner</StatHeader>
          <StatHeader columnWidth='5px'>Def Sub In</StatHeader>
          <StatHeader columnWidth='5px'>Def Sub Out</StatHeader>
        </tr>
      </thead>
      <PlayerTableBody>
        {thisGridPlayers.map(mapToPlayerRow)}
      </PlayerTableBody>
    </TeamManagementTable>
  </Wrapper>

  function mapToPlayerRow(player: PlayerRoleState, index: number) {
    const { playerDetails } = player;
    const { playerId } = playerDetails
    const positionType = getPositionType(playerDetails.position);

    const warnings = distinctBy(playerDetails.generatedPlayer_Warnings, w => w.errorKey);

    const pitcherRolesDisabled: DisabledCriteria = [
      ...disableEditRoles,
      { isDisabled: positionType === 'Pitcher', tooltipIfDisabled: "Pitcher's team roles cannot be edited" }
    ]

    const disableSendUpOrDownWithLineup: DisabledCriteria = [
      ...disableSendUpOrDown,
      { isDisabled: !!player.orderInNoDHLineup, tooltipIfDisabled: "Player may not be sent down because they are currently in the No DH Lineup" },
      { isDisabled: !!player.orderInDHLineup, tooltipIfDisabled: "Player may not be sent down because they are currently in the DH Lineup" }
    ]

    return <PlayerRow key={playerId}>
      <PlayerCell>
        {startingNumber+index}
      </PlayerCell>
      <PlayerCell>
        <Button
          size='Small'
          variant='Outline'
          title={playerDetails.canEdit
            ? 'Edit'
            : 'View'}
          icon={playerDetails.canEdit
            ? 'user-pen'
            : 'eye'}
          squarePadding
          onClick={() => editPlayer(playerId)} />
      </PlayerCell>
      <PlayerCell>
        <ContextMenuButton
          size='Small'
          variant='Outline'
          icon='right-left'
          {...toDisabledProps('Replace player', ...disableManageRoster)}
          squarePadding
          menuItems={<>
            <ContextMenuItem 
              icon='copy'
              {...toDisabledProps('Make editable copy of player', ...disableManageRoster)}
              onClick={() => replacePlayerWithCopy(playerId)}>
                Replace with copy
            </ContextMenuItem>
            <ContextMenuItem 
              icon='box-archive'
              {...toDisabledProps('Replace with an existing player from the database', ...disableManageRoster)}
              onClick={() => replacePlayerWithExisting(playerId)}>
                Replace with existing
            </ContextMenuItem>
            <ContextMenuItem 
              icon='wand-magic-sparkles'
              {...toDisabledProps('Replace with a new generated player', ...disableManageRoster)}
              onClick={() => replaceWithGeneratedPlayer(playerId)}>
                Replace with generated player
            </ContextMenuItem>
            <ContextMenuItem 
              icon='user-plus'
              {...toDisabledProps('Replace with a new default player', ...disableManageRoster)}
              onClick={() => replaceWithNewPlayer(playerId, playerDetails.position === 'Pitcher')}>
                Replace with new
            </ContextMenuItem>
          </>} />
      </PlayerCell>
      <PlayerCell>
        <Button
          size='Small'
          variant='Outline'
          {...toDisabledProps(isAAA ? 'Call up' : 'Send down', ...disableSendUpOrDownWithLineup)}
          icon={isAAA ? 'person-arrow-up-from-line' : 'person-arrow-down-to-line'}
          squarePadding
          onClick={() => sendUpOrDown(playerId)} />
      </PlayerCell>
      <PlayerCell>
        {playerDetails.generatedPlayer_IsUnedited && 
        <Icon icon='asterisk' title='Generated player has not yet been edited' />}
      </PlayerCell>
      <PlayerCell>
        {warnings.length > 0 && 
        <FlyoutAnchor
          isOpen={warningsOpenPlayerId == playerId}
          onCloseTrigger={() => setWarningsOpenPlayerId(null)}
          onOpenTrigger={() => setWarningsOpenPlayerId(playerId)}
          flyout={<PlayerGenerationWarningsFlyout>
            {warnings.map(w => <div key={w.errorKey}>{w.message}</div>)}
          </PlayerGenerationWarningsFlyout>}>
            <Icon icon='triangle-exclamation' style={{ color: COLORS.attentionYellow.regular_45 }} />
        </FlyoutAnchor>
        }
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
            <PositionBubble 
              positionType={positionType} 
              size='Medium' 
              squarePadding>
                {playerDetails.positionAbbreviation}
            </PositionBubble>
          </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <PlayerNameBubble 
            appContext={appContext}
            sourceType={playerDetails.sourceType}
            playerId={playerDetails.playerId}
            positionType={positionType} 
            size='Medium'
            fullWidth
            title={playerDetails.fullName}>
              {playerDetails.savedName}
          </PlayerNameBubble>
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>{playerDetails.overall}</PlayerCell>
      <PlayerCell>{playerDetails.batsAndThrows}</PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isPinchHitter} 
            {...toDisabledProps('Is pinch hitter', ...pitcherRolesDisabled)}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsPinchHitter' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isPinchRunner} 
            {...toDisabledProps('Is pinch runner', ...pitcherRolesDisabled)}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsPinchRunner' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isDefensiveReplacement} 
            {...toDisabledProps('Is defensive replacement', ...pitcherRolesDisabled)}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsDefensiveReplacement' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isDefensiveLiability} 
            {...toDisabledProps('Is defensive liability', ...pitcherRolesDisabled)}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsDefensiveLiability' })} />
        </CenteringWrapper>
      </PlayerCell>
    </PlayerRow>
  }

  function editPlayer(playerId: number) {
    saveTempTeam();
    appContext.setPage({ page: 'PlayerEditorPage', playerId: playerId });
  }

  async function replacePlayerWithCopy(playerId: number) {
    const response = await copyingApiClientRef.current.execute({ playerId: playerId });
    updatePlayer(playerId, { type: 'replacePlayer', playerDetails: toPlayerDetails(response) });
  }

  function replacePlayerWithExisting(playerToReplaceId: number) {
    appContext.openModal(closeDialog => <PlayerSelectionModal 
      appContext={appContext} 
      isPlayerDisabled={isPlayerDisabled}
      closeDialog={async playerToInsertId => { 
        closeDialog(); 
        if(!!playerToInsertId) {
          const details = await detailsApiClientRef.current.execute({ playerId: playerToInsertId })
          updatePlayer(playerToReplaceId, { type: 'replacePlayer', playerDetails: details })
        }
      }} 
    />)
  }

  function isPlayerDisabled(player: PlayerSearchResultDto): DisableResult {
    const isDisabled = allPlayers.some(p => p.playerDetails.playerId === player.playerId);
    return {
      disabled: isDisabled,
      message: isDisabled 
        ? 'Player is already on selected team'
        : undefined
    }
  }

  async function replaceWithGeneratedPlayer(playerId: number) {
    appContext.openModal(closeDialog => <PlayerGenerationModal
      appContext={appContext}
      closeDialog={async generatedPlayerId => {
        closeDialog();
        if(!!generatedPlayerId) {
          const details = await detailsApiClientRef.current.execute({ playerId: generatedPlayerId })
          updatePlayer(playerId, { type: 'replacePlayer', playerDetails: details })
        }
      }}
    />);
  }

  async function replaceWithNewPlayer(playerId: number, isPitcher: boolean) {
    const response = await creationApiClientRef.current.execute({ isPitcher: isPitcher });
    updatePlayer(playerId, { type: 'replacePlayer', playerDetails: toPlayerDetails(response) });
  }

  async function addExistingPlayer() {
    appContext.openModal(closeDialog => <PlayerSelectionModal 
      appContext={appContext} 
      isPlayerDisabled={isPlayerDisabled}
      closeDialog={async playerToInsertId => { 
        closeDialog(); 
        if(!!playerToInsertId) {
          const details = await detailsApiClientRef.current.execute({ playerId: playerToInsertId })
          addPlayer(details);
        }
      }} 
    />)
  }

  async function addGeneratedPlayer() {
    appContext.openModal(closeDialog => <PlayerGenerationModal
      appContext={appContext}
      closeDialog={async generatedPlayerId => {
        closeDialog();
        if(!!generatedPlayerId) {
          const details = await detailsApiClientRef.current.execute({ playerId: generatedPlayerId })
          addPlayer(details);
        }
      }}
    />);
  }

  async function addNewPlayer(isPitcher: boolean) {
    const response = await creationApiClientRef.current.execute({ isPitcher: isPitcher });
    addPlayer(toPlayerDetails(response));
  }
}

const Wrapper = styled.div`
  padding-bottom: 16px;
`

const PlayerGroupHeader = styled.div`
  display: flex;
  align-items: center;
  gap: 16px;
`

const TeamManagementTable = styled.table`
  width: 100%;
  border-collapse: collapse;
`

const PlayerTableBody = styled.tbody`
  text-align: center;
`

const StatHeader = styled.th<{ columnWidth?: string }>`
  background-color: ${COLORS.jet.lighter_71};
  font-style: italic;
  width: ${p => p.columnWidth ?? '32px'};
  white-space: nowrap;
`

const IconHeader = styled.th`
  background-color: ${COLORS.jet.lighter_71};
  width: 1px;
  padding: 0 8px;
`

const PlayerRow = styled.tr`
  &:nth-child(even) {
    background-color: ${COLORS.jet.superlight_85};
  } 
`

const PlayerCell = styled.td`
  white-space: nowrap;
`

const PlayerGenerationWarningsFlyout = styled.div`
  background-color: ${COLORS.white.regular_100};
  padding: 8px;
  font-size: ${FONT_SIZES._14};
`