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
import { EntitySourceType } from "../shared/entitySourceType";
import { getPositionAbbreviation, getPositionType, isPosition, Position } from "../shared/positionCode";

export interface LineupEditorProps {
  players: LineupSlotDefinition[];
  useDh: boolean;
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

export function LineupEditor(props: LineupEditorProps) {
  const { players, useDh, updateLineupOrder, swapPositions, swapPlayers } = props;

  const sortedPlayers = players.sort(byOrder);
  const playersInLineup = sortedPlayers.filter(p => !!p.orderInLineup);
  const playersOnBench = sortedPlayers.filter(p => !p.orderInLineup);
  const lineup = useDh
    ? playersInLineup
    : lineupWithPitcherSlot(playersInLineup);   
  
  // I'm using two separate drag and drop libraries in this editor. 
  // One to handle lineup reordering and another to handle player and position swapping.

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
        index={index}
        details={slot.details} 
        position={slot.position!}
        swapPositions={swapPositions}
        swapPlayers={swapPlayers} />
    </PlayerRowWrapper>
  }

  function toPlayerTile(slot: LineupSlotDefinition) {
    return <PlayerTile 
      key={slot.details!.playerId} 
      details={slot.details!} 
      swapWithPlayer={other => swapPlayers(slot.details.playerId, other)}
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
}

const EditorWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  max-width: 800px;
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
  index: number;
  details: HitterDetails | undefined;
  position: Position;
  swapPositions: (position1: Position, position2: Position) => void;
  swapPlayers: (playerId1: number, playerId2: number) => void;
}

function SlotTile(props: SlotTileProps) {
  const { index, details, position, swapPositions, swapPlayers } = props;
  
  return <Draggable index={index} draggableId={details?.playerId?.toString() ?? 'Pitcher'}>
    {provided => 
    <PlayerTileWrapper ref={provided.innerRef} {...provided.draggableProps}>
      <NameContainer>
        <Icon icon='bars' {...provided.dragHandleProps} />
        {position === 'Pitcher' && 
        <PositionBubble
          size='Medium'
          positionType={getPositionType(position)}>
            {getPositionAbbreviation(position)}
        </PositionBubble>}
        {position !== 'Pitcher' && <PositionTile position={position} swapWithPosition={other => swapPositions(position, other)} />}
        <NameContentContainer>
          {position === 'Pitcher' && <PitcherTile />}
          {position !== 'Pitcher' && <PlayerTile details={details!} swapWithPlayer={other => swapPlayers(details!.playerId, other)} />}
        </NameContentContainer>
      </NameContainer>
    </PlayerTileWrapper>}
  </Draggable>
}

const PlayerTileWrapper = styled.div`
  width: 20rem;
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
  swapWithPosition: (position: Position) => void;
}

function PositionTile(props: PositionTileProps) {
  const { position, swapWithPosition } = props;

  return <DragSwapTile
    swapId={position}
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
  details: HitterDetails;
  swapWithPlayer: (playerId: number) => void;
}

function PlayerTile(props: PlayerTileProps) {
  const { details, swapWithPlayer } = props;
  
  return <DragSwapTile
    swapId={details.playerId.toString()}
    isSwappable={swapId => !isPosition(swapId)}
    onSwap={swapId => swapWithPlayer(Number.parseInt(swapId))}>
      <PlayerNameBubble 
        positionType={getPositionType(details.position)}
        size='Medium'      
        title={details.fullName}
        fullWidth
        sourceType={details.sourceType}> 
          {details.savedName}
      </PlayerNameBubble>
  </DragSwapTile>
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
  isSwappable: (swapId: string) => boolean;
  onSwap: (swapId: string) => void;
}

function DragSwapTile(props: PropsWithChildren<DragSwapTileProps>) {
  const { swapId, isSwappable, onSwap, children } = props;
  
  const [isDropping, setIsDropping] = useState(false);

  return <DragSwapTileWrapper
    id={swapId}
    draggable
    onDragStart={e => dragStart(e, swapId)}
    onDragOver={dragOver}
    onDragLeave={dragLeave}
    onDrop={drop}
    onDragEnd={dragEnd}>
      {children}
      <DroppingIndicator isDropping={isDropping} />
  </DragSwapTileWrapper>

  function dragStart(event: React.DragEvent<HTMLDivElement>, swapId: string) {
    const draggingElement = event.target as HTMLDivElement;
    event.dataTransfer.setData('text', draggingElement.id as Position)
  }

  function dragOver(event: React.DragEvent<HTMLDivElement>) {
    if(isSwappable((event.target as HTMLElement).id)) {
      event.preventDefault();
      setIsDropping(true);
    }
  }

  function dragLeave() {
    if(isDropping)
      setIsDropping(false);
  }

  function drop(event: React.DragEvent<HTMLDivElement>) {
    setIsDropping(false);
    const swapId = event.dataTransfer.getData('text');
    
    if(isSwappable(swapId))
      onSwap(swapId);
  }

  function dragEnd() {
    if(isDropping)
      setIsDropping(false);
  }
}

const DragSwapTileWrapper = styled.div`
  border: 2px solid ${COLORS.primaryBlue.regular_45_t40};
  border-radius: 10px;
  user-select: none;
  cursor: grab;
  position: relative;

  &:hover {
    border-color: ${COLORS.primaryBlue.regular_45};
  }

  &:active {
    cursor: grabbing;
  }
`

const DroppingIndicator = styled.div<{ isDropping: boolean }>`
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  ${p => p.isDropping 
    ? `
      background-color: ${COLORS.primaryBlue.lighter_69_t80};
      border: 2px solid ${COLORS.primaryBlue.regular_45_t80};
      border-radius: 10px;
      `
    : undefined}
`