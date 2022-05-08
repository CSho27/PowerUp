import { useRef } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { CenteringWrapper } from "../../components/centeringWrapper/cetneringWrapper";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { Icon } from "../../components/icon/icon";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { COLORS } from "../../style/constants";
import { AppContext } from "../app";
import { PlayerSelectionGridPlayer } from "../playerSelectionModal/playerSelectionGrid";
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
  canManageRoster: boolean;
  canEditRoles: boolean;
  canSendUpOrDown: boolean;
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
    canManageRoster,
    canEditRoles,
    canSendUpOrDown,
    updatePlayer,
    sendUpOrDown,
    addPlayer,
    saveTempTeam
  } = props;

  const copyingApiClientRef = useRef(new CopyPlayerApiClient(appContext.commandFetcher));
  const creationApiClientRef = useRef(new CreatePlayerApiClient(appContext.commandFetcher));
  const detailsApiClientRef = useRef(new GetPlayerDetailsApiClient(appContext.commandFetcher));

  const allPlayers = [...mlbPlayers, ...aaaPlayers];
  const thisGridPlayers = isAAA 
    ? aaaPlayers.sort((p1, p2) => positionCompare(p1.playerDetails.position, p2.playerDetails.position))
    : mlbPlayers.sort((p1, p2) => positionCompare(p1.playerDetails.position, p2.playerDetails.position));

  const canAddMorePlayers = isAAA 
    ? allPlayers.length < 40
    : mlbPlayers.length < 25;
  const cannotAddMorePlayersMessage = !canAddMorePlayers
    ? isAAA
      ? 'Cannot add player. Roster already has 40 people on it'
      : 'Cannot add player. Roster already has 25 people on it'
    : undefined

  return <Wrapper>
    <PlayerGroupHeader>
      <h2>{isAAA ? 'AAA' : 'MLB'}</h2>
      <ContextMenuButton
        size='Small'
        variant='Outline'
        title={cannotAddMorePlayersMessage ?? 'Add Player'}
        icon='plus'
        squarePadding
        disabled={!canAddMorePlayers}
        menuItems={<>
          <ContextMenuItem 
              icon='box-archive'
              disabled={!canManageRoster}
              onClick={addExistingPlayer}>
                Add existing
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
          title='Replace'
          icon='right-left'
          squarePadding
          menuItems={<>
            <ContextMenuItem 
              icon='copy'
              disabled={!canManageRoster}
              onClick={() => replacePlayerWithCopy(playerId)}>
                Replace with copy
            </ContextMenuItem>
            <ContextMenuItem 
              icon='box-archive'
              disabled={!canManageRoster}
              onClick={() => replacePlayerWithExisting(playerId)}>
                Replace with existing
            </ContextMenuItem>
            <ContextMenuItem 
              icon='user-plus'
              disabled={!canManageRoster}
              onClick={() => replaceWithNewPlayer(playerId, playerDetails.position === 'Pitcher')}>
                Replace with new
            </ContextMenuItem>
          </>} />
      </PlayerCell>
      <PlayerCell>
        <Button
          size='Small'
          variant='Outline'
          disabled={!canSendUpOrDown}
          title={isAAA ? 'Call up' : 'Send down'}
          icon={isAAA ? 'person-arrow-up-from-line' : 'person-arrow-down-to-line'}
          squarePadding
          onClick={() => sendUpOrDown(playerId)} />
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
            sourceType={playerDetails.sourceType}
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
            disabled={positionType === 'Pitcher' || !canEditRoles}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsPinchHitter' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isPinchRunner} 
            disabled={positionType === 'Pitcher' || !canEditRoles}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsPinchRunner' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isDefensiveReplacement} 
            disabled={positionType === 'Pitcher' || !canEditRoles}
            onToggle={() => updatePlayer(playerId, { type: 'toggleIsDefensiveReplacement' })} />
        </CenteringWrapper>
      </PlayerCell>
      <PlayerCell>
        <CenteringWrapper>
          <CheckboxField 
            checked={player.isDefensiveLiability} 
            disabled={positionType === 'Pitcher' || !canEditRoles}
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

  function isPlayerDisabled(player: PlayerSelectionGridPlayer): DisableResult {
    const isDisabled = allPlayers.some(p => p.playerDetails.playerId === player.playerId);
    return {
      disabled: isDisabled,
      message: isDisabled 
        ? 'Player is already on selected team'
        : undefined
    }
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

const PlayerRow = styled.tr`
  &:nth-child(even) {
    background-color: ${COLORS.jet.superlight_85};
  } 
`

const PlayerCell = styled.td`
  white-space: nowrap;
`
