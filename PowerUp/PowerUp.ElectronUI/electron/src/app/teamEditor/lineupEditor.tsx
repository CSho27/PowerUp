import React, { PropsWithChildren, useState } from "react";
import { DragDropContext, Draggable, Droppable, DropResult, ResponderProvided } from "react-beautiful-dnd";
import styled from "styled-components";
import { Icon } from "../../components/icon/icon";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { TextBubble } from "../../components/textBubble/textBubble";
import { COLORS } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { insert } from "../../utils/arrayUtils";
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { AppContext } from "../appContext";
import { EntitySourceType } from "../shared/entitySourceType";
import { getPositionAbbreviation, getPositionType, isPosition, Position, positionCompare } from "../shared/positionCode";

export interface LineupEditorProps {
  appContext: AppContext;
  players: LineupSlotDefinition[];
  useDh: boolean;
  disabled: DisabledCriteria;
  updateLineupOrder: (playerId: number | 'Pitcher', currentOrderInLineup: number, newOrderInLineup: number) => void;
  swapPositions: (position1: Position, position2: Position) => void;
  swapPlayers: (playerId1: number, playerId2: number) => void;
}

export interface LineupSlotDefinition {
  orderInLineup: number | undefined;
  position: Position | undefined;
  details: HitterDetails;
}

interface InternalLineupSlotDefinition {
  orderInLineup: number | undefined;
  position: Position | undefined;
  details: HitterDetails | undefined;
}

export interface HitterDetails {
  sourceType: EntitySourceType;
  playerId: number;
  savedName: string;
  fullName: string;
  position: Position;
}

interface DndContext {
  draggingElementId: string | null;
  setDraggingElementId: (elementId: string | null) => void;
}

export function LineupEditor(props: LineupEditorProps) {
  const { appContext, players, useDh, disabled, updateLineupOrder, swapPositions, swapPlayers } = props;

  const sortedPlayers = players.slice().sort(byOrder);
  const playersInLineup = sortedPlayers.filter(p => !!p.orderInLineup);
  const playersOnBench = sortedPlayers.filter(p => !p.orderInLineup).slice().sort((p1, p2) => positionCompare(p1.details.position, p2.details.position));
  const lineup = useDh
    ? playersInLineup
    : lineupWithPitcherSlot(playersInLineup);   

  const [draggingElementId, setDraggingElementId] = useState<string|null>(null);
  const dndContext: DndContext = {
    draggingElementId: draggingElementId,
    setDraggingElementId: setDraggingElementId
  }

  return <EditorWrapper>
      <DragDropContext onDragEnd={handleLineupReorderDragEnd}>
        <Droppable droppableId='starting-lineup'>
          {provided => 
          <PlayerGroupWrapper ref={provided.innerRef} {...provided.droppableProps}>
            <PlayerGroupHeading>Starting Lineup</PlayerGroupHeading>
              {lineup.map(toLineupSlot)}
              {provided.placeholder}
          </PlayerGroupWrapper>}
        </Droppable>
        <PlayerGroupWrapper>
          <PlayerGroupHeading>Bench</PlayerGroupHeading>
          {playersOnBench.map(toPlayerTile)}
        </PlayerGroupWrapper>
      </DragDropContext>
  </EditorWrapper>

  function toLineupSlot(slot: InternalLineupSlotDefinition, index: number) {
    return <PlayerRowWrapper key={slot.position === 'Pitcher' ? 'Pitcher' : slot.details!.playerId}>
      {slot.orderInLineup && <div>{slot.orderInLineup}.</div>}
      <SlotTile 
        appContext={appContext}
        index={index}
        details={slot.details} 
        position={slot.position!}
        dndContext={dndContext}
        disabled={disabled}
        swapPositions={swapPositions}
        swapPlayers={swapPlayers}
        canSwapPlayers={canSwapPlayers} />
    </PlayerRowWrapper>
  }

  function toPlayerTile(slot: LineupSlotDefinition) {
    return <PlayerTile 
      appContext={appContext}
      key={slot.details!.playerId} 
      details={slot.details!} 
      dndContext={dndContext}
      disabled={disabled}
      swapWithPlayer={other => swapPlayers(slot.details.playerId, other)}
      canSwap={other => canSwapPlayers(slot.details.playerId, other)}
    />
  }

  function byOrder(playerA: LineupSlotDefinition, playerB: LineupSlotDefinition): number {
    if(playerA.orderInLineup && playerB.orderInLineup)
      return playerA.orderInLineup - playerB.orderInLineup;
    if(playerA.orderInLineup && !playerB.orderInLineup)
      return -1;
    if(playerB.orderInLineup && !playerA.orderInLineup)
      return 1;
    else
      return 0;
  }

  function lineupWithPitcherSlot(playersInLineup: LineupSlotDefinition[]): InternalLineupSlotDefinition[] {
    let pitcherIndex = 0;
    playersInLineup.forEach((p, i) => {
      if(p.orderInLineup == i+1)
        pitcherIndex++;
    });

    return insert<InternalLineupSlotDefinition>(playersInLineup, { details: undefined, orderInLineup: pitcherIndex + 1, position: 'Pitcher' }, pitcherIndex);
  }

  function handleLineupReorderDragEnd(result: DropResult, provided: ResponderProvided) {
    if(!result.destination)
      return;
    
    const draggedPlayer: LineupSlotDefinition | undefined = players.find(p => p.details.playerId.toString() === result.draggableId);
    updateLineupOrder(draggedPlayer?.details?.playerId ?? 'Pitcher', result.source.index+1, result.destination.index+1);
  }

  function canSwapPlayers(playerId1: number, playerId2: number): boolean {
    const player1IsStarter = lineup.some(l => l.details?.playerId === playerId1);
    const player2IsStarter = lineup.some(l => l.details?.playerId === playerId2);

    return player1IsStarter || player2IsStarter;
  }
}

const EditorWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  max-width: 875px;
`

const PlayerGroupWrapper = styled.div`
  width: fit-content;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  gap: 4px;
  padding: 4px;
  border: 1px solid ${COLORS.richBlack.regular_5};
  border-radius: 16px;
  background-color: ${COLORS.jet.superlight_85};
`

const PlayerGroupHeading = styled.h2`
  text-align: center;
`

const PlayerRowWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 16px;
`

interface SlotTileProps {
  appContext: AppContext;
  index: number;
  details: HitterDetails | undefined;
  position: Position;
  dndContext: DndContext;
  disabled: DisabledCriteria;
  swapPositions: (position1: Position, position2: Position) => void;
  swapPlayers: (playerId1: number, playerId2: number) => void;
  canSwapPlayers: (playerId1: number, playerId2: number) => boolean;
}

function SlotTile(props: SlotTileProps) {
  const { appContext, index, details, position, dndContext, disabled, swapPositions, swapPlayers, canSwapPlayers } = props;
  
  const disabledProps = toDisabledProps('Drag to reorder lineup', ...disabled);


  return <Draggable 
    index={index} 
    draggableId={details?.playerId?.toString() ?? 'Pitcher'} 
    isDragDisabled={disabledProps.disabled}>
      {provided => 
      <PlayerTileWrapper ref={provided.innerRef} {...provided.draggableProps} title={disabledProps.title}>
        <NameContainer>
          {!disabledProps.disabled && <Icon icon='bars' {...provided.dragHandleProps} />}
          {disabledProps.disabled && <div/>}
          {position === 'Pitcher' && 
          <PositionBubble
            size='Medium'
            positionType={getPositionType(position)}>
              {getPositionAbbreviation(position)}
          </PositionBubble>}
          {position !== 'Pitcher' && 
          <PositionTile 
            position={position} 
            dndContext={dndContext}
            disabled={disabled}
            swapWithPosition={other => swapPositions(position, other)} 
          />}
          <NameContentContainer>
            {position === 'Pitcher' && <PitcherTile />}
            {position !== 'Pitcher' && 
            <PlayerTile 
              appContext={appContext}
              details={details!} 
              dndContext={dndContext}
              disabled={disabled}
              swapWithPlayer={other => swapPlayers(details!.playerId, other)} 
              canSwap={other => canSwapPlayers(details!.playerId, other)}
            />}
          </NameContentContainer>
        </NameContainer>
      </PlayerTileWrapper>}
  </Draggable>
}

const PlayerTileWrapper = styled.div`
  width: 26rem;
  padding: 2px 4px;
  border: 2px solid ${COLORS.richBlack.regular_5};
  border-radius: 8px;
  background-color: ${COLORS.white.regular_100_t60};
`

const NameContainer = styled.div`
  display: flex;
  gap: 8px;
  align-items: center;
`

const NameContentContainer = styled.div`
  flex: 1 1 auto;
`

const DraggableTypes = {
  POSITION: 'Position',
  PLAYER: 'Player'
}

interface PositionTileProps {
  position: Position;
  dndContext: DndContext;
  disabled: DisabledCriteria;
  swapWithPosition: (position: Position) => void;
}

function PositionTile(props: PositionTileProps) {
  const { position, dndContext, disabled, swapWithPosition } = props;

  return <DragSwapTile
    swapId={position}
    dndContext={dndContext}
    disabled={disabled}
    isSwappable={swapId => isPosition(swapId)}
    onSwap={swapId => swapWithPosition(swapId as Position)}>
      <PositionBubble
        size='Medium'
        positionType={getPositionType(position)}>
          {getPositionAbbreviation(position)}
      </PositionBubble>
  </DragSwapTile>
}

interface PlayerTileProps {
  appContext: AppContext;
  details: HitterDetails;
  dndContext: DndContext;
  disabled: DisabledCriteria;
  swapWithPlayer: (playerId: number) => void;
  canSwap: (playerId: number) => boolean;
}

function PlayerTile(props: PlayerTileProps) {
  const { appContext, details, dndContext, disabled, swapWithPlayer, canSwap } = props;
  
  return <DragSwapTile
    swapId={details.playerId.toString()}
    dndContext={dndContext}
    disabled={disabled}
    isSwappable={canSwapCallback}
    onSwap={swapId => swapWithPlayer(Number.parseInt(swapId))}>
      <PlayerNameBubble 
        appContext={appContext}
        sourceType={details.sourceType}
        playerId={details.playerId}
        positionType={getPositionType(details.position)}
        size='Medium'      
        title={details.fullName}
        fullWidth> 
          {details.savedName}
      </PlayerNameBubble>
  </DragSwapTile>

  function canSwapCallback(swapId: string) {
    const parseResult = Number.parseInt(swapId);
    if(Number.isNaN(parseResult))
      return false;

    return canSwap(parseResult);
  } 
}

function PitcherTile() {
  return <TextBubble size='Medium' positionType='Pitcher' fullWidth>
    <PitcherTitle>Pitcher</PitcherTitle>
  </TextBubble>
}

const PitcherTitle = styled.div`
  flex: 1 1 auto;
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
  white-space: nowrap;
  text-overflow: ellipsis;
`

interface DragSwapTileProps {
  swapId: string;
  dndContext: DndContext;
  disabled: DisabledCriteria;
  isSwappable: (swapId: string) => boolean;
  onSwap: (swapId: string) => void;
}

function DragSwapTile(props: PropsWithChildren<DragSwapTileProps>) {
  const { swapId, dndContext, disabled, isSwappable, onSwap, children } = props;
  const { draggingElementId: currentDraggingElementId, setDraggingElementId } = dndContext;
  
  const [dragEnterCount, setDragEnterCount] = useState(0);
  const disabledProps = toDisabledProps('Drag tile to swap', ...disabled);

  return <DragSwapTileWrapper
    id={swapId}
    draggable={!disabledProps.disabled}
    {...disabledProps}
    onDragStart={dragStart}
    onDragOver={dragOver}
    onDragEnter={dragEnter}
    onDragLeave={dragLeave}
    onDrop={drop}
    onDragEnd={dragEnd}>
      {children}
      <DroppingIndicator isDropping={dragEnterCount > 0} />
  </DragSwapTileWrapper>

  function dragStart(event: React.DragEvent<HTMLDivElement>) {
    event.dataTransfer.setData('text', swapId);
    setDraggingElementId(swapId);
  }

  function dragEnter() {
    if(isSwappable(currentDraggingElementId!))
      setDragEnterCount(dragEnterCount + 1);
  }

  function dragOver(event: React.DragEvent<HTMLDivElement>) {
    if(isSwappable(currentDraggingElementId!))
      event.preventDefault();
  }

  function dragLeave() {
    if(dragEnterCount > 0)
      setDragEnterCount(dragEnterCount - 1);
  }

  function drop(event: React.DragEvent<HTMLDivElement>) {
    setDragEnterCount(0);
    setDraggingElementId(null);
    const swapId = event.dataTransfer.getData('text');
    
    if(isSwappable(swapId))
      onSwap(swapId);
  }

  function dragEnd() {
    setDraggingElementId(null);
    if(dragEnterCount > 0)
      setDragEnterCount(0);
  }
}

const DragSwapTileWrapper = styled.div<{ disabled?: boolean }>`
  border: 2px solid ${COLORS.primaryBlue.regular_45_t40};
  border-radius: 10px;
  user-select: none;
  cursor: ${p => p.disabled ? undefined : 'grab'};
  position: relative;

  &:hover {
    border-color: ${p => p.disabled ? undefined : COLORS.primaryBlue.regular_45};
  }
`

const DroppingIndicator = styled.div<{ isDropping: boolean }>`
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  pointer-events: none;
  ${p => p.isDropping 
    ? `
      background-color: ${COLORS.primaryBlue.lighter_69_t80};
      border: 2px solid ${COLORS.primaryBlue.regular_45_t80};
      border-radius: 10px;
      `
    : undefined}
`